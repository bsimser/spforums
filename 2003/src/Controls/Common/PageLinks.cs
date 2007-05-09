using System;
using System.Data;
using System.Web.UI;

namespace BilSimser.SharePoint.WebParts.Forums.Controls.Common
{
	/// <summary>
	/// Summary description for PageLinks.
	/// </summary>
	public class PageLinks : Control
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PageLinks"/> class.
		/// </summary>
		public PageLinks()
		{
		}

		/// <summary>
		/// Adds the link.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="url">The URL.</param>
		public void AddLink(string title, string url)
		{
			DataTable dataTable = (DataTable) ViewState["data"];
			if (dataTable == null)
			{
				dataTable = new DataTable();
				dataTable.Columns.Add("Title", typeof (string));
				dataTable.Columns.Add("URL", typeof (string));
				ViewState["data"] = dataTable;
			}
			DataRow dr = dataTable.NewRow();
			dr["Title"] = title;
			dr["URL"] = url;
			dataTable.Rows.Add(dr);
		}

		/// <summary>
		/// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to
		/// be rendered on
		/// the client.
		/// </summary>
		/// <param name="writer">The <see langword="HtmlTextWriter"/> object that receives the server control content.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			DataTable m_links = (DataTable) ViewState["data"];
			if (m_links == null || m_links.Rows.Count == 0)
				return;

			writer.WriteLine("<p class=\"ms-navheader\">");

			bool bFirst = true;
			foreach (DataRow row in m_links.Rows)
			{
				if (!bFirst)
				{
					writer.WriteLine("&#187;");
				}
				else
				{
					bFirst = false;
				}
				writer.WriteLine(String.Format("<a href='{0}'>{1}</a>", row["URL"], row["Title"]));
			}

			writer.WriteLine("</p>");
		}
	}
}