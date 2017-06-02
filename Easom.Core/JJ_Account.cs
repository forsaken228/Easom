// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-18
// ���ߣ�loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Easom.Core
{

	///<summary>
	/// JJ_Account  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class JJ_Account
	{
		private static IJJ_Account _actor = null;
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
		/// AccountName
		///</summary>
		public string AccountName
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
		/// Remark1
		///</summary>
		public string Remark1
		{
			get;
			set;
		}

		///<summary>
		/// Remark2
		///</summary>
		public string Remark2
		{
			get;
			set;
		}

       
        /// <summary>
        /// ��������
        /// </summary>
        public string SectionName
        {
            get
            {
                if (SectionID > 0)
                {
                    Section entity = Section.Actor.GetByID(this.SectionID);
                    if (entity != null)
                    {
                        return entity.Name;
                    }
                }
                return null;
            }
        }


		#endregion ����

		///<summary>
		/// Actor
		///</summary>
		public static IJJ_Account Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new JJ_AccountService();
						}
					}
				}

				return _actor;
			}
		}


        public static bool GetJJ_AccountAuthority(IList<JJ_Account> entityList, int id)
        {
            bool returnData = false;
            if (entityList != null && entityList.Count > 0)
            {
                var entity = from hospital in entityList where hospital.ID == id select hospital;
                if (entity != null && entity.Count() > 0)
                {
                    returnData = true;
                }
            }
            return returnData;
        }


	}
}
