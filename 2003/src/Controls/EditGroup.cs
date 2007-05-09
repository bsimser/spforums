using System;
using System.Web;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class EditGroup : AdminBaseControl
	{
		private int groupId;
		private TextBox txtName;
		private SPButton btnSubmit;
		private SPButton btnCancel;
		private Group group;

		public EditGroup()
		{
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.ManageGroups);
		}

		protected override void CreateAdminChildControls()
		{
			CreateControls();
			LoadControlValues();
			BuildUI();
		}

		private void BuildUI()
		{
			AddBoxHeader(String.Format("Editing Group \"{0}\"", group.Name), false, 2);
			Controls.Add(CreateRow(txtName, "Group Name"));
			Controls.Add(CreateButtonRow(btnSubmit, btnCancel));
			CloseBox();
		}

		private void LoadControlValues()
		{
			groupId = ValidInt(HttpContext.Current.Request.QueryString["group"]);
			if (groupId == 0)
				group = new Group("New Group");
			else
				group = RepositoryRegistry.GroupRepository.FindById(groupId);
			txtName.Text = group.Name;
		}

		private void CreateControls()
		{
			txtName = new TextBox();

			btnSubmit = new SPButton("Submit");
			btnSubmit.Click += new EventHandler(btnSubmit_Click);

			btnCancel = new SPButton("Cancel");
			btnCancel.Click += new EventHandler(btnCancel_Click);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			RedirectToParent();
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			RedirectToParent();
		}

		private void SaveControlValues()
		{
			groupId = ValidInt(HttpContext.Current.Request.QueryString["group"]);
			if (groupId == 0)
				group = new Group("New Group");
			else
				group = RepositoryRegistry.GroupRepository.FindById(groupId);
			group.Name = txtName.Text;
			RepositoryRegistry.GroupRepository.Save(group);
		}
	}
}