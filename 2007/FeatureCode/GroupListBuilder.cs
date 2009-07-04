using BilSimser.SharePointForums.WebPartCode;

namespace BilSimser.SharePointForums.FeatureCode
{
    public class GroupListBuilder : ListBuilder
    {
        public GroupListBuilder()
        {
            this.listName = ForumConstants.Lists_Groups;
        }

        public override void AddFields()
        {
        }

        public override void AddSampleData()
        {
            if (ListExists)
            {
                string[] values = new string[2];
                values[0] = "Title";
                values[1] = "Reader";
                AddListValues(values);
                values[0] = "Title";
                values[1] = "Contributor";
                AddListValues(values);
                values[0] = "Title";
                values[1] = "Administrator";
                AddListValues(values);
            }
        }
    }
}