#region Using Directives

using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class ManageUsers : AdminBaseControl
	{
		protected override void CreateAdminChildControls()
		{
			AddBoxHeader("Manage Groups");

			Controls.Add(new LiteralControl("<table width=100% cellspacing=0 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td width=1% class=\"ms-ToolPaneTitle\">&nbsp;</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">User Name</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">Email</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">Date Joined</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">Number of Posts</td>"));
			Controls.Add(new LiteralControl("<td align=center valign=top class=\"ms-ToolPaneTitle\">Actions</td>"));
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
				Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\" colspan=2>{0}</td>", user.Name)));
				Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\"><a href=\"mailto:{0}\">{0}</a></td>", user.Email)));
				Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\">{0}</td>", user.Joined)));
				Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\">{0}</td>", user.NumPosts)));
				string editLink = ForumApplication.Instance.GetLink(SharePointForumControls.EditUser, "id={0}", user.Id);
				Controls.Add(new LiteralControl(string.Format("<td align=center class=\"ms-TPHeader\"><a href=\"{0}\">Edit</a></td>", editLink)));
				Controls.Add(new LiteralControl("</tr>"));
			}
		}
	}
}