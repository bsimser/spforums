#region Using Directives

using System;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities
{
	public class Topic : DomainObject
	{
		#region Fields

		private int _forumId;
		private ForumUser _author;
		private int _views;
		private DateTime _lastPost;
		private int _topicStarterId;
		private MessageCollection _messages;
		private int _numPosts;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Topic"/> class.
		/// </summary>
		private Topic()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Topic"/> class.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="forumId">The forum id.</param>
		/// <param name="name">The name.</param>
		public Topic(int id, int forumId, string name)
		{
			Id = id;
			ForumId = forumId;
			Name = name;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Topic"/> class.
		/// </summary>
		/// <param name="forumId">The forum id.</param>
		/// <param name="name">The name.</param>
		public Topic(int forumId, string name)
		{
			ForumId = forumId;
			Name = name;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the topic starter id.
		/// </summary>
		/// <value>The topic starter id.</value>
		public int TopicStarterId
		{
			get { return _topicStarterId; }
			set { _topicStarterId = value; }
		}

		/// <summary>
		/// Gets the posts.
		/// </summary>
		/// <value>The posts.</value>
		public MessageCollection Messages
		{
			get
			{
				if (null == _messages)
				{
					_messages = RepositoryRegistry.MessageRepository.GetByTopicId(Id);
				}

				return _messages;
			}
		}

		public int Replies
		{
			get
			{
				return NumPosts - 1;
			}
		}

		/// <summary>
		/// Gets or sets the num posts.
		/// </summary>
		/// <value>The num posts.</value>
		public int NumPosts
		{
			get
			{
				return _numPosts;
			}
			set
			{
				_numPosts = value;
			}
		}

		/// <summary>
		/// Gets or sets the author.
		/// </summary>
		/// <value>The author.</value>
		public ForumUser Author
		{
			get { return _author; }
			set { _author = value; }
		}

		/// <summary>
		/// Gets or sets the views.
		/// </summary>
		/// <value>The views.</value>
		public int Views
		{
			get { return _views; }
			set { _views = value; }
		}

		/// <summary>
		/// Gets or sets the last post.
		/// </summary>
		/// <value>The last post.</value>
		public DateTime LastPost
		{
			get { return _lastPost; }
			set { _lastPost = value; }
		}

		/// <summary>
		/// Gets or sets the forum id.
		/// </summary>
		/// <value>The forum id.</value>
		public int ForumId
		{
			get { return _forumId; }
			set { _forumId = value; }
		}

		#endregion
	}
}