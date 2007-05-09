#region Using Directives

using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data
{
	public class CategoryDao : DataProviderBase
	{
		/// <summary>
		/// Gets a category by its identity.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Category GetById(int id)
		{
			SharePointListItem currentItem = Provider.GetListItembyID(ForumConstants.Lists_Category, id);
			return CategoryMapper.CreateDomainObject(currentItem);
		}

		/// <summary>
		/// Gets all categories.
		/// </summary>
		/// <returns></returns>
		public CategoryCollection GetAll()
		{
			SharePointListDescriptor items = Provider.GetAllListItems(ForumConstants.Lists_Category);

			CategoryCollection categoryCollection = new CategoryCollection();
			foreach (SharePointListItem listItem in items.SharePointListItems)
			{
				categoryCollection.Add(CategoryMapper.CreateDomainObject(listItem));
			}
			return categoryCollection;
		}

		/// <summary>
		/// Saves the specified category.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <returns></returns>
		public int Save(Category category)
		{
			SharePointListItem listItem = CategoryMapper.CreateDto(category);
			int categoryId;

			if (listItem.Id == 0)
				categoryId = Provider.AddListItem(ForumConstants.Lists_Category, listItem);
			else
				categoryId = Provider.UpdateListItem(ForumConstants.Lists_Category, listItem);

			return categoryId;
		}
	}
}