using System.Collections.Generic;

namespace BilSimser.SharePointForums.WebPartCode
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

        public IList<ForumUser> GetAll()
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