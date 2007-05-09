#region Using Directives

using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	/// <summary>
	/// Summary description for Members.
	/// </summary>
	public class ViewMembers : BaseForumControl
	{
		/// <summary>
		/// Notifies server controls that use composition-based implementation to create any child
		/// controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			Controls.Add(BuildPageLinks("Members", ForumApplication.Instance.GetLink(SharePointForumControls.ViewMembers)));

			Controls.Add(new LiteralControl("<table width=100% cellspacing=0 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td width=1% class=\"ms-ToolPaneTitle\">&nbsp;</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">User Name</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">Email</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">Date Joined</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">Number of Posts</td>"));
			Controls.Add(new LiteralControl("</tr>"));
			
			DisplayUsers();
			
			Controls.Add(new LiteralControl("</table>"));
		}

		private void DisplayUsers()
		{
			ForumUserCollection users = RepositoryRegistry.ForumUserRepository.GetAll();

			foreach (ForumUser user in users)
			{
				Controls.Add(new LiteralControl("<tr>"));
				string userLink = ForumApplication.Instance.GetLink(SharePointForumControls.ViewProfile, "userId={0}", user.UserId);
				Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\" colspan=2><a href=\"{0}\"><strong>{1}</strong></a></td>", userLink, user.Name)));
				Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\"><a href=\"mailto:{0}\">{0}</a></td>", user.Email)));
				Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\">{0}</td>", user.Joined)));
				Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\">{0}</td>", user.NumPosts)));
				Controls.Add(new LiteralControl("</tr>"));
			}
		}
	}
}