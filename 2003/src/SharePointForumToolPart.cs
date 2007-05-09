using System;
using System.Web.UI;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebPartPages;

namespace BilSimser.SharePoint.WebParts.Forums
{
	/// <summary>
	/// Description of the toolpart. Override the GetToolParts method in your WebPart
	/// class to invoke this toolpart. To establish a reference to the Web Part 
	/// the user has selected, use the ParentToolPane.SelectedWebPart property.
	/// </summary>
	public class SharePointForumToolPart : ToolPart
	{
		// declaring a sample varialble 
		private string inputname;

		// an event handler for the Init event
		private void ToolPart1_Init(object sender, EventArgs e)
		{
			inputname = this.UniqueID + "message";
		}

		/// <summary>
		/// Constructor for the class. A great place to set Set 
		/// default values for additional base class properties 
		/// here.
		/// <summary>
		public SharePointForumToolPart()
		{
			// Set default properties
			this.Title = "Custom Text";
			this.Init += new EventHandler(ToolPart1_Init);
		}


		///	<summary>
		///	Called by the tool pane to apply property changes to 
		/// the selected Web Part. 
		///	</summary>
		public override void ApplyChanges()
		{
			SharePointForumWebPart wp1 = (SharePointForumWebPart) this.ParentToolPane.SelectedWebPart;
			ForumApplication.Instance.Title = Page.Request.Form[inputname];
		}


		/// <summary>
		///	If the ApplyChanges method succeeds, this method is 
		/// called by the tool pane to refresh the specified 
		/// property values in the toolpart user interface.
		/// </summary>
		public override void SyncChanges()
		{
			// sync with the new property changes here
		}


		/// <summary>
		///	Called by the tool pane if the user discards changes 
		/// to the selected Web Part. 
		/// </summary>
		public override void CancelChanges()
		{
		}


		/// <summary>
		/// Render this Tool part to the output parameter 
		/// specified.
		/// </summary>
		/// <param name="output"> 
		/// The HTML writer to write out to 
		/// </param>
		protected override void RenderToolPart(HtmlTextWriter output)
		{
			// Establish a reference to the Web Part.
			SharePointForumWebPart wp1 = (SharePointForumWebPart) this.ParentToolPane.SelectedWebPart;
			output.Write("Enter your custom text: ");
			output.Write("<input name= '" + inputname);
			output.Write("' type='text' value='" + SPEncode.HtmlEncode(ForumApplication.Instance.Title) + "'> <br>");
		}
	}
}