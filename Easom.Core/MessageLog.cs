// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
namespace Easom.Core
{

	///<summary>
	/// MessageLog  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class MessageLog
	{
		private static IMessageLog _actor = null;
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
		/// ToNumber
		///</summary>
		public string ToNumber
		{
			get;
			set;
		}
        ///<summary>
		/// ToNumberDESC
		///</summary>
		public string ToDESNumber
        {
            get
            {
                string tel = "";
                if (!string.IsNullOrEmpty(this.ToNumber))
                {
                    // tel = SecurityUtility.DESDecrypt(this.Telephone, desCode);
                    tel = this.ToNumber;
                    if (tel.Length == 11)//185 0089 8765
                    {
                        tel = tel.Substring(0, 3) + "****" + tel.Substring(7, 4);
                    }
                    else if (tel.Length <= 8 && tel.Length > 4)
                    {
                        tel = "****" + tel.Substring(4, 3);
                    }
                    else
                    {
                        tel = "*******";
                    }
                    return tel;
                }
                return "";
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

		///<summary>
		/// SendContent
		///</summary>
		public string SendContent
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

        ///<summary>
        /// UserName
        ///</summary>
        public UserInfo UserInfo 
        {
            get
            {
                if (UserID > 0)
                {
                    UserInfo entity = UserInfo.Actor.GetByID(this.UserID);
                    if (entity != null)
                    {
                        return entity;
                    }
                }
                return null;
            }
        }


		#endregion ����

		///<summary>
		/// Actor
		///</summary>
		public static IMessageLog Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new MessageLogService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
