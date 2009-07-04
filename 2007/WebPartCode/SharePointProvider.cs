using System;
using Microsoft.SharePoint;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class SharePointProvider
    {
        private SPSite site;
        private SPWeb web;

        public SharePointProvider()
            : this("http://localhost")
        {
        }

        public SharePointProvider(string url)
        {
            this.site = new SPSite(url);
            try
            {
                Console.WriteLine(string.Format("Site ID is {0}", this.site.ID));
                this.site.AllowUnsafeUpdates = true;
                this.web = this.site.OpenWeb();
                this.web.AllowUnsafeUpdates = true;
            }
            catch (SPException exception)
            {
                Console.WriteLine(exception.ToString());
                throw new ApplicationException(string.Format("Invalid url {0} for portal.", url));
            }
        }

        public Guid CreateList(string listName)
        {
            SharePointListProvider provider = new SharePointListProvider(this.web);
            return provider.CreateList(listName);
        }

        public bool DeleteList(Guid guid)
        {
            SharePointListProvider provider = new SharePointListProvider(this.web);
            return provider.DeleteList(guid);
        }

        public SharePointListDescriptor GetListById(Guid guid)
        {
            SharePointListProvider provider = new SharePointListProvider(this.web);
            return provider.GetListById(guid);
        }

        public Guid GetListByName(string listName)
        {
            SharePointListProvider provider = new SharePointListProvider(this.web);
            return provider.GetListByName(listName);
        }

        public bool IsValidSite
        {
            get
            {
                return (this.site.ID != Guid.Empty);
            }
        }
    }
}