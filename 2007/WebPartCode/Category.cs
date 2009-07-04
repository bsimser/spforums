using System.Collections.Generic;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class Category : DomainObject
    {
        private int sortOrder;
        private IList<Forum> forums;

        private Category()
        {
        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Category(string name)
        {
            Name = name;
        }

        public int SortOrder
        {
            get { return sortOrder; }
            set { sortOrder = value; }
        }

        public IList<Forum> Forums
        {
            get
            {
                if (null == forums)
                {
                    forums = RepositoryRegistry.ForumRepository.FindByCategoryId(Id);
                }

                return forums;
            }
        }

        /// <summary>
        /// Determines whether the specified user has access 
        /// to any forum in the category.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// 	<c>true</c> if the specified user has access; otherwise, <c>false</c>.
        /// </returns>
        public bool HasAccess(ForumUser user, Permission.Rights right)
        {
            foreach (Forum forum in Forums)
            {
                if (forum.HasAccess(user, right))
                {
                    return true;
                }
            }

            return false;
        }
    }
}