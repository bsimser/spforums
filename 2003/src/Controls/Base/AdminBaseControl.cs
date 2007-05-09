#region Using Directives

using System;
using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls.Base
{
	public abstract class AdminBaseControl : BaseForumControl
	{
		#region Constructors

		public AdminBaseControl()
		{
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.AdministrationPanel);
		}

		#endregion

		#region Protected Methods

		protected override void OnInit(EventArgs e)
		{
			ForumUser currentUser = ForumApplication.Instance.CurrentUser;
			if (!currentUser.IsAdmin)
			{
				Context.Response.Redirect(ForumApplication.Instance.GetLink(SharePointForumControls.ViewForums));
			}
			base.OnInit(e);
		}

		protected abstract void CreateAdminChildControls();

		/// <summary>
		/// Notifies server controls that use composition-based implementation to create any child
		/// controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			Controls.Add(BuildPageLinks("Administration", ForumApplication.Instance.GetLink(SharePointForumControls.AdministrationPanel)));
			StartAdminMenuSection();
			CreateAdminChildControls();
			EndAdminMenuSection();
		}

		/// <summary>
		/// Ends the admin menu section.
		/// </summary>
		private void EndAdminMenuSection()
		{
			AddText("</td></tr></table>");
		}

		protected void AddText(string text)
		{
			Controls.Add(new LiteralControl(text));
		}
		
		#endregion

		#region Private Methods

		/// <summary>
		/// Starts the admin menu section.
		/// </summary>
		private void StartAdminMenuSection()
		{
			AddText("<table width='100%' cellspacing='0' cellpadding='0'><tr>");
			AddText("<td width='10%' valign='top'>");

			AddText("<table class=\"content\" width=\"100%\" cellspacing=0 cellpadding=0><tr><td class=\"post\" valign='top'>");
			AddText("<table width=\"100%\" cellspacing=0 cellpadding=2 border=0>");

			AddMenuSectionTitle("Admin");
			AddMenuSectionItem("Admin Index", ForumApplication.Instance.GetLink(SharePointForumControls.AdministrationPanel));
			AddMenuSectionItem("Configuration", ForumApplication.Instance.GetLink(SharePointForumControls.ConfigureForums));
			AddMenuSectionItem("Forums", ForumApplication.Instance.GetLink(SharePointForumControls.ManageForums));
			AddMenuSectionItem("Recalculate Totals", ForumApplication.Instance.GetLink(SharePointForumControls.UpdateCounts));
			AddMenuSectionItem("Create Sample Data", ForumApplication.Instance.GetLink(SharePointForumControls.CreateSampleData));
			AddMenuSectionItem("Delete Forums", ForumApplication.Instance.GetLink(SharePointForumControls.DeleteForums));
			AddBlankRow();

			AddMenuSectionTitle("Users and Roles");
			AddMenuSectionItem("Groups", ForumApplication.Instance.GetLink(SharePointForumControls.ManageGroups));
			AddMenuSectionItem("Users", ForumApplication.Instance.GetLink(SharePointForumControls.ManageUsers));

			AddText("</table>");
			AddText("</td></tr></table>");

			AddText("</td><td valign='top'>&nbsp;&nbsp;");
			AddText("</td><td width='90%' valign='top'>");
		}

		private void AddBlankRow()
		{
			AddText("<tr><td>&nbsp;</td></tr>");
		}

		/// <summary>
		/// Adds the menu section item.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="link">The link.</param>
		private void AddMenuSectionItem(string title, string link)
		{
			AddText(string.Format("<tr><td nowrap class=post><a href=\"{0}\">{1}</a></td></tr>", link, title));
		}

		/// <summary>
		/// Adds the menu section title.
		/// </summary>
		/// <param name="title">The title.</param>
		private void AddMenuSectionTitle(string title)
		{
			AddText(string.Format("<tr><td nowrap class=\"ms-ToolPaneTitle\"><b>{0}</b></td></tr>", title));
		}

		#endregion
	}
}