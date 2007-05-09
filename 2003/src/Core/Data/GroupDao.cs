#region Using Directives

using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data
{
	public class GroupDao : DataProviderBase
	{
		/// <summary>
		/// Finds the by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Group FindById(int id)
		{
			SharePointListItem listItem = Provider.GetListItembyID(ForumConstants.Lists_Groups, id);
			Group group = new Group();
			group.Id = listItem.Id;
			group.Name = listItem["Title"];
			return group;
		}

		/// <summary>
		/// Gets all.
		/// </summary>
		/// <returns></returns>
		public GroupCollection GetAll()
		{
			GroupCollection groups = new GroupCollection();
			SharePointListDescriptor items = Provider.GetAllListItems(ForumConstants.Lists_Groups);

			foreach (SharePointListItem listItem in items.SharePointListItems)
			{
				groups.Add(new Group(listItem.Id, listItem["Title"]));
			}

			return groups;
		}

		/// <summary>
		/// Saves the specified group.
		/// </summary>
		/// <param name="group">The group.</param>
		/// <returns></returns>
		public int Save(Group group)
		{
			string[] values = {
				"Title", group.Name,
			};
			SharePointListItem listItem = new SharePointListItem(group.Id, values);

			if (group.Id == 0)
				return Provider.AddListItem(ForumConstants.Lists_Groups, listItem);
			else
				return Provider.UpdateListItem(ForumConstants.Lists_Groups, listItem);
		}
	}
}