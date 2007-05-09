using System.Collections;
using System.Web.UI;

namespace BilSimser.SharePoint.WebParts.Forums.Controls.Common
{
	/// <summary>
	/// Summary description for SharePointToolBarItem.
	/// </summary>
	internal class SharePointToolBarItem
	{
		#region Fields

		private string link;
		private string title;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SharePointToolBarItem"/> class.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="link">The link.</param>
		public SharePointToolBarItem(string title, string link)
		{
			if (title == null || link == null)
				return;

			this.title = title;
			this.link = link;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the link.
		/// </summary>
		/// <value>The link.</value>
		public string Link
		{
			get { return link; }
			set { link = value; }
		}

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public string Title
		{
			get { return title; }
			set { title = value; }
		}

		#endregion
	}

	/// <summary>
	/// Summary description for SharePointToolBar.
	/// </summary>
	public class SharePointToolBar
	{
		#region Fields

		private string userName;
		private ArrayList items = new ArrayList();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SharePointToolBar"/> class.
		/// </summary>
		public SharePointToolBar(string userName)
		{
			if (userName == null)
				return;

			this.userName = userName;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Renders the specified output.
		/// </summary>
		/// <param name="output">The output.</param>
		public void Render(HtmlTextWriter output)
		{
			if (output == null)
				return;

			int count = 0;

			output.Write("<table width=100% class=\"ms-toolbar\" border=0 height=25 cellpadding=0 cellspacing=0>");
			output.Write("<tr>");
			output.Write("<tr>");
			output.Write("<td width=\"50%\" align=\"left\">");
			output.Write("<table>");
			output.Write("<tr>");
			output.Write(string.Format("<td>Welcome {0}</td>", userName));
			output.Write("</tr>");
			output.Write("</table>");
			output.Write("</td>");
			output.Write("<td width=\"50%\" align=\"right\">");
			output.Write("<table>");
			output.Write("<tr>");

			foreach (SharePointToolBarItem toolBarItem in items)
			{
				output.Write(string.Format("<td><span><a href=\"{0}\">{1}</a></span>", toolBarItem.Link, toolBarItem.Title));
				if (++count < items.Count)
					output.Write("&nbsp;|&nbsp;");
				output.Write(string.Format("</td>", toolBarItem.Link, toolBarItem.Title));
			}
			output.Write("</tr>");
			output.Write("</table>");
			output.Write("</td>");
			output.Write("</tr>");
			output.Write("</table>");
		}

		/// <summary>
		/// Adds the item.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="link">The link.</param>
		public void AddItem(string title, string link)
		{
			if (title == null || link == null)
				return;

			items.Add(new SharePointToolBarItem(title, link));
		}

		#endregion
	}
}