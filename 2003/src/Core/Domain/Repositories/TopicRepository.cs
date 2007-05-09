#region Using Directives

using System;
using BilSimser.SharePoint.WebParts.Forums.Core.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Repositories
{
	public class TopicRepository
	{
		private TopicDao _dao;
		
		public TopicRepository()
		{
			_dao = new TopicDao();
		}

		public void IncreaseViewCount(int id)
		{
			Topic topic = GetById(id);
			topic.Views++;
			Save(topic);
		}

		public Topic GetById(int id)
		{
			return _dao.GetById(id);
		}

		public int Save(Topic topic)
		{
			return _dao.Save(topic);
		}

		public TopicCollection FindByForumId(int id)
		{
			return _dao.FindByForumId(id);
		}

		public TopicCollection GetAll()
		{
			return _dao.GetAll();
		}

		public void Delete(Topic topic)
		{
			_dao.Delete(topic);
		}

		public TopicCollection FindByDate(DateTime dateCriteria)
		{
			return _dao.FindByDate(dateCriteria);
		}

		public TopicCollection FindInactive()
		{
			return _dao.FindInactive();
		}
	}
}