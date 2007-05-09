using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Repositories;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Service.Application
{
	public class RepositoryRegistry
	{
		#region Fields

		private static readonly RepositoryRegistry instance = new RepositoryRegistry();

		#endregion

		#region Constructors

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

		#endregion

		#region Private methods

		private static RepositoryRegistry GetInstance()
		{
			return instance;
		}

		#endregion

		#region Protected Methods

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

		#endregion

		#region Public Methods

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

		#endregion
	}

	/// <summary>
	/// The mock repository registry returns registry objects 
	/// (based on a repository interface) so testing can be done
	/// without having to be tied to concrete classes.
	/// </summary>
	public class MockRepositoryRegistry : RepositoryRegistry
	{
		// TODO override protected methods to return mock repositories
	}
}