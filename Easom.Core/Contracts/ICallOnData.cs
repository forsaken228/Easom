// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
using System;
namespace Easom.Core.Contracts
{

	///<summary>
	/// ICallOnData  契约接口类
	///</summary>
	public interface ICallOnData
	{
		int Insert(CallOnData entity);

		void Delete(int id);

		void Update(CallOnData entity);

		CallOnData GetByID(int id);

        /// <summary>
        /// 通过预约数据取出所有回访记录
        /// </summary>
        /// <param name="oderId">预约ID</param>
        /// <param name="state">提醒还是普通预约0，为提醒，1为普通</param>
        /// <returns>CallOnData集合</returns>
        IList<CallOnData> GetByOrderID(int orderId,int state);

        /// <summary>
        /// 取出该用户的所有提醒时间不超过今天的回访记录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="state">提醒还是普通预约0，为提醒，1为普通</param>
        /// <returns>CallOnData集合</returns>
        IList<CallOnData> GetByUserIDAndState(int userId, int state, int IsValid,string section);

        /// <summary>
        /// 取出该用户的所有回访记录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="state">提醒还是普通预约0，为提醒，1为普通</param>
        /// <returns>CallOnData集合</returns>
        IList<CallOnData> GetByUserIDAllCallOnData(int userId, int state, int IsValid, string section);

        ///// <summary>
        ///// 通过预约数据取出最新的回访记录
        ///// </summary>
        ///// <param name="orderId">预约ID</param>
        ///// <returns>CallOnData</returns>
        //CallOnData GetNewDataByOrderID(int orderId);

        /// <summary>
        /// 删除提醒
        /// </summary>
        void DeleteRamind(int ordersID, int callUserID, DateTime datetime);

        /// <summary>
        /// 通过ordersID删除所有提醒
        /// </summary>
        void DeleteRamind(int ordersID);

        /// <summary>
        /// 取出最新的数据
        /// </summary>
        /// <param name="ordersID">预约ID</param>
        /// <param name="state">提醒还是普通预约0，为提醒，1为普通</param>
        /// <param name="IsValid">0，为预约数据，1为个人资料库</param>
        /// <returns>CallOnData</returns>
        CallOnData GetLastDataByOrderID(int ordersID, int state);

		IList<CallOnData> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
