using Microsoft.SharePoint;

namespace BilSimser.SharePointForums.FeatureCode
{
    internal class SharePointForumsFeatureReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            // TODO: create lists
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