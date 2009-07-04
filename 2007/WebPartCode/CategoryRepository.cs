using System.Collections.Generic;
using Microsoft.SharePoint;

namespace BilSimser.SharePointForums.WebPartCode
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

        public IList<Category> GetAll()
        {
            return _dao.GetAll();
        }

        public void Save(IList<Category> collection)
        {
            foreach (Category category in collection)
            {
                _dao.Save(category);
            }
        }

        public int Save(Category category)
        {
            return _dao.Save(category);
        }

        public IList<Category> GetVisibleCategoriesForUser(SPUser user)
        {
            return _dao.GetAll();
        }
    }
}