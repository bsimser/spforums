using System;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Utility
{
	public class HtmlUtility
	{
		/// <summary>
		/// Creates a link to the profile page for a given user. Used
		/// in various places for other users to navigate to a persons
		/// profile page.
		/// </summary>
		/// <param name="user">The user to link to.</param>
		/// <returns>A formatted link that can be used in any HTML display</returns>
		public static string CreateProfileLink(ForumUser user)
		{
			string link;

			string profileLink = ForumApplication.Instance.GetLink(SharePointForumControls.ViewProfile, "userid={0}", user.UserId);
			link = String.Format("<a href=\"{0}\">{1}</a>", profileLink, user.Name);

			return link;
		}

		public static string CreateProfileLinkFromId(int id)
		{
			return CreateProfileLink(RepositoryRegistry.ForumUserRepository.GetBySharePointId(id));
		}
	}
}