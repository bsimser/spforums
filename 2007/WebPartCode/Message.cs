using System;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class Message : DomainObject
    {
        private ForumUser author;
        private DateTime created;
        private DateTime _modified;
        private string body;
        private int topicId;
        private int userId;

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        private Message()
        {
        }

        public Message(int topicId)
        {
            TopicId = topicId;
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>The user id.</value>
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        /// <value>The author.</value>
        public ForumUser Author
        {
            get { return author; }
            set { author = value; }
        }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>The body.</value>
        public string Body
        {
            get { return body; }
            set { body = value; }
        }

        /// <summary>
        /// Gets or sets the topic id.
        /// </summary>
        /// <value>The topic id.</value>
        public int TopicId
        {
            get { return topicId; }
            set { topicId = value; }
        }

        public DateTime Modified
        {
            get { return _modified; }
            set
            {
                _modified = value;
            }
        }
    }
}