using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using Microsoft.SharePoint;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Service.Builders
{
	public class ForumListBuilder : ListBuilder
	{
		public ForumListBuilder()
		{
			this.listName = ForumConstants.Lists_Forums;
		}

		public override void AddFields()
		{
			AddFieldToList("CategoryID", SPFieldType.Number, true);
			AddFieldToList("Description", SPFieldType.Note, false);
			AddFieldToList("SortOrder", SPFieldType.Number, true);
			AddFieldToList("TopicCount", SPFieldType.Number, false);
			AddFieldToList("PostCount", SPFieldType.Number, false);
		}

		public override void AddSampleData()
		{
			Forum forum = new Forum(1, "Test Forum 1");
			forum.Description = "This is just a test forum, nothing special here.";
			RepositoryRegistry.ForumRepository.Save(forum);
		}
	}
}