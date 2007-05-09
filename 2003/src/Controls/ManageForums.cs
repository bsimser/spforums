using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	/// <summary>
	/// Summary description for ManageForums.
	/// </summary>
	public class ManageForums : AdminBaseControl
	{
		public ManageForums()
		{
		}

		protected override void CreateAdminChildControls()
		{
			AddBoxHeader("Manage Forums");

			Controls.Add(new LiteralControl("<table width=100% cellspacing=0 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td width=1% class=\"ms-ToolPaneTitle\">&nbsp;</td>"));
			Controls.Add(new LiteralControl("<td align=left class=\"ms-ToolPaneTitle\">Forum</td>"));
			Controls.Add(new LiteralControl("<td align=center valign=top class=\"ms-ToolPaneTitle\">Actions</td>"));
			Controls.Add(new LiteralControl("<td align=center valign=top class=\"ms-ToolPaneTitle\">Permissions</td>"));
			Controls.Add(new LiteralControl("</tr>"));
			DisplayCategories();
			Controls.Add(new LiteralControl("</table>"));

			string addForumLink = ForumApplication.Instance.GetLink(SharePointForumControls.EditForum, "forum=0");
			string addCategoryLink = ForumApplication.Instance.GetLink(SharePointForumControls.EditCategory, "category=0");
			string permissionLink = ForumApplication.Instance.GetLink(SharePointForumControls.ManageForumPermissions, "forum=0");

			Controls.Add(new LiteralControl(string.Format("<img src=\"/_layouts/images/rect.gif\">&nbsp;<a href=\"{0}\">Add Category</a>", addCategoryLink)));
			Controls.Add(new LiteralControl("&nbsp;"));
			Controls.Add(new LiteralControl(string.Format("<img src=\"/_layouts/images/rect.gif\">&nbsp;<a href=\"{0}\">Add Forum</a>", addForumLink)));
			Controls.Add(new LiteralControl("&nbsp;"));
			Controls.Add(new LiteralControl(string.Format("<img src=\"/_layouts/images/rect.gif\">&nbsp;<a href=\"{0}\">Edit Default Forum Permissions</a>", permissionLink)));
		}

		private void DisplayCategories()
		{
			CategoryCollection categories = RepositoryRegistry.CategoryRepository.GetAll();

			foreach (Category category in categories)
			{
				string categoryName = category.Name;
				int categoryId = category.Id;

				Controls.Add(new LiteralControl("<tr>"));
				string link = ForumApplication.Instance.GetLink(SharePointForumControls.ViewForums, "category={0}", categoryId);
				Controls.Add(new LiteralControl(string.Format("<td class=\"ms-TPHeader\" colspan=2><a href=\"{0}\"><strong>{1}</strong></a></td>", link, categoryName)));
				string editLink = ForumApplication.Instance.GetLink(SharePointForumControls.EditCategory, "category={0}", categoryId);
				Controls.Add(new LiteralControl(string.Format("<td align=center class=\"ms-TPHeader\"><a href=\"{0}\">Edit</a></td>", editLink)));
				Controls.Add(new LiteralControl("<td class=\"ms-TPHeader\">&nbsp;</td>"));
				Controls.Add(new LiteralControl("</tr>"));

				ForumCollection forumCollection = category.Forums;
				if (forumCollection.Count > 0)
				{
					DisplayForum(forumCollection);
				}
			}
		}

		private void DisplayForum(ForumCollection forumCollection)
		{
			foreach (Forum forum in forumCollection)
			{
				Controls.Add(new LiteralControl("<tr class=\"ms-alternating\">"));
				Controls.Add(new LiteralControl(string.Format("<td valign=\"top\"><img src=\"{0}\"></td>", ForumApplication.Instance.ForumImage)));

				string forumLink = ForumApplication.Instance.GetLink(SharePointForumControls.ViewTopics, "forum={0}", forum.Id);
				Controls.Add(new LiteralControl(string.Format("<td><a href=\"{0}\">{1}</a><br>{2}</td>", forumLink, forum.Name, forum.Description)));

				string editLink = ForumApplication.Instance.GetLink(SharePointForumControls.EditForum, "forum={0}", forum.Id);
				Controls.Add(new LiteralControl(string.Format("<td align=center><a href=\"{0}\">Edit</a></td>", editLink)));

				string permissionLink = ForumApplication.Instance.GetLink(SharePointForumControls.ManageForumPermissions, "forum={0}", forum.Id);
				Controls.Add(new LiteralControl(
					string.Format("<td align=center><a href=\"{0}\">Manage Permissions</a></td>", permissionLink)));
				Controls.Add(new LiteralControl("</tr>"));
			}
		}
	}
}