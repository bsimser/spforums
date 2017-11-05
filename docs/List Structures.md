### List Structures

The SharePoint Forums Web Part stores all of it's information (categories, topics, posts, users, forums, etc.) in a set of lists in SharePoint itself. These are based on the Custom list template with additional fields added.

This page contains the names and structures (as of v1.0.0.0) of the lists used in the SharePoint Forums Web Part. It is for information only and the lists are not meant to be modified outside of the Web Part.

**spforums_posts**
List of messages posted from the Web Part.
Standard Fields:
Title (Text) - If post is the first in a Topic, then value = Subject line, otherwise value = (none).
Custom Fields:
TopicID (Number) - spforums_topics.ID
UserID (Number) - spforums_users.ID
Body (Multi-line Text) - HTML content of the body of the message, enclosed within <div> tags.

**spforums_category**
List of categories for the Web Part.
Title (Text) - Name of Category (i.e. "Test Category 1")
SortOrder (Number) - Future use to allow manual sorting of categories.

**spforums_forums**
List of forums for the Web Part.
Title (Text) - Name of forum (i.e. "Test Forum 1")
CategoryID (Number) - spforums_category.ID
Description (Multi-line Text) - Description of the forum ("This is just a test forum, nothing special here.")
SortOrder (Number) - Future use to allow manual sorting of forums.
TopicCount (Number) - Not used as counts are now created dynamically. Will be used later.
PostCount (Number) - Not used as counts are now created dynamically. Will be used later.

**spforums_topics**
List of topics for the Web Part.
Title (Text) - Topic text (i.e. "Welcome to your new SharePoint Forum.")
ForumID (Number) - spforums_forums.ID
Views (Number) - Count field for views on a topic
NumPosts (Number) - Not used as counts are now created dynamically. Will be used later.
TopicStarterID (Number) - spforums_users.ID of who started the topic.

**spforums_users**
List of users for the Web Part.
UserID (Number) - SharePoint ID for user
Title (Text) - User's login name
NumPosts (Number) - Number of posts by this user
LastVisit (DateTime) - Last "login" time, not the time of his/her last post
IsAdmin (Yes/No) - Yes = Forum Administrator
Groups (Text) - GroupID's to which this user belongs, separated by semicolons

**spsforums_groups**
Title (Text) - Name of forum security group (Reader, Contributor, Administrator, etc.)

**spsforums__forum__access**
Title (Text) - Binary number representing permissions:
1000000000000000000000 = Reader
1111100000000000000000 = Contributor
1111100000010000000000 = Administrator
ForumID (Number) - spforums_forums.ID, however there is a "Forum 0". "Forum 0" is the default permissions applied to any new forum created.
GroupID (Number) - spsforums_groups.ID

MORE DETAILS TO COME