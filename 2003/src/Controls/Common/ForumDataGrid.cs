using System;
using System.Web.UI.WebControls;

namespace BilSimser.SharePoint.WebParts.Forums.Controls.Common
{
	/// <summary>
	/// Common data grid class for all forum screens. Sets up
	/// standard look and feel and also uses common SharePoint
	/// styles and controls.
	/// </summary>
	public class ForumDataGrid : DataGrid
	{
		public ForumDataGrid()
		{
			AutoGenerateColumns = false;
			Width = Unit.Percentage(100);
			BorderWidth = Unit.Pixel(0);
		}

		public void AddTemplateColumn(string colText, string lblName)
		{
			TemplateColumn col = new TemplateColumn();
			col.ItemTemplate = new CreateItemTemplateLabel(colText, lblName);
			col.HeaderText = String.Format("<strong>{0}</strong>", lblName);
			col.HeaderStyle.CssClass = "ms-TPHeader";
			col.ItemStyle.CssClass = "ms-alternating";
			this.Columns.Add(col);
		}

		/// <summary>
		/// Adds a bound column to the datagrid.
		/// </summary>
		/// <param name="headerText">The header text.</param>
		/// <param name="dataField">The data field.</param>
		/// <param name="readOnly">if set to <c>true</c> [read only].</param>
		public void AddBoundColumn(string headerText, string dataField, bool readOnly)
		{
			AddBoundColumn(headerText, dataField, string.Empty, readOnly);
		}

		/// <summary>
		/// Adds a bound column to the datagrid with a format string for the
		/// data field. This is used to create hyperlinks to say an email address.
		/// </summary>
		/// <param name="headerText">The header text.</param>
		/// <param name="dataField">The data field.</param>
		/// <param name="dataFormat">The data format.</param>
		/// <param name="readOnly">if set to <c>true</c> [read only].</param>
		public void AddBoundColumn(string headerText, string dataField, string dataFormat, bool readOnly)
		{
			BoundColumn column;
			column = new BoundColumn();
			column.HeaderText = String.Format("<strong>{0}</strong>", headerText);
			column.DataField = dataField;
			column.ReadOnly = readOnly;
			column.HeaderStyle.CssClass = "ms-TPHeader";
			column.ItemStyle.CssClass = "ms-alternating";
			if (dataFormat != String.Empty)
				column.DataFormatString = dataFormat;
			this.Columns.Add(column);
		}

		public void AddEditColumn()
		{
			EditCommandColumn cmdColumn = new EditCommandColumn();
			cmdColumn.EditText = "Edit";
			cmdColumn.UpdateText = "Update";
			cmdColumn.CancelText = "Cancel";
			cmdColumn.HeaderText = "<strong>Actions</strong>";
			cmdColumn.HeaderStyle.CssClass = "ms-TPHeader";
			cmdColumn.ItemStyle.CssClass = "ms-alternating";
			this.Columns.Add(cmdColumn);
		}

		public void AddDeleteColumn()
		{
			ButtonColumn btnColumn = new ButtonColumn();
			btnColumn.ButtonType = ButtonColumnType.LinkButton;
			btnColumn.Text = "Delete";
			btnColumn.CommandName = "Delete";
			btnColumn.HeaderText = "<strong>Delete</strong>";
			btnColumn.HeaderStyle.CssClass = "ms-TPHeader";
			btnColumn.ItemStyle.CssClass = "ms-alternating";
			this.Columns.Add(btnColumn);
		}

		public void BindData(object dataSource)
		{
			this.DataSource = dataSource;
			this.DataBind();
		}
	}
}