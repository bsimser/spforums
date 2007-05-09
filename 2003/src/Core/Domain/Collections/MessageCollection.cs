using System.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections
{
	/// <summary>
	/// Summary description for PostCollection.
	/// </summary>
	public class MessageCollection : CollectionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageCollection"/> class.
		/// </summary>
		public MessageCollection()
		{
		}

		/// <summary>
		/// Adds the specified post.
		/// </summary>
		/// <param name="message">The post.</param>
		/// <returns></returns>
		public int Add(Message message)
		{
			return List.Add(message);
		}

		/// <summary>
		/// Sorts the specified sort property name.
		/// </summary>
		/// <param name="sortPropertyName">Name of the sort property.</param>
		/// <param name="sortDirection">The sort direction.</param>
		public void Sort(string sortPropertyName, Domain.SortDirection sortDirection)
		{
			InnerList.Sort(new UniversalSorter(sortPropertyName, sortDirection));
		}

		public Message this[int index]
		{
			get { return (Message)List[index]; }
			set { List[index] = value; }
		}
	}

	
}