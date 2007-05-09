#region Using Directives

using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Data.Mappers;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data.Dao
{
	public class MessageDao
	{
		public MessageCollection GetAll()
		{
			MessageCollection messages = new MessageCollection();
			SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
			SharePointListDescriptor postItems = provider.GetAllListItems(ForumConstants.Lists_Posts);
			foreach (SharePointListItem postItem in postItems.SharePointListItems)
			{
				messages.Add(MessageMapper.CreateDomainObject(postItem));
			}
			return messages;
		}
	}
}