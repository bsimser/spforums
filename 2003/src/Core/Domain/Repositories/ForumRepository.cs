#region Using Directives

using System.Collections;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Repositories
{
	public class ForumRepository
	{
		#region Fields
		private ForumDao _dao;
		#endregion

		#region Constructors
		public ForumRepository()
		{
			_dao = new ForumDao();
		}
		#endregion

		#region Public Methods

		public int SavePermissions(int groupId, int forumId, Permission permission)
		{
			return _dao.SavePermissions(groupId, forumId, permission);
		}

		public Hashtable GetPermissionsForForum(int id)
		{
			return _dao.GetPermissionsForForum(id);
		}

		public int Save(Forum forum)
		{
			return _dao.Save(forum);
		}

		public ForumCollection GetAll()
		{
			return _dao.GetAll();
		}

		public ForumCollection FindByCategoryId(int id)
		{
			return _dao.FindByCategoryId(id);
		}

		public Forum GetById(int id)
		{
			return _dao.GetById(id);
		}

		public void IncreaseCount(int id)
		{
			Forum forum = GetById(id);
			forum.TopicCount++;
			Save(forum);
		}

		#endregion
	}
}