using System;
using System.Web;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class EditForum : AdminBaseControl
	{
		private int id;
		private TextBox txtName;
		private DropDownList ddlCategories;
		private TextBox txtDescription;
		private SPButton btnSubmit;
		private SPButton btnCancel;
		private Forum forum;

		public EditForum()
		{
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.ManageForums);
		}

		protected override void CreateAdminChildControls()
		{
			CreateControls();
			LoadControlValues();
			BuildUI();
		}

		private void BuildUI()
		{
			AddBoxHeader(String.Format("Editing Forum \"{0}\"", forum.Name), false, 2);
			Controls.Add(CreateRow(txtName, "Forum Name"));
			Controls.Add(CreateRow(ddlCategories, "Category"));
			Controls.Add(CreateRow(txtDescription, "Description"));
			Controls.Add(CreateButtonRow(btnSubmit, btnCancel));
			CloseBox();
		}

		private void LoadControlValues()
		{
			id = ValidInt(HttpContext.Current.Request.QueryString["forum"]);
			if (id == 0)
				forum = new Forum(1, "New Forum");
			else
				forum = RepositoryRegistry.ForumRepository.GetById(id);

			txtName.Text = forum.Name;
			ddlCategories.SelectedIndex = forum.CategoryId - 1;
			txtDescription.Text = forum.Description;
		}

		private void CreateControls()
		{
			txtName = new TextBox();

			ddlCategories = new DropDownList();
			ddlCategories.DataTextField = "Name";
			ddlCategories.DataValueField = "Id";
			ddlCategories.DataSource = RepositoryRegistry.CategoryRepository.GetAll();
			ddlCategories.DataBind();

			txtDescription = new TextBox();
			txtDescription.TextMode = TextBoxMode.MultiLine;
			txtDescription.Rows = 10;
			txtDescription.Columns = 30;

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
			int tempId = SaveControlValues();
//			id = ValidInt(HttpContext.Current.Request.QueryString["forum"]);
//			if(id == 0)
//			{
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.ManageForumPermissions, "forum={0}", tempId);
//			}
//			else
			{
				RedirectToParent();
			}
		}

		private int SaveControlValues()
		{
			id = ValidInt(HttpContext.Current.Request.QueryString["forum"]);
			if (id == 0)
				forum = new Forum(1, "");
			else
				forum = RepositoryRegistry.ForumRepository.GetById(id);

			forum.Name = txtName.Text;
			forum.CategoryId = ddlCategories.SelectedIndex + 1;
			forum.Description = txtDescription.Text;

			return RepositoryRegistry.ForumRepository.Save(forum);
		}
	}
}