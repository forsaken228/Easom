// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-18
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
using System.Data;
using System;

namespace Easom.Core.Contracts
{

	///<summary>
	/// IJJ_Swt_Report  契约接口类
	///</summary>
	public interface IJJ_Swt_Report
	{
		int Insert(JJ_Swt_Report entity);

		void Delete(int id);

		void Update(JJ_Swt_Report entity);

		JJ_Swt_Report GetByID(int id);

        IList<JJ_Swt_Report> GetAllJJ_Swt_Report(int hospitalID);

        IList<JJ_Swt_Report> GetAllJJ_Swt_Report(int hospitalID, int sectionID);

		IList<JJ_Swt_Report> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);


        /// <summary>
        /// 对话统计报表
        /// </summary>
        /// <param name="SectionID">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet</returns>
        DataSet GetJJ_Swt_ReportHospital(DateTime beginTime, DateTime endTime, int hospital);


        /// <summary>
        /// 获取商务通各个字段聚合列表
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="hospital">医院ID</param>
        /// <param name="SectionID">科室ID</param>
        /// <param name="tableSort">字段类别</param>
        /// <param name="chatState">对话种类</param>
        /// <returns>DataSet</returns>
        DataSet GetSwtTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, int tableSort, int chatState);

        /// <summary>
        /// 百度计划分析
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="hospital">医院ID</param>
        /// <param name="SectionID">科室ID</param>
        /// <param name="tableSort">字段类别</param>
        /// <param name="chatState">对话种类</param>
        /// <returns>DataSet</returns>
        DataSet GetBaiduPlanTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, string tableSort, string chatState);

        /// <summary>
        /// 百度关键词分析
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="hospital">医院ID</param>
        /// <param name="SectionID">科室ID</param>
        /// <param name="tableSort">字段类别</param>
        /// <param name="chatState">对话种类</param>
        /// <returns>DataSet</returns>
        DataSet GetBaiduKeyWordsTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, string tableSort, string chatState);

	}
}
