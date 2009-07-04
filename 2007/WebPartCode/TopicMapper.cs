using System;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class TopicMapper
    {
        public static SharePointListItem CreateDto(Topic topic)
        {
            string[] topicValues = {
				"Title", topic.Name,
				"ForumID", topic.ForumId.ToString(),
				"Views", topic.Views.ToString(),
				"TopicStarterID", topic.TopicStarterId.ToString(),
			};

            return new SharePointListItem(topic.Id, topicValues);
        }

        public static Topic CreateDomainObject(SharePointListItem item)
        {
            Topic topic = new Topic(item.Id, Convert.ToInt32(item["ForumID"]), item["Title"]);

            topic.Views = Convert.ToInt32(item["Views"]);
            topic.TopicStarterId = Convert.ToInt32(item["TopicStarterID"]);
            topic.Author = RepositoryRegistry.ForumUserRepository.GetBySharePointId(topic.TopicStarterId);

            // Built in values from SharePoint list
            // TODO might be the wrong value
            topic.LastPost = Convert.ToDateTime(item["Modified"]);

            return topic;
        }
    }
}