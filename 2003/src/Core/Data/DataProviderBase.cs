using System;
using BilSimser.SharePoint.Common.Data;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Data
{
	public abstract class DataProviderBase
	{
		private readonly SharePointListProvider _provider;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataProviderBase"/> class.
		/// </summary>
		protected DataProviderBase()
		{
			_provider = new SharePointListProvider(ForumApplication.Instance.SpWeb);
		}

		/// <summary>
		/// Gets the provider.
		/// </summary>
		/// <value>The provider.</value>
		protected SharePointListProvider Provider
		{
			get { return _provider; }
		}

		/*
		 * TODO something like this to get paged support
		 * and then you can call GetAll(start, pagelength) from the client
		 * need more interfaces or abstract classes in the data layer to implement this though
		 * 
		protected void Fill(SharePointListDescriptor rows, CollectionBase collection, int start, int pageLength)
		{
			if(rows.SharePointListItems.Length > start)
				return collection;

			for(int i=start; i<pageLength; i++)
			{
				SharePointListItem item = rows.SharePointListItems[i];
			}

			return collection;
		}
		*/
	}
}
