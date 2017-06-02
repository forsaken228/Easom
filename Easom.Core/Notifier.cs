// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-05-19
// ���ߣ�loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using Easom.Core.Support;

namespace Easom.Core
{

	///<summary>
	/// Notifier  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class Notifier
	{
		private static INotifier _actor = null;
		private static readonly object _lockActor = new object();

        #region ����

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
        /// �û���ʵ����
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
        /// ����״̬���ɹ�orʧ�ܣ�
        ///</summary>
        public SendState SendState
        {
            get;
            set;
        }


        ///<summary>
        /// ����Ƶ��
        ///</summary>
        public int NotifyCycle
        {
            get;
            set;
        }

        ///<summary>
        /// ״̬������/������
        ///</summary>
        public bool Disable
        {
            get;
            set;
        }

        ///<summary>
        /// ����ʱ��
        ///</summary>
        public DateTime SendDate
        {
            get;
            set;
        }

        ///<summary>
        /// ����ʱ��
        ///</summary>
        public DateTime CreateDate
        {
            get;
            set;
        }



        #endregion ����

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
