using System;
using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class ShowInactive : BaseForumControl
	{
		protected override void CreateChildControls()
		{
			Controls.Add(BuildPageLinks("Inactive Topics", ""));
			TopicCollection topics = RepositoryRegistry.TopicRepository.FindInactive();
			StartTopicTable();
			DisplayTopics(topics);
			EndTopicTable();
		}	

		private void StartTopicTable()
		{
			Controls.Add(new LiteralControl("<table width=100% cellspacing=1 cellpadding=3>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl(string.Format("<td colspan=6 class=\"ms-ToolPaneTitle\">{0}</td>", "Todays Topics")));
			Controls.Add(new LiteralControl("</tr>"));
			Controls.Add(new LiteralControl("<tr>"));
			Controls.Add(new LiteralControl("<td class=\"ms-TPHeader\" width=1%>&nbsp;</td>"));
			Controls.Add(new LiteralControl("<td class=\"ms-TPHeader\" align=left><strong>Topics</strong></td>"));
			Controls.Add(new LiteralControl("<td class=\"ms-TPHeader\" align=middle width=7%><strong>Replies</strong></td>"));
			Controls.Add(new LiteralControl("<td class=\"ms-TPHeader\" align=middle width=20%><strong>Author</strong></td>"));
			Controls.Add(new LiteralControl("<td class=\"ms-TPHeader\" align=middle width=7%><strong>Views</strong></td>"));
			Controls.Add(new LiteralControl("<td class=\"ms-TPHeader\" align=middle width=25%><strong>Last Post</strong></td>"));
			Controls.Add(new LiteralControl("</tr>"));
		}

		private void EndTopicTable()
		{
			Controls.Add(new LiteralControl("</table>"));
		}
	}
}
