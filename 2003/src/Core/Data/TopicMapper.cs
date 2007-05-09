#region Using Directives

using System;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data
{
	public class TopicMapper
	{
		/// <summary>
		/// Creates the dto.
		/// </summary>
		/// <param name="topic">The topic.</param>
		/// <returns></returns>
		public static SharePointListItem CreateDto(Topic topic)
		{
			string[] topicValues = {
				"Title", topic.Name,
				"ForumID", topic.ForumId.ToString(),
				"Views", topic.Views.ToString(),
				"TopicStarterID", topic.TopicStarterId.ToString(),
				"NumPosts", topic.NumPosts.ToString(),
			};

			return new SharePointListItem(topic.Id, topicValues);
		}

		/// <summary>
		/// Creates the domain object.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		public static Topic CreateDomainObject(SharePointListItem item)
		{
			Topic topic = new Topic(item.Id, Convert.ToInt32(item["ForumID"]), item["Title"]);

			topic.Views = Convert.ToInt32(item["Views"]);
			topic.TopicStarterId = Convert.ToInt32(item["TopicStarterID"]);
			topic.Author = RepositoryRegistry.ForumUserRepository.GetBySharePointId(topic.TopicStarterId);
			
			if(item["NumPosts"] == null || item["NumPosts"] == string.Empty)
				topic.NumPosts = 0;
			else
				topic.NumPosts = Convert.ToInt32(item["NumPosts"]);

			MessageCollection messages = topic.Messages;
			messages.Sort("Modified", SortDirection.Descending);
			topic.LastPost = Convert.ToDateTime(messages[messages.Count-1].Modified);

			return topic;
		}
	}
}