using System;
using System.Collections;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class SharePointListItem
    {
        private Hashtable fieldIndexes;
        private int id;
        private SharePointListField[] sharePointListFields;

        public SharePointListItem()
        {
            this.sharePointListFields = new SharePointListField[0];
        }

        public SharePointListItem(int id, params string[] values)
        {
            this.sharePointListFields = new SharePointListField[0];
            int index = 0;
            this.id = id;
            while (index < values.Length)
            {
                this.Add(values[index], values[index + 1], false);
                index += 2;
            }
        }

        public void Add(string name, string fieldValue, bool readOnly)
        {
            ArrayList list = new ArrayList(this.SharePointListFields);
            list.Add(new SharePointListField(name, fieldValue, readOnly));
            this.sharePointListFields = new SharePointListField[list.Count];
            list.CopyTo(this.SharePointListFields);
        }

        private void InitFieldIndexes()
        {
            this.fieldIndexes = new Hashtable();
            this.fieldIndexes.Clear();
            foreach (SharePointListField field in this.SharePointListFields)
            {
                if (!this.fieldIndexes.ContainsKey(field.Name))
                {
                    this.fieldIndexes.Add(field.Name, field);
                }
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string this[string name]
        {
            get
            {
                if (this.fieldIndexes == null)
                {
                    this.InitFieldIndexes();
                }
                if (this.fieldIndexes.ContainsKey(name))
                {
                    return ((SharePointListField)this.fieldIndexes[name]).Value;
                }
                return null;
            }
            set
            {
                if (this.fieldIndexes == null)
                {
                    this.InitFieldIndexes();
                }
                if (!this.fieldIndexes.ContainsKey(name))
                {
                    throw new IndexOutOfRangeException();
                }
                ((SharePointListField)this.fieldIndexes[name]).Value = value;
            }
        }

        public SharePointListField[] SharePointListFields
        {
            get
            {
                return this.sharePointListFields;
            }
            set
            {
                this.sharePointListFields = value;
            }
        }
    }
}