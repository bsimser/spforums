using System;
using System.Collections.Generic;

namespace BilSimser.SharePointForums.WebPartCode
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

        public IList<Topic> FindByForumId(int id)
        {
            return _dao.FindByForumId(id);
        }

        public IList<Topic> GetAll()
        {
            return _dao.GetAll();
        }

        public void Delete(Topic topic)
        {
            _dao.Delete(topic);
        }

        public IList<Topic> FindByDate(DateTime dateCriteria)
        {
            return _dao.FindByDate(dateCriteria);
        }

        public IList<Topic> FindInactive()
        {
            return _dao.FindInactive();
        }
    }
}