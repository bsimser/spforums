#region Using Directives

using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities
{
	public class Category : DomainObject
	{
		#region Fields

		private int sortOrder;
		private ForumCollection forums;

		#endregion

		#region Constructors

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

		#endregion

		#region Properties

		public int SortOrder
		{
			get { return sortOrder; }
			set { sortOrder = value; }
		}

		public ForumCollection Forums
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

		#endregion

		#region Public Methods

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

		#endregion
	}
}