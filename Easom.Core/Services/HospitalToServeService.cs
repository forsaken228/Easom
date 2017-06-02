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
	/// HospitalToServeService  服务处理类
	///</summary>
	public class HospitalToServeService : IHospitalToServe
	{
		private static readonly string _cacheKey = "Easom.Core.HospitalToServeService";
		private static readonly HospitalToServeDataAdapter dal = new HospitalToServeDataAdapter();

		#region IHospitalToServe 成员

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(HospitalToServe entity)
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
		public void Update(HospitalToServe entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 读取一条记录
		///</summary>
		public HospitalToServe GetByID(int id)
		{
			HospitalToServe entity = (HospitalToServe)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<HospitalToServe> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<HospitalToServe>)objList[0];;
		}

        /// <summary>
        /// 获取一条医院服务信息
        /// </summary>
        /// <param name="hospital">医院ID</param>
        /// <param name="serverID">服务ID</param>
        /// <returns>该服务其他信息</returns>
        public HospitalToServe GetByHospitalID(int hospital,string serverKey)
        {
            HospitalToServe entity = (HospitalToServe)CacheUtility.Get(_cacheKey, new string[] { hospital.ToString(), serverKey.ToString(), "GetByHospitalID" });
            if (entity == null)
            {
                entity = dal.GetByHospitalID(hospital, serverKey);
                CacheUtility.Add(entity, _cacheKey, new string[] { hospital.ToString(), serverKey.ToString(), "GetByKey" });
            }
            return entity;
        }

		#endregion

	}
}
