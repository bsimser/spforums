#region Using Directives

using System;
using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using BilSimser.SharePoint.WebParts.Forums.Utility;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	/// <summary>
	/// Summary description for Topics.
	/// </summary>
	public class ViewTopics : BaseForumControl
	{
		/// <summary>
		/// Notifies server controls that use composition-based implementation to create any child
		/// controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			Controls.Add(BuildBasePageLinks());
			Controls.Add(new LiteralControl("<br>"));

			Controls.Add(new LiteralControl("<table width=100% cellspacing=0 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td align=left>&nbsp;</td>"));
			Controls.Add(new LiteralControl("<td align=right>"));

			Forum forum = RepositoryRegistry.ForumRepository.GetById(forumID);
			if (forum.HasAccess(ForumApplication.Instance.CurrentUser, Permission.Rights.Add))
			{
				Controls.Add(new LiteralControl(
					String.Format("<a href=\"{0}\">{1}</a>",
					              ForumApplication.Instance.GetNewTopicLink(forum.Id, PostMode.New.ToString()),
								this.WebPartParent.LoadResource("Text.NewTopic"))
					));
			}

			Controls.Add(new LiteralControl("</td>"));
			Controls.Add(new LiteralControl("</tr>"));
			Controls.Add(new LiteralControl("</table>"));

			Controls.Add(new LiteralControl("<table width=100% cellspacing=1 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl(string.Format("<td colspan=6 class=\"ms-ToolPaneTitle\">{0}</td>", forumName)));
			Controls.Add(new LiteralControl("</tr>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td class=\"ms-TPHeader\" width=1%>&nbsp;</td>"));
			Controls.Add(new LiteralControl(String.Format("<td class=\"ms-TPHeader\" align=left><strong>{0}</strong></td>", 
				this.WebPartParent.LoadResource("Text.Topics"))));
			Controls.Add(new LiteralControl(String.Format("<td class=\"ms-TPHeader\" align=middle width=7%><strong>{0}</strong></td>",
				this.WebPartParent.LoadResource("Text.Replies"))));
			Controls.Add(new LiteralControl(String.Format("<td class=\"ms-TPHeader\" align=middle width=20%><strong>{0}</strong></td>",
				this.WebPartParent.LoadResource("Text.Author"))));
			Controls.Add(new LiteralControl(String.Format("<td class=\"ms-TPHeader\" align=middle width=7%><strong>{0}</strong></td>",
				this.WebPartParent.LoadResource("Text.Views"))));
			Controls.Add(new LiteralControl(String.Format("<td class=\"ms-TPHeader\" align=middle width=25%><strong>{0}</strong></td>",
				this.WebPartParent.LoadResource("Text.LastPost"))));
			Controls.Add(new LiteralControl("</tr>"));

			DisplayTopics(RepositoryRegistry.TopicRepository.FindByForumId(forumID));

			DisplayFooterSectionWithRssFeed();

			Controls.Add(new LiteralControl("</table>"));
		}

		private void DisplayFooterSectionWithRssFeed()
		{
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td colspan=\"6\" align=\"right\">"));
			string feedLink = ForumApplication.Instance.GetLink(SharePointForumControls.SynFeed, "forum={0}", forumID);
			Controls.Add(new LiteralControl(string.Format("<a href=\"{0}\">{1}</a>", feedLink, this.WebPartParent.LoadResource("Text.RSS"))));
			Controls.Add(new LiteralControl("</td>"));
			Controls.Add(new LiteralControl("</tr>"));
		}
	}
}