namespace BilSimser.SharePointForums.WebPartCode
{
    public class RepositoryRegistry
    {
        private static readonly RepositoryRegistry instance = new RepositoryRegistry();

        /// <summary>
        /// Initializes the <see cref="RepositoryRegistry"/> class.
        /// </summary>
        static RepositoryRegistry()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryRegistry"/> class.
        /// </summary>
        protected RepositoryRegistry()
        {
        }

        private static RepositoryRegistry GetInstance()
        {
            return instance;
        }

        protected CategoryRepository GetCategoryRepository()
        {
            return new CategoryRepository();
        }

        protected ForumRepository GetForumRepository()
        {
            return new ForumRepository();
        }

        protected ForumUserRepository GetForumUserRepository()
        {
            return new ForumUserRepository();
        }

        protected GroupRepository GetGroupRepository()
        {
            return new GroupRepository();
        }

        protected MessageRepository GetMessageRepository()
        {
            return new MessageRepository();
        }

        protected TopicRepository GetTopicRepository()
        {
            return new TopicRepository();
        }

        public static CategoryRepository CategoryRepository
        {
            get { return GetInstance().GetCategoryRepository(); }
        }

        public static ForumRepository ForumRepository
        {
            get { return GetInstance().GetForumRepository(); }
        }

        public static ForumUserRepository ForumUserRepository
        {
            get { return GetInstance().GetForumUserRepository(); }
        }

        public static GroupRepository GroupRepository
        {
            get { return GetInstance().GetGroupRepository(); }
        }

        public static MessageRepository MessageRepository
        {
            get { return GetInstance().GetMessageRepository(); }
        }

        public static TopicRepository TopicRepository
        {
            get { return GetInstance().GetTopicRepository(); }
        }
    }
}