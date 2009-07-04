using System.Collections;
using System.Collections.Generic;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class ForumRepository
    {
        private ForumDao _dao;

        public ForumRepository()
        {
            _dao = new ForumDao();
        }

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

        public IList<Forum> GetAll()
        {
            return _dao.GetAll();
        }

        public IList<Forum> FindByCategoryId(int id)
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
    }
}