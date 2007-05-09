using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using Microsoft.SharePoint;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Service.Builders
{
	/// <summary>
	/// Summary description for CategoryListBuilder.
	/// </summary>
	public class CategoryListBuilder : ListBuilder
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryListBuilder"/> class.
		/// </summary>
		public CategoryListBuilder()
		{
			listName = ForumConstants.Lists_Category;
		}

		/// <summary>
		/// Setups the default values.
		/// </summary>
		public override void AddSampleData()
		{
			if (ListExists)
			{
				string[] values = {
					"Title", "Test Category 1",
					"SortOrder", "1",
				};

				AddListValues(values);
			}
		}

		/// <summary>
		/// Adds the fields.
		/// </summary>
		public override void AddFields()
		{
			AddFieldToList("SortOrder", SPFieldType.Number, true);
		}
	}
}