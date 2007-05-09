#region Using Directives

using System;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Repositories
{
	public class MessageRepository
	{
		#region Fields
		private MessageDao _dao;
		#endregion

		#region Constructors
		public MessageRepository()
		{
			_dao = new MessageDao();
		}
		#endregion

		#region Public Methods

		public MessageCollection FindByKeywords(string keywords)
		{
			return _dao.FindByKeywords(keywords);
		}

		public MessageCollection FindByDate(DateTime dateCriteria)
		{
			return _dao.FindByDate(dateCriteria);
		}

		public void Save(Message message)
		{
			_dao.Save(message);
		}

		public Message GetById(int id)
		{
			return _dao.GetById(id);
		}

		public MessageCollection GetAll()
		{
			return _dao.GetAll();
		}

		public MessageCollection GetByTopicId(int id)
		{
			return _dao.GetByTopicId(id);
		}
		#endregion
	}
}