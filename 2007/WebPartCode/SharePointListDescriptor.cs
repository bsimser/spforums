namespace BilSimser.SharePointForums.WebPartCode
{
    public class SharePointListDescriptor
    {
        private string listId;
        private string listName;
        private SharePointListItem[] sharePointListItems;

        public string ListId
        {
            get
            {
                return this.listId;
            }
            set
            {
                this.listId = value;
            }
        }

        public string ListName
        {
            get
            {
                return this.listName;
            }
            set
            {
                this.listName = value;
            }
        }

        public SharePointListItem[] SharePointListItems
        {
            get
            {
                return this.sharePointListItems;
            }
            set
            {
                this.sharePointListItems = value;
            }
        }
    }
}