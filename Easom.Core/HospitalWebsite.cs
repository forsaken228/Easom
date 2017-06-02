// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
using System.Linq;
using System.Collections.Generic;
namespace Easom.Core
{

	///<summary>
	/// HospitalWebsite  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class HospitalWebsite
	{
		private static IHospitalWebsite _actor = null;
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
		/// HospitalID
		///</summary>
		public int HospitalID
		{
			get;
			set;
		}

		///<summary>
		/// Title
		///</summary>
		public string Title
		{
			get;
			set;
		}

		///<summary>
		/// URL
		///</summary>
		public string URL
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
		/// SiteType
		///</summary>
		public int SiteType
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

        public static bool GetHospitalWebsiteAuthority(IList<HospitalWebsite> entityList, int id) 
        {
            bool returnData = false;
            if (entityList != null && entityList.Count > 0)
            {
                var entity = from website in entityList where website.ID == id select website;
                if (entity != null && entity.Count() > 0)
                {
                    returnData = true;
                }
            }
            return returnData;
        }

		#endregion ����

		///<summary>
		/// Actor
		///</summary>
		public static IHospitalWebsite Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new HospitalWebsiteService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
