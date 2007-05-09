#region Using Directives

using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.Common.Controls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using Microsoft.SharePoint.WebControls;
using OWSSubmitButton = BilSimser.SharePoint.Common.Controls.OWSSubmitButton;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class UpdateMessage : BaseForumControl
	{
		#region Fields

		private OWSForm _form;
		private OWSTextField _txtSubject;
		private OWSSubmitButton _btnPost;
		private OWSSubmitButton _btnCancel;
		private OWSRichTextField _txtBody;
		private string _postModeTitle = "Post";

		#endregion

		#region Constructors

		#endregion

		/// <summary>
		/// Notifies server controls that use composition-based implementation to create any child
		/// controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			try
			{
				string topicName = string.Empty;

				_form = new OWSForm();
				Controls.Add(_form);

				_form.Controls.Add(BuildBasePageLinks());
				_form.Controls.Add(new LiteralControl("<br>"));

				_form.Controls.Add(new LiteralControl("<table cellspacing=1 cellpadding=4 width=100% align=center border=1>"));
				_form.Controls.Add(new LiteralControl("<tr>"));
				_form.Controls.Add(new LiteralControl(string.Format("<td class=ms-ToolPaneTitle align=middle colspan=2>{0}</td>", _postModeTitle)));

				OWSLabelField lblSubject = new OWSLabelField();
				lblSubject.Text = "Subject:";

				_txtSubject = new OWSTextField();
				_txtSubject.ID = "txtSubject";
				_txtSubject.NumLines = 1;

				if (MessageMode != PostMode.New)
				{
					lblSubject.Visible = false;
					_txtSubject.Text = topicName;
					_txtSubject.Visible = false;
				}

				CreatePreviewSection();
				CreateMessageBodySection(topicName);
				CreateButtonSection();

				_form.Controls.Add(new LiteralControl("</table>"));
			}
			catch (Exception ex)
			{
				WebPartParent.AddError(ex);
			}
		}

		/// <summary>
		/// Creates the quoted message.
		/// </summary>
		/// <param name="topicName">Name of the topic.</param>
		private void CreateQuotedMessage(string topicName)
		{
			Message message = RepositoryRegistry.MessageRepository.GetById(messageID);
			_txtSubject.Text = string.Format("RE: {0}", topicName);
			string body = message.Body;
			if (MessageMode == PostMode.Quote)
			{
				body = string.Format("<strong>{0} wrote:</strong>\r\n<blockquote dir=ltr style=\"margin-right:0px; border-style: solid; border-width: 1px;\">\r\n<div>{1}</blockquote></div>",
				                     message.Author.Name, body);
			}
			_txtBody.Value = body;
		}

		/// <summary>
		/// Handles the Click event of the btnPost control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		/// <remarks>
		/// TODO move all this crap logic to the repository or somewhere... bad bad bad
		/// </remarks>
		private void btnPost_Click(object sender, EventArgs e)
		{
			Topic parentTopic;
			int listItemId = 0;

			if (MessageMode == PostMode.Edit)
			{
				parentTopic = RepositoryRegistry.TopicRepository.GetById(topicID);
				listItemId = messageID;
			}
			else if (MessageMode == PostMode.New)
			{
				parentTopic = new Topic(forumID, _txtSubject.Text);
				parentTopic.LastPost = DateTime.Now;
				parentTopic.TopicStarterId = ForumApplication.Instance.SpUser.ID;
				topicID = RepositoryRegistry.TopicRepository.Save(parentTopic);
				this.WebPartParent.TopicCount++;
			}
			else
			{
				parentTopic = RepositoryRegistry.TopicRepository.GetById(topicID);
			}

			string messageTitle;
			if(MessageMode == PostMode.New)
				messageTitle = _txtSubject.Text;
			else
				messageTitle = string.Format("RE: {0}", parentTopic.Name);

			Message message = new Message(topicID);
			message.Name = messageTitle;
			message.Id = listItemId;
			message.Body = _txtBody.Text;
			message.UserId = ForumApplication.Instance.SpUser.ID;
			message.Author = ForumApplication.Instance.CurrentUser;
			RepositoryRegistry.MessageRepository.Save(message);

			// Increase the post count in the main web part
			this.WebPartParent.PostCount++;
			this.WebPartParent.PersistProperties();

			// Increase the number of posts for this user
			message.Author.NumPosts++;
			RepositoryRegistry.ForumUserRepository.Save(message.Author);

			// Redirect to the new post
			string url = ForumApplication.Instance.GetLink(SharePointForumControls.ViewMessages, "topic={0}", topicID);
			Page.Response.Redirect(url);
		}

		/// <summary>
		/// Handles the Click event of the btnCancel control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			string url;

			if (MessageMode == PostMode.New)
			{
				url = ForumApplication.Instance.GetLink(SharePointForumControls.ViewTopics, "forum={0}", forumID);
			}
			else
			{
				url = ForumApplication.Instance.GetLink(SharePointForumControls.ViewMessages, "topic={0}", topicID);
			}

			Page.Response.Redirect(url);
		}

		private void CreateSubjectArea(OWSLabelField lblSubject)
		{
			_form.Controls.Add(new LiteralControl("<tr>"));
			_form.Controls.Add(new LiteralControl("<td valign=top align=right>"));
			_form.Controls.Add(lblSubject);
			_form.Controls.Add(new LiteralControl("</td>"));
			_form.Controls.Add(new LiteralControl("<td>"));
			_form.Controls.Add(_txtSubject);
			_form.Controls.Add(new LiteralControl("</td>"));
			_form.Controls.Add(new LiteralControl("</tr>"));
		}

		private void CreateMessageBodySection(string topicName)
		{
			_form.Controls.Add(new LiteralControl("<tr>"));
			OWSLabelField lblMessage = new OWSLabelField();
			lblMessage.Text = "Message:";
			_form.Controls.Add(new LiteralControl("<td valign=top align=right>"));
			_form.Controls.Add(lblMessage);
			_form.Controls.Add(new LiteralControl("</td>"));

			_form.Controls.Add(new LiteralControl("<td>"));

			_txtBody = new OWSRichTextField();
			_txtBody.ID = "txtBody";
			_txtBody.NumLines = 15;

			// Fill the body if we're editing or quoting
			if ((MessageMode == PostMode.Edit) || (MessageMode == PostMode.Quote))
			{
				CreateQuotedMessage(topicName);
			}

			_form.Controls.Add(_txtBody);

			_form.Controls.Add(new LiteralControl("</td>"));
			_form.Controls.Add(new LiteralControl("</tr>"));
		}

		private void CreatePreviewSection()
		{
			if(messageID != 0 && MessageMode == PostMode.Reply)
			{
				_form.Controls.Add(new LiteralControl("<tr>"));
				_form.Controls.Add(new LiteralControl("<td valign=top align=right>"));
				_form.Controls.Add(new LiteralControl("<div class=\"ms-formdescription\" valign=top align=left>Preview:</div>"));
				_form.Controls.Add(new LiteralControl("</td>"));
				_form.Controls.Add(new LiteralControl("<td>"));
				Message message = RepositoryRegistry.MessageRepository.GetById(messageID);
				string messageDisplay = string.Format("<strong>Reply to #{0}:</strong><br>{1}", messageID, message.Body);
				_form.Controls.Add(new LiteralControl(messageDisplay));
				_form.Controls.Add(new LiteralControl("</td>"));
				_form.Controls.Add(new LiteralControl("</tr>"));
			}
		}
		
		private void CreateButtonSection()
		{
			_form.Controls.Add(new LiteralControl("<tr>"));
			_form.Controls.Add(new LiteralControl("<td align=middle colspan=2>"));

			_form.Controls.Add(new LiteralControl("&nbsp;"));

			_btnPost = new OWSSubmitButton();
			_btnPost.Click += new EventHandler(btnPost_Click);
			_btnPost.Text = "Post";
			_btnPost.ID = "btnPost";
			_form.Controls.Add(_btnPost);

			_form.Controls.Add(new LiteralControl("&nbsp;"));
			_form.Controls.Add(new LiteralControl("&nbsp;"));
			_form.Controls.Add(new LiteralControl("&nbsp;"));

			_btnCancel = new OWSSubmitButton();
			_btnCancel.Click += new EventHandler(btnCancel_Click);
			_btnCancel.Text = "Cancel";
			_btnCancel.ID = "btnCancel";
			_form.Controls.Add(_btnCancel);

			_form.Controls.Add(new LiteralControl("</td>"));
			_form.Controls.Add(new LiteralControl("</tr>"));
		}
	}
}