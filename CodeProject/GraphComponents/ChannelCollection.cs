#region --- Revision History ---
/*
 * 
 *  (C) The Code Project Open Source Licence Agreement
 * 
 *  Solution:   -
 * 
 *  Project:    CodeProject.GraphComponents
 * 
 *  File name:  ChannelCollection.cs
 * 
 *  Revision History
 *  ----------------
 *  Date        Version Author          Comments
 *  06/15/10    1.0     K.McD           1.  First entry into TortoiseSVN.
 * 
 */
#endregion --- Revision History ---

using System;
using System.Collections;

namespace CodeProject.GraphComponents
{
	/// <summary>
	/// This class provides a mechanism for the user to treat the channels of the plotter as a single collection.
	/// </summary>
	public class ChannelCollection : CollectionBase
    {
        #region --- Methods ---
        /// <summary>
		/// Add a new <c>Channel</c> to the collection.
		/// </summary>
        /// <returns>The index of the added channel.</returns>
		public int Add(Channel channel)
		{
			if (channel == null)
				throw new ArgumentNullException ();

			return( List.Add( channel ) );
		}

		/// <summary>
		/// Returns the index of the specified channel.
		/// </summary>
        /// <returns>The index of the specified channel.</returns>
		public int IndexOf( Channel value )
		{
			return( List.IndexOf( value ) );
		}

		/// <summary>
		/// Inserts a channel at the specified index.
		/// </summary>
		public void Insert( int index, Channel value )
		{
			if (value == null || index >= List.Count)
				return;

			List.Insert( index, value );
		}

		/// <summary>
		/// Removes the specified channel from the plotter.
		/// </summary>
		public void Remove ( Channel value )
		{
			if (value == null)
				return;

			if (! List.Contains (value))
				throw new ArgumentException ();

			for (int i = 0; i < List.Count; i ++)
			{
				if ( ((Channel) List[i]).YAxisName == value.YAxisName)
				{
					List.RemoveAt (i);
					break;
				}
			}
		}
		
		/// <summary>
		/// Checks whether the specified channel is contained within the collection.
		/// </summary>
		/// <param name="channel">Reference to the channel that is to be checked.</param>
		/// <returns>True, if the specified reference is contained within the collection; otherwise, false.</returns>
		public bool Contains( Channel channel )
		{
			// If value is not of type Channel, this will return false.
			return( List.Contains( channel ) );
        }
        #endregion --- Methods ---

        #region --- Properties ---
        /// <summary>
        /// Gets or sets the Channel object at the particular index.
        /// </summary>
        public Channel this[int index]
        {
            get
            {
                return (Channel)List[index];
            }
            set
            {
                List[index] = value;
            }
        }
        #endregion --- Properties ---
    }
}
