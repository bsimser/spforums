using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class ManageForumGroupPermissions : AdminBaseControl
	{
		private int forumId;
		private int groupId;
		private CheckBox chkRead;
		private CheckBox chkAdd;
		private CheckBox chkEdit;
		private CheckBox chkReply;
		private CheckBox chkDelete;
		private SPButton btnSubmit;
		private SPButton btnCancel;

		public ManageForumGroupPermissions()
		{
		}

		protected override void CreateAdminChildControls()
		{
			CreateControls();

			forumId = ValidInt(HttpContext.Current.Request.QueryString["forum"]);
			groupId = ValidInt(HttpContext.Current.Request.QueryString["group"]);

			Forum forum = RepositoryRegistry.ForumRepository.GetById(forumId);
			Group group = RepositoryRegistry.GroupRepository.FindById(groupId);
			Permission perms = forum.Permissions[groupId] as Permission;
			if (perms == null)
				perms = new Permission();

//			string permissionLink = ForumApplication.Instance.GetLink(SharePointForumControls.ManageForumPermissions, "forum={0}", forum.Id);
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.ManageForumPermissions, "forum={0}", forumId);

			AddBoxHeader(String.Format("Manage Permissions for \"{0}\" Group in Forum \"{1}\"", group.Name, forum.Name), false, 2);

			Controls.Add(CreateTableRow("Read", chkRead, perms.HasPermission(Permission.Rights.Read)));
			Controls.Add(CreateTableRow("Add", chkAdd, perms.HasPermission(Permission.Rights.Add)));
			Controls.Add(CreateTableRow("Edit", chkEdit, perms.HasPermission(Permission.Rights.Edit)));
			Controls.Add(CreateTableRow("Reply", chkReply, perms.HasPermission(Permission.Rights.Reply)));
			Controls.Add(CreateTableRow("Delete", chkDelete, perms.HasPermission(Permission.Rights.Delete)));
			Controls.Add(CreateButtonRow(btnSubmit, btnCancel));

			CloseBox();
		}

		private void CreateControls()
		{
			chkRead = new CheckBox();
			chkAdd = new CheckBox();
			chkEdit = new CheckBox();
			chkReply = new CheckBox();
			chkDelete = new CheckBox();

			btnSubmit = new SPButton("Submit");
			btnSubmit.Click += new EventHandler(btnSubmit_Click);

			btnCancel = new SPButton("Cancel");
			btnCancel.Click += new EventHandler(btnCancel_Click);
		}

		private TableRow CreateTableRow(string label, CheckBox cb, bool isChecked)
		{
			TableCell tableCell;
			TableRow tableRow;
			tableRow = new TableRow();
			tableCell = new TableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			tableCell.CssClass = "ms-navframe";
			tableCell.Width = Unit.Percentage(50);
			tableCell.Controls.Add(new LiteralControl(String.Format("<strong>{0}</strong>", label)));
			tableRow.Cells.Add(tableCell);
			tableCell = new TableCell();
			tableCell.Width = Unit.Percentage(50);
			cb.Checked = isChecked;
			tableCell.Controls.Add(cb);
			tableRow.Cells.Add(tableCell);
			return tableRow;
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			Forum forum = RepositoryRegistry.ForumRepository.GetById(forumId);
			Permission perms = forum.Permissions[groupId] as Permission;
			if (perms == null)
				perms = new Permission();

			perms.SetPermission(Permission.Rights.Read, chkRead.Checked);
			perms.SetPermission(Permission.Rights.Add, chkAdd.Checked);
			perms.SetPermission(Permission.Rights.Edit, chkEdit.Checked);
			perms.SetPermission(Permission.Rights.Reply, chkReply.Checked);
			perms.SetPermission(Permission.Rights.Delete, chkDelete.Checked);

			RepositoryRegistry.ForumRepository.SavePermissions(groupId, forumId, perms);

			RedirectToParent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			RedirectToParent();
		}
	}
}