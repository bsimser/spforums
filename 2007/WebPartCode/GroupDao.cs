using System.Collections.Generic;

namespace BilSimser.SharePointForums.WebPartCode
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

        public IList<Group> GetAll()
        {
            IList<Group> groups = new List<Group>();
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