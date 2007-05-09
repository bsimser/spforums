#region Using Directives

using System;
using System.Collections;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using BilSimser.SharePoint.WebParts.Forums.Utility;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	/// <summary>
	/// This will display each forum with
	/// each category along with stats for that forum.
	/// </summary>
	public class ViewForums : BaseForumControl
	{
		/// <summary>
		/// Notifies server controls that use composition-based implementation to create any child
		/// controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			Controls.Add(BuildBasePageLinks());

			DisplayWelcomeBanner();

			DisplayHeading();			
			DisplayCategory();

			Controls.Add(new LiteralControl("<br>"));
			Controls.Add(new LiteralControl("<table cellspacing=0 cellpadding=3 width=100%>"));
			DisplayBannerRow("Information");
			/*
			DisplayActiveUsers();
			*/
			DisplayHeaderRow(string.Format("{0} Statistics", WebPartParent.Name));
			DisplayStatsRow();
			Controls.Add(new LiteralControl("</table>"));

			DisplayFooter();
		}

		private void DisplayWelcomeBanner()
		{
			Controls.Add(new LiteralControl("<p>"));
			Controls.Add(new LiteralControl("<table cellspacing=0 cellpadding=0>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td>"));

			Label lblTimeNow = new Label();
			lblTimeNow.Text = string.Format("Current time: {0}<br />", DateTime.Now.ToString("T"));
			Controls.Add(lblTimeNow);
			Label lblTimeLastVisit = new Label();
			lblTimeLastVisit.Text = string.Format("Your last visit: {0}<br />", ForumApplication.Instance.CurrentUser.LastVisit.ToString("F"));
			Controls.Add(lblTimeLastVisit);
			
			/* TODO
			HyperLink linkUnreadMsgs = new HyperLink();
			linkUnreadMsgs.Text = "Unread Messages";
			linkUnreadMsgs.Visible = false;
			Controls.Add(linkUnreadMsgs);
			*/

			Controls.Add(new LiteralControl("</td>"));
			Controls.Add(new LiteralControl("</tr>"));
			Controls.Add(new LiteralControl("</table>"));
			Controls.Add(new LiteralControl("</p>"));
		}

		private void DisplayHeading()
		{
			Controls.Add(new LiteralControl("<table width=100% cellspacing=0 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td align=left>&nbsp;</td>"));
			Controls.Add(new LiteralControl("<td align=right>"));

			Controls.Add(new LiteralControl(
				String.Format("<a href=\"{0}\">Todays Topics</a>&nbsp;|&nbsp;",
				ForumApplication.Instance.GetLink(SharePointForumControls.ShowToday))));

			Controls.Add(new LiteralControl(
				String.Format("<a href=\"{0}\">Inactive Topics</a>",
				ForumApplication.Instance.GetLink(SharePointForumControls.ShowInactive))));

			Controls.Add(new LiteralControl("</td>"));
			Controls.Add(new LiteralControl("</tr>"));
			Controls.Add(new LiteralControl("</table>"));
		}

		private void DisplayStatsRow()
		{
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td valign=\"top\" width=1% class=\"ms-alternating\"><img src=\"/_layouts/images/allmeet.gif\"></td>"));
			Controls.Add(new LiteralControl(string.Format("<td class=\"ms-alternating\">{0}</td>", GetStatsString())));
			Controls.Add(new LiteralControl("</tr>"));
		}

		private void DisplayHeaderRow(string headerTitle)
		{
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\" colspan=2><strong>{0}</strong></td>", headerTitle)));
			Controls.Add(new LiteralControl("</tr>"));
		}

		private void DisplayBannerRow(string bannerTitle)
		{
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl(string.Format("<td class=ms-ToolPaneTitle colspan=2>{0}</td>", bannerTitle)));
			Controls.Add(new LiteralControl("</tr>"));
		}

		private void DisplayCategory()
		{
			Controls.Add(new LiteralControl("<table width=100% cellspacing=0 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td width=1% class=\"ms-ToolPaneTitle\">&nbsp;</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">Forum</td>"));
			Controls.Add(new LiteralControl("<td align=center width=7% class=\"ms-ToolPaneTitle\">Topics</td>"));
			Controls.Add(new LiteralControl("<td align=center width=7% class=\"ms-ToolPaneTitle\">Posts</td>"));
			Controls.Add(new LiteralControl("<td align=center width=25% class=\"ms-ToolPaneTitle\">Last Post</td>"));
			Controls.Add(new LiteralControl("</tr>"));

			UserSession session = GetSession();
			CategoryCollection categories = session.UserCategories;

			foreach (Category category in categories)
			{
				string categoryName = category.Name;
				int categoryId = category.Id;

				if(category.HasAccess(ForumApplication.Instance.CurrentUser, Permission.Rights.Read))
				{
					Controls.Add(new LiteralControl("<tr>"));
					string link = ForumApplication.Instance.GetLink(SharePointForumControls.ViewForums, "category={0}", categoryId);
					Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\" colspan=5><a href=\"{0}\"><strong>{1}</strong></a></td>", link, categoryName)));
					Controls.Add(new LiteralControl("</tr>"));
					ForumCollection forumCollection = category.Forums;
					if (forumCollection.Count > 0)
					{
						DisplayForum(forumCollection);
					}
				}
			}

			Controls.Add(new LiteralControl("</table>"));
		}

		private void DisplayFooter()
		{
			Controls.Add(new LiteralControl("<table cellspacing=0 cellpadding=3 width=100%>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td></td>"));
			Controls.Add(new LiteralControl("<td align=right>"));

			/* TODO
			Controls.Add(new LiteralControl("<a href=\"#\">Mark all forums as read</a>"));
			*/
			
			Controls.Add(new LiteralControl("</td>"));
			Controls.Add(new LiteralControl("</tr>"));
			Controls.Add(new LiteralControl("</table>"));
		}

		private void DisplayForum(ForumCollection forumCollection)
		{
			foreach (Forum forum in forumCollection)
			{
				if (forum.HasAccess(ForumApplication.Instance.CurrentUser, Permission.Rights.Read))
				{
					Controls.Add(new LiteralControl("<tr class=\"ms-alternating\">"));
					Controls.Add(new LiteralControl(string.Format("<td valign=\"top\"><img src=\"{0}\"></td>", ForumApplication.Instance.ForumImage)));
					string link = ForumApplication.Instance.GetLink(SharePointForumControls.ViewTopics, "forum={0}", forum.Id);
					Controls.Add(new LiteralControl(string.Format("<td valign=\"top\"><a href=\"{0}\">{1}</a><br>{2}</td>", link, forum.Name, forum.Description)));
					Controls.Add(new LiteralControl(string.Format("<td align=center valign=\"top\">{0}</td>", forum.TopicCount)));
					Controls.Add(new LiteralControl(string.Format("<td align=center valign=\"top\">{0}</td>", forum.PostCount)));
					Controls.Add(new LiteralControl(string.Format("<td align=center valign=\"top\">{0}</td>", forum.LastPost)));
					Controls.Add(new LiteralControl("</tr>"));
				}
			}
		}

		/// <summary>
		/// Gets the stats string.
		/// </summary>
		/// <returns></returns>
		private string GetStatsString()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("There are {0:N0} posts in {1:N0} topics in {2:N0} forums.", 
							WebPartParent.PostCount, // BUG count is off by 1? 
			                WebPartParent.TopicCount, 
			                WebPartParent.ForumCount);
			sb.Append("<br/>");

			//			if(!stats.IsNull("LastPost")) 
			//			{
			//				sb.AppendFormat("Last post {0} by {1}.",
			//			                DateTime.Now.ToString("T"),
			//			                String.Format("<a href=\"{0}\">{1}</a>", ForumWrapperControl.GetLink(ForumWrapperControl.Actions.MyProfile, "u={0}", 1), "Administrator"));
			//				sb.Append("<br/>");
			//			}

//			DisplayOnlineUsers(sb);

			sb.AppendFormat("We have {0:N0} registered members.", RepositoryRegistry.ForumUserRepository.GetCount());
			sb.Append("<br/>");

			sb.AppendFormat("Please welcome our newest member {0}.",
			                String.Format("{0}", HtmlUtility.CreateProfileLink(RepositoryRegistry.ForumUserRepository.GetLast())));
			sb.Append("<br/>");

			return sb.ToString();
		}

		/// <summary>
		/// Displays the online users.
		/// </summary>
		/// <param name="sb">The sb.</param>
		private static void DisplayOnlineUsers(StringBuilder sb)
		{
			ArrayList onlineUsers = ReadOnlineUsersFromCache();
			int userCount = onlineUsers.Count;
			sb.AppendFormat("There {0} {1} user{2} online: ",
			                userCount == 1 ? "is" : "are",
			                userCount,
			                userCount == 1 ? "" : "s");

			foreach (string user in onlineUsers)
			{
				string userName = user;
				sb.AppendFormat("{0} ", userName);
			}
			sb.Append("<br/>");
		}

		/// <summary>
		/// Reads the online users from cache.
		/// </summary>
		/// <returns></returns>
		private static ArrayList ReadOnlineUsersFromCache()
		{
			ArrayList onlineUsers = new ArrayList();
			IDictionaryEnumerator enumerator;
			enumerator = ForumApplication.Instance.ForumCache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string key = enumerator.Key.ToString();
				if (key.StartsWith(ForumConstants.Forum_Cache_Key) && key.EndsWith(ForumConstants.Forum_Cache_Key))
				{
					UserSession session = enumerator.Value as UserSession;
					onlineUsers.Add(session.UserDetails.Name);
				}
			}
			return onlineUsers;
		}
	}
}