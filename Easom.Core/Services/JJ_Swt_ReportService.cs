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
	/// JJ_Swt_ReportService  服务处理类
	///</summary>
	public class JJ_Swt_ReportService : IJJ_Swt_Report
	{
		private static readonly string _cacheKey = "Easom.Core.JJ_Swt_ReportService";
		private static readonly JJ_Swt_ReportDataAdapter dal = new JJ_Swt_ReportDataAdapter();

		#region IJJ_Swt_Report 成员

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(JJ_Swt_Report entity)
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
		public void Update(JJ_Swt_Report entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 读取一条记录
		///</summary>
		public JJ_Swt_Report GetByID(int id)
		{
			JJ_Swt_Report entity = (JJ_Swt_Report)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

        /// <summary>
        /// 获取所有商务通
        /// </summary>
        /// <returns></returns>
        public IList<JJ_Swt_Report> GetAllJJ_Swt_Report(int hospitalID)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { hospitalID.ToString(), "GetAllJJ_Swt_Report" });
            if (objList == null)
            {
                objList = new object[1];
                objList[0] = dal.GetAllJJ_Swt_Report(hospitalID);
                CacheUtility.Add(objList, _cacheKey, new string[] { hospitalID.ToString(), "GetAllJJ_Swt_Report" });
            }
            return (IList<JJ_Swt_Report>)objList[0];
        }
        /// <summary>
        /// 返回通过sectionID获取商务通
        /// </summary>
        /// <param name="prentid"></param>
        /// <returns></returns>
        public IList<JJ_Swt_Report> GetAllJJ_Swt_Report(int hospitalID, int sectionID)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { hospitalID.ToString(), sectionID.ToString(), "GetAllJJ_Swt_Report" });
            if (objList == null)
            {
                objList = new object[1];
                objList[0] = dal.GetAllJJ_Swt_Report(hospitalID, sectionID);
                CacheUtility.Add(objList, _cacheKey, new string[] { hospitalID.ToString(), sectionID.ToString(), "GetAllJJ_Swt_Report" });
            }
            return (IList<JJ_Swt_Report>)objList[0];
        }

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<JJ_Swt_Report> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<JJ_Swt_Report>)objList[0];;
		}



        /// <summary>
        /// 对话统计报表
        /// </summary>
        /// <param name="SectionID">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet</returns>
        public DataSet GetJJ_Swt_ReportHospital(DateTime beginTime, DateTime endTime, int hospital)
        {
            return dal.GetJJ_Swt_ReportHospital(beginTime, endTime, hospital);
        }

        public DataSet GetSwtTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string SectionID, int tableSort, int chatState)
        {
            return dal.GetSwtTableSortStatistics(beginTime, endTime, hospital,SectionID,tableSort,chatState);
        }
        #endregion


        public DataSet GetBaiduPlanTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, string tableSort, string chatState)
        {
            return dal.GetBaiduPlanTableSortStatistics(beginTime, endTime, hospital, Section, tableSort, chatState);
        }


        public DataSet GetBaiduKeyWordsTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, string tableSort, string chatState)
        {
            return dal.GetBaiduKeyWordsTableSortStatistics(beginTime, endTime, hospital, Section, tableSort, chatState);
        }
    }
}
