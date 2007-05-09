#region Using Directives

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class DeleteTopic : BaseForumControl
	{
		#region Fields

		private Topic topic;
		private Label lblWarning;
		private SPButton btnSubmit;
		private SPButton btnCancel;

		#endregion

		public DeleteTopic()
		{
		}

		protected override void CreateChildControls()
		{
			InitControlVariables();
			CreateControls();
			BuildUI();
		}

		private void InitControlVariables()
		{
			if(this.Page.IsPostBack)
			{
				topicID = (int) ViewState["topicID"];
			}
			else
			{
				ViewState["topicID"] = topicID;				
			}

			this.topic = RepositoryRegistry.TopicRepository.GetById(topicID);
			base.ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.ViewTopics, "forum={0}", forumID);
		}

		private void CreateControls()
		{
			lblWarning = new Label();
			lblWarning.Text = string.Format("Warning, you are about to delete the topic named<br>\"{0}\"<br>containing {1} posts.<p>This operation cannot be undone.<p>Are you sure?", topic.Name, topic.NumPosts);
			lblWarning.CssClass = "ms-alerttext";

			btnSubmit = new SPButton("Submit");
			btnSubmit.Click += new EventHandler(btnSubmit_Click);

			btnCancel = new SPButton("Cancel");
			btnCancel.Click += new EventHandler(btnCancel_Click);
		}

		private void BuildUI()
		{
			Controls.Add(BuildBasePageLinks());
			Controls.Add(new LiteralControl("<br>"));
	
			AddBoxHeader(String.Format("Delete Topic \"{0}\"", topic.Name), false, 2);
			Controls.Add(CreateRow(lblWarning));
			Controls.Add(CreateButtonRow(btnSubmit, btnCancel));
	
			CloseBox();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			RedirectToParent();
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			RepositoryRegistry.TopicRepository.Delete(this.topic);
			RedirectToParent();
		}
	}
}
