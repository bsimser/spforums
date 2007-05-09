#region Using Directives

using System;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data.Mappers
{
	/// <summary>
	/// Summary description for ForumMapper.
	/// </summary>
	public class ForumMapper
	{
		/// <summary>
		/// Creates the specified forum.
		/// </summary>
		/// <param name="forum">The forum.</param>
		/// <returns></returns>
		public static SharePointListItem CreateDto(Forum forum)
		{
			string[] values = {
				"Title", forum.Name,
				"Description", forum.Description,
				"CategoryID", forum.CategoryId.ToString(),
			};

			return new SharePointListItem(forum.Id, values);
		}

		/// <summary>
		/// Loads the specified list item.
		/// </summary>
		/// <param name="listItem">The list item.</param>
		/// <returns></returns>
		public static Forum CreateDomainObject(SharePointListItem listItem)
		{
			int categoryId = Convert.ToInt32(listItem["CategoryID"]);
			Forum forum = new Forum(listItem.Id, categoryId, listItem["Title"]);

			forum.Description = listItem["Description"];

			// TODO I think this is wrong. The last post should be set when
			// a topic is added, not modified
			forum.LastPost = Convert.ToDateTime(listItem["Modified"]);

			return forum;
		}
	}
}