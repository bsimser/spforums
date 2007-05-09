using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class ManageGroups : AdminBaseControl
	{
		public ManageGroups()
		{
		}

		protected override void CreateAdminChildControls()
		{
			AddBoxHeader("Manage Groups");

			Controls.Add(new LiteralControl("<table width=100% cellspacing=0 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td width=1% class=\"ms-ToolPaneTitle\">&nbsp;</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">Group</td>"));
			Controls.Add(new LiteralControl("<td align=center valign=top class=\"ms-ToolPaneTitle\">Actions</td>"));
			Controls.Add(new LiteralControl("</tr>"));
			
			DisplayGroups();
			
			Controls.Add(new LiteralControl("</table>"));

			string addGroupLink = ForumApplication.Instance.GetLink(SharePointForumControls.EditGroup, "group=0");
			Controls.Add(new LiteralControl(string.Format("<img src=\"/_layouts/images/rect.gif\">&nbsp;<a href=\"{0}\">Add Group</a>", addGroupLink)));
		}

		private void DisplayGroups()
		{
			GroupCollection groups = RepositoryRegistry.GroupRepository.GetAll();

			foreach (Group group in groups)
			{
				Controls.Add(new LiteralControl("<tr>"));
				Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\" colspan=2><strong>{0}</strong></td>", group.Name)));
				string editLink = ForumApplication.Instance.GetLink(SharePointForumControls.EditGroup, "group={0}", group.Id);
				Controls.Add(new LiteralControl(string.Format("<td align=center class=\"ms-TPHeader\"><a href=\"{0}\">Edit</a></td>", editLink)));
				Controls.Add(new LiteralControl("</tr>"));
			}
		}
	}
}