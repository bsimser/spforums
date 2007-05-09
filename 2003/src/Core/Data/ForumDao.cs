#region Using Directives

using System;
using System.Collections;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data
{
	public class ForumDao : DataProviderBase
	{
		#region Fields
		private const int DEFAULT_FORUM_ID = 0;
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ForumDao"/> class.
		/// </summary>
		public ForumDao()
		{
		}
		#endregion

		#region Public Methods

		public int SavePermissions(int groupId, int forumId, Permission permission)
		{
			string[] fieldNames = {"GroupID", "ForumID"};
			string[] fieldValues = {groupId.ToString(), forumId.ToString()};
			SharePointListItem listItem = Provider.GetListItemByField(ForumConstants.Lists_ForumAccess, fieldNames, fieldValues);

			int rc = 0;
			if (listItem == null)
			{
				string[] values = {
									  "Title", permission.ToString(),
									  "GroupID", groupId.ToString(),
									  "ForumID", forumId.ToString(),
				};

				listItem = new SharePointListItem(0, values);
				rc = Provider.AddListItem(ForumConstants.Lists_ForumAccess, listItem);
			}
			else
			{
				listItem["Title"] = permission.ToString();
				rc = Provider.UpdateListItem(ForumConstants.Lists_ForumAccess, listItem);
			}

			return rc;
		}

		public Hashtable GetPermissionsForForum(int id)
		{
			Hashtable permissions = new Hashtable();
			SharePointListDescriptor listItems = Provider.GetListItemsByField(ForumConstants.Lists_ForumAccess, "ForumID", id.ToString());
			foreach (SharePointListItem listItem in listItems.SharePointListItems)
			{
				Permission perm = new Permission(listItem["Title"]);
				permissions.Add(Convert.ToInt32(listItem["GroupID"]), perm);
			}
			return permissions;
		}

		public int Save(Forum forum)
		{
			SharePointListItem listItem = ForumMapper.CreateDto(forum);
			int newId = 0;

			if (forum.Id == 0)
			{
				newId = Provider.AddListItem(ForumConstants.Lists_Forums, listItem);
				SetupDefaultPermissions(newId);
			}
			else
			{
				newId = Provider.UpdateListItem(ForumConstants.Lists_Forums, listItem);
			}

			return newId;
		}
		
		public ForumCollection GetAll()
		{
			SharePointListDescriptor descriptor = Provider.GetAllListItems(ForumConstants.Lists_Forums);
			ForumCollection forumCollection = new ForumCollection();
			foreach (SharePointListItem listItem in descriptor.SharePointListItems)
			{
				forumCollection.Add(ForumMapper.CreateDomainObject(listItem));
			}

			return forumCollection;
		}

		public ForumCollection FindByCategoryId(int id)
		{
			SharePointListDescriptor descriptor = Provider.GetListItemsByField(ForumConstants.Lists_Forums, "CategoryID", id.ToString());
			ForumCollection forumCollection = new ForumCollection();
			foreach (SharePointListItem listItem in descriptor.SharePointListItems)
			{
				forumCollection.Add(ForumMapper.CreateDomainObject(listItem));
			}

			return forumCollection;
		}

		public Forum GetById(int id)
		{
			SharePointListItem currentItem = Provider.GetListItemByField(ForumConstants.Lists_Forums, "ID", id.ToString());
			if(currentItem == null)
				return new Forum(id, 1, "Default Forum");

			return ForumMapper.CreateDomainObject(currentItem);
		}
		#endregion

		#region Private Methods
		private void SetupDefaultPermissions(int newId)
		{
			Hashtable permissions = GetPermissionsForForum(DEFAULT_FORUM_ID);
			GroupCollection groups = RepositoryRegistry.GroupRepository.GetAll();
			foreach (Group group in groups)
			{
				Permission perms = permissions[group.Id] as Permission;
				if (perms == null)
					perms = new Permission();
				SavePermissions(group.Id, newId, perms);
			}
		}
		#endregion

	}
}
