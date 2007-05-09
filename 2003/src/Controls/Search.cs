#region Using Directives

using System.Web.UI;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Controls.Common;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Collections;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class Search : BaseForumControl
	{
		#region Fields
		
		private SPButton btnSearch;
		private TextBox txtSearch;
		
		#endregion

		#region Protected Methods
		protected override void CreateChildControls()
		{
			CreateControls();

			Controls.Add(BuildPageLinks("Search", "link"));

			/*
			 * Search Query
			 * Search for keywords: []
			 *                       * seach topic title and mssage text
			 *                       * search message text only
			 * search for author:   []
			 * Search Options
			 * Forum [All]
			 * Sort by: [Post Time | Post Subject | Title | Author | Forum]
			 * * Ascending
			 * * Descending
			 * Return first: [All avaialble | 0, 25, 50, 100, 200 (default), 500, 1000]
			 * characters of post
			 */

			AddBoxHeader("Search Query", false, 2);

			Controls.Add(CreateRow(txtSearch, "Search for keywords:"));
//			AddRow("*", "Search topic title and message text.");
//			AddRow("*", "Search message text only.");
//			AddRow("Forum", "All");
//			AddRow("Sort by:", "Post Time");
//			AddRow("*", "Ascending");
//			AddRow("Return first:", "200 characters of post.");

			Controls.Add(CreateButtonRow(btnSearch));

			CloseBox();
		}
		#endregion

		#region Private Methods
		private void CreateControls()
		{
			txtSearch = new TextBox();
			
			btnSearch = new SPButton("Search");
			btnSearch.Click += new System.EventHandler(btnSearch_Click);
		}
		#endregion

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			string searchTerms = txtSearch.Text;
			this.Context.Response.Write(searchTerms);
			MessageCollection messages = RepositoryRegistry.MessageRepository.FindByKeywords(searchTerms);
			foreach(Message message in messages)
			{
				this.Context.Response.Write(message.Name);
			}
		}
	}
}
