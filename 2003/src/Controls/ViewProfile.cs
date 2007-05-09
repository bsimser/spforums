#region Using Directives

using System;
using System.Web;
using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class ViewProfile : BaseForumControl
	{
		#region Protected Methods
		protected override void CreateChildControls()
		{
			ForumUser user = null;
			int userId = ValidInt(HttpContext.Current.Request.QueryString["userid"]);
			if (userId != 0)
			{
				user = RepositoryRegistry.ForumUserRepository.GetBySharePointId(userId);
			}
			if (null == user)
			{
				user = ForumApplication.Instance.CurrentUser;
			}

			Controls.Add(BuildPageLinks(user.Name, ForumApplication.Instance.GetLink(SharePointForumControls.ViewProfile)));

			AddBoxHeader(String.Format("{0} {1}", this.WebPartParent.LoadResource("UserInfoBox.View"), user.Name), false, 2);

			AddUserDetailRow(this.WebPartParent.LoadResource("UserInfoBox.Joined"), user.Joined.ToString());
			AddUserDetailRow(this.WebPartParent.LoadResource("UserInfoBox.LastVisit"), user.LastVisit.ToString());
			AddUserDetailRow(this.WebPartParent.LoadResource("UserInfoBox.NumberOfPosts"), user.NumPosts.ToString());
			AddUserDetailRow(this.WebPartParent.LoadResource("UserInfoBox.Email"), String.Format("<a href=\"mailto:{0}\">{0}</a>", user.Email));

			CloseBox();
		}
		#endregion

		#region Private Methods
		private void AddUserDetailRow(string label, string text)
		{
			Controls.Add(new LiteralControl(String.Format("<tr><td align=right class=ms-navframe><strong>{0}</strong></td><td>{1}</td></tr>", label, text)));
		}
		#endregion
	}
}