using System;
using System.Web.UI.WebControls;

namespace BilSimser.SharePoint.WebParts.Forums.Controls.Common
{
	/// <summary>
	/// Wraps up a SharePoint-style button with
	/// some default values like CSS class and properties
	/// </summary>
	public class SPButton : Button
	{
		public SPButton(string text)
		{
			Text = text;
			CssClass = "UserButton";
		}
	}
}
