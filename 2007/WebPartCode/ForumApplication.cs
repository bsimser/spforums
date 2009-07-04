using System.Web.Caching;
using Microsoft.SharePoint;

namespace BilSimser.SharePointForums.WebPartCode
{
    /// <summary>
    /// I'm not proud of this, but it provides a way to
    /// capture all the properties of the app that we need
    /// to share between the UI and Domain.
    /// </summary>
    /// <remarks>
    /// We don't have a good separation of domain and infrastruture
    /// as this is the only class in the Core project that relies
    /// on SharePoint (for SPUser and SPWeb). The repositories and mappers
    /// use it to retrieve the current web that's in context (using impersonation)
    /// but it's ugly.
    /// </remarks>
    public sealed class ForumApplication
    {
        private static readonly ForumApplication instance = new ForumApplication();
        private SPUser spUser;
        private SPUser appPoolUser;
        private SPWeb spWeb;
        private string title = "Discussion Forums";
        private string classResourcePath;
        private Cache forumCache;
        private string basePath;

        /// <summary>
        /// Initializes the <see cref="ForumApplication"/> class.
        /// </summary>
        static ForumApplication()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForumApplication"/> class.
        /// </summary>
        private ForumApplication()
        {
        }

        public Cache ForumCache
        {
            get { return forumCache; }
            set { forumCache = value; }
        }

        public string BasePath
        {
            get { return basePath; }
            set { basePath = value; }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ForumApplication Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Gets or sets the class resource path.
        /// </summary>
        /// <value>The class resource path.</value>
        public string ClassResourcePath
        {
            get { return classResourcePath; }
            set { classResourcePath = value; }
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

        /// <summary>
        /// Gets or sets the sp user.
        /// </summary>
        /// <value>The sp user.</value>
        public SPUser SpUser
        {
            get { return spUser; }
            set { spUser = value; }
        }

        /// <summary>
        /// Gets or sets the sp web.
        /// </summary>
        /// <value>The sp web.</value>
        public SPWeb SpWeb
        {
            get { return spWeb; }
            set { spWeb = value; }
        }

        /// <summary>
        /// Gets or sets the app pool user.
        /// </summary>
        /// <value>The app pool user.</value>
        /// <remarks>
        /// Used for debugging only
        /// </remarks>
        public SPUser AppPoolUser
        {
            get { return appPoolUser; }
            set { appPoolUser = value; }
        }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>The current user.</value>
        /// <remarks>
        /// Move this property out of the ForumApplication class as it should
        /// be in the UserSession class. ForumApplication contains information
        /// about the forum, not users.
        /// </remarks>
        public ForumUser CurrentUser
        {
            get { return RepositoryRegistry.ForumUserRepository.GetBySharePointId(spUser.ID); }
        }

        /// <summary>
        /// Gets the forum image.
        /// </summary>
        /// <value>The forum image.</value>
        public string ForumImage
        {
            get { return string.Format("{0}/{1}", "/_layouts/images/", ForumConstants.Image_ForumNew); }
        }

        /// <summary>
        /// Constructs a link to a given action control.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public string GetLink(SharePointForumControls action)
        {
            return string.Format("{0}?control={1}", BasePath, action.ToString());
        }

        /// <summary>
        /// Constructs a link to a given action control with a set of parameters.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public string GetLink(SharePointForumControls action, string format, params object[] args)
        {
            return string.Format("{0}?control={1}&{2}", BasePath, action, string.Format(format, args));
        }

        public string GetNewTopicLink(int forumId, string postMethod)
        {
            return GetLink(
                SharePointForumControls.UpdateMessage,
                "forum={0}&{1}={2}", forumId, ForumConstants.Query_PostMethod, postMethod);
        }

        public string GetReplyLink(int topicId, string postMethod)
        {
            return ForumApplication.Instance.GetLink(
                SharePointForumControls.UpdateMessage,
                "topic={0}&{1}={2}", topicId, ForumConstants.Query_PostMethod, postMethod);
        }

        public string GetDefaultGroupsForNewUser()
        {
            return "1;2";
        }
    }
}