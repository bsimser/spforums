using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using Microsoft.SharePoint;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Service.Builders
{
	/// <summary>
	/// Summary description for MessageListBuilder.
	/// </summary>
	public class MessageListBuilder : ListBuilder
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageListBuilder"/> class.
		/// </summary>
		public MessageListBuilder()
		{
			this.listName = ForumConstants.Lists_Posts;
		}

		/// <summary>
		/// Adds the fields.
		/// </summary>
		public override void AddFields()
		{
			AddFieldToList("TopicID", SPFieldType.Number, true);
			AddFieldToList("UserID", SPFieldType.Number, false);
			AddFieldToList("Body", SPFieldType.Note, false);
		}

		/// <summary>
		/// Setups the default values.
		/// </summary>
		public override void AddSampleData()
		{
			Message message = new Message(1);
			message.Name = "Welcome to your new SharePoint Forum";
			message.UserId = 1;
			message.Body = "<DIV>This is an example post in your SharePoint Forum installation. You may delete this post, this topic and even this forum if you like since everything seems to be working!</DIV>";
			RepositoryRegistry.MessageRepository.Save(message);
		}
	}
}