using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class ManageForumPermissions : AdminBaseControl
	{
		public ManageForumPermissions()
		{
		}

		protected override void CreateAdminChildControls()
		{
			int forumId = ValidInt(HttpContext.Current.Request.QueryString["forum"]);
			Forum forum = RepositoryRegistry.ForumRepository.GetById(forumId);

			AddBoxHeader(string.Format("Manage Permissions for Forum \"{0}\"", forum.Name));

			Controls.Add(new LiteralControl("<table width=100% cellspacing=0 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td width=1% class=\"ms-ToolPaneTitle\">&nbsp;</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">Forum</td>"));
			Controls.Add(new LiteralControl("<td align=center valign=top class=\"ms-ToolPaneTitle\">Actions</td>"));
			Controls.Add(new LiteralControl("<td align=center valign=top class=\"ms-ToolPaneTitle\">Permissions</td>"));
			Controls.Add(new LiteralControl("</tr>"));

			GroupCollection groups = RepositoryRegistry.GroupRepository.GetAll();
			foreach (Group group in groups)
			{
				DisplayGroups(group, forum);
			}

			Controls.Add(new LiteralControl("</table>"));
		}

		private void DisplayGroups(Group group, Forum forum)
		{
			Controls.Add(new LiteralControl("<tr class=\"ms-alternating\">"));

			Controls.Add(new LiteralControl("<td>&nbsp;</td>"));

			Controls.Add(new LiteralControl(string.Format("<td>{0}</td>", group.Name)));

			string editLink = ForumApplication.Instance.GetLink(SharePointForumControls.ManageForumGroupPermissions, "forum={0}&group={1}", forum.Id, group.Id);
			Controls.Add(new LiteralControl(string.Format("<td align=center><a href=\"{0}\">Edit</a></td>", editLink)));

			string permissionDisplay = "None";
			foreach (DictionaryEntry permission in forum.Permissions)
			{
				if (Convert.ToInt32(permission.Key) == group.Id)
				{
					Permission perm = new Permission(permission.Value.ToString());
					permissionDisplay = perm.DisplayString;
				}
			}
			Controls.Add(new LiteralControl(string.Format("<td align=center>{0}</td>", permissionDisplay)));

			Controls.Add(new LiteralControl("</tr>"));
		}
	}
}