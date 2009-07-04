namespace BilSimser.SharePointForums.WebPartCode
{
    /// <summary>
    /// Enumerated value of that are loaded at runtime
    /// based on the request by the system.
    /// </summary>
    /// <remarks>
    /// These must match the control class names because they are used
    /// to dynamically create the objects of the same name.
    /// </remarks>
    public enum SharePointForumControls
    {
        ViewForums,
        ViewTopics,
        ViewMessages,
        ViewProfile,
        ViewMembers,
        UpdateMessage,
        AdministrationPanel,
        ConfigureForums,
        ManageForums,
        ManageForumPermissions,
        ManageForumGroupPermissions,
        ManageUsers,
        ManageGroups,
        EditUser,
        EditCategory,
        EditForum,
        EditGroup,
        DeleteTopic,
        SynFeed,
        Search,
        UpdateCounts,
        ShowToday,
        CreateSampleData,
        DeleteForums,
        ShowInactive,
    }
}