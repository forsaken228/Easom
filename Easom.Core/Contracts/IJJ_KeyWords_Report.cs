// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-18
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
using System.Data;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IJJ_KeyWords_Report  契约接口类
	///</summary>
	public interface IJJ_KeyWords_Report
	{
		long Insert(JJ_KeyWords_Report entity);

		void Delete(long id);

		void Update(JJ_KeyWords_Report entity);

		JJ_KeyWords_Report GetByID(int id);

		IList<JJ_KeyWords_Report> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        DataSet GetHourData(int accountID, System.DateTime beginTime, System.DateTime endTime);
        /// <summary>
        /// 根据名称获取当天已经上串某个关键词的所有数据之和
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="plan"></param>
        /// <param name="cell"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        JJ_KeyWords_Report GetbaiduHourSumByKeywordName(int accountID, System.DateTime begintime, System.DateTime endtime, string plan, string cell, string keyword);
        /// <summary>
        /// 根据关键词，计划名称，单元名称获取该关键词的消费情况
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="plan"></param>
        /// <param name="cell"></param>
        /// <param name="keyword"></param>
        /// <param name="timestate"></param>
        /// <returns></returns>
        JJ_KeyWords_Report GetByKeywordName(int accountID, System.DateTime begintime, System.DateTime endtime, string plan, string cell, string keyword, int timestate);

        /// <summary>
        /// 删除一个小时内上传的关键词
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="timestate"></param>
        void DeleteDataByHour(int accountID, System.DateTime begintime, System.DateTime endtime, int timestate);
	}
}
