using System.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections
{
	/// <summary>
	/// Summary description for TopicCollection.
	/// </summary>
	public class TopicCollection : CollectionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TopicCollection"/> class.
		/// </summary>
		public TopicCollection()
		{
		}

		/// <summary>
		/// Adds the specified topic.
		/// </summary>
		/// <param name="topic">The topic.</param>
		/// <returns></returns>
		public int Add(Topic topic)
		{
			return List.Add(topic);
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
	}
}