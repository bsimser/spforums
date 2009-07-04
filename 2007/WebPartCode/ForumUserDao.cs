using System;
using System.Collections.Generic;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class ForumUserDao
    {
        public int Save(ForumUser user)
        {
            int userId;

            SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
            SharePointListItem listItem = UserMapper.CreateDto(user);

            if (user.Id == 0)
            {
                userId = provider.AddListItem(ForumConstants.Lists_Users, listItem);
            }
            else
            {
                userId = provider.UpdateListItem(ForumConstants.Lists_Users, listItem);
            }

            return userId;
        }

        public int Count
        {
            get
            {
                SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
                SharePointListDescriptor descriptor = provider.GetAllListItems(ForumConstants.Lists_Users);
                return descriptor.SharePointListItems.Length;
            }
        }

        public IList<ForumUser> GetAll()
        {
            IList<ForumUser> users = new List<ForumUser>();

            SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
            SharePointListDescriptor descriptor = provider.GetAllListItems(ForumConstants.Lists_Users);
            foreach (SharePointListItem listItem in descriptor.SharePointListItems)
            {
                users.Add(UserMapper.CreateDomainObject(listItem));
            }

            return users;
        }

        public ForumUser GetById(int id)
        {
            SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
            SharePointListItem listItem = provider.GetListItemByField(ForumConstants.Lists_Users, "ID", id.ToString());

            if (null == listItem)
            {
                CreateUser(id);
                listItem = provider.GetListItemByField(ForumConstants.Lists_Users, "UserID", id.ToString());
            }

            return UserMapper.CreateDomainObject(listItem);
        }

        public ForumUser GetBySharePointId(int id)
        {
            SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
            SharePointListItem listItem = provider.GetListItemByField(ForumConstants.Lists_Users, "UserID", id.ToString());

            if (null == listItem)
            {
                CreateUserFromSharePointId(id);
                listItem = provider.GetListItemByField(ForumConstants.Lists_Users, "UserID", id.ToString());
            }

            return UserMapper.CreateDomainObject(listItem);
        }

        private void CreateUser(int id)
        {
            ForumUser user = new ForumUser();
            user.Name = ForumApplication.Instance.SpUser.Name;
            user.Id = id;
            user.UserId = ForumApplication.Instance.SpUser.ID;
            user.LastVisit = DateTime.Now;
            user.NumPosts = id == 1 ? 1 : 0;
            user.IsAdmin = id == 1 ? true : false;
            Save(user);
        }

        private void CreateUserFromSharePointId(int id)
        {
            ForumUser user = new ForumUser();
            user.Name = ForumApplication.Instance.SpUser.Name;
            user.Id = 0;
            user.UserId = id;
            user.LastVisit = DateTime.Now;
            user.NumPosts = id == 1 ? 1 : 0;
            user.IsAdmin = ForumApplication.Instance.SpUser.IsSiteAdmin ? true : false;
            Save(user);
        }

        public ForumUser GetLast()
        {
            SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
            SharePointListDescriptor descriptor = provider.GetAllListItems(ForumConstants.Lists_Users);
            SharePointListItem item = descriptor.SharePointListItems[descriptor.SharePointListItems.Length - 1];
            return UserMapper.CreateDomainObject(item);
        }
    }
}