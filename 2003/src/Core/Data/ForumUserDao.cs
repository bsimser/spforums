#region Using Directives

using System;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data
{
	public class ForumUserDao : DataProviderBase
	{
		public int Save(ForumUser user)
		{
			int userId;

			SharePointListItem listItem = UserMapper.CreateDto(user);

			if (user.Id == 0)
			{
				userId = Provider.AddListItem(ForumConstants.Lists_Users, listItem);
			}
			else
			{
				userId = Provider.UpdateListItem(ForumConstants.Lists_Users, listItem);
			}

			return userId;
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get
			{
				SharePointListDescriptor descriptor = Provider.GetAllListItems(ForumConstants.Lists_Users);
				return descriptor.SharePointListItems.Length;
			}
		}

		/// <summary>
		/// Gets all.
		/// </summary>
		/// <returns></returns>
		public ForumUserCollection GetAll()
		{
			ForumUserCollection users = new ForumUserCollection();

			SharePointListDescriptor descriptor = Provider.GetAllListItems(ForumConstants.Lists_Users);
			foreach (SharePointListItem listItem in descriptor.SharePointListItems)
			{
				users.Add(UserMapper.CreateDomainObject(listItem));
			}

			return users;
		}

		/// <summary>
		/// Gets the by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public ForumUser GetById(int id)
		{
			SharePointListItem listItem = Provider.GetListItemByField(ForumConstants.Lists_Users, "ID", id.ToString());

			if (null == listItem)
			{
				CreateUser(id);
				listItem = Provider.GetListItemByField(ForumConstants.Lists_Users, "UserID", id.ToString());
			}

			return UserMapper.CreateDomainObject(listItem);
		}

		/// <summary>
		/// Gets the by share point id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public ForumUser GetBySharePointId(int id)
		{
			SharePointListItem listItem = Provider.GetListItemByField(ForumConstants.Lists_Users, "UserID", id.ToString());

			if (null == listItem)
			{
				CreateUserFromSharePointId(id);
				listItem = Provider.GetListItemByField(ForumConstants.Lists_Users, "UserID", id.ToString());
			}

			return UserMapper.CreateDomainObject(listItem);
		}

		/// <summary>
		/// Creates the user.
		/// </summary>
		/// <param name="id">The id.</param>
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

		/// <summary>
		/// Creates the user from share point id.
		/// </summary>
		/// <param name="id">The id.</param>
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

		/// <summary>
		/// Gets the last user in the system.
		/// </summary>
		/// <returns></returns>
		public ForumUser GetLast()
		{
			SharePointListDescriptor descriptor = Provider.GetAllListItems(ForumConstants.Lists_Users);
			SharePointListItem item = descriptor.SharePointListItems[descriptor.SharePointListItems.Length - 1];
			return UserMapper.CreateDomainObject(item);
		}
	}
}