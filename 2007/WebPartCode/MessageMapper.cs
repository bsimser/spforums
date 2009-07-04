using System;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class MessageMapper
    {
        public static Message CreateDomainObject(SharePointListItem listItem)
        {
            Message message = new Message(Convert.ToInt32(listItem["TopicID"]));

            message.Name = listItem["Title"];
            message.UserId = Convert.ToInt32(listItem["UserID"]);
            message.Author = RepositoryRegistry.ForumUserRepository.GetBySharePointId(message.UserId);
            message.Created = Convert.ToDateTime(listItem["Created"]);
            message.Body = listItem["Body"];
            message.Id = listItem.Id;

            return message;
        }

        public static SharePointListItem CreateDto(Message message)
        {
            string[] postValues = {
				"Title", message.Name,
				"TopicID", message.TopicId.ToString(),
				"Body", message.Body,
				"UserID", message.UserId.ToString(),
			};

            return new SharePointListItem(message.Id, postValues);
        }
    }
}