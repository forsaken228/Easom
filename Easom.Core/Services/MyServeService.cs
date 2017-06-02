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
	/// MyServeService  服务处理类
	///</summary>
	public class MyServeService : IMyServe
	{
		private static readonly string _cacheKey = "Easom.Core.MyServeService";
		private static readonly MyServeDataAdapter dal = new MyServeDataAdapter();

		#region IMyServe 成员

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(MyServe entity)
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
		public void Update(MyServe entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 读取一条记录
		///</summary>
		public MyServe GetByID(int id)
		{
			MyServe entity = (MyServe)CacheUtility.Get(id, _cacheKey);
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
		public IList<MyServe> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<MyServe>)objList[0];;
		}

		#endregion


        /// <summary>
        /// 获取所有服务
        /// </summary>
        /// <returns>服务集合</returns>
        public IList<MyServe> GetByAllServe()
        {
            IList<MyServe> entity = (IList<MyServe>)CacheUtility.Get(_cacheKey, new string[] { "GetByAllServe" });
            if (entity == null)
            {
                entity = dal.GetByAllServe();
                CacheUtility.Add(entity,_cacheKey,new string[]{"GetByAllServe"});
            }
            return entity;
        }

        /// <summary>
        /// 通过Key获取数据
        /// </summary>
        /// <param name="ServeKey"></param>
        /// <returns></returns>
        public MyServe GetByKey(string ServeKey)
        {
            MyServe entity = (MyServe)CacheUtility.Get(_cacheKey, new string[] { ServeKey, "GetByKey" });
            if (entity == null)
            {
                entity = dal.GetByKey(ServeKey);
                CacheUtility.Add(entity, _cacheKey,new string[] { ServeKey, "GetByKey" });
            }
            return entity;
        }

    }
}
