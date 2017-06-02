// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2017-01-04
// ���ߣ�loskiv@gmail.com

using Easom.Core.Contracts;
using Easom.Core.Services;
using Easom.Core.Support;
using System;

namespace Easom.Core
{

	///<summary>
	/// OperationLog  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class OperationLog
	{
		private static IOperationLog _actor = null;
		private static readonly object _lockActor = new object();

		#region ����

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
        /// ����������
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
                    return "����";
                }
                if (OperationType == OperationTypeEnum.Delete)
                {
                    return "ɾ��";
                }
                if (OperationType == OperationTypeEnum.Update)
                {
                    return "�޸�";
                }
                if (OperationType == OperationTypeEnum.View)
                {
                    return "��";
                }
                return "��";
            }
        }
        ///<summary>
        /// LinkID����ID
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
        #endregion ����

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
