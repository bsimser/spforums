#region Using Directives

using System.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections
{
	public class ForumCollection : CollectionBase
	{
		/// <summary>
		/// Adds the specified forum.
		/// </summary>
		/// <param name="forum">The forum.</param>
		/// <returns></returns>
		public int Add(Forum forum)
		{
			return List.Add(forum);
		}
	}
}