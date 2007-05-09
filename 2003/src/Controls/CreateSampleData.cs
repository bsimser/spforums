#region Using Directives

using System;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class CreateSampleData : AdminBaseControl
	{
		private SPButton btnExecute;

		public CreateSampleData()
		{
			ParentLink = ForumApplication.Instance.GetLink(SharePointForumControls.CreateSampleData);
		}

		protected override void CreateAdminChildControls()
		{
			AddBoxHeader("Create Sample Data");
			AddText("This page will allow you to create a random number of messages for testing purposes.");
			AddText("</p>");
			AddText("<div class=\"ms-alerttext\">WARNING!!! This will overwrite or potentially damage a real forum, proceed with caution!</div>");
			AddText("</p>");

			btnExecute = new SPButton("Create Sample Data");
			btnExecute.Click += new EventHandler(btnExecute_Click);
			Controls.Add(btnExecute);
		}

		private void btnExecute_Click(object sender, EventArgs e)
		{
			try
			{
				Random rnd = new Random();
				int max1, max2, max3, max4;
				
				max1 = rnd.Next(2, 5);
				for (int c = 1; c < max1; c++)
				{
					string categoryName = string.Format("Test Category {0}", c+1);
					Category category = new Category(categoryName);
					int catId = RepositoryRegistry.CategoryRepository.Save(category);
					
					max2 = rnd.Next(3, 8);
					for (int f = 1; f < max2; f++)
					{
						string forumName = string.Format("Test Forum {0} in category {1}", f+1, categoryName);
						Forum forum = new Forum(catId, forumName);
						forum.Description = "This is just a test forum, nothing special here.";
						int forumId = RepositoryRegistry.ForumRepository.Save(forum);
						
						max3 = rnd.Next(3, 10);
						for (int t = 1; t < max3; t++)
						{
							string topicName = string.Format("Test Topic {0} in forum {1}", t+1, forumName);
							Topic topic = new Topic(forumId, topicName);
							topic.TopicStarterId = 1;
							int topicId = RepositoryRegistry.TopicRepository.Save(topic);
							
							max4 = rnd.Next(3, 10);
							for (int m = 0; m < max4; m++)
							{
								Message message = new Message(topicId);
								message.Name = "Just a test message.";
								message.Body = "You'll want to delete these messages. Use the Admin function \"Delete Forums\" to clear them out.";
								message.UserId = 1;
								RepositoryRegistry.MessageRepository.Save(message);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				RedirectToParent();
			}
		}
	}
}
