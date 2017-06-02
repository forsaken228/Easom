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
	/// Section  客户端实体操作类
	///</summary>
	[Serializable]
	public class Section
	{
		private static ISection _actor = null;
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

        public static bool IsChecked(IList<Section> entityList, int id)
        {
            bool returnData = false;
            if (entityList != null && entityList.Count > 0)
            {
                var entity = from section in entityList where section.ID == id select section;
                if (entity != null && entity.Count() > 0)
                {
                    returnData = true;
                }
            }
            return returnData;
        }


        ///<summary>
        /// IsDelete
        ///</summary>
        public int IsDelete
        {
            get;
            set;
        }



		#endregion 属性

		///<summary>
		/// Actor
		///</summary>
		public static ISection Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new SectionService();
						}
					}
				}

				return _actor;
			}
		}


        public static bool GetSectionAuthority(IList<Section> entityList, int id) 
        {
            bool returnData = false;
            if (entityList != null && entityList.Count > 0)
            {
                var entity = from Section in entityList where Section.ID == id select Section;
                if (entity != null && entity.Count() > 0)
                {
                    returnData = true;
                }
            }
            return returnData;
        }


	}
}
