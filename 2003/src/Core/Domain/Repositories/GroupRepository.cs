#region Using Directives

using BilSimser.SharePoint.WebParts.Forums.Core.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Repositories
{
	public class GroupRepository
	{
		private GroupDao _dao;

		/// <summary>
		/// Initializes a new instance of the <see cref="GroupRepository"/> class.
		/// </summary>
		public GroupRepository()
		{
			_dao = new GroupDao();
		}

		/// <summary>
		/// Finds the by id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Group FindById(int id)
		{
			return _dao.FindById(id);
		}

		/// <summary>
		/// Gets all.
		/// </summary>
		/// <returns></returns>
		public GroupCollection GetAll()
		{
			return _dao.GetAll();
		}

		/// <summary>
		/// Saves the specified group.
		/// </summary>
		/// <param name="group">The group.</param>
		/// <returns></returns>
		public int Save(Group group)
		{
			return _dao.Save(group);
		}
	}
}