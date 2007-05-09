using System.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections
{
	public class GroupCollection : CollectionBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GroupCollection"/> class.
		/// </summary>
		public GroupCollection()
		{
		}

		/// <summary>
		/// Adds the specified group.
		/// </summary>
		/// <param name="group">The group.</param>
		/// <returns></returns>
		public int Add(Group group)
		{
			return List.Add(group);
		}

		/// <summary>
		/// Finds the specified id.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public Group Find(int id)
		{
			foreach (Group group in List)
			{
				if (group.Id == id)
					return group;
			}

			return null;
		}

		/// <summary>
		/// Gets or sets the <see cref="Group"/> at the specified index.
		/// </summary>
		/// <value></value>
		public Group this[int index]
		{
			get { return List[index] as Group; }
			set { List[index] = value; }
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the 
		/// current <see cref="T:System.Object"/> as a set of values in the 
		/// form of "groupno;groupno;..."
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
		public override string ToString()
		{
			string groupString = string.Empty;

			for (int i = 0; i < List.Count; i++)
			{
				Group group = this[i];
				groupString += group.Id.ToString();
				if (i < List.Count - 1)
					groupString += ";";
			}

			return groupString;
		}

		public virtual void Remove(Group group)
		{
			List.Remove(group);
		}

	}
}