#region Using Directives

using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data
{
	public class MessageDao : DataProviderBase
	{
		/// <summary>
		/// Gets all.
		/// </summary>
		/// <returns></returns>
		public MessageCollection GetAll()
		{
			MessageCollection messages = new MessageCollection();
			SharePointListDescriptor postItems = Provider.GetAllListItems(ForumConstants.Lists_Posts);
			foreach (SharePointListItem postItem in postItems.SharePointListItems)
			{
				messages.Add(MessageMapper.CreateDomainObject(postItem));
			}
			return messages;
		}

		public MessageCollection GetByTopicId(int id)
		{
			MessageCollection messages = new MessageCollection();
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

		public MessageCollection FindByDate(DateTime dateCriteria)
		{
			string isoDate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(dateCriteria);			
			SharePointListDescriptor listItems = Provider.GetListItemsByField(ForumConstants.Lists_Posts, "Modified", isoDate);
			MessageCollection messages = new MessageCollection();
			foreach (SharePointListItem item in listItems.SharePointListItems)
			{
				messages.Add(MessageMapper.CreateDomainObject(item));
			}
			return messages;
		}

		public MessageCollection FindByKeywords(string keywords)
		{
			// TODO this is broken and needs to use CAML instead
			MessageCollection messages = new MessageCollection();
			if (keywords == null)
				return messages;
			
			SPSearchResultCollection searchResults = ForumApplication.Instance.SpWeb.SearchListItems(keywords);
			foreach(SPSearchResult result in searchResults)
			{
				if(result.ListName.ToUpper() == ForumConstants.Lists_Posts.ToUpper())
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