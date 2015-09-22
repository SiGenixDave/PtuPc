#region --- Revision History ---
/*
 * 
 *  This class is taken from the book 'Programming NET Components' by Juval Lowy.
 * 
 *  (C) 2005 IDesign Inc. All rights reserved"
 * 
 *  Solution:   Portable Test Unit
 * 
 *  Project:    Common
 * 
 *  File name:  WorkerThread.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  07/28/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  01/12/11    1.1     K.McD           1.  Bug fix SNCR001.84. Added the second parameter to the call to the System.Threading.WaitHandle.WaitOne() method, as advised by 
 *                                          the following blog entries: 
 *                                              (1) http://www.mikeplate.com/missingmethodexception-and-waitone/ and (2) 
 *                                              (2) http://blog.darrenstokes.com/2009/03/30/watch-out-for-those-waitone-overloads-when-you-need-backwards-compatibility 
 *                                              
 *  08/24/11    1.2     K.McD           1.  Implemented the disposal pattern.
 *                                      2.  Modified a number of comments and XML tags and renamed a number of: local and member variables and properties.
 *                                      3.  Removed the Mutex used to access the EndLoop property.
 *                                      4.  Included a watchdog counter.
 *                                      5.  Removed the while loop associated with the IsAlive propery.
 *                                      6.  Added a timeout to the Join statement in the Kill() method and added a Thread.Abort() call if the underlying thread did 
 *                                          not terminate within the timeout period.
 *                                      7.  Added a check as to whether the handle of the underlying thread was instantiated before calling the ManualResetEvent.Set() 
 *                                          method in the Run() method.
 *
 */
#endregion --- Revision History ---

using System;
using System.Diagnostics;
using System.Threading;

namespace Common
{
    /// <summary>
    /// WorkerThread is a wrapper class around an underlying managed thread. It provides easy to use overloaded constructors, Kill() and Start() methods.
    /// </summary>
    public class WorkerThread : IDisposable
    {
        #region - [Constants] -
        /// <summary>
        /// The default number of ms to wait before releasing the mutex. Value: 2,000 ms.
        /// </summary>
        protected const int DefaultMutexWaitDurationMs = 2000;

        /// <summary>
        /// The timeout, in ms, associated with the thread Join() method. Value: 500 ms.
        /// </summary>
        protected const int TimeoutMsJoin = 500;

        /// <summary>
        /// The name of the underlying thread. Value: "Worker Thread".
        /// </summary>
        private const string ThreadName = "Worker Thread";
        #endregion - [Constants] -

        #region - [Member Variables] -
        /// <summary>
        /// Flag to indicate whether the Dispose() method has been called. True indicates that the Dispose() method has been called; otherwise, false.
        /// </summary>
        protected bool m_IsDisposed;

        /// <summary>
        /// Used to notify waiting threads that an event has occurred.
        /// </summary>
        protected ManualResetEvent m_ThreadHandle;

        /// <summary>
        /// Reference to the underlying thread.
        /// </summary>
        protected Thread m_Thread;

        /// <summary>
        /// A flag used to control thread execution. True, to stop thread execution; otherwise, false.
        /// </summary>
        protected bool m_StopThread;

        /// <summary>
        /// A watchdog counter that continually increments if the thread is still executing.
        /// </summary>
        protected int m_Watchdog;
        #endregion - [Member Variables] -

        #region - [Constructors] -
        /// <summary>
        /// Initialize a new instance of the class without starting the underlying thread.
        /// </summary>
        /// <example>   
        /// <code>
        /// workerThread workerThread = new WorkerThread();
        /// workerThread.Start();
        /// /* When you are done */
        /// workerThread.Kill();
        /// </code>
        /// </example>
        public WorkerThread()
        {
            m_StopThread = false;
            m_Thread = null;
            m_ThreadHandle = new ManualResetEvent(false);

            m_Thread = new Thread(Run);
            m_Thread.Name = ThreadName;
        }
        #endregion - [Constructors] -

        #region - [Methods] -
        /// <summary>
        /// Get the managed hash code associated with the underlying thread.
        /// </summary>
        /// <value></value>
        public override int GetHashCode()
        {
            return m_Thread.GetHashCode();
        }

        /// <summary>
        /// Determine if the specified object is equal to the current thread.
        /// </summary>
        /// <param name="obj">The object against which the thread is to be tested.</param>
        /// <returns>True, if the thread is equal to the specified object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return m_Thread.Equals(obj);
        }

        /// <summary>
        ///Start the underlying thread.
        /// </summary>
        public void Start()
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            Debug.Assert(m_Thread != null);
            Debug.Assert(m_Thread.IsAlive == false);
            m_Thread.Start();
        }

        /// <summary>
        /// Dispose of the thread.
        /// </summary>
        public void Dispose()
        {
            Kill();
            m_IsDisposed = true;
        }

        /// <summary>
        /// The code that is to be executed by the thread. The code is defined in the child class.
        /// </summary>
        /// <example>
        /// Example of the Run method code that should be included in the child class.
        /// <code>
        /// protected override void Run(object parameter)
        /// {
        ///     try
        ///    {
        ///        int i = 0;
        ///        while (EndLoop == false)
        ///        {
        ///            Trace.WriteLine("Thread is alive, Counter is " + i);
        ///            i++;
        ///            Thread.Sleep(0);
        ///        }
        ///    }
        ///    finally
        ///    {
        ///        m_ThreadHandle.Set();
        ///    }
        /// }
        /// </code>
        /// </example>
        public virtual void Run()
        {
            if (m_IsDisposed == true)
            {
                return;
            }

            if (m_ThreadHandle != null)
            {
                m_ThreadHandle.Set();
            }
        }

        /// <summary>
        /// Kill the underlying thread.
        /// </summary>
        public virtual void Kill()
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            //Kill is called on client thread - must use cached thread object.
            Debug.Assert(m_Thread != null);

            if (IsAlive == false)
            {
                return;
            }
            StopThread = true;

            //Wait for thread to die or for timeout.
            bool isTerminated = Join(TimeoutMsJoin);

            // Call the Abort if the thread did not terminate after the specified timeout.
            if (isTerminated == false)
            {
                m_Thread.Abort();
            }

            m_ThreadHandle.Close();
        }

        #region - [Join] -
        /// <summary>
        /// Block the calling thread until the underlying thread terminates.
        /// </summary>
        public void Join()
        {
            // Skip, if the Dispose() method has been called.
            if (m_IsDisposed)
            {
                return;
            }

            Join(Timeout.Infinite);
        }

        /// <summary>
        /// Block the calling thread until the underlying thread terminates or the specified number of milliseconds elapses.
        /// </summary>
        /// <param name="timeoutMs">The number of milliseconds to wait for the underlying thread to terminate</param>
        /// <returns>True, if the underlying thread has terminated; false, if the timeout has elapsed.</returns>
        public bool Join(int timeoutMs)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(timeoutMs);
            return Join(timeSpan);
        }

        /// <summary>
        /// Block the calling thread until the underlying thread terminates or the specified time span elapses.
        /// </summary>
        /// <param name="timeout">A TimeSpan set to the amount of time to wait for the underlying thread to terminate</param>
        /// <returns>True, if the underlying thread has terminated; false, if the timeout has elapsed.</returns>
        public bool Join(TimeSpan timeout)
        {
            //Join is called on client thread - must use cached thread object
            Debug.Assert(m_Thread != null);
            if (IsAlive == false)
            {
                return true;
            }
            Debug.Assert(Thread.CurrentThread.ManagedThreadId != m_Thread.ManagedThreadId);

            return m_Thread.Join(timeout);
        }
        #endregion - [Join] -
        #endregion - [Methods] -

        #region - [Properties] -
        /// <summary>
        /// Gets the managed thread ID associated with the underlying thread.
        /// </summary>
        /// <value></value>
        public int ManagedThreadId
        {
            get { return m_Thread.ManagedThreadId; }
        }

        /// <summary>
        /// Gets or sets the name of the underlying thread.
        /// </summary>
        public string Name
        {
            get { return m_Thread.Name; }
            set { m_Thread.Name = value; }
        }

        /// <summary>
        /// Gets the flag that indicates whether the underlying thread is alive or not. True, if the thread is alive; otherwise, false.
        /// </summary>
        public bool IsAlive
        {
            get
            {
                Debug.Assert(m_Thread != null);
                return m_Thread.IsAlive;
            }
        }

        /// <summary>
        /// Gets a waitable handle representing the underlying thread. You can combine the handle in wait operations with other handles. The handle is signaled 
        /// when the thread terminates. 
        /// </summary>
        public WaitHandle Handle
        {
            get { return m_ThreadHandle;  }
        }

        /// <summary>
        /// Gets the underlying Thread object, used for calling Suspend(), Resume(), etc.
        /// </summary>
        public Thread Thread
        {
            get { return m_Thread; }
        }

        /// <summary>
        /// Gets or sets the flag used to control thread execution.
        /// </summary>
        public bool StopThread
        {
            set { m_StopThread = value; }
            get
            {
                bool result = false;
                result = m_StopThread;
                return result;
            }
        }

        /// <summary>
        /// Gets the current value of the watchdog counter.
        /// </summary>
        public int Watchdog
        {
            get { return m_Watchdog; }
        }
        #endregion - [Properties] -
    }
}