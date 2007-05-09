#region Using Directives

using System;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using Microsoft.SharePoint;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data.Mappers
{
	public class UserMapper
	{
		public static SharePointListItem CreateDto(ForumUser user)
		{
			string[] values = {
				"Title", user.Name,
				"UserID", user.UserId.ToString(),
				"LastVisit", user.LastVisit.ToString(),
				"NumPosts", user.NumPosts.ToString(),
				"IsAdmin", user.IsAdmin.ToString(),
				"Groups", user.Id == 0
					? ForumApplication.Instance.GetDefaultGroupsForNewUser()
					: user.Groups.ToString(),
			};

			return new SharePointListItem(user.Id, values);
		}

		public static ForumUser CreateDomainObject(SharePointListItem listItem)
		{
			ForumUser user = new ForumUser();

			user.Id = Convert.ToInt32(listItem.Id);
			user.LastVisit = Convert.ToDateTime(listItem["LastVisit"]);
			user.UserId = Convert.ToInt32(listItem["UserID"]);
			user.NumPosts = Convert.ToInt32(listItem["NumPosts"]);
			user.Joined = Convert.ToDateTime(listItem["Created"]);
			user.IsAdmin = Convert.ToBoolean(listItem["IsAdmin"]);

			LoadGroupsForUser(listItem, user);
			LoadSharePointInfoForUser(user);

			return user;
		}

		private static void LoadSharePointInfoForUser(ForumUser user)
		{
			try
			{
				SPUser spCurrentUser = ForumApplication.Instance.SpWeb.SiteUsers.GetByID(user.UserId);
				if(null != spCurrentUser)
				{
					user.Name = spCurrentUser.Name;
					user.Email = spCurrentUser.Email;
				}
			}
			catch (Exception)
			{
			}
		}

		private static void LoadGroupsForUser(SharePointListItem listItem, ForumUser user)
		{
			string groupIds = listItem["Groups"];
			string delim = ";";
			char[] delimArray = delim.ToCharArray();
			string[] groupArray = groupIds.Split(delimArray);
			foreach (string s in groupArray)
			{
				int groupId = Convert.ToInt32(s);
				Group group = RepositoryRegistry.GroupRepository.FindById(groupId);
				user.Groups.Add(group);
			}
		}
	}
}