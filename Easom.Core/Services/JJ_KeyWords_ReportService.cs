// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-18
// 作者：loskiv@gmail.com
      
using System;

using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;
using CHCMS.Utility;
using System.Data;
namespace Easom.Core.Services
{

	///<summary>
	/// JJ_KeyWords_ReportService  服务处理类
	///</summary>
	public class JJ_KeyWords_ReportService : IJJ_KeyWords_Report
	{
		private static readonly string _cacheKey = "Easom.Core.JJ_KeyWords_ReportService";
		private static readonly JJ_KeyWords_ReportDataAdapter dal = new JJ_KeyWords_ReportDataAdapter();

		#region IJJ_KeyWords_Report 成员

		///<summary>
		/// 插入一条记录
		///</summary>
		public long Insert(JJ_KeyWords_Report entity)
		{
			long r = dal.Insert(entity);
			CacheUtility.Delete(_cacheKey);
			return r;
		}

		///<summary>
		/// 删除一条记录
		///</summary>
		public void Delete(long id)
		{
			dal.Delete(id);
			CacheUtility.Delete(_cacheKey);
		}

        public void DeleteDataByHour(int accountID, System.DateTime beginTime, System.DateTime endTime, int timestate)
        {
            dal.DeleteDataByHour(accountID,beginTime,endTime,timestate);
            CacheUtility.Delete(_cacheKey);
        }

		///<summary>
		/// 更新一条记录
		///</summary>
		public void Update(JJ_KeyWords_Report entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}


        public DataSet GetHourData(int accountID, System.DateTime beginTime, System.DateTime endTime)
        {
            return dal.GetHourData(accountID, beginTime,endTime);
        }


        ///<summary>
        /// 读取一条记录
        ///</summary>
        public JJ_KeyWords_Report GetByID(int id)
        {
            JJ_KeyWords_Report entity = (JJ_KeyWords_Report)CacheUtility.Get(id, _cacheKey);
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
        public JJ_KeyWords_Report GetbaiduHourSumByKeywordName(int accountID, System.DateTime beginTime, System.DateTime endTime, string plan, string cell, string keyword)
		{

            return dal.GetbaiduHourSumByKeywordName(accountID, beginTime, endTime,plan,cell, keyword); 
		}


        ///<summary>
        /// 读取一条记录
        ///</summary>
        public JJ_KeyWords_Report GetByKeywordName(int accountID, System.DateTime beginTime, System.DateTime endTime, string plan, string cell, string keyword, int timestate) 
        {

            return dal.GetByKeywordName(accountID, beginTime, endTime, plan, cell, keyword, timestate);
        }

        

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<JJ_KeyWords_Report> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<JJ_KeyWords_Report>)objList[0];;
		}

		#endregion

	}
}
