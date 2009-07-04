using System;
using System.Collections.Generic;
using Microsoft.SharePoint.Utilities;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class TopicDao : DataProviderBase
    {
        public TopicDao()
        {
        }

        public Topic GetById(int id)
        {
            SharePointListItem currentItem = Provider.GetListItemByField(ForumConstants.Lists_Topics, "ID", id.ToString());

            // FIXME bit of a hack
            if (currentItem == null)
                return new Topic(id, 1, "Unknown Topic");

            return TopicMapper.CreateDomainObject(currentItem);
        }

        public int Save(Topic topic)
        {
            SharePointListItem listItem = TopicMapper.CreateDto(topic);
            int newTopicId = 0;

            if (topic.Id == 0)
            {
                newTopicId = Provider.AddListItem(ForumConstants.Lists_Topics, listItem);
                RepositoryRegistry.ForumRepository.IncreaseCount(topic.ForumId);
            }
            else
            {
                newTopicId = Provider.UpdateListItem(ForumConstants.Lists_Topics, listItem);
            }

            return newTopicId;
        }

        public IList<Topic> FindByForumId(int id)
        {
            IList<Topic> topicCollection = new List<Topic>();

            SharePointListDescriptor descriptor = Provider.GetListItemsByField(ForumConstants.Lists_Topics, "ForumID", id.ToString());
            foreach (SharePointListItem listItem in descriptor.SharePointListItems)
            {
                topicCollection.Add(TopicMapper.CreateDomainObject(listItem));
            }

            return topicCollection;
        }

        public IList<Topic> GetAll()
        {
            IList<Topic> topicCollection = new List<Topic>();

            SharePointListDescriptor descriptor = Provider.GetAllListItems(ForumConstants.Lists_Topics);
            foreach (SharePointListItem listItem in descriptor.SharePointListItems)
            {
                topicCollection.Add(TopicMapper.CreateDomainObject(listItem));
            }

            return topicCollection;
        }

        public void Delete(Topic topic)
        {
            foreach (Message message in topic.Messages)
            {
                Provider.DeleteListItem(ForumConstants.Lists_Posts, message.Id);
            }
            Provider.DeleteListItem(ForumConstants.Lists_Topics, topic.Id);
        }

        public IList<Topic> FindByDate(DateTime dateCriteria)
        {
            string isoDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(dateCriteria);
            SharePointListDescriptor listItems = Provider.GetListItemsByField(ForumConstants.Lists_Topics, "Modified", isoDate);
            IList<Topic> topics = new List<Topic>();
            foreach (SharePointListItem item in listItems.SharePointListItems)
            {
                topics.Add(TopicMapper.CreateDomainObject(item));
            }
            return topics;
        }

        public IList<Topic> FindInactive()
        {
            SharePointListDescriptor listItems = Provider.GetListItemsByField(
                ForumConstants.Lists_Topics, "NumPosts", "1");
            IList<Topic> topics = new List<Topic>();
            foreach (SharePointListItem item in listItems.SharePointListItems)
            {
                topics.Add(TopicMapper.CreateDomainObject(item));
            }
            return topics;
        }
    }
}