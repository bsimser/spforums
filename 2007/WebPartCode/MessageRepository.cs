using System;
using System.Collections.Generic;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class MessageRepository
    {
        private MessageDao _dao;

        public MessageRepository()
        {
            _dao = new MessageDao();
        }

        public IList<Message> FindByKeywords(string keywords)
        {
            return _dao.FindByKeywords(keywords);
        }

        public IList<Message> FindByDate(DateTime dateCriteria)
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

        public IList<Message> GetAll()
        {
            return _dao.GetAll();
        }

        public IList<Message> GetByTopicId(int id)
        {
            return _dao.GetByTopicId(id);
        }
    }
}