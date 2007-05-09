using System;
using System.Diagnostics;
using System.Web.UI.WebControls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Domain.Entities;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;

namespace BilSimser.SharePoint.WebParts.Forums.Controls
{
	public class AdministrationPanel : AdminBaseControl
	{
		protected override void CreateAdminChildControls()
		{
			AddBoxHeader("Administration");
			AddText("The administration control panel is used to control and manage all aspects of the foums. Please select an area or task from the left hand side of the page.");
		}
	}
}