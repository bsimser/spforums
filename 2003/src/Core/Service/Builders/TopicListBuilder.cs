using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using Microsoft.SharePoint;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Service.Builders
{
	/// <summary>
	/// Summary description for TopicListBuilder.
	/// </summary>
	public class TopicListBuilder : ListBuilder
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TopicListBuilder"/> class.
		/// </summary>
		public TopicListBuilder()
		{
			this.listName = ForumConstants.Lists_Topics;
		}

		/// <summary>
		/// Adds the fields.
		/// </summary>
		public override void AddFields()
		{
			AddFieldToList("ForumID", SPFieldType.Number, true);
			AddFieldToList("Views", SPFieldType.Number, false);
			AddFieldToList("NumPosts", SPFieldType.Number, false);
			AddFieldToList("TopicStarterID", SPFieldType.Number, false);
		}

		/// <summary>
		/// Setups the default values.
		/// </summary>
		public override void AddSampleData()
		{
			Topic topic = new Topic(1, "Welcome to your new SharePoint Forum");
			topic.TopicStarterId = 1;
			RepositoryRegistry.TopicRepository.Save(topic);
		}
	}
}