#region Using Directives

using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using Microsoft.SharePoint;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Service.Application
{
	/// <summary>
	/// Summary description for UserSession.
	/// </summary>
	public class UserSession
	{
		#region Fields

		private CategoryCollection categoryCollection = null;
		private SPUser userDetails;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="UserSession"/> class.
		/// </summary>
		private UserSession(SPUser user)
		{
			userDetails = user;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Logins this instance.
		/// </summary>
		/// <returns></returns>
		public static UserSession CreateSession(SPUser user)
		{
			return new UserSession(user);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns a collection of categories available to the user
		/// </summary>
		/// <value>The user categories.</value>
		public CategoryCollection UserCategories
		{
			get
			{
				if (categoryCollection == null)
				{
					categoryCollection = RepositoryRegistry.CategoryRepository.GetVisibleCategoriesForUser(this.userDetails);
				}
				return (categoryCollection);
			}
		}

		/// <summary>
		/// Gets the user details.
		/// </summary>
		/// <value>The user details.</value>
		public SPUser UserDetails
		{
			get { return userDetails; }
		}

		#endregion
	}
}