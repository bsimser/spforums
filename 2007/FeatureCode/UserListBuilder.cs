using BilSimser.SharePointForums.WebPartCode;
using Microsoft.SharePoint;

namespace BilSimser.SharePointForums.FeatureCode
{
    public class UserListBuilder : ListBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserListBuilder"/> class.
        /// </summary>
        public UserListBuilder()
        {
            this.listName = ForumConstants.Lists_Users;
        }

        /// <summary>
        /// Adds the fields.
        /// </summary>
        public override void AddFields()
        {
            AddFieldToList("UserID", SPFieldType.Number, false);
            AddFieldToList("NumPosts", SPFieldType.Number, false);
            AddFieldToList("LastVisit", SPFieldType.DateTime, false);
            AddFieldToList("IsAdmin", SPFieldType.Boolean, false);
            AddFieldToList("Groups", SPFieldType.Text, false);
        }

        /// <summary>
        /// Adds the sample data.
        /// </summary>
        public override void AddSampleData()
        {
        }
    }
}