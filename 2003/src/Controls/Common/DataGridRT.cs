using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BilSimser.SharePoint.WebParts.Forums.Controls.Common
{

	#region Item Template for Link Button	

	public class CreateItemTemplateLinkBtn : ITemplate
	{
		private string strColumnText;
		private string strLinkButtonName;
		private string strCommandName;
		private bool Visible = true;
		private bool Enable = true;

		public CreateItemTemplateLinkBtn(string LinkButtonName, string ColText, string CommandName)
		{
			this.strColumnText = ColText;
			this.strLinkButtonName = LinkButtonName;
			this.strCommandName = CommandName;
		}

		public CreateItemTemplateLinkBtn(string LinkButtonName, string ColText, string CommandName, bool Enabling, bool Visibility)
		{
			this.strColumnText = ColText;
			this.strLinkButtonName = LinkButtonName;
			this.Enable = Enabling;
			this.Visible = Visibility;
			this.strCommandName = CommandName;
		}

		public void InstantiateIn(Control objContainer)
		{
			LinkButton lnkbtn = new LinkButton();
			lnkbtn.CommandName = strCommandName;
			lnkbtn.DataBinding += new EventHandler(lnkbtn_DataBinding);
			objContainer.Controls.Add(lnkbtn);
		}

		private void lnkbtn_DataBinding(object sender, EventArgs e)
		{
			LinkButton lnkbtn = (LinkButton) sender;
			lnkbtn.ID = strLinkButtonName;
			lnkbtn.Text = strColumnText;
			lnkbtn.Visible = Visible;
			lnkbtn.Enabled = Enable;
			lnkbtn.CommandName = strCommandName;
			lnkbtn.CausesValidation = false;
		}
	}

	#endregion 

	#region Item Template for Push button

	public class CreateItemTemplatePushButton : ITemplate
	{
		private string strColumnText;
		private string strButtonName;
		private string strCommandName;
		private bool Visibile = true;
		private bool Enable = true;

		public CreateItemTemplatePushButton(string ButtonName, string ColText, string CommandName)
		{
			this.strColumnText = ColText;
			this.strButtonName = ButtonName;
			this.strCommandName = CommandName;
		}

		public CreateItemTemplatePushButton(string ButtonName, string ColText, string CommandName, bool Enabling, bool Visibiling)
		{
			this.strColumnText = ColText;
			this.strButtonName = ButtonName;
			this.Visibile = Visibiling;
			this.Enable = Enabling;
			this.strCommandName = CommandName;
		}

		public void InstantiateIn(Control objContainer)
		{
			Button btn = new Button();
			btn.CommandName = strCommandName;
			btn.DataBinding += new EventHandler(btn_DataBinding);
			objContainer.Controls.Add(btn);
		}

		private void btn_DataBinding(object sender, EventArgs e)
		{
			Button btn = (Button) sender;
			btn.ID = strButtonName;
			btn.Text = strColumnText;
			btn.Visible = Visibile;
			btn.Enabled = Enable;
			btn.CommandName = strCommandName;
			btn.CausesValidation = false;
		}
	}

	#endregion Item Template for Push button

	#region Item Template for Label

	public class CreateItemTemplateLabel : ITemplate
	{
		private string strColumnText;
		private string strLabelName;

		public CreateItemTemplateLabel(string ColText, string LabelName)
		{
			this.strColumnText = ColText;
			this.strLabelName = LabelName;
		}

		public void InstantiateIn(Control objContainer)
		{
			Label lbl = new Label();
			lbl.ID = strLabelName;
			lbl.DataBinding += new EventHandler(lbl_DataBinding);
			objContainer.Controls.Add(lbl);
		}

		private void lbl_DataBinding(object sender, EventArgs e)
		{
			Label lbl = (Label) sender;
			lbl.ID = strLabelName;
			lbl.Text = strColumnText;
		}
	}

	#endregion

	#region Item Template for TextBox

	public class CreateItemTemplateTextBox : ITemplate
	{
		private string strColumnText;
		private string strTextBoxName;
		private bool TextMode;
		private int MaxLength = 0;
		private bool ReadOnly = false;

		public CreateItemTemplateTextBox(string ColText, string TextBoxName, bool TextArea)
		{
			this.strColumnText = ColText;
			this.strTextBoxName = TextBoxName;
			this.TextMode = TextArea;
		}

		public CreateItemTemplateTextBox(string ColText, string TextBoxName, bool TextArea, int MaxLength)
		{
			this.strColumnText = ColText;
			this.strTextBoxName = TextBoxName;
			this.TextMode = TextArea;
			this.MaxLength = MaxLength;
		}

		public CreateItemTemplateTextBox(string ColText, string TextBoxName, bool TextArea, int MaxLength, bool ReadOnlyProperty)
		{
			this.strColumnText = ColText;
			this.strTextBoxName = TextBoxName;
			this.TextMode = TextArea;
			this.MaxLength = MaxLength;
			this.ReadOnly = ReadOnlyProperty;
		}

		public void InstantiateIn(Control objContainer)
		{
			TextBox txt = new TextBox();
			txt.ID = strTextBoxName;
			txt.DataBinding += new EventHandler(txt_DataBinding);
			objContainer.Controls.Add(txt);
		}

		private void txt_DataBinding(object sender, EventArgs e)
		{
			TextBox txt = (TextBox) sender;
			if (TextMode == true)
			{
				txt.TextMode = TextBoxMode.MultiLine;
			}
			else
			{
				txt.TextMode = TextBoxMode.SingleLine;
			}
			txt.ID = strTextBoxName;
			txt.MaxLength = MaxLength;
			txt.ReadOnly = ReadOnly;
			txt.Text = strColumnText;
		}
	}

	#endregion

	#region Item Template for Image Button	

	public class CreateItemTemplateImageButton : ITemplate
	{
		private string strImageURL;
		private string strImageButtonName;
		private string strCommandName;
		private bool Visibility = true;

		public CreateItemTemplateImageButton(string ImageButtonName, string ImageUrl, string CommandName)
		{
			this.strImageURL = ImageUrl;
			this.strImageButtonName = ImageButtonName;
			this.strCommandName = CommandName;
		}

		public CreateItemTemplateImageButton(string ImageButtonName, string ImageUrl, string CommandName, bool Visibility)
		{
			this.strImageURL = ImageUrl;
			this.strImageButtonName = ImageButtonName;
			this.Visibility = Visibility;
			this.strCommandName = CommandName;
		}

		public void InstantiateIn(Control objContainer)
		{
			ImageButton imgbtn = new ImageButton();
			imgbtn.CommandName = strCommandName;
			imgbtn.DataBinding += new EventHandler(imgbtn_DataBinding);
			objContainer.Controls.Add(imgbtn);
		}

		private void imgbtn_DataBinding(object sender, EventArgs e)
		{
			ImageButton imgBtn = (ImageButton) sender;
			imgBtn.ImageUrl = strImageURL;
			imgBtn.Visible = Visibility;
			imgBtn.CommandName = strCommandName;
			imgBtn.CausesValidation = false;
		}
	}

	#endregion 

	#region Item Template for DropDownList

	public class CreateItemTemplateDDL : ITemplate
	{
		private DataTable dtBind;
		private string strddlName;
		private string strDataValueField;
		private string strDataTextField;

		public CreateItemTemplateDDL(string DDLName, string DataValueField, string DataTextField, DataTable DDLSource)
		{
			this.dtBind = DDLSource;
			this.strDataValueField = DataValueField;
			this.strDataTextField = DataTextField;
			this.strddlName = DDLName;
		}

		public void InstantiateIn(Control objContainer)
		{
			DropDownList ddl = new DropDownList();
			ddl.DataBinding += new EventHandler(ddl_DataBinding);
			objContainer.Controls.Add(ddl);
		}

		private void ddl_DataBinding(object sender, EventArgs e)
		{
			DropDownList ddl = (DropDownList) sender;
			ddl.ID = strddlName;
			ddl.DataSource = dtBind;
			ddl.DataValueField = strDataValueField;
			ddl.DataTextField = strDataTextField;
			//ddl.DataBind();
		}
	}

	#endregion Item Template for DropDownList

	#region Edit Item Template for DropDownList

	public class CreateEditItemTemplateDDL : ITemplate
	{
		private DataTable dtBind;
		private string strddlName;
		private string strSelectedID;
		private string strDataValueField;
		private string strDataTextField;

		public CreateEditItemTemplateDDL(string DDLName, string DataValueField, string DataTextField, string SelectedValueField, DataTable DDLSource)
		{
			this.dtBind = DDLSource;
			this.strDataValueField = DataValueField;
			this.strDataTextField = DataTextField;
			this.strSelectedID = SelectedValueField;
			this.strddlName = DDLName;
		}

		public void InstantiateIn(Control objContainer)
		{
			DropDownList ddl = new DropDownList();
			ddl.DataBinding += new EventHandler(ddl_DataBinding);
			objContainer.Controls.Add(ddl);
		}

		private void ddl_DataBinding(object sender, EventArgs e)
		{
			DropDownList ddl = (DropDownList) sender;
			ddl.ID = strddlName;
			ddl.DataSource = dtBind;
			ddl.DataValueField = strDataValueField;
			ddl.DataTextField = strDataTextField;
			for (int i = 0; i < dtBind.Rows.Count; i++)
			{
				if (strSelectedID == dtBind.Rows[i][strDataValueField].ToString())
				{
					ddl.SelectedIndex = i;
				}
			}
			//ddl.DataBind();
		}
	}

	#endregion Edit Item Template for DropDownList

	#region Item Template for Radio Button	

	public class CreateItemTemplateRadioButton : ITemplate
	{
		private string strText;
		private string strRadioButtonName;
		private bool Visibility = true;
		private bool blChecked = false;

		public CreateItemTemplateRadioButton(string RadioButtonName, string Text, bool AutoCheck)
		{
			this.strText = Text;
			this.strRadioButtonName = RadioButtonName;
			this.blChecked = AutoCheck;
		}

		public CreateItemTemplateRadioButton(string RadioButtonName, string Text, bool AutoCheck, bool Visibility)
		{
			this.strText = Text;
			this.strRadioButtonName = RadioButtonName;
			this.Visibility = Visibility;
			this.blChecked = AutoCheck;
		}

		public void InstantiateIn(Control objContainer)
		{
			HtmlInputRadioButton rad = new HtmlInputRadioButton();
			rad.DataBinding += new EventHandler(rad_DataBinding);
			objContainer.Controls.Add(rad);
		}

		private void rad_DataBinding(object sender, EventArgs e)
		{
			HtmlInputRadioButton rad = (HtmlInputRadioButton) sender;
			rad.Value = strText;
			rad.Checked = blChecked;
			rad.Visible = Visibility;
		}
	}

	#endregion 

	#region Item Template for Html Image

	public class CreateItemTemplateHtmlImage : ITemplate
	{
		private string strImageName;
		private string strImageUrl;
		private int intHeight = -1;
		private int intWidth = -1;

		public CreateItemTemplateHtmlImage(string ImageName, string ImageUrl)
		{
			this.strImageName = ImageName;
			this.strImageUrl = ImageUrl;
		}

		public CreateItemTemplateHtmlImage(string ImageName, string ImageUrl, int Height_IN_PX, int Width_IN_PX)
		{
			this.strImageName = ImageName;
			this.strImageUrl = ImageUrl;
			if (Height_IN_PX == -1)
			{
				this.intHeight = 25;
			}
			if (Width_IN_PX == -1)
			{
				this.intWidth = 25;
			}
			this.intHeight = Height_IN_PX;
			this.intWidth = Width_IN_PX;
		}

		public void InstantiateIn(Control objCntrl)
		{
			HtmlImage hImg = new HtmlImage();
			hImg.DataBinding += new EventHandler(hImg_DataBinding);
			objCntrl.Controls.Add(hImg);
		}

		private void hImg_DataBinding(object sender, EventArgs e)
		{
			HtmlImage hImg = (HtmlImage) sender;
			hImg.ID = strImageName;
			hImg.Src = strImageUrl;
			if (intHeight != -1)
			{
				hImg.Height = intHeight;
			}
			if (intWidth != -1)
			{
				hImg.Width = intWidth;
			}
		}
	}

	#endregion Item Template for Html Image

	#region Item Template for File Field

	public class CreateItemTemplateFileField : ITemplate
	{
		private string strfileFieldName;
		private int intWidth;
		private bool blValidation;
		private string strExpr = ".jpg|.jpeg";

		public CreateItemTemplateFileField(string FileFieldName, int Width, bool RequireValidation, string AllowingFiles)
		{
			this.strfileFieldName = FileFieldName;
			this.intWidth = Width;
			this.blValidation = RequireValidation;
			if (AllowingFiles == "")
			{
				this.strExpr = ".jpg|.jpeg";
			}
			else
			{
				this.strExpr = AllowingFiles;
			}
		}

		public CreateItemTemplateFileField(string FileFieldName, int Width)
		{
			this.strfileFieldName = FileFieldName;
			this.intWidth = Width;
		}

		public void InstantiateIn(Control objCntrl)
		{
			HtmlInputFile hFile = new HtmlInputFile();
			hFile.DataBinding += new EventHandler(hFile_DataBinding);
			objCntrl.Controls.Add(hFile);
		}

		private void hFile_DataBinding(object sender, EventArgs e)
		{
			HtmlInputFile hFile = (HtmlInputFile) sender;
			hFile.Size = intWidth;
			if (blValidation == true)
			{
				RegularExpressionValidator rgv = new RegularExpressionValidator();
				rgv.ControlToValidate = "hFile";
				rgv.Display = ValidatorDisplay.None;
				rgv.ErrorMessage = "Please Select the File names of the type : " + strExpr;
				rgv.ValidationExpression = @"^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(" + @strExpr + ")$";
			}

		}
	}

	#endregion Item Template for Html Image

	#region Item Template for CheckBox

	public class CreateItemTemplateCheckBox : ITemplate
	{
		private string strText;
		private string strRadioButtonName;
		private bool Visibility = true;
		private bool blChecked = false;

		public CreateItemTemplateCheckBox(string RadioButtonName, string Text, bool AutoCheck)
		{
			this.strText = Text;
			this.strRadioButtonName = RadioButtonName;
			this.blChecked = AutoCheck;
		}

		public CreateItemTemplateCheckBox(string RadioButtonName, string Text, bool AutoCheck, bool Visibility)
		{
			this.strText = Text;
			this.strRadioButtonName = RadioButtonName;
			this.Visibility = Visibility;
			this.blChecked = AutoCheck;
		}

		public void InstantiateIn(Control objContainer)
		{
			CheckBox cb = new CheckBox();
			cb.DataBinding += new EventHandler(rad_DataBinding);
			objContainer.Controls.Add(cb);
		}

		private void rad_DataBinding(object sender, EventArgs e)
		{
			CheckBox cb = sender as CheckBox;
			cb.Checked = blChecked;
			cb.Visible = Visibility;
		}
	}

	#endregion 
}