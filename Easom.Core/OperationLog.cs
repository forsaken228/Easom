// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2017-01-04
// 作者：loskiv@gmail.com

using Easom.Core.Contracts;
using Easom.Core.Services;
using Easom.Core.Support;
using System;

namespace Easom.Core
{

	///<summary>
	/// OperationLog  客户端实体操作类
	///</summary>
	[Serializable]
	public class OperationLog
	{
		private static IOperationLog _actor = null;
		private static readonly object _lockActor = new object();

		#region 属性

		///<summary>
		/// ID
		///</summary>
		public long ID
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
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string UserName
        {
            get
            {
                UserInfo user = UserInfo.Actor.GetByID(UserID);
                if (user != null)
                {
                    return user.TrueName;
                }
                return null;
            }
        }
        ///<summary>
        /// CreateTime
        ///</summary>
        public DateTime CreateTime
		{
			get;
			set;
		}

		///<summary>
		/// OperationInfo
		///</summary>
		public string OperationInfo
		{
			get;
			set;
		}

		///<summary>
		/// TableName
		///</summary>
		public string TableName
		{
			get;
			set;
		}

		///<summary>
		/// OperationType
		///</summary>
		public OperationTypeEnum OperationType
        {
			get;
			set;
		}

        ///<summary>
		/// OperationType
		///</summary>
		public string OperationTypeName
        {
            get
            {
                if (OperationType == OperationTypeEnum.Add)
                {
                    return "增加";
                }
                if (OperationType == OperationTypeEnum.Delete)
                {
                    return "删除";
                }
                if (OperationType == OperationTypeEnum.Update)
                {
                    return "修改";
                }
                if (OperationType == OperationTypeEnum.View)
                {
                    return "查";
                }
                return "无";
            }
        }
        ///<summary>
        /// LinkID关联ID
        ///</summary>
        public long LinkID
        {
            get;
            set;
        }

        ///<summary>
        /// HospitalID
        ///</summary>
        public int HospitalID
        {
            get;
            set;
        }

        ///<summary>
        /// SectionID
        ///</summary>
        public int SectionID
        {
            get;
            set;
        }
        #endregion 属性

        ///<summary>
        /// Actor
        ///</summary>
        public static IOperationLog Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new OperationLogService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
