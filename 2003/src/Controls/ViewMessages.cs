#region Using Directives

using System;
using System.Text;
using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using BilSimser.SharePoint.WebParts.Forums.Utility;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	/// <summary>
	/// MessageControl is responsible for displaying all the indvidual
	/// messages (posts) for a given topic.
	/// </summary>
	public class ViewMessages : BaseForumControl
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewMessages"/> class.
		/// </summary>
		public ViewMessages()
		{
			Load += new EventHandler(Posts_Load);
		}

		#endregion

		#region Protected Methods
		/// <summary>
		/// Notifies server controls that use composition-based implementation to create any child
		/// controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			Topic topic = RepositoryRegistry.TopicRepository.GetById(topicID);

			Controls.Add(BuildBasePageLinks());
			Controls.Add(new LiteralControl("<br>"));

			Controls.Add(new LiteralControl("<table width=100% cellspacing=0 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td align=left>&nbsp;</td>"));
			Controls.Add(new LiteralControl("<td align=right>"));

			Forum forum = RepositoryRegistry.ForumRepository.GetById(forumID);
			if (forum.HasAccess(ForumApplication.Instance.CurrentUser, Permission.Rights.Reply))
			{
				Controls.Add(new LiteralControl(
					String.Format("<a href=\"{0}\">{1}</a>&nbsp;|",
					              ForumApplication.Instance.GetReplyLink(topicID, PostMode.Reply.ToString()),
								this.WebPartParent.LoadResource("Text.Reply")	
					)));
			}

			Controls.Add(new LiteralControl("&nbsp;"));
			if (forum.HasAccess(ForumApplication.Instance.CurrentUser, Permission.Rights.Add))
			{
				Controls.Add(new LiteralControl(
					String.Format("<a href=\"{0}\">{1}</a>&nbsp;|",
					              ForumApplication.Instance.GetNewTopicLink(forumID, PostMode.New.ToString()),
								this.WebPartParent.LoadResource("Text.NewTopic")
					)));
			}

			Controls.Add(new LiteralControl("&nbsp;"));
			if (ForumApplication.Instance.CurrentUser.IsAdmin)
			{
				string deleteLink = ForumApplication.Instance.GetLink(SharePointForumControls.DeleteTopic, "topic={0}", topic.Id);
				Controls.Add(new LiteralControl(
					string.Format("<a href=\"{0}\">{1}</a>", deleteLink, this.WebPartParent.LoadResource("Text.DeleteTopic"))));
			}

			Controls.Add(new LiteralControl("</td>"));
			Controls.Add(new LiteralControl("</tr>"));
			Controls.Add(new LiteralControl("</table>"));

			Controls.Add(new LiteralControl("<table width=100%>"));

			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl(string.Format("<td colspan=2 class=\"ms-ToolPaneTitle\">{0}</td>", topic.Name)));
			Controls.Add(new LiteralControl("</tr>"));

			DisplayMessages(topic.Messages, forum);

			Controls.Add(new LiteralControl("</table>"));
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Handles the Load event of the Posts control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void Posts_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				RepositoryRegistry.TopicRepository.IncreaseViewCount(topicID);
			}
		}
		#endregion
	}
}