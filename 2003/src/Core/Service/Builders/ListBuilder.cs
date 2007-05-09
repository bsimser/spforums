using System;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using Microsoft.SharePoint;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Service.Builders
{
	/// <summary>
	/// Summary description for ListBuilder.
	/// </summary>
	public abstract class ListBuilder
	{
		protected string listName;
		protected bool createDefaultValues = false;

		/// <summary>
		/// Initializes a new instance of the <see cref="ListBuilder"/> class.
		/// </summary>
		protected ListBuilder()
		{
		}

		/// <summary>
		/// Gets a value indicating whether [create default values].
		/// </summary>
		/// <value><c>true</c> if [create default values]; otherwise, <c>false</c>.</value>
		public bool CreateDefaultValues
		{
			get { return createDefaultValues; }
		}

		/// <summary>
		/// Gets a value indicating whether the List for 
		/// this <see cref="ListBuilder"/> exists or not.
		/// </summary>
		/// <value><c>true</c> if exists; otherwise, <c>false</c>.</value>
		public bool ListExists
		{
			get
			{
				bool listExists = false;

				try
				{
					Console.WriteLine(ForumApplication.Instance.SpWeb.Lists[listName]);
					listExists = true;
				}
				catch (Exception)
				{
				}

				return listExists;
			}
		}

		/// <summary>
		/// Creates the list.
		/// </summary>
		public void CreateList()
		{
			if (!ListExists)
			{
				InternalCreateList();
			}
		}

		/// <summary>
		/// Adds the field to list. Only adds the field if it doesn't already exist.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="fieldType">Type of the field.</param>
		/// <param name="isRequired">if set to <c>true</c> [is required].</param>
		/// <returns></returns>
		protected string AddFieldToList(string fieldName, SPFieldType fieldType, bool isRequired)
		{
			string field = string.Empty;
			SPList list = null;

			ForumApplication.Instance.SpWeb.AllowUnsafeUpdates = true;

			try
			{
				if (ListExists)
				{
					list = ForumApplication.Instance.SpWeb.Lists[listName];
					field = list.Fields[fieldName].ToString();
				}
			}
			catch
			{
				try
				{
					field = list.Fields.Add(fieldName, fieldType, isRequired);
				}
				catch (Exception)
				{
				}
			}

			return field;
		}

		/// <summary>
		/// Adds the list values.
		/// </summary>
		/// <param name="values">The values.</param>
		protected void AddListValues(string[] values)
		{
			SharePointListItem listItem = new SharePointListItem(0, values);
			SharePointListProvider provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
			provider.AddListItem(listName, listItem);
		}

		/// <summary>
		/// Creates the SharePoint list and adds it to the SPListCollection.
		/// </summary>
		/// <returns>The new Guid for the list added.</returns>
		private Guid InternalCreateList()
		{
			ForumApplication.Instance.SpWeb.AllowUnsafeUpdates = true;
			createDefaultValues = true;
			return ForumApplication.Instance.SpWeb.Lists.Add(listName, string.Empty, SPListTemplateType.GenericList);
		}

		/// <summary>
		/// Hides the list.
		/// </summary>
		public void HideList()
		{
			ForumApplication.Instance.SpWeb.AllowUnsafeUpdates = true;
			SPList list = ForumApplication.Instance.SpWeb.Lists[listName];
			list.Hidden = true;
			list.Update();
		}

		// Pure virtual methods to be overridden by child classes
		public abstract void AddFields();
		public abstract void AddSampleData();
	}
}