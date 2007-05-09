using System;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities
{
	/// <summary>
	/// Summary description for ForumUser.
	/// </summary>
	public class ForumUser : DomainObject
	{
		private int userId;
		private DateTime lastVisit;
		private string email;
		private int numPosts;
		private DateTime joined;
		private string rank;
		private bool isAdmin;
		private GroupCollection groups;

		/// <summary>
		/// Initializes a new instance of the <see cref="ForumUser"/> class.
		/// </summary>
		public ForumUser()
		{
			groups = new GroupCollection();
		}

		/// <summary>
		/// Gets or sets the joined.
		/// </summary>
		/// <value>The joined.</value>
		public DateTime Joined
		{
			get { return joined; }
			set { joined = value; }
		}

		/// <summary>
		/// Gets or sets the num posts.
		/// </summary>
		/// <value>The num posts.</value>
		public int NumPosts
		{
			get { return numPosts; }
			set { numPosts = value; }
		}

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>The email.</value>
		public string Email
		{
			get { return email; }
			set { email = value; }
		}

		/// <summary>
		/// Gets or sets the user id. The user id is the id from
		/// the SharePoint SiteUsers list.
		/// </summary>
		/// <value>The user id.</value>
		public int UserId
		{
			get { return userId; }
			set { userId = value; }
		}

		/// <summary>
		/// Gets or sets the last visit.
		/// </summary>
		/// <value>The last visit.</value>
		public DateTime LastVisit
		{
			get { return lastVisit; }
			set { lastVisit = value; }
		}

		/// <summary>
		/// Gets the rank.
		/// </summary>
		/// <value>The rank.</value>
		public string Rank
		{
			get { return rank; }
			set { rank = value; }
		}

		/// <summary>
		/// Gets a value indicating whether this instance is admin.
		/// </summary>
		/// <value><c>true</c> if this instance is admin; otherwise, <c>false</c>.</value>
		public bool IsAdmin
		{
			get { return isAdmin; }
			set { isAdmin = value; }
		}

		/// <summary>
		/// Gets the groups.
		/// </summary>
		/// <value>The groups.</value>
		public GroupCollection Groups
		{
			get { return groups; }
		}
	}
}