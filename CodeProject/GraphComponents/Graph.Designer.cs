#region --- Revision History ---
/*
 *  This class was developed under the terms of the Code Project Open Source License agreement (CPOL), see CPOL.html, and was originally 
 *  written by Anup. V (anupshubha@yahoo.com). The CPOL is intended to provide those developers who choose to share their code with a 
 *  license that protects them and provides users of their code with a clear statement regarding how the code can be used.
 * 
 *  Under the terms and conditions of the CPOL, all derivative work must also be developed under the same licence agreement. The full 
 *  CPOL terms and conditions are given in the file CPOL.html located in the 'Solution Items' directory.
 * 
 *  (C) 2007 - 2010    The Code Project
 *
 *  Solution:   
 * 
 *  Project:    GraphComponents
 * 
 *  File name:  Graph.Designer.cs
 * 
 *  Revision History
 *  --------------------
 * 
 *  Date        Version Author          Comments
 *  09/27/09    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections.Generic;
using System.Text;

namespace CodeProject.GraphComponents
{
    partial class Graph
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region --- Disposal ---
        /// <summary>
        /// Destructor / Finalizer. Because Dispose() calls the GC.SuppressFinalize() method, this method is called by the garbage collection process only
        /// if the consumer of the object doesn't call the Dispose() method, as it should.
        /// </summary>
        ~Graph()
        {
            Dispose(false);
        }

        /// <summary>
        /// Public implementation of the IDisposable.Dispose method. Called by the consumer of the object in order to free unmanaged resources
        /// deterministically.
        /// </summary>
        public new void Dispose()
        {
            // Call the protected Dispose overload and pass a value of 'true' to indicate that the Dispose is being called by consumer code, not
            // by the garbage collector.
            Dispose(true);

            // Because the Dispose method performs all necessary cleanup, ensure the garbage collector does not call the class destructor.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources that are currently being used.
        /// </summary>
        /// <param name="disposing">True, if the managed resources should be disposed of; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            lock (this)
            {
                if (m_IsDisposed == false)
                {
                    Cleanup(disposing);

                    m_IsDisposed = true;
                    base.Dispose(disposing);
                }
            }
        }
        #endregion --- Disposal ---
    }
}
