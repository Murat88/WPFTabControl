using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OzyaysanBusinessEngine
{
    [Serializable]
    public class BaseClass
    {
        private int m_ID;
        private string m_Name;
        private string m_Desc;
        private DateTime m_CreationDate;
        private int m_CreationUserID = 0;
        private string m_CreationIP;
        private DateTime m_UpdateDate;
        private int m_UpdateUserID = 0;
        private string m_UpdateIP;
        private bool m_IsDirty;
        private Enumarations.State m_State;
        private bool m_IsVerified;
        private Enumarations.State m_MinState;
        private int m_OrderNo;
        /// <summary>
        /// ID
        /// </summary>
        public virtual int ID
        {
            get
            {
                return this.m_ID;
            }
            set
            {
                this.m_ID = value;
            }
        }

        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public string Desc
        {
            get
            {
                return m_Desc;
            }
            set
            {
                m_Desc = value;
            }
        }


        /// <summary>
        /// Creation Date
        /// </summary>
        public virtual DateTime CreationDate
        {
            get
            {
                return m_CreationDate;
            }
            set
            {
                m_CreationDate = value;
            }
        }

        /// <summary>
        /// Creator User ID
        /// </summary>
        public virtual int CreationUserID
        {
            get
            {
                return m_CreationUserID;
            }
            set
            {
                m_CreationUserID = value;
                if (m_UpdateUserID == 0)
                {
                    m_UpdateUserID = m_CreationUserID;
                }

            }
        }

        /// <summary>
        /// Creator IP
        /// </summary>
        public virtual string CreationIP
        {
            get
            {
                return m_CreationIP;
            }
            set
            {
                m_CreationIP = value;
            }
        }

        /// <summary>
        /// Update Date
        /// </summary>
        public virtual DateTime UpdateDate
        {
            get
            {
                return m_UpdateDate;
            }
            set
            {
                m_UpdateDate = value;
            }
        }

        /// <summary>
        /// Updater User ID
        /// </summary>
        public virtual int UpdateUserID
        {
            get
            {
                return m_UpdateUserID;
            }
            set
            {
                m_UpdateUserID = value;
            }
        }

        /// <summary>
        /// Updater IP
        /// </summary>
        public virtual string UpdateIP
        {
            get
            {
                return m_UpdateIP;
            }
            set
            {
                m_UpdateIP = value;
            }
        }

        public virtual bool IsDirty
        {
            get
            {
                return m_IsDirty;
            }
            set
            {
                m_IsDirty = value;
            }
        }

        /// <summary>
        /// State
        /// </summary>
        public virtual Enumarations.State State
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
            }
        }

        public bool IsVerified
        {
            get
            {
                return m_IsVerified;
            }
            set
            {
                m_IsVerified = value;
            }
        }

        public Enumarations.State MinState
        {
            get
            {
                return m_MinState;
            }
            set
            {
                m_MinState = value;
            }
        }
        public virtual int OrderNo
        {
            get
            {
                return this.m_OrderNo;
            }
            set
            {
                this.m_OrderNo = value;
            }
        }
    }
}