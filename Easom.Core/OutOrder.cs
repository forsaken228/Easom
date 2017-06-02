// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
namespace Easom.Core
{

	///<summary>
	/// OutOrder  客户端实体操作类
	///</summary>
	[Serializable]
	public class OutOrder
	{
		private static IOutOrder _actor = null;
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
		/// CatID
		///</summary>
		public int CatID
		{
			get;
			set;
		}

		///<summary>
		/// OutSiteToHospiatlID
		///</summary>
		public int OutSiteToHospiatlID
		{
			get;
			set;
		}

        ///<summary>
        /// OutSiteToHospiatlID
        ///</summary>
        public HospitalWebsite HospitalWebsite
        {
            get {
              HospitalWebsite hospital=HospitalWebsite.Actor.GetByID(OutSiteToHospiatlID);
              return hospital;
            }
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
		/// Sex
		///</summary>
		public bool Sex
		{
			get;
			set;
		}

		///<summary>
		/// State
		///</summary>
		public bool State
		{
			get;
			set;
		}

		///<summary>
		/// OrderTime
		///</summary>
		public DateTime OrderTime
		{
			get;
			set;
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
		/// Age
		///</summary>
		public string Age
		{
			get;
			set;
		}

		///<summary>
		/// UserName
		///</summary>
		public string UserName
		{
			get;
			set;
		}

		///<summary>
		/// Telephone
		///</summary>
		public string Telephone
		{
			get;
			set;
		}

		///<summary>
		/// Description
		///</summary>
		public string Description
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

        public HospitalWebsite CurrentOutSiteToHospital
        {
            get
            {
                HospitalWebsite currentEntity = null;
                if (this.OutSiteToHospiatlID > 0)
                {
                    currentEntity = HospitalWebsite.Actor.GetByID(this.OutSiteToHospiatlID);
                    return currentEntity;
                }
                return currentEntity;
            }
        }

		#endregion 属性

		///<summary>
		/// Actor
		///</summary>
		public static IOutOrder Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new OutOrderService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
