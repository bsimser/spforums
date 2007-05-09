using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using Microsoft.SharePoint;

namespace BilSimser.SharePoint.WebParts.Forums.Core.Service.Builders
{
	/// <summary>
	/// Summary description for ForumAccessListBuilder.
	/// </summary>
	public class ForumAccessListBuilder : ListBuilder
	{
		public ForumAccessListBuilder()
		{
			this.listName = ForumConstants.Lists_ForumAccess;
		}

		public override void AddFields()
		{
			AddFieldToList("ForumID", SPFieldType.Number, false);
			AddFieldToList("GroupID", SPFieldType.Number, false);
		}

		public override void AddSampleData()
		{
			if (ListExists)
			{
				Permission perm = new Permission();
				string[] values = new string[6];

				perm.SetPermission(Permission.Rights.Read, true);
				values[0] = "Title";
				values[1] = perm.ToString();
				values[2] = "ForumID";
				values[3] = "1";
				values[4] = "GroupID";
				values[5] = "1"; // reader
				AddListValues(values);

				perm.SetPermission(Permission.Rights.Read, true);
				values[0] = "Title";
				values[1] = perm.ToString();
				values[2] = "ForumID";
				values[3] = "0"; // default (non-existant) forum
				values[4] = "GroupID";
				values[5] = "1"; // reader
				AddListValues(values);

				perm.SetPermission(Permission.Rights.Add, true);
				perm.SetPermission(Permission.Rights.Edit, true);
				perm.SetPermission(Permission.Rights.Reply, true);
				perm.SetPermission(Permission.Rights.Delete, true);
				values[0] = "Title";
				values[1] = perm.ToString();
				values[2] = "ForumID";
				values[3] = "1";
				values[4] = "GroupID";
				values[5] = "2"; // contributor
				AddListValues(values);

				perm.SetPermission(Permission.Rights.Add, true);
				perm.SetPermission(Permission.Rights.Edit, true);
				perm.SetPermission(Permission.Rights.Reply, true);
				perm.SetPermission(Permission.Rights.Delete, true);
				values[0] = "Title";
				values[1] = perm.ToString();
				values[2] = "ForumID";
				values[3] = "0"; // default forum
				values[4] = "GroupID";
				values[5] = "2"; // contributor
				AddListValues(values);

				perm.SetPermission(Permission.Rights.Admin, true);
				values[0] = "Title";
				values[1] = perm.ToString();
				values[2] = "ForumID";
				values[3] = "1";
				values[4] = "GroupID";
				values[5] = "3"; // administrator
				AddListValues(values);

				perm.SetPermission(Permission.Rights.Admin, true);
				values[0] = "Title";
				values[1] = perm.ToString();
				values[2] = "ForumID";
				values[3] = "0"; // default forum
				values[4] = "GroupID";
				values[5] = "3"; // administrator
				AddListValues(values);
			}
		}
	}
}