#region Using Directives

using BilSimser.SharePoint.WebParts.Forums.Core.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using Microsoft.SharePoint;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Repositories
{
	public class CategoryRepository
	{
		private CategoryDao _dao;

		public CategoryRepository()
		{
			_dao = new CategoryDao();
		}

		public Category GetById(int id)
		{
			return _dao.GetById(id);
		}

		public CategoryCollection GetAll()
		{
			return _dao.GetAll();
		}

		public void Save(CategoryCollection collection)
		{
			foreach(Category category in collection)
			{
				_dao.Save(category);
			}
		}

		public int Save(Category category)
		{
			return _dao.Save(category);
		}

		public CategoryCollection GetVisibleCategoriesForUser(SPUser user)
		{
			return _dao.GetAll();
		}
	}
}