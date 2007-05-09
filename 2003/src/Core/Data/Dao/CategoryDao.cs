#region Using Directives

using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Data.Mappers;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data.Dao
{
	public class CategoryDao
	{
		private SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);

		public Category GetById(int id)
		{
			SharePointListItem currentItem = provider.GetListItembyID(ForumConstants.Lists_Category, id);
			return CategoryMapper.CreateDomainObject(currentItem);
		}

		/// <summary>
		/// Gets a collection of all categories in the system.
		/// </summary>
		/// <returns></returns>
		public CategoryCollection GetAll()
		{
			SharePointListDescriptor items = provider.GetAllListItems(ForumConstants.Lists_Category);

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
				categoryId = provider.AddListItem(ForumConstants.Lists_Category, listItem);
			else
				categoryId = provider.UpdateListItem(ForumConstants.Lists_Category, listItem);

			return categoryId;
		}
	}
}