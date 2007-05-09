#region Using Directives

using System;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data
{
	public class ForumMapper
	{
		/// <summary>
		/// Creates the dto.
		/// </summary>
		/// <param name="forum">The forum.</param>
		/// <returns></returns>
		public static SharePointListItem CreateDto(Forum forum)
		{
			string[] values = {
				"Title", forum.Name,
				"Description", forum.Description,
				"CategoryID", forum.CategoryId.ToString(),
				"TopicCount", forum.TopicCount.ToString(),
				"PostCount", forum.PostCount.ToString(),
			};

			return new SharePointListItem(forum.Id, values);
		}

		/// <summary>
		/// Creates the domain object.
		/// </summary>
		/// <param name="listItem">The list item.</param>
		/// <returns></returns>
		public static Forum CreateDomainObject(SharePointListItem listItem)
		{
			int categoryId = Convert.ToInt32(listItem["CategoryID"]);
			Forum forum = new Forum(listItem.Id, categoryId, listItem["Title"]);

			forum.Description = listItem["Description"];
			if(listItem["TopicCount"] == null || listItem["TopicCount"] == string.Empty)
				forum.TopicCount = 0;
			else
				forum.TopicCount = Convert.ToInt32(listItem["TopicCount"]);

			if(listItem["PostCount"] == null || listItem["PostCount"] == string.Empty)
				forum.PostCount = 0;
			else
				forum.PostCount = Convert.ToInt32(listItem["PostCount"]);

			// TODO I think this is wrong. The last post should be set when
			// a topic is added, not modified
			forum.LastPost = Convert.ToDateTime(listItem["Modified"]);

			return forum;
		}
	}
}