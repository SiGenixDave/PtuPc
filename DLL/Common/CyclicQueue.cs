#region --- Revision History ---
/*
 * 
 *  This document and its contents are the property of Bombardier Inc. or its subsidiaries and contains confidential, proprietary information.
 *  The reproduction, distribution, utilization or the communication of this document, or any part thereof, without express authorization is strictly prohibited.  
 *  Offenders will be held liable for the payment of damages.
 * 
 *  (C) 2010    Bombardier Inc. or its subsidiaries. All rights reserved.
 * 
 *  Solution:   PTU
 * 
 *  Project:    PTU Application
 * 
 *  File name:  CyclicQueue.cs
 * 
 *  Revision History
 *  ----------------
 * 
 *  Date        Version Author          Comments
 *  05/27/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 *  08/20/10    1.1     K.McD           1.  Removed reference to ICyclicQueue.
 * 
 *  02/28/11    1.2     K.McD           1.  Auto-modified as a result of a number of resource name changes.
 *
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;

using Common.Properties;

namespace Common
{   
    /// <summary>
    /// <para>A CyclicQueue is a queue with a fixed size. It is called 'cyclic' because when an item is added to a full queue, the 'oldest' item in the queue 
    /// is removed to free space for the new item (so the internal buffer can be regarded as a cyclic buffer).</para>
    /// 
    /// <para>The CyclicQueue provides similar functionality to the System.Collections.Queue, except for the limit on the buffer size and the behavior it imposes.</para>
    /// </summary>
    /// <remarks>
    /// Note: This implementation does not support wrap-around of the internal indices at present. In other words, if you plan to call Enqueue more than long.MaxValue 
    /// times, the behavior is undefined.
    /// </remarks>
    [Serializable]
    public class CyclicQueue<T> : Queue<T>
    {
        #region --- Constants ---
        /// <summary>
        /// Value corresponding to the index entry being empty.
        /// </summary>
        private const int Empty = -1;
        #endregion --- Constants ---

        #region --- Member Variables ---
        /// <summary>
        /// Index of the last inserted item.
        /// </summary>
        private int m_TailIndex = Empty;

        /// <summary>
        /// Index of the last extracted item.
        /// </summary>
        private int m_HeadIndex = Empty;

        /// <summary>
        /// The generic buffer that holds the items.
        /// </summary>
        private T[] m_CyclicBuffer;

        /// <summary>
        /// The size of the cyclic buffer. 
        /// </summary>
        private int m_Size;                                     
        #endregion Member Variables

        #region --- Constructors ---

        /// <summary>
        /// Initializes a new instance of the class with the specified number of elements.
        /// </summary>
        /// <param name="bufferSize">The maximum number of elements in the cyclic queue.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if bufferSize is less than or equal to 0.</exception>
        public CyclicQueue(int bufferSize)
        {
            m_Size = bufferSize;                                        // Keep a record of the specified cyclic buffer size.
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException("bufferSize", bufferSize, "CyclicQueue.Constructor()" + CommonConstants.BindingMessage + Resources.EMCyclicQueueInvalidBufferSize);
            }
            m_CyclicBuffer = new T[bufferSize];
        }
        #endregion --- Constructors ---

        #region --- Queue Overridden Methods ---
        /// <summary>
        /// Removes all entries from the CyclicQueue.
        /// </summary>
        public new void Clear()
        {
            m_CyclicBuffer = new T[m_CyclicBuffer.Length];
            m_HeadIndex = Empty;
            m_TailIndex = Empty;
        }

        /// <summary>
        /// Determines whether an element is in the CyclicQueue.
        /// </summary>
        /// <param name="queueEntry">The entry that is to be checked.</param>
        /// <returns>True if the specified object was found; otherwise, false.</returns>
        public new bool Contains(T queueEntry)
        {
            foreach (T entry in m_CyclicBuffer)
            {
                if (queueEntry == null)
                {
                    if (entry == null)
                    {
                        return true;
                    }
                }
                else if (queueEntry.Equals(entry))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Removes and returns the entry at the beginning of the CyclicQueue.
        /// </summary>
        /// <returns>The entry found at the beginning of the queue.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the method is called whilst the queue is empty.</exception>
        public new T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("CyclicQueue.Dequeue()" + CommonConstants.BindingMessage + Resources.EMCyclicQueueQueueEmptyDequeue);
            }
            m_HeadIndex++;
            T queueEntry = m_CyclicBuffer[m_HeadIndex % m_CyclicBuffer.Length];
            return queueEntry;
        }

        /// <summary>
        /// Adds the specified entry to the end of the CyclicQueue. If the CyclicQueue is full, the entry at the beginning of the CyclicQueue 
        /// is removed prior to adding the new entry.
        /// </summary>
        /// <param name="queueEntry">The entry to be added to the queue.</param>
        public new void Enqueue(T queueEntry)
        {
            if ((m_TailIndex - m_HeadIndex == m_CyclicBuffer.Length) || (m_TailIndex < m_HeadIndex))
            {
                // The buffer is full - we have to drop the oldest object (in head)
                Dequeue();
            }
            m_TailIndex++;
            m_CyclicBuffer[m_TailIndex % m_CyclicBuffer.Length] = queueEntry;
        }

        /// <summary>
        /// Returns the object at the beginning of the CyclicQueue without removing it.
        /// </summary>
        /// <returns>The object at the beginning of the queue.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the method is called whilst the queue is empty.</exception>
        public new T Peek()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("CyclicQueue.Peek()" + CommonConstants.BindingMessage + Resources.EMCyclicQueueQueueEmptyPeek);
            }
            return m_CyclicBuffer[(m_HeadIndex + 1) % m_CyclicBuffer.Length];
        }

        /// <summary>
        /// Copies the CyclicQueue elements to a new array. 
        /// </summary>
        /// <returns>A generic array of the objects contained within the queue.</returns>
        public new T[] ToArray()
        {
            T[] queueEntries = new T[this.Count];
            if (this.Count > 0)
            {
                this.CopyTo(queueEntries, 0);
            }
            return queueEntries;
        }

        /// <summary>
        /// Sets the capacity to the actual number of elements in the CyclicQueue. After this method is
        /// called the buffer's size may have been decreased, and adding additional items to the CyclicQueue
        /// will cause the removal of the items at the beginning of the CyclicQueue.
        /// </summary>
        public void TrimToSize()
        {
            T[] newBuffer = this.ToArray();
            m_CyclicBuffer = newBuffer;
        }

        /// <summary>
        /// Copies the CyclicQueue elements to an existing one-dimensional Array, starting at the specified array index.
        /// </summary>
        /// <param name="array">Tha array where the cyclic queue is to be copied to.</param>
        /// <param name="index">The starting index of the array where the cyclic queue is to be copied to, normally 0.</param>
        /// <exception cref="ArgumentNullException">Thrown if the specified array is null.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the specified array is smaller than the number of elements in the cyclic queue OR the array is not
        /// a one dimensional array.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">If the specified start index is less than 0.</exception>
        public void CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array", "CyclicQueue.CopyTo()" + CommonConstants.BindingMessage + Resources.EMNullReference);
            }
            if (array.Rank != 1)
            {
                throw new ArgumentException("CyclicQueue.CopyTo()" + CommonConstants.BindingMessage + Resources.EMCyclicQueueInvalidRank, "array");
            }
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", "CyclicQueue.CopyTo()" + CommonConstants.BindingMessage + Resources.EMOutOfRange);
            }
            if ((array.Length - index) < this.Count)
            {
                throw new ArgumentException("CyclicQueue.CopyTo()" + CommonConstants.BindingMessage + Resources.EMCyclicQueueInvalidArraySize, "array");
            }

            int arrayIndex = index;
            for (int i = m_HeadIndex + 1; i <= m_TailIndex; i++, arrayIndex++)
            {
                array.SetValue(m_CyclicBuffer[i % m_CyclicBuffer.Length], arrayIndex);
            }
        }

        /// <summary>
        /// Creates an exact copy of the CyclicQueue.
        /// </summary>
        /// <returns>An exact copy of the cyclic queue.</returns>
        public CyclicQueue<T> Clone()
        {
            CyclicQueue<T> cyclicQueue = new CyclicQueue<T>(m_CyclicBuffer.Length);
            Array.Copy(m_CyclicBuffer, 0, cyclicQueue.m_CyclicBuffer, 0, m_CyclicBuffer.Length);
            cyclicQueue.m_HeadIndex = this.m_HeadIndex;
            cyclicQueue.m_TailIndex = this.m_TailIndex;
            return cyclicQueue;
        }
        #endregion --- Queue Overridden Methods ---

        #region --- Properties ---
        /// <summary>
        /// The current Tail index of the Cyclic Queue.
        /// </summary>
        public int TailIndex
        {
            get { return m_TailIndex; }
        }

        /// <summary>
        /// The current Head index of the Cyclic Queue.
        /// </summary>
        public int HeadIndex
        {
            get { return m_HeadIndex; }
        }
        /// <summary>
        /// Gets a value indicating whether access to the CyclicQueue is synchronized (thread-safe).
        /// </summary>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the number of elements contained in the CyclicQueue.
        /// </summary>
        public new int Count
        {
            get { return (m_TailIndex - m_HeadIndex); }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the CyclicQueue.
        /// </summary>
        public  object SyncRoot
        {
            get { return this; }
        }

        /// <summary>
        /// Gets the size of the cyclic buffer.
        /// </summary>
        public int  Size
        {
            get { return m_Size; }
        }
        #endregion --- Properties ---
    }
}