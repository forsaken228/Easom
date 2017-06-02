// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-05-19
// 作者：loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using Easom.Core.Support;

namespace Easom.Core
{

	///<summary>
	/// Notifier  客户端实体操作类
	///</summary>
	[Serializable]
	public class Notifier
	{
		private static INotifier _actor = null;
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
        /// Name
        ///</summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string TrueName
        {
            get;
            set;
        }

        ///<summary>
        /// NotifyType
        ///</summary>
        public NotifyTypeEnum NotifyType
        {
            get;
            set;
        }

        ///<summary>
        /// 发送状态（成功or失败）
        ///</summary>
        public SendState SendState
        {
            get;
            set;
        }


        ///<summary>
        /// 发送频率
        ///</summary>
        public int NotifyCycle
        {
            get;
            set;
        }

        ///<summary>
        /// 状态（禁用/启动）
        ///</summary>
        public bool Disable
        {
            get;
            set;
        }

        ///<summary>
        /// 发送时间
        ///</summary>
        public DateTime SendDate
        {
            get;
            set;
        }

        ///<summary>
        /// 创建时间
        ///</summary>
        public DateTime CreateDate
        {
            get;
            set;
        }



        #endregion 属性

		///<summary>
		/// Actor
		///</summary>
		public static INotifier Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new NotifierService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
