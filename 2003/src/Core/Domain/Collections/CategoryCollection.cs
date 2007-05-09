#region Using Directives

using System.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections
{
	public class CategoryCollection : CollectionBase
	{
		/// <summary>
		/// Adds the specified category.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <returns></returns>
		public int Add(Category category)
		{
			return List.Add(category);
		}

		/// <summary>
		/// Indexes the of.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <returns></returns>
		public int IndexOf(Category category)
		{
			for (int i = 0; i < List.Count; i++)
				if (this[i] == category) // Found it
					return i;
			return -1;
		}

		/// <summary>
		/// Inserts the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="category">The category.</param>
		public void Insert(int index, Category category)
		{
			List.Insert(index, category);
		}

		/// <summary>
		/// Removes the specified category.
		/// </summary>
		/// <param name="category">The category.</param>
		public void Remove(Category category)
		{
			List.Remove(category);
		}

		/// <summary>
		/// Finds the specified category.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <returns></returns>
		public Category Find(Category category)
		{
			foreach (Category lCategoryItem in this)
				if (lCategoryItem == category) // Found it
					return lCategoryItem;
			return null; // Not found
		}

		/// <summary>
		/// Determines whether [contains] [the specified category].
		/// </summary>
		/// <param name="category">The category.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified category]; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(Category category)
		{
			return (Find(category) != null);
		}

		/// <summary>
		/// Gets or sets the <see cref="Category"/> at the specified index.
		/// </summary>
		/// <value></value>
		public Category this[int index]
		{
			get { return (Category) List[index]; }
			set { List[index] = value; }
		}
	}
}