using System;
using System.Web;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class EditCategory : AdminBaseControl
	{
		private int categoryId;
		private TextBox txtName;
		private SPButton btnSubmit;
		private SPButton btnCancel;
		private Category category;

		public EditCategory()
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
			AddBoxHeader(String.Format("Editing Category \"{0}\"", category.Name), false, 2);
			Controls.Add(CreateRow(txtName, "Category Name"));
			Controls.Add(CreateButtonRow(btnSubmit, btnCancel));
			CloseBox();
		}

		private void LoadControlValues()
		{
			categoryId = ValidInt(HttpContext.Current.Request.QueryString["category"]);
			if (categoryId == 0)
				category = new Category("New Category");
			else
				category = RepositoryRegistry.CategoryRepository.GetById(categoryId);
			txtName.Text = category.Name;
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
			categoryId = ValidInt(HttpContext.Current.Request.QueryString["category"]);
			if (categoryId == 0)
				category = new Category("New Category");
			else
				category = RepositoryRegistry.CategoryRepository.GetById(categoryId);
			category.Name = txtName.Text;
			RepositoryRegistry.CategoryRepository.Save(category);
		}
	}
}