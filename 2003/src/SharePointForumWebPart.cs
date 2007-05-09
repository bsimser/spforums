#region Using Directives

using System;
using System.ComponentModel;
using System.Threading;
using System.Web.UI;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Text;
using BilSimser.SharePoint.Common.Service;
using BilSimser.SharePoint.WebParts.Forums.Controls;
using BilSimser.SharePoint.WebParts.Forums.Controls.Base;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Application;
using BilSimser.SharePoint.WebParts.Forums.Core.Service.Builders;
using BilSimser.SharePoint.WebParts.Forums.Utility;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.WebPartPages;

#endregion

namespace BilSimser.SharePoint.WebParts.Forums
{
	[ToolboxData("<{0}:SharePointForumWebPart runat=server></{0}:SharePointForumWebPart>")]
	[XmlRoot(Namespace="BilSimser.SharePoint.WebParts.Forums")]
	public class SharePointForumWebPart : WebPart
	{
		#region Fields

		private const string STR_RESOURCE_STRING = "/strings/string[@id='{0}']";
		private const string STR_LANGUAGE_FILTER = "*.lng.xml";
		private const string STR_DEFAULT_LANGUAGE = "1033";
		private const string STR_DEFAULT_FORUM_NAME = "Discussion Forums";

		private string _name = STR_DEFAULT_FORUM_NAME;
		private int _postCount = 0;
		private int _topicCount = 0;
		private int _forumCount = 0;
        private string _currentLanguage = STR_DEFAULT_LANGUAGE;
		private XmlDocument _resourceFile;
		private ArrayList _errorList = new ArrayList();

		#endregion

		#region Properties

		public ArrayList ErrorList
		{
			get { return _errorList; }
		}

		public bool IsValid
		{
			get { return ErrorList.Count == 0; }
		}

		[Category("Forum Properties")]
		[DefaultValue(STR_DEFAULT_FORUM_NAME)]
		[WebPartStorage(Storage.Shared)]
		[FriendlyNameAttribute("Forum Name")]
		[Description("Display name for your discussion forum.")]
		[Browsable(false)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[Category("Forum Properties")]
		[DefaultValue(0)]
		[WebPartStorage(Storage.Shared)]
		[FriendlyNameAttribute("Post Count")]
		[Description("Number of posts in this forum.")]
		[Browsable(false)]
		public int PostCount
		{
			get { return _postCount; }
			set { _postCount = value; }
		}

		[Category("Forum Properties")]
		[DefaultValue(0)]
		[WebPartStorage(Storage.Shared)]
		[FriendlyNameAttribute("Forum Count")]
		[Description("Number of forums in this site.")]
		[Browsable(false)]
		public int ForumCount
		{
			get { return _forumCount; }
			set { _forumCount = value; }
		}

		[Category("Forum Properties")]
		[DefaultValue(0)]
		[WebPartStorage(Storage.Shared)]
		[FriendlyNameAttribute("Topic Count")]
		[Description("Number of topics in this forum.")]
		[Browsable(false)]
		public int TopicCount
		{
			get { return _topicCount; }
			set { _topicCount = value; }
		}

		#endregion

		#region Protected Methods

		protected override void OnInit(EventArgs e)
		{
			using (Identity.ImpersonateAppPool())
			{
				LoadResourceFileStrings();
				InitializeApplication();
				CheckAndBuildLists();
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			/*
			 * BUG: update isn't working
			 * 
			using (Identity.ImpersonateAppPool())
			{
				UpdateCurrentUserVisitTime();
			}
			*/
		}

		protected override void RenderWebPart(HtmlTextWriter output)
		{
			if (output == null)
				return;

			try
			{
				EnsureChildControls();
				
				if(IsValid)
				{
					using (Identity.ImpersonateAppPool())
					{
						foreach (Control control in this.Controls)
						{
							control.RenderControl(output);
						}
					}
				}
				else
				{
					RenderError(output);
				}
			}
			catch (ThreadAbortException)
			{
				// this is thrown when the RSS feed completes so ignore it		
				// TODO: find a better way to render RSS so we don't throw the exception
			}
			catch (Exception unhandledException)
			{
				RenderError(output, unhandledException);
			}
		}

		/// <summary>
		/// Notifies server controls that use composition-based implementation to create any child
		/// controls they contain in preparation for posting back or rendering.
		/// This will read in a QueryString called "control" and instantiate a server
		/// control with the same name then add that to the Web Parts controls.
		/// 
		/// If no QueryString called "control" is found or invalid, we fall back to the
		/// basic <see cref="ViewForums"/> which just displays the default forum(s).
		/// </summary>
		protected override void CreateChildControls()
		{
			using (Identity.ImpersonateAppPool())
			{
				SharePointForumControls childControl;

				try
				{
					childControl = (SharePointForumControls) Enum.Parse(typeof (SharePointForumControls), Page.Request.QueryString["control"], true);
				}
				catch
				{
					childControl = SharePointForumControls.ViewForums;
				}

				BaseForumControl control = CreateDynamicForumControl(childControl);
				if(control != null)
					Controls.Add(control);
			}
		}

		private BaseForumControl CreateDynamicForumControl(SharePointForumControls childControl)
		{
			BaseForumControl control = null;
			
			try
			{
				string controlTypeName = string.Format("{0}.{1}", ForumConstants.Control_Namespace, childControl.ToString());
				Type controlType = Type.GetType(controlTypeName);
				control = Activator.CreateInstance(controlType) as BaseForumControl;
				control.WebPartParent = this;
			}
			catch(ArgumentNullException nullEx)
			{
				AddError(nullEx);
			}

			return control;
		}

		#endregion

		#region Private Methods

		public void RenderError(HtmlTextWriter output, Exception ex)
		{
			AddError(ex);
			RenderError(output);
		}

		private void RenderError(HtmlTextWriter output)
		{
			StringBuilder sb = new StringBuilder();
			foreach(Exception ex in ErrorList)
			{
				sb.AppendFormat("{0}<br>", ex.Message);
			}

			output.WriteLine("An error has occurred with the Forums Web Part.");
			output.WriteLine("See the details below for more information.</p>");
			output.WriteLine("<div class=\"ms-alerttext\">{0}</div>", sb.ToString());

			// TODO: get more system information to email
			output.WriteLine("</p><strong><a href=\"mailto:{0}?Subject=Forum Error&Body={1}\">Email error information to support</a></strong></p>", ForumConstants.Config_Author_Email, sb.ToString());
			output.WriteLine("<a href=\"{1}\">Return to {0} Home</a>", this.Name, ForumApplication.Instance.GetLink(SharePointForumControls.ViewForums));
		}

		private void LoadResourceFileStrings()
		{
			try
			{
				string resources = GetResourceDirectory();
				DirectoryInfo directoryInfo = new DirectoryInfo(this.Page.Server.MapPath(resources));
				FileInfo[] languageFileInfoArray = directoryInfo.GetFiles(STR_LANGUAGE_FILTER);

				SPWeb web;
				for (int n = 0; n < languageFileInfoArray.Length; n++)
				{
					FileInfo fileInfo = languageFileInfoArray[n];
					web = SPControl.GetContextWeb(this.Context);
					if (fileInfo.Name == (web.Language.ToString() + ".lng.xml"))
					{
						this._currentLanguage = web.Language.ToString();
					}
				}

				if (this._currentLanguage == "")
				{
					this._currentLanguage = "1033";
				}

				this._resourceFile = new XmlDocument();
				XmlTextReader reader = new XmlTextReader(this.Page.Server.MapPath(resources + "/" + this._currentLanguage + ".lng.xml"));
				this._resourceFile.Load(reader);
				reader.Close();

			}
			catch (Exception ex)
			{
				AddError(ex);
			}
		}

		private string GetResourceDirectory()
		{
			string resources = this.ClassResourcePath;
			int p = resources.ToLower().IndexOf("wpresources");
			string s = resources.Substring(1, p);
			p = s.LastIndexOf("/") + 1;
			if(p > 1) resources = resources.Substring(p, resources.Length-p);
			return resources;
		}

		/// <summary>
		/// Uses the Builder pattern to check and create lists if they're needed.
		/// </summary>
		private static void CheckAndBuildLists()
		{
			ListDirector director = new ListDirector();
			director.ConstructList(new GroupListBuilder());
			director.ConstructList(new UserListBuilder());
			director.ConstructList(new CategoryListBuilder());
			director.ConstructList(new ForumAccessListBuilder());
			director.ConstructList(new ForumListBuilder());
			director.ConstructList(new TopicListBuilder());
			director.ConstructList(new MessageListBuilder());
		}

		private static void UpdateCurrentUserVisitTime()
		{
			ForumApplication.Instance.CurrentUser.LastVisit = DateTime.Now;
			RepositoryRegistry.ForumUserRepository.Save(ForumApplication.Instance.CurrentUser);
		}

		private void InitializeApplication()
		{
			UrlQuery query = new UrlQuery(this.Page.Request.Url.ToString());
			ForumApplication.Instance.BasePath = SPEncode.UrlEncodeAsUrl(query.Url);
			ForumApplication.Instance.Title = this.Name;
			ForumApplication.Instance.ForumCache = this.Page.Cache;
			ForumApplication.Instance.ClassResourcePath = this.ClassResourcePath;
			ForumApplication.Instance.SpUser = SPControl.GetContextWeb(Context).CurrentUser;
			ForumApplication.Instance.AppPoolUser = SPControl.GetContextSite(Context).OpenWeb().CurrentUser;
			ForumApplication.Instance.SpWeb = SPControl.GetContextSite(Context).OpenWeb();
		}

		#endregion

		#region Public Methods

		public void AddError(Exception ex)
		{
			if (ex == null)
				return;
			ErrorList.Add(ex);
		}

		public override string LoadResource(string id)
		{
			string translatedResource;

			try
			{
				translatedResource = this._resourceFile.DocumentElement.SelectSingleNode(String.Format(STR_RESOURCE_STRING, id)).InnerText;
			}
			catch
			{
				translatedResource = string.Format("Error reading resource ID=\"{0}\".", id);
			}

            return translatedResource;
		}

		public void PersistProperties()
		{
			try
			{
				SPWeb web = ForumApplication.Instance.SpWeb;
				SPFile file = web.GetFile(this.Context.Request.Path);
				SPWebPartCollection webPartCollection = file.GetWebPartCollection(Storage.Shared);
				web.AllowUnsafeUpdates = true;
				//this.SaveProperties = true;
				webPartCollection.SaveChanges(this.StorageKey);
			}
			catch (Exception ex)
			{
				AddError(ex);	
			}
		}

		#endregion
	}
}