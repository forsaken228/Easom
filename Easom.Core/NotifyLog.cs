// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-05-19
// 作者：loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;

namespace Easom.Core
{

	///<summary>
	/// NotifyLog  客户端实体操作类
	///</summary>
	[Serializable]
	public class NotifyLog
	{
		private static INotifyLog _actor = null;
		private static readonly object _lockActor = new object();

        #region 属性

        ///<summary>
        /// ID
        ///</summary>
        public int ID
        {
            get;
            set;
        }

        ///<summary>
        /// UserID
        ///</summary>
        public int UserID
        {
            get;
            set;
        }

        ///<summary>
        /// TrueName
        ///</summary>
        public UserInfo UserInfo
        {
            get
            {
                if (UserID != null)
                {
                    UserInfo result = UserInfo.Actor.GetByID(UserID);
                    if (result != null)
                    {
                        return result;
                    }
                }
                return null;
            }
        }

        ///<summary>
        /// SendState
        ///</summary>
        public int SendState
        {
            get;
            set;
        }

        ///<summary>
        /// SendDate
        ///</summary>
        public DateTime SendDate
        {
            get;
            set;
        }


        #endregion 属性

		///<summary>
		/// Actor
		///</summary>
		public static INotifyLog Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new NotifyLogService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
