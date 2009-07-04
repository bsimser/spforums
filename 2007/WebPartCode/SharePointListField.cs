namespace BilSimser.SharePointForums.WebPartCode
{
    public class SharePointListField
    {
        private string fieldValue;
        private string name;
        private bool readOnly;

        public SharePointListField()
        {
        }

        public SharePointListField(string name, string val, bool readOnly)
        {
            this.name = name;
            this.fieldValue = val;
            this.readOnly = readOnly;
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return this.readOnly;
            }
            set
            {
                this.readOnly = value;
            }
        }

        public string Value
        {
            get
            {
                return this.fieldValue;
            }
            set
            {
                this.fieldValue = value;
            }
        }
    }
}