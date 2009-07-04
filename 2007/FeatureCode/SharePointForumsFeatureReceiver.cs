using Microsoft.SharePoint;

namespace BilSimser.SharePointForums.FeatureCode
{
    internal class SharePointForumsFeatureReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var director = new ListDirector();
            director.ConstructList(new GroupListBuilder());
            director.ConstructList(new UserListBuilder());
            director.ConstructList(new CategoryListBuilder());
            director.ConstructList(new ForumAccessListBuilder());
            director.ConstructList(new ForumListBuilder());
            director.ConstructList(new TopicListBuilder());
            director.ConstructList(new MessageListBuilder());
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            // TODO: delete lists
        }

        public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        {
        }

        public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        {
        }
    }
}