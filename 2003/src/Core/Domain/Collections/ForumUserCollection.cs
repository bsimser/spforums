using System.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections
{
	/// <summary>
	/// Summary description for ForumUserCollection.
	/// </summary>
	public class ForumUserCollection : CollectionBase
	{
		public ForumUserCollection()
		{
		}

		/// <summary>
		/// Adds the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public int Add(ForumUser user)
		{
			return List.Add(user);
		}

		/// <summary>
		/// Determines whether [contains] [the specified user].
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified user]; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(ForumUser user)
		{
			return List.Contains(user);
		}

		/// <summary>
		/// Indexes the of.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public int IndexOf(ForumUser user)
		{
			return List.IndexOf(user);
		}

		/// <summary>
		/// Gets or sets the <see cref="ForumUser"/> at the specified index.
		/// </summary>
		/// <value></value>
		public ForumUser this[int index]
		{
			get { return List[index] as ForumUser; }
			set { List[index] = value; }
		}
	}
}