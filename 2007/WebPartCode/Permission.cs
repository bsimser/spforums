using System;
using System.Collections;
using System.Text;

namespace BilSimser.SharePointForums.WebPartCode
{
    public class Permission
    {
        private BitArray bitMask;

        public enum Rights
        {
            // User functions
            Read = 0, // read messages
            Add, // add new messages
            Edit, // edit their own message
            Reply, // reply to other peoples messages
            Delete, // delete their own message
            Sticky, // create sticky posts
            Announcement, // create announcement posts
            Polls, // create polls
            Vote, // vote in existing polls
            Moderate, // moderate a forum
            Attach, // add attachment to a message
            Admin, // admins have access to everything
            // Placeholders for future flags so we don't have to handle conversions of data
            PlaceHolder0,
            PlaceHolder1,
            PlaceHolder2,
            PlaceHolder3,
            PlaceHolder4,
            PlaceHolder5,
            PlaceHolder6,
            PlaceHolder7,
            PlaceHolder8,
            PlaceHolder9,
            // This needs to be here to determine the size of the bit array
            LastRight
        } ;

        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class
        /// with all rights are set to false.
        /// </summary>
        public Permission()
        {
            bitMask = new BitArray((int)Rights.LastRight, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        /// <param name="permissionMask">The permission mask.</param>
        public Permission(string permissionMask)
            : this()
        {
            if (null == permissionMask)
                throw new ApplicationException("Cannot create a permission mask from nothing.");

            if (permissionMask.Length == 0)
                throw new ApplicationException("Cannot create a permission mask with an empty string.");

            if (permissionMask.Length < (int)Rights.LastRight)
                throw new ApplicationException("Permission mask length is incorrect.");

            for (int i = 0; i < permissionMask.Length; i++)
            {
                char c = permissionMask[i];
                if (c >= '0' && c <= '1')
                {
                    bool permission = (c == '1' ? true : false);
                    Rights right = (Rights)i;
                    SetPermission(right, permission);
                }
                else
                {
                    throw new ApplicationException(
                        String.Format("Invalid character {0} (position {1}) in string {2} passed in for creation.",
                                      c, i, permissionMask));
                }
            }
        }

        /// <summary>
        /// Determines whether the specified right has permission.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <returns>
        /// 	<c>true</c> if the specified right has permission; otherwise, <c>false</c>.
        /// </returns>
        public bool HasPermission(Rights right)
        {
            return bitMask.Get((int)right);
        }

        /// <summary>
        /// Sets the permission.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <param name="permission">if set to <c>true</c> [permission].</param>
        public void SetPermission(Rights right, bool permission)
        {
            bitMask.Set((int)right, permission);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (bool b in bitMask)
            {
                sb.Append(Convert.ToByte(b));
            }
            return sb.ToString();
        }

        public string DisplayString
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < (int)Rights.LastRight; i++)
                {
                    Rights right = (Rights)i;
                    if (HasPermission(right))
                    {
                        sb.AppendFormat("{0} ", right.ToString());
                    }
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Sets all rights.
        /// </summary>
        /// <param name="permission">if set to <c>true</c> [permission].</param>
        public void SetAllRights(bool permission)
        {
            for (int i = 0; i < (int)Rights.LastRight; i++)
            {
                Rights right = (Rights)i;
                SetPermission(right, permission);
            }
        }
    }
}