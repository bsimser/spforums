using System;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class DeleteForums : AdminBaseControl
	{
		private SPButton btnExecute;

		public DeleteForums()
		{
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.DeleteForums);
		}

		protected override void CreateAdminChildControls()
		{
			AddBoxHeader("Delete Forum Data");
			AddText("This page will delete all forums and reset the system back to the default.");
			AddText("</p>");
			AddText("<div class=\"ms-alerttext\">WARNING!!! This will PERMANENTLY delete ALL your forums! Be sure you to do this.</div>");
			AddText("</p>");

			btnExecute = new SPButton("Delete Forums");
			btnExecute.Click += new EventHandler(btnExecute_Click);
			Controls.Add(btnExecute);
		}

		private void btnExecute_Click(object sender, EventArgs e)
		{
			try
			{
				DeleteList(ForumConstants.Lists_Posts);
				DeleteList(ForumConstants.Lists_Topics);
				DeleteList(ForumConstants.Lists_Forums);
				DeleteList(ForumConstants.Lists_ForumAccess);
				DeleteList(ForumConstants.Lists_Category);
				DeleteList(ForumConstants.Lists_Users);
				DeleteList(ForumConstants.Lists_Groups);
			}
			catch (ArgumentException)
			{
				throw;
			}
			finally
			{
				RedirectToParent();
			}
		}

		private void DeleteList(string listName)
		{
			SPList list;
			list = ForumApplication.Instance.SpWeb.Lists[listName];
			ForumApplication.Instance.SpWeb.Lists.Delete(list.ID);
		}
	}
}
