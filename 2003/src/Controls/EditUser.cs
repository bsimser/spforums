using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class EditUser : AdminBaseControl
	{
		private int userId;
		private SPButton btnSubmit;
		private SPButton btnCancel;
		private CheckBox cbIsAdmin;
		private ForumUser user;

		public EditUser()
		{
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.ManageUsers);
		}

		protected override void CreateAdminChildControls()
		{
			CreateControls();
			LoadControlValues();
			BuildUI();
		}

		private void LoadControlValues()
		{
			userId = ValidInt(HttpContext.Current.Request.QueryString["id"]);
			user = RepositoryRegistry.ForumUserRepository.GetById(userId);
			cbIsAdmin.Checked = user.IsAdmin;
		}

		private void SaveControlValues()
		{
			userId = ValidInt(HttpContext.Current.Request.QueryString["id"]);
			user = RepositoryRegistry.ForumUserRepository.GetById(userId);

			user.IsAdmin = cbIsAdmin.Checked;

			foreach (object control in Controls)
			{
				if (control is TableRow)
				{
					TableRow row = control as TableRow;
					foreach (object o in row.Controls)
					{
						if (o is TableCell)
						{
							TableCell cell = o as TableCell;
							foreach (object control1 in cell.Controls)
							{
								if (control1 is CheckBox)
								{
									CheckBox cb = control1 as CheckBox;
									int id = Convert.ToInt32(cb.ID);
									if (id != 0)
									{
										Group group = RepositoryRegistry.GroupRepository.FindById(id);
										Group userGroup = user.Groups.Find(group.Id);
										if (cb.Checked)
										{
											if (userGroup == null)
												user.Groups.Add(group);
										}
										else
										{
											if (userGroup != null)
												user.Groups.Remove(userGroup);
										}
									}
								}
							}
						}
					}
				}
			}

			RepositoryRegistry.ForumUserRepository.Save(user);
		}

		private void CreateControls()
		{
			cbIsAdmin = new CheckBox();

			btnSubmit = new SPButton("Submit");
			btnSubmit.Click += new EventHandler(btnSubmit_Click);

			btnCancel = new SPButton("Cancel");
			btnCancel.Click += new EventHandler(btnCancel_Click);
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.RedirectToParent();
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			SaveControlValues();
			base.RedirectToParent();
		}

		private void BuildUI()
		{
			AddBoxHeader(String.Format("Editing User \"{0}\"", user.Name), false, 2);

			Controls.Add(CreateRow(new LiteralControl(user.Name), "User Name"));
			AddUserDetailRow("Joined", user.Joined.ToString());
			AddUserDetailRow("Last Visit", user.LastVisit.ToString());
			AddUserDetailRow("Number of Posts", user.NumPosts.ToString());
			AddUserDetailRow("Email", String.Format("<a href=\"mailto:{0}\">{0}</a>", user.Email));

			BuildUserGroupUI();

			Controls.Add(CreateRow(cbIsAdmin, "Is Administrator"));
			Controls.Add(CreateButtonRow(btnSubmit, btnCancel));

			CloseBox();
		}

		private void BuildUserGroupUI()
		{
			GroupCollection allGroups = RepositoryRegistry.GroupRepository.GetAll();

			foreach (Group group in allGroups)
			{
				CheckBox cb = new CheckBox();
				cb.ID = group.Id.ToString();

				if (user.Groups.Find(group.Id) != null)
				{
					cb.Checked = true;
				}

				Controls.Add(CreateRow(cb, group.Name));
			}
		}

		private void AddUserDetailRow(string label, string text)
		{
			Controls.Add(CreateRow(new LiteralControl(text), label));
		}
	}
}