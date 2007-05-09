#region Using Directives

using BilSimser.SharePoint.WebParts.Forums.Core.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Repositories
{
	public class ForumUserRepository
	{
		private ForumUserDao _dao;

		public ForumUserRepository()
		{
			_dao = new ForumUserDao();
		}

		public ForumUser GetBySharePointId(int id)
		{
			return _dao.GetBySharePointId(id);
		}

		public ForumUserCollection GetAll()
		{
			return _dao.GetAll();
		}

		public int GetCount()
		{
			return _dao.Count;
		}

		public int Save(ForumUser user)
		{
			return _dao.Save(user);
		}

		public ForumUser GetLast()
		{
			return _dao.GetLast();
		}

		public ForumUser GetById(int id)
		{
			return _dao.GetById(id);
		}
	}
}