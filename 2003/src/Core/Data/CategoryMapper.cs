#region Using Directives

using System;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data
{
	/// <summary>
	/// Maps domain objects to and from data transfer objects. The
	/// DTO is a <see cref="SharePointListItem"/> which is an abstract
	/// type representing a row in a SharePoint list.
	/// 
	/// Other than the builder classes (that need field information for
	/// creation) this is the only class that you need to update when
	/// you add/remove fields. All other classes in the system will read
	/// domain objects or list items and only reference property names.
	/// </summary>
	public class CategoryMapper
	{
		/// <summary>
		/// Creates the domain object.
		/// </summary>
		/// <param name="listItem">The list item.</param>
		/// <returns></returns>
		public static Category CreateDomainObject(SharePointListItem listItem)
		{
			Category category = new Category(listItem.Id, listItem["Title"]);

			category.SortOrder = Convert.ToInt32(listItem["SortOrder"]);

			return category;
		}

		/// <summary>
		/// Creates the dto.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <returns></returns>
		public static SharePointListItem CreateDto(Category category)
		{
			string[] values = {
				"Title", category.Name,
				"SortOrder", category.SortOrder.ToString(),
			};

			return new SharePointListItem(category.Id, values);
		}
	}
}