#region Using Directives

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using BilSimser.SharePoint.WebParts.Forums.Utility;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls.Base
{
	/// <summary>
	/// Super class from which all forum controls will derive from
	/// </summary>
	public class BaseForumControl : Control, INamingContainer
	{
		#region Fields

		private ForumTimer forumTimer = new ForumTimer(true);
		private PostMode messageMode = PostMode.New;
		private SharePointToolBar toolBar;
		private string parentLink;
		private SharePointForumWebPart parent;

		protected int categoryID = 0;
		protected int topicID = 0;
		protected int forumID = 0;
		protected int messageID = 0;
		protected string forumName = string.Empty;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseForumControl"/> class.
		/// </summary>
		public BaseForumControl()
		{
			Load += new EventHandler(BaseForumControl_Load);
			PreRender += new EventHandler(BaseForumControl_PreRender);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the parent link.
		/// </summary>
		/// <value>The parent link.</value>
		public string ParentLink
		{
			get { return parentLink; }
			set { parentLink = value; }
		}

		/// <summary>
		/// Gets or sets the message mode.
		/// </summary>
		/// <value>The message mode.</value>
		public PostMode MessageMode
		{
			get { return messageMode; }
			set { messageMode = value; }
		}

		/// <summary>
		/// Gets a reference to the server control's parent control in the page control hierarchy.
		/// </summary>
		/// <value></value>
		public SharePointForumWebPart WebPartParent
		{
			get { return parent; }
			set { parent = value; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Validates and integer. Used for getting values from QueryString.
		/// Will return 0 for invalid or null objects.
		/// </summary>
		/// <param name="obj">The o.</param>
		/// <returns></returns>
		public static int ValidInt(object obj)
		{
			try
			{
				if (obj == null)
					return 0;
				return int.Parse(obj.ToString());
			}
			catch (Exception)
			{
				return 0;
			}
		}

		#endregion

		#region Private Methods

		private void BuildQuoteLinkUI(Forum forum, Message post)
		{
			bool canQuote = false;

			if (forum.HasAccess(ForumApplication.Instance.CurrentUser, Permission.Rights.Reply))
			{
				canQuote = true;
			}

			if (canQuote)
			{
				string quoteLink = ForumApplication.Instance.GetLink(
					SharePointForumControls.UpdateMessage,
					"message={0}&{1}={2}", post.Id, ForumConstants.Query_PostMethod, PostMode.Quote);
				Controls.Add(new LiteralControl(string.Format("<a href={0}>{1}</a>&nbsp;", 
					quoteLink, this.WebPartParent.LoadResource("Text.Quote"))));
			}
		}

		private void BuildReplyLinkUI(Forum forum, Message post)
		{
			if (forum.HasAccess(ForumApplication.Instance.CurrentUser, Permission.Rights.Reply))
			{
				string replyLink = ForumApplication.Instance.GetLink(
					SharePointForumControls.UpdateMessage,
					"topic={0}&{1}={2}&message={3}", post.TopicId, ForumConstants.Query_PostMethod, PostMode.Reply, post.Id);
				Controls.Add(new LiteralControl(
					String.Format("<a href=\"{0}\">{1}</a>&nbsp;|&nbsp;", replyLink, 
						this.WebPartParent.LoadResource("Text.Reply"))));
			}
		}

		private void BuildEditLinkUI(Forum forum, Message post)
		{
			bool canEdit = false;

			if (ForumApplication.Instance.CurrentUser.IsAdmin)
			{
				canEdit = true;
			}
			else
			{
				if (forum.HasAccess(ForumApplication.Instance.CurrentUser, Permission.Rights.Edit)
					&& post.Author.Id == ForumApplication.Instance.CurrentUser.Id)
					canEdit = true;
			}

			if (canEdit)
			{
				string editLink = ForumApplication.Instance.GetLink(
					SharePointForumControls.UpdateMessage,
					"message={0}&{1}={2}", post.Id, ForumConstants.Query_PostMethod, PostMode.Edit);
				Controls.Add(new LiteralControl(string.Format("<a href={0}>{1}</a>&nbsp;|&nbsp;", editLink,
					this.WebPartParent.LoadResource("Text.Edit"))));
			}
		}

		private string FillUserInfoBox(ForumUser user)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendFormat("{0}: {1}<br>", this.WebPartParent.LoadResource("UserInfoBox.Joined"), user.Joined.ToShortDateString());
			sb.AppendFormat("{0}: {1}<br>", this.WebPartParent.LoadResource("UserInfoBox.Posts"), user.NumPosts);

			return sb.ToString();
		}

		[Conditional("DEBUG")]
		private void DisplayDebugUserInformation(StringBuilder footer)
		{
			footer.AppendFormat("<br><br>Debug Information<br>");
			footer.AppendFormat("AppPool user: {0} (ID={1} [From SPWeb object])<br>",
			                    ForumApplication.Instance.AppPoolUser.LoginName,
			                    ForumApplication.Instance.AppPoolUser.ID);
			footer.AppendFormat("SPWeb user: {0} (ID={1} [From ForumApp.SpUser object])<br>",
			                    ForumApplication.Instance.SpUser.LoginName,
			                    ForumApplication.Instance.SpUser.ID);

			footer.AppendFormat("Forum user= {0} (ID={1} [From spforums_users list])<br>",
			                    ForumApplication.Instance.CurrentUser.Name,
			                    ForumApplication.Instance.CurrentUser.Id);

			footer.AppendFormat("Base Path={0}", ForumApplication.Instance.BasePath);
		}

		/// <summary>
		/// Handles the PreRender event of the BaseForumControl control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void BaseForumControl_PreRender(object sender, EventArgs e)
		{
			toolBar = new SharePointToolBar(ForumApplication.Instance.SpUser.Name);

			toolBar.AddItem(this.WebPartParent.LoadResource("Toolbar.Home"), ForumApplication.Instance.GetLink(SharePointForumControls.ViewForums));

			/*
			toolBar.AddItem(this.WebPartParent.LoadResource("Toolbar.Search"), ForumApplication.Instance.GetLink(SharePointForumControls.Search));
			*/

			toolBar.AddItem(this.WebPartParent.LoadResource("Toolbar.MyProfile"), ForumApplication.Instance.GetLink(SharePointForumControls.ViewProfile));

			toolBar.AddItem(this.WebPartParent.LoadResource("Toolbar.Members"), ForumApplication.Instance.GetLink(SharePointForumControls.ViewMembers));

			if (ForumApplication.Instance.CurrentUser.IsAdmin)
				toolBar.AddItem(this.WebPartParent.LoadResource("Toolbar.Admin"), ForumApplication.Instance.GetLink(SharePointForumControls.AdministrationPanel));
		}

		/// <summary>
		/// Determines the post method.
		/// </summary>
		private void DeterminePostMethod()
		{
			string postMethod = HttpContext.Current.Request.QueryString[ForumConstants.Query_PostMethod];
			if (null != postMethod)
			{
				MessageMode = (PostMode) PostMode.Parse(typeof (PostMode), postMethod);
			}
			else
			{
				MessageMode = PostMode.New;
			}
		}

		/// <summary>
		/// Assigns the ids from query strings.
		/// </summary>
		private void AssignIdsFromQueryStrings()
		{
			messageID = ValidInt(HttpContext.Current.Request.QueryString["message"]);
			topicID = ValidInt(HttpContext.Current.Request.QueryString["topic"]);
			forumID = ValidInt(HttpContext.Current.Request.QueryString["forum"]);
			categoryID = ValidInt(HttpContext.Current.Request.QueryString["category"]);

			// if there's a message id but no topic id, derive the topic from the message
			if ((messageID != 0) && (topicID == 0))
			{
				topicID = RepositoryRegistry.MessageRepository.GetById(messageID).TopicId;
			}

			// if there's a topic id but no forum id, derive the forum id from the topic
			if ((topicID != 0) && (forumID == 0))
			{
				forumID = RepositoryRegistry.TopicRepository.GetById(topicID).ForumId;
			}

			// if there's a forum id but not category then derive the category from the forum
			if ((forumID != 0) && (categoryID == 0))
			{
				categoryID = RepositoryRegistry.ForumRepository.GetById(forumID).CategoryId;
			}
		}

		/// <summary>
		/// Handles the Load event of the BaseForumControl control. This
		/// sets up all the ids for later retrieval based on a QueryString
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void BaseForumControl_Load(object sender, EventArgs e)
		{
			AssignIdsFromQueryStrings();
			DeterminePostMethod();
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.ViewForums);
		}

		/// <summary>
		/// Gets the session for the user (which contains 
		/// details about the user and what they can see).
		/// </summary>
		/// <returns></returns>
		protected UserSession GetSession()
		{
			return UserSession.CreateSession(ForumApplication.Instance.SpUser);
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Builds the page links.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="url">The link.</param>
		/// <returns></returns>
		protected PageLinks BuildPageLinks(string title, string url)
		{
			PageLinks pageLinks = BuildBasePageLinks();
			pageLinks.AddLink(title, url);
			return pageLinks;
		}

		/// <summary>
		/// Adds the box header.
		/// </summary>
		/// <param name="title">The title.</param>
		protected void AddBoxHeader(string title)
		{
			AddBoxHeader(title, true, 1);
		}

		protected void AddBoxHeader(string title, bool closeBox)
		{
			AddBoxHeader(title, closeBox, 1);
		}

		protected void AddBoxHeader(string title, bool closeBox, int colSpan)
		{
			Controls.Add(new LiteralControl("<table width=100% cellspacing=0 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl(String.Format("<td colSpan=\"{1}\" class=\"ms-ToolPaneTitle\">{0}</td>", title, colSpan)));
			Controls.Add(new LiteralControl("</tr>"));

			if (closeBox)
			{
				CloseBox();
			}
		}

		protected void CloseBox()
		{
			Controls.Add(new LiteralControl("</table>"));
			Controls.Add(new LiteralControl("<br/>"));
		}

		/// <summary>
		/// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to
		/// be rendered on
		/// the client.
		/// </summary>
		/// <param name="writer">The <see langword="HtmlTextWriter"/> object that receives the server control content.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			EnsureChildControls();
			RenderToolBar(writer);
			RenderBody(writer);
			RenderFooter(writer);
		}

		/// <summary>
		/// Renders the footer.
		/// </summary>
		/// <param name="writer">The writer.</param>
		private void RenderFooter(HtmlTextWriter writer)
		{
			StringBuilder footer = new StringBuilder();

//			ddlForumJump.RenderControl(writer);

			footer.AppendFormat("<p style=\"text-align:center;font-size:7pt\">");

			footer.AppendFormat("Powered by {0} version {1}",
			                    String.Format("<a title=\"SharePoint Forums Home Page\" href=\"{0}\">SharePoint Forums</a>",
			                                  ForumConstants.Config_Author_WebSite),
			                    String.Format("{0} - {1}",
			                                  Assembly.GetExecutingAssembly().GetName().Version.ToString(),
			                                  DateTime.Now.ToShortDateString()));

			footer.AppendFormat(String.Format("<br/>Copyright &copy; {0} by <a href=\"mailto:{1}\">{2}</a>. All rights reserved.",
			                                  DateTime.Now.Year,
			                                  ForumConstants.Config_Author_Email,
			                                  ForumConstants.Config_Author_Name)
				);

			footer.AppendFormat("<br/>");

			forumTimer.Stop();

			footer.AppendFormat("This page was generated in {0:N3} seconds.", forumTimer.Duration);

			DisplayDebugUserInformation(footer);

			footer.AppendFormat("</p>");
			writer.Write(footer.ToString());
		}

		/// <summary>
		/// Renders the tool bar.
		/// </summary>
		/// <param name="writer">The writer.</param>
		private void RenderToolBar(HtmlTextWriter writer)
		{
			toolBar.Render(writer);
		}

		/// <summary>
		/// This renders the body of the control by looping through
		/// any child controls in the control array.
		/// </summary>
		/// <param name="writer"></param>
		protected virtual void RenderBody(HtmlTextWriter writer)
		{
			foreach (Control control in Controls)
			{
				control.RenderControl(writer);
			}
		}

		/// <summary>
		/// Given the parameters passed into a page, builds the page links
		/// going backwards from post -> topic -> forum -> category
		/// </summary>
		/// <returns></returns>
		protected PageLinks BuildBasePageLinks()
		{
			PageLinks pageLinks = new PageLinks();
			pageLinks.AddLink(ForumApplication.Instance.Title, ForumApplication.Instance.GetLink(SharePointForumControls.ViewForums));

			if (messageID != 0)
			{
				Message message = RepositoryRegistry.MessageRepository.GetById(messageID);
				topicID = message.TopicId;
			}

			string topicName = string.Empty;
			if (topicID != 0)
			{
				Topic topic = RepositoryRegistry.TopicRepository.GetById(topicID);
				forumID = topic.ForumId;
				topicName = topic.Name;
			}

			if (forumID != 0)
			{
				Forum forum = RepositoryRegistry.ForumRepository.GetById(forumID);
				forumName = forum.Name;
				categoryID = forum.CategoryId;
			}

			string categoryName = string.Empty;
			if (categoryID != 0)
			{
				Category category = RepositoryRegistry.CategoryRepository.GetById(categoryID);
				categoryName = category.Name;
			}

			// Have to do this check again to build the links in the right order
			if (categoryID != 0)
				pageLinks.AddLink(categoryName, ForumApplication.Instance.GetLink(SharePointForumControls.ViewForums, "category={0}", categoryID));

			if (forumID != 0)
				pageLinks.AddLink(forumName, ForumApplication.Instance.GetLink(SharePointForumControls.ViewTopics, "forum={0}", forumID));

			if (topicID != 0)
				pageLinks.AddLink(topicName, ForumApplication.Instance.GetLink(SharePointForumControls.ViewMessages, "topic={0}", topicID));

			return pageLinks;
		}

		protected TableRow CreateButtonRow(SPButton button)
		{
			TableCell tableCell;
			TableRow tableRow = new TableRow();

			tableCell = new TableCell();
			tableCell.ColumnSpan = 2;
			tableCell.Width = Unit.Percentage(100);
			tableCell.HorizontalAlign = HorizontalAlign.Center;
			tableCell.Controls.Add(button);

			tableRow.Cells.Add(tableCell);

			return tableRow;
		}

		/// <summary>
		/// Creates the button row.
		/// </summary>
		/// <param name="left">The left.</param>
		/// <param name="right">The right.</param>
		/// <returns></returns>
		protected TableRow CreateButtonRow(Button left, Button right)
		{
			TableCell tableCell;
			TableRow tableRow = new TableRow();

			tableCell = new TableCell();
			tableCell.ColumnSpan = 2;
			tableCell.Width = Unit.Percentage(100);
			tableCell.HorizontalAlign = HorizontalAlign.Center;

			tableCell.Controls.Add(left);
			tableCell.Controls.Add(new LiteralControl("&nbsp;"));
			tableCell.Controls.Add(right);

			tableRow.Cells.Add(tableCell);

			return tableRow;
		}

		/// <summary>
		/// Redirects to parent.
		/// </summary>
		protected void RedirectToParent()
		{
			if(ParentLink == null)
				ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.ViewForums);
			this.Context.Response.Redirect(ParentLink);
		}

		protected void AddRow(string label, string control)
		{
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl(string.Format("<td align=right class=ms-navframe width=50%><strong>{0}</strong></td>", label)));
			Controls.Add(new LiteralControl(string.Format("<td class=ms-navframe width=50%>{0}</td>", control)));
			Controls.Add(new LiteralControl("</tr>"));
		}

		protected TableRow CreateRow(Control control, string labelName)
		{
			TableCell tableCell;
			TableRow tableRow;
			tableRow = new TableRow();
			tableCell = new TableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			tableCell.CssClass = "ms-navframe";
			tableCell.Width = Unit.Percentage(50);
			tableCell.Controls.Add(new LiteralControl(String.Format("<strong>{0}</strong>", labelName)));
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableCell.Width = Unit.Percentage(50);
			tableCell.Controls.Add(control);
			tableRow.Cells.Add(tableCell);
			return tableRow;
		}

		protected TableRow CreateRow(Control control)
		{
			TableCell tableCell;
			TableRow tableRow;
			tableRow = new TableRow();
			tableCell = new TableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Center;
			tableCell.Width = Unit.Percentage(100);
			tableCell.Controls.Add(control);
			tableRow.Cells.Add(tableCell);
			return tableRow;
		}

		protected void DisplayMessages(MessageCollection messages)
		{
			DisplayMessages(messages, null);
		}

		protected void DisplayMessages(MessageCollection messages, Forum forum)
		{
			foreach (Message post in messages)
			{
				Controls.Add(new LiteralControl("<tr>"));

				Controls.Add(new LiteralControl("<td class=\"ms-TPHeader\" width=140px>"));
				Controls.Add(new LiteralControl(string.Format("{0}", HtmlUtility.CreateProfileLink(post.Author))));
				Controls.Add(new LiteralControl("</td>"));

				Controls.Add(new LiteralControl("<td class=\"ms-TPHeader\" width=80%>"));
				Controls.Add(new LiteralControl("<table cellspacing=0 cellpadding=0 width=100%>"));
				Controls.Add(new LiteralControl("<tr>"));
				Controls.Add(new LiteralControl(string.Format("<td><strong>{0}:&nbsp;</strong>{1}</td>", this.WebPartParent.LoadResource("Text.Posted"), post.Created)));
				Controls.Add(new LiteralControl(string.Format("<td align=right>")));

				if(forum != null)
				{
					BuildReplyLinkUI(forum, post);
					BuildEditLinkUI(forum, post);
					BuildQuoteLinkUI(forum, post);
				}

				Controls.Add(new LiteralControl(string.Format("</td>")));
				Controls.Add(new LiteralControl("</tr>"));
				Controls.Add(new LiteralControl("</table>"));
				Controls.Add(new LiteralControl("</td>"));

				Controls.Add(new LiteralControl("</tr>"));

				Controls.Add(new LiteralControl("<tr class=\"ms-alternating\">"));
				Controls.Add(new LiteralControl(string.Format("<td valign=\"top\">{0}</td>", FillUserInfoBox(post.Author))));
				Controls.Add(new LiteralControl(string.Format("<td valign=\"top\">{0}</td>", post.Body)));
				Controls.Add(new LiteralControl("</tr>"));

				Controls.Add(new LiteralControl("<tr>"));
				Controls.Add(new LiteralControl("<td colspan=2 class=\"ms-ToolPaneTitle\" style=\"height:5px\"></td>"));
				Controls.Add(new LiteralControl("</tr>"));
			}
		}


		protected void DisplayTopics(TopicCollection topics)
		{
			topics.Sort("LastPost", SortDirection.Descending);
			foreach (Topic topic in topics)
			{
				Controls.Add(new LiteralControl("<tr class=\"ms-alternating\">"));
				Controls.Add(new LiteralControl(string.Format("<td valign=\"top\"><img src=\"{0}\"></td>", ForumApplication.Instance.ForumImage)));
				string postLink = ForumApplication.Instance.GetLink(SharePointForumControls.ViewMessages, "topic={0}", topic.Id);
				Controls.Add(new LiteralControl(string.Format("<td align=left><strong><a href=\"{0}\">{1}</a></strong></td>", postLink, topic.Name)));
				Controls.Add(new LiteralControl(string.Format("<td align=middle width=7%>{0}</td>", topic.Replies)));
				Controls.Add(new LiteralControl(string.Format("<td align=middle width=20%>{0}</td>", HtmlUtility.CreateProfileLink(topic.Author))));
				Controls.Add(new LiteralControl(string.Format("<td align=middle width=7%>{0}</td>", topic.Views)));
				Controls.Add(new LiteralControl(string.Format("<td align=middle width=25%>{0}<br>{1}</td>", topic.LastPost.ToString("ddd MMM d, yyyy h:m tt"), topic.Author.Name)));
				Controls.Add(new LiteralControl("</tr>"));
			}
		}

		#endregion
	}
}