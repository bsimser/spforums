namespace BilSimser.SharePointForums.WebPartCode
{
    /// <summary>
    /// Contains all the fixed strings for the SharePoint Forums Web Part.
    /// </summary>
    public class ForumConstants
    {
        public const string Control_Namespace = "BilSimser.SharePoint.WebParts.Forums.Controls";

        // Key for adding and retreiving values from the ASP.NET cache object
        public const string Forum_Cache_Key = "SPSFORUMSKEY";

        // General stuff
        // These could be in a config file, but they don't change much and I don't 
        // want to distribute extra files 
        public const string Config_Author_Name = "Bil Simser";
        public const string Config_Author_Email = "emailme@bilsimser.com";
        public const string Config_Author_WebSite = "http://www.codeplex.com/Wiki/View.aspx?ProjectName=SPFORUMS";

        // Internal names of SharePoint lists
        public const string Lists_Category = "spforums_category";
        public const string Lists_Forums = "spforums_forums";
        public const string Lists_Topics = "spforums_topics";
        public const string Lists_Posts = "spforums_posts";
        public const string Lists_Users = "spforums_users";
        public const string Lists_ForumAccess = "spsforums_forum_access";
        public const string Lists_Groups = "spsforums_groups";

        // Image filenames 
        public const string Image_Forum = "COMACT.GIF";
        public const string Image_ForumLock = "COMACT.GIF";
        public const string Image_ForumNew = "COMACT.GIF";
        public const string Image_ForumReply = "COMACT.GIF";

        // QueryString names
        public const string Query_PostMethod = "postmethod";

        /// <summary>
        /// Initializes the <see cref="ForumConstants"/> class.
        /// </summary>
        static ForumConstants()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForumConstants"/> class.
        /// </summary>
        public ForumConstants()
        {
        }
    }
}