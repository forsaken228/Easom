// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
using System.Collections.Generic;
using System.Linq;
namespace Easom.Core
{

	///<summary>
	/// Disease  客户端实体操作类
	///</summary>
	[Serializable]
	public class Disease
	{
		private static IDisease _actor = null;
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
		/// Priority
		///</summary>
		public int Priority
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
		/// IsMain
		///</summary>
		public bool IsMain
		{
			get;
			set;
		}

		///<summary>
		/// CreatTime
		///</summary>
		public DateTime CreatTime
		{
			get;
			set;
		}

		///<summary>
		/// Cure
		///</summary>
		public string Cure
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

		///<summary>
		/// Intro
		///</summary>
		public string Intro
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
		/// IsDelete
		///</summary>
		public int IsDelete
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



        public static bool GetDiseaseAuthority(IList<Disease> entityList, int id) 
        {
            bool returnData = false;
            if (entityList != null && entityList.Count > 0)
            {
                var entity = from disease in entityList where disease.ID == id select disease;
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
		public static IDisease Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new DiseaseService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
