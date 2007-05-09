#region Using Directives

using System;
using System.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities
{
	/// <summary>
	/// Summary description for Forum.
	/// </summary>
	public class Forum : DomainObject
	{
		#region Fields

		private string description;
		private int categoryId;
		private DateTime lastPost;
		private TopicCollection topics;
		private Hashtable permissions;
		private int _topicCount;
		private int _postCount;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DomainObject"/> class.
		/// </summary>
		private Forum()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DomainObject"/> class.
		/// </summary>
		/// <param name="categoryId">The category id.</param>
		/// <param name="name">The name.</param>
		public Forum(int categoryId, string name)
		{
			CategoryId = categoryId;
			Name = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DomainObject"/> class.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="categoryId">The category id.</param>
		/// <param name="name">The name.</param>
		public Forum(int id, int categoryId, string name)
		{
			Id = id;
			CategoryId = categoryId;
			Name = name;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the topics.
		/// </summary>
		/// <value>The topics.</value>
		public TopicCollection Topics
		{
			get
			{
				if (null == topics)
				{
					topics = RepositoryRegistry.TopicRepository.FindByForumId(Id);
				}

				return topics;
			}
		}

		/// <summary>
		/// Gets or sets the post count.
		/// </summary>
		/// <value>The post count.</value>
		public int PostCount
		{
			get
			{
				return _postCount;
			}
			set
			{
				_postCount = value;
			}
		}

		/// <summary>
		/// Gets or sets the topic count.
		/// </summary>
		/// <value>The topic count.</value>
		public int TopicCount
		{
			get
			{
				return _topicCount;
			}
			set
			{
				_topicCount = value;
			}
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		/// <summary>
		/// Gets or sets the category id.
		/// </summary>
		/// <value>The category id.</value>
		public int CategoryId
		{
			get { return categoryId; }
			set { categoryId = value; }
		}

		/// <summary>
		/// Gets or sets the last post.
		/// </summary>
		/// <value>The last post.</value>
		public DateTime LastPost
		{
			get { return lastPost; }
			set { lastPost = value; }
		}

		/// <summary>
		/// Gets the permissions for this forum. Permissions are
		/// kept in a hashtable. The key is the group id and the
		/// value contains a <see cref="Permissions"/>object with the
		/// rights for the forum, corresponding to that group id.
		/// </summary>
		/// <value>The permissions.</value>
		public Hashtable Permissions
		{
			get
			{
				if (null == permissions)
				{
					permissions = RepositoryRegistry.ForumRepository.GetPermissionsForForum(Id);
				}
				return permissions;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Determines whether the specified user has access.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="rights">The rights.</param>
		/// <returns>
		/// 	<c>true</c> if the specified user has access; otherwise, <c>false</c>.
		/// </returns>
		public bool HasAccess(ForumUser user, Permission.Rights rights)
		{
			foreach (Group userGroup in user.Groups)
			{
				Permission perm = Permissions[userGroup.Id] as Permission;
				if (perm != null)
				{
					if (perm.HasPermission(rights))
						return true;
				}
			}

			return false;
		}

		#endregion
	}
}