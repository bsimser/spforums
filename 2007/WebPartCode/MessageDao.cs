using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class MessageDao : DataProviderBase
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IList<Message> GetAll()
        {
            IList<Message> messages = new List<Message>();
            SharePointListDescriptor postItems = Provider.GetAllListItems(ForumConstants.Lists_Posts);
            foreach (SharePointListItem postItem in postItems.SharePointListItems)
            {
                messages.Add(MessageMapper.CreateDomainObject(postItem));
            }
            return messages;
        }

        public IList<Message> GetByTopicId(int id)
        {
            IList<Message> messages = new List<Message>();
            SharePointListDescriptor postItems = Provider.GetListItemsByField(ForumConstants.Lists_Posts, "TopicID", id.ToString());
            foreach (SharePointListItem postItem in postItems.SharePointListItems)
            {
                messages.Add(MessageMapper.CreateDomainObject(postItem));
            }
            return messages;
        }

        public Message GetById(int id)
        {
            SharePointListItem postItem = Provider.GetListItemByField(ForumConstants.Lists_Posts, "ID", id.ToString());
            return MessageMapper.CreateDomainObject(postItem);
        }

        public void Save(Message message)
        {
            SharePointListItem listItem = MessageMapper.CreateDto(message);
            if (message.Id == 0)
            {
                Provider.AddListItem(ForumConstants.Lists_Posts, listItem);
                //				TopicRepository.IncreasePostCount(message.TopicId);
            }
            else
            {
                Provider.UpdateListItem(ForumConstants.Lists_Posts, listItem);
            }
        }

        public IList<Message> FindByDate(DateTime dateCriteria)
        {
            string isoDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(dateCriteria);
            SharePointListDescriptor listItems = Provider.GetListItemsByField(ForumConstants.Lists_Posts, "Modified", isoDate);
            IList<Message> messages = new List<Message>();
            foreach (SharePointListItem item in listItems.SharePointListItems)
            {
                messages.Add(MessageMapper.CreateDomainObject(item));
            }
            return messages;
        }

        public IList<Message> FindByKeywords(string keywords)
        {
            // TODO this is broken and needs to use CAML instead
            IList<Message> messages = new List<Message>();
            if (keywords == null)
                return messages;

            SPSearchResultCollection searchResults = ForumApplication.Instance.SpWeb.SearchListItems(keywords);
            foreach (SPSearchResult result in searchResults)
            {
                if (result.ListName.ToUpper() == ForumConstants.Lists_Posts.ToUpper())
                {
                    Message message = new Message(0);
                    message.Name = result.Title;
                    messages.Add(message);
                }
            }

            return messages;
        }
    }
}