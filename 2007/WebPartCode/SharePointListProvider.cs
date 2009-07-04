using System;
using Microsoft.SharePoint;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class SharePointListProvider
    {
        private SPWeb web = null;

        public SharePointListProvider(SPWeb web)
        {
            this.web = web;
        }

        public int AddListItem(string listName, SharePointListItem newItem)
        {
            using (Identity.ImpersonateAppPool())
            {
                SPList list = this.GetList(listName);
                list.ParentWeb.AllowUnsafeUpdates = true;
                SPListItem spListItem = list.Items.Add();
                this.UpdateSPListItem(spListItem, newItem);
                spListItem.Update();
                return spListItem.ID;
            }
        }

        public Guid CreateList(string listName)
        {
            try
            {
                SPList list = this.web.Lists[listName];
                return list.ID;
            }
            catch (Exception)
            {
                return this.web.Lists.Add(listName, string.Empty, SPListTemplateType.GenericList);
            }
        }

        public bool DeleteList(Guid guid)
        {
            try
            {
                this.web.Lists.Delete(guid);
                this.GetListById(guid);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public void DeleteListItem(string listName, int itemID)
        {
            using (Identity.ImpersonateAppPool())
            {
                this.web.Lists.IncludeRootFolder = true;
                SPList list = this.web.Lists[listName];
                list.Items.DeleteItemById(itemID);
                list.Update();
            }
        }

        public SharePointListDescriptor GetAllListItems(string listName)
        {
            try
            {
                SPList list = this.GetList(listName);
                SharePointListDescriptor descriptor = new SharePointListDescriptor();
                descriptor.ListId = list.ID.ToString();
                descriptor.ListName = list.Title;
                descriptor.SharePointListItems = new SharePointListItem[list.ItemCount];
                for (int i = 0; i < list.ItemCount; i++)
                {
                    descriptor.SharePointListItems[i] = this.SPListItemtoDescriptor(list.Items[i]);
                }
                return descriptor;
            }
            catch (Exception)
            {
                return new SharePointListDescriptor();
            }
        }

        private string GetCAMLClause(SPList spList, string fieldName, string fieldValue)
        {
            return this.GetCAMLClause(spList, fieldName, fieldValue, "Eq");
        }

        private string GetCAMLClause(SPList spList, string fieldName, string fieldValue, string queryType)
        {
            string typeAsString = spList.Fields.GetField(fieldName).TypeAsString;
            if (typeAsString == "DateTime")
            {
                fieldValue = DateTime.Parse(fieldValue).ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            else
            {
                int index = fieldValue.IndexOf("*");
                if (index == 0)
                {
                    queryType = "Contains";
                    fieldValue = fieldValue.Remove(index, 1);
                }
                else if (index == (fieldValue.Length - 1))
                {
                    queryType = "BeginsWith";
                    fieldValue = fieldValue.Remove(index, 1);
                }
            }
            return string.Format("<{0}><FieldRef Name=\"{1}\" /><Value Type=\"{2}\">{3}</Value></{0}>", new object[] { queryType, spList.Fields.GetField(fieldName).InternalName, typeAsString, fieldValue.ToUpper() });
        }

        private SPList GetList(string listName)
        {
            this.web.Lists.IncludeRootFolder = true;
            if (listName.Trim().Length == 0x24)
            {
                Guid guid = new Guid(listName);
                if (guid.ToString() == listName)
                {
                    return this.web.Lists[new Guid(listName)];
                }
            }
            return this.web.Lists[listName];
        }

        public SharePointListDescriptor GetListById(Guid guid)
        {
            this.web.Lists.IncludeRootFolder = true;
            SharePointListDescriptor descriptor = new SharePointListDescriptor();
            SPList list = this.GetList(guid.ToString());
            descriptor.ListId = list.ID.ToString();
            descriptor.ListName = list.Title;
            return descriptor;
        }

        public Guid GetListByName(string name)
        {
            try
            {
                return this.GetList(name).ID;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public SharePointListItem GetListItemByField(string listName, string fieldName, string fieldValue)
        {
            SPList spList = this.GetList(listName);
            SPQuery query = new SPQuery();
            query.Query = "<Where>" + this.GetCAMLClause(spList, fieldName, fieldValue) + "</Where>";
            SPListItemCollection items = spList.GetItems(query);
            if (items.Count > 0)
            {
                return this.SPListItemtoDescriptor(items[0]);
            }
            return null;
        }

        public SharePointListItem GetListItemByField(string listName, string[] fieldNames, string[] fieldValues)
        {
            SPList spList = this.GetList(listName);
            string str = "";
            if (fieldNames.Length != fieldValues.Length)
            {
                throw new Exception("# of values must match # of fields");
            }
            for (int i = 0; i < fieldNames.Length; i++)
            {
                string str2 = this.GetCAMLClause(spList, fieldNames[i], fieldValues[i]);
                if (str.Length > 0)
                {
                    str = "<And>" + str + str2 + "</And>";
                }
                else
                {
                    str = str2;
                }
            }
            SPQuery query = new SPQuery();
            query.Query = "<Where>" + str + "</Where>";
            SPListItemCollection items = spList.GetItems(query);
            if (items.Count > 0)
            {
                return this.SPListItemtoDescriptor(items[0]);
            }
            return null;
        }

        public SharePointListItem GetListItembyID(string listName, int itemID)
        {
            SPListItem itemById = this.GetList(listName).Items.GetItemById(itemID);
            return this.SPListItemtoDescriptor(itemById);
        }

        public SharePointListDescriptor GetListItemsByField(string listName, string[] fieldNames, string[] fieldValues)
        {
            SPList spList = this.GetList(listName);
            string str = "";
            if (fieldNames.Length != fieldValues.Length)
            {
                throw new Exception("# of values must match # of fields");
            }
            for (int i = 0; i < fieldNames.Length; i++)
            {
                string str2 = this.GetCAMLClause(spList, fieldNames[i], fieldValues[i]);
                if (str.Length > 0)
                {
                    str = "<And>" + str + str2 + "</And>";
                }
                else
                {
                    str = str2;
                }
            }
            SPQuery query = new SPQuery();
            query.Query = "<Where>" + str + "</Where>";
            SPListItemCollection items = spList.GetItems(query);
            SharePointListDescriptor descriptor = new SharePointListDescriptor();
            descriptor.ListId = spList.ID.ToString();
            descriptor.SharePointListItems = new SharePointListItem[items.Count];
            descriptor.ListName = spList.Title;
            for (int j = 0; j < items.Count; j++)
            {
                descriptor.SharePointListItems[j] = this.SPListItemtoDescriptor(items[j]);
            }
            return descriptor;
        }

        public SharePointListDescriptor GetListItemsByField(string listName, string fieldNames, string fieldValues)
        {
            return this.GetListItemsByField(listName, fieldNames, fieldValues, "Eq");
        }

        public SharePointListDescriptor GetListItemsByField(string listName, string fieldNames, string fieldValues, string queryType)
        {
            SPList spList = this.GetList(listName);
            SPQuery query = new SPQuery();
            query.Query = "<Where>" + this.GetCAMLClause(spList, fieldNames, fieldValues, queryType) + "</Where>";
            SPListItemCollection items = spList.GetItems(query);
            SharePointListDescriptor descriptor = new SharePointListDescriptor();
            descriptor.ListId = spList.ID.ToString();
            descriptor.SharePointListItems = new SharePointListItem[items.Count];
            descriptor.ListName = spList.Title;
            for (int i = 0; i < items.Count; i++)
            {
                descriptor.SharePointListItems[i] = this.SPListItemtoDescriptor(items[i]);
            }
            return descriptor;
        }

        public SharePointListDescriptor[] GetLists()
        {
            this.web.Lists.IncludeRootFolder = true;
            SharePointListDescriptor[] descriptorArray = new SharePointListDescriptor[this.web.Lists.Count];
            for (int i = 0; i < descriptorArray.Length; i++)
            {
                descriptorArray[i] = new SharePointListDescriptor();
                descriptorArray[i].ListId = this.web.Lists[i].ID.ToString();
                descriptorArray[i].ListName = this.web.Lists[i].Title;
                descriptorArray[i].SharePointListItems = null;
            }
            return descriptorArray;
        }

        private SharePointListItem SPListItemtoDescriptor(SPListItem spListItem)
        {
            SharePointListItem item = new SharePointListItem();
            for (int i = 0; i < spListItem.Fields.Count; i++)
            {
                if (!spListItem.Fields[i].Hidden)
                {
                    try
                    {
                        item.Id = spListItem.ID;
                        string internalName = spListItem.Fields[i].InternalName;
                        string fieldValue = (spListItem[internalName] != null) ? spListItem[spListItem.Fields[i].InternalName].ToString() : "";
                        item.Add(internalName, fieldValue, spListItem.Fields[i].ReadOnlyField);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return item;
        }

        public int UpdateListItem(string listName, SharePointListItem updateItem)
        {
            using (Identity.ImpersonateAppPool())
            {
                SPList list = this.GetList(listName);
                list.ParentWeb.AllowUnsafeUpdates = true;
                SPListItem itemById = list.Items.GetItemById(updateItem.Id);
                this.UpdateSPListItem(itemById, updateItem);
                itemById.Update();
                return itemById.ID;
            }
        }

        private void UpdateSPListItem(SPListItem spListItem, SharePointListItem sharePointListItem)
        {
            foreach (SharePointListField field in sharePointListItem.SharePointListFields)
            {
                try
                {
                    SPField field2 = spListItem.Fields.GetField(field.Name);
                    if (!field2.ReadOnlyField)
                    {
                        if (field2.Type == SPFieldType.DateTime)
                        {
                            spListItem[field.Name] = DateTime.Parse(field.Value);
                        }
                        else
                        {
                            spListItem[field.Name] = field.Value;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}