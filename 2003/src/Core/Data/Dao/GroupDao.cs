#region Using Directives

using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data.Dao
{
	public class GroupDao
	{
		public Group FindById(int id)
		{
			SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
			SharePointListItem listItem = provider.GetListItembyID(ForumConstants.Lists_Groups, id);
			Group group = new Group();
			group.Id = listItem.Id;
			group.Name = listItem["Title"];
			return group;
		}

		public GroupCollection GetAll()
		{
			GroupCollection groups = new GroupCollection();
			SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
			SharePointListDescriptor items = provider.GetAllListItems(ForumConstants.Lists_Groups);

			foreach (SharePointListItem listItem in items.SharePointListItems)
			{
				groups.Add(new Group(listItem.Id, listItem["Title"]));
			}

			return groups;
		}

		public int Save(Group group)
		{
			SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
			string[] values = {
				"Title", group.Name,
			};
			SharePointListItem listItem = new SharePointListItem(group.Id, values);
			if (group.Id == 0)
				return provider.AddListItem(ForumConstants.Lists_Groups, listItem);
			else
				return provider.UpdateListItem(ForumConstants.Lists_Groups, listItem);
		}
	}
}