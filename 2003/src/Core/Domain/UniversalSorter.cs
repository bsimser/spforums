using System;
using System.Collections;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain
{
	/// <summary>
	/// A generic sorter, inheriting from IComparer, 
	/// intended to allow for the sorting of
	/// strongly-typed collections on any named public property
	/// which implements IComparable.
	/// </summary>
	/// <example>
	/// To use this class, within a collection which inherits from 
	/// System.Collections.CollectionBase, we just need to expose a 
	/// Sort() method along the lines of:
	/// public void Sort(string sortPropertyName, SortDirection sortDirection)
	/// {
	///		InnerList.Sort(new UniversalSorter(sortPropertyName,sortDirection));
	/// }
	/// </example>
	public class UniversalSorter : IComparer
	{
		private string sortPropertyName;
		private SortDirection direction;

		public UniversalSorter(string sortPropertyName)
		{
			this.sortPropertyName = sortPropertyName;
			direction = SortDirection.Ascending; // default to ascending order
		}

		public UniversalSorter(string sortPropertyName, SortDirection direction)
		{
			this.sortPropertyName = sortPropertyName;
			this.direction = direction;
		}

		/// <summary>
		/// Get the values of the relevant property on the
		/// x and y objects using reflection.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Compare(object x, object y)
		{
			object valueOfX = x.GetType().GetProperty(sortPropertyName).GetValue(x, null);
			object valueOfY = y.GetType().GetProperty(sortPropertyName).GetValue(y, null); // Do the comparison
			if (direction == SortDirection.Ascending)
			{
				return ((IComparable) valueOfX).CompareTo(valueOfY);
			}
			else

			{
				return ((IComparable) valueOfY).CompareTo(valueOfX);
			}
		}
	}

	/// 
	/// Enumerator to indicate whether to sort in ascending or
	/// descending order
	/// 
	public enum SortDirection
	{
		Ascending,
		Descending
	}
}