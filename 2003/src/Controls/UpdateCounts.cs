using System;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class UpdateCounts : AdminBaseControl
	{
		private SPButton btnUpdateCounts;
		
		protected override void CreateAdminChildControls()
		{
			AddBoxHeader("Update and Recalculate Forum Totals");
			AddText("This page will recalculate forum totals and update the counts for display in statistics.");
			AddText("</p>");

			btnUpdateCounts = new SPButton("Update Totals");
			btnUpdateCounts.Click += new EventHandler(btnUpdateCounts_Click);
			Controls.Add(btnUpdateCounts);
		}

		private void btnUpdateCounts_Click(object sender, EventArgs e)
		{
			int numForums = 0;
			int numTopics = 0;
			int numPosts = 0;
			
			CategoryCollection categories = RepositoryRegistry.CategoryRepository.GetAll();
			foreach (Category category in categories)
			{
				numForums += category.Forums.Count;
			
				foreach (Forum forum in category.Forums)
				{
					forum.TopicCount = forum.Topics.Count;
					numTopics += forum.TopicCount;

					forum.PostCount = 0;
					foreach (Topic topic in forum.Topics)
					{
						forum.PostCount += topic.Messages.Count;
						numPosts += forum.PostCount;
					}

					RepositoryRegistry.ForumRepository.Save(forum);
				}
			}

			WebPartParent.TopicCount = numTopics;
			WebPartParent.ForumCount = numForums;
			WebPartParent.PostCount = numPosts;
			WebPartParent.PersistProperties();
		}
	}
}
