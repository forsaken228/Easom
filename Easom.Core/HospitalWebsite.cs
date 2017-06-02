// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
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
	/// HospitalWebsite  客户端实体操作类
	///</summary>
	[Serializable]
	public class HospitalWebsite
	{
		private static IHospitalWebsite _actor = null;
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
        /// 科室名称
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

		#endregion 属性

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
