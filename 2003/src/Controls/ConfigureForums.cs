#region Using Directives

using System;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class ConfigureForums : AdminBaseControl
	{
		#region Fields

		private TextBox txtForumName;
		private SPButton btnSubmit;
		private SPButton btnCancel;

		#endregion

		#region Constructors

		public ConfigureForums()
		{
		}

		#endregion

		#region Protected Methods

		protected override void CreateAdminChildControls()
		{
			AddBoxHeader("Forum Configuration", false, 2);

			CreateControls();
			LoadControlValues();

			Controls.Add(CreateRow(txtForumName, "Forum Name"));
			Controls.Add(CreateButtonRow(btnSubmit, btnCancel));

			CloseBox();
		}

		#endregion

		#region Private Methods

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.AdministrationPanel);
			RedirectToParent();
		}

		private void CreateControls()
		{
			txtForumName = new TextBox();

			btnSubmit = new SPButton("Submit");
			btnSubmit.Click += new EventHandler(btnSubmit_Click);

			btnCancel = new SPButton("Cancel");
			btnCancel.Click += new EventHandler(btnCancel_Click);
		}

		private void LoadControlValues()
		{
			txtForumName.Text = WebPartParent.Name;
		}

		private void SaveControlValues()
		{
			ForumApplication.Instance.Title = txtForumName.Text;
			WebPartParent.Name = ForumApplication.Instance.Title;
			WebPartParent.PersistProperties();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.AdministrationPanel);
			RedirectToParent();
		}

		#endregion
	}
}