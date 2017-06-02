// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System;

using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;
using CHCMS.Utility;
namespace Easom.Core.Services
{

	///<summary>
	/// DiseaseService  服务处理类
	///</summary>
	public class DiseaseService : IDisease
	{
		private static readonly string _cacheKey = "Easom.Core.DiseaseService";
		private static readonly DiseaseDataAdapter dal = new DiseaseDataAdapter();

		#region IDisease 成员

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(Disease entity)
		{
			int r = dal.Insert(entity);
			CacheUtility.Delete(_cacheKey);
			return r;
		}

		///<summary>
		/// 删除一条记录
		///</summary>
		public void Delete(int id)
		{
			dal.Delete(id);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 更新一条记录
		///</summary>
		public void Update(Disease entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 读取一条记录
		///</summary>
		public Disease GetByID(int id)
		{
			Disease entity = (Disease)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

        ///<summary>
        /// 读取一条记录
        ///</summary>
        public IList<Disease> GetDiseaseBySection(string section)
        {
            IList<Disease> entity = (IList<Disease>)CacheUtility.Get(_cacheKey, new string[] { section.ToString(), "GetDiseaseBySection" });
            if (entity == null)
            {
                entity = dal.GetDiseaseBySection(section);
                CacheUtility.Add(entity, _cacheKey, new string[] { section.ToString(), "GetDiseaseBySection" });
            }
            return entity;
        }

        /// <summary>
        /// 读取当前医院所有疾病信息
        /// </summary>
        /// <returns></returns>
        public IList<Disease> GetAllDisease(int hospitalid)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { hospitalid.ToString() });
            if (objList == null)
            {
                objList = new object[1];
                objList[0] = dal.GetAllDisease(hospitalid);
                CacheUtility.Add(objList, _cacheKey, new string[] { hospitalid.ToString() });
            }
            return (IList<Disease>)objList[0];
        }

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<Disease> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderField.ToString(), isDesc.ToString() });
			if (objList == null)
			{
				objList = new object[2];
				objList[0] = dal.Search(out pageCount, pageIndex, pageSize, where, orderField, isDesc);
				objList[1] = pageCount;
				CacheUtility.Add(objList, _cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderField.ToString(), isDesc.ToString() });
			}
			pageCount = Convert.ToInt32(objList[1]);
			return (IList<Disease>)objList[0];;
		}

		#endregion

	}
}
