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
    /// CallOnDataService  服务处理类
    ///</summary>
    public class CallOnDataService : ICallOnData
    {
        private static readonly string _cacheKey = "Easom.Core.CallOnDataService";
        private static readonly CallOnDataDataAdapter dal = new CallOnDataDataAdapter();

        #region ICallOnData 成员

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(CallOnData entity)
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

        /// <summary>
        /// 删除提醒
        /// </summary>
        /// <returns>CallOnData</returns>
        public void DeleteRamind(int ordersID, int callUserID, DateTime datetime)
        {
            dal.DeleteRamind(ordersID, callUserID, datetime);
            CacheUtility.Delete(_cacheKey);
        }

        /// <summary>
        /// 删除提醒
        /// </summary>
        /// <returns>CallOnData</returns>
        public void DeleteRamind(int ordersID)
        {
            dal.DeleteRamind(ordersID);
            CacheUtility.Delete(_cacheKey);
        }

        ///<summary>
        /// 更新一条记录
        ///</summary>
        public void Update(CallOnData entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }

        ///<summary>
        /// 读取一条记录
        ///</summary>
        public CallOnData GetByID(int id)
        {
            CallOnData entity = (CallOnData)CacheUtility.Get(id, _cacheKey);
            if (entity == null)
            {
                entity = dal.GetByID(id);
                CacheUtility.Add(entity, id, _cacheKey);
            }
            return entity;
        }
        /// <summary>
        /// 通过预约数据取出所有回访记录
        /// </summary>
        /// <param name="oderId">预约ID</param>
        /// <returns>CallOnData集合</returns>
        public IList<CallOnData> GetByOrderID(int orderId, int state)
        {
            IList<CallOnData> objList = (IList<CallOnData>)CacheUtility.Get(_cacheKey, new string[] { orderId.ToString(), state.ToString(), "GetByOrderID" });
            if (objList == null)
            {
                objList = dal.GetByOrderID(orderId, state);
                CacheUtility.Add(objList, _cacheKey, new string[] { orderId.ToString(), state.ToString(), "GetByOrderID" });
            }
            return objList;
        }

          /// <summary>
        /// 根据用户取出最新的数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="state">提醒还是普通预约0，为提醒，1为普通</param>
        /// <returns>CallOnData泛型集合</returns>
        public IList<CallOnData> GetByUserIDAndState(int userId, int state, int IsValid,string section)
        {
            IList<CallOnData> objList = (IList<CallOnData>)CacheUtility.Get(_cacheKey, new string[] { userId.ToString(), state.ToString(), IsValid.ToString(), "GetByUseridAndState" });
            if (objList == null)
            {
                objList = dal.GetByUserIDAndState(userId, state,IsValid,section);
                CacheUtility.Add(objList, _cacheKey, new string[] { userId.ToString(), state.ToString(), "GetByUseridAndState" });
            }
            return objList;
        }

        /// <summary>
        /// 取出最新的数据
        /// </summary>
        /// <param name="ordersID">预约ID</param>
        /// <param name="state">提醒还是普通预约0，为提醒，1为普通</param>
        /// <returns>CallOnData</returns>
        public CallOnData GetLastDataByOrderID(int ordersID, int state)
        {
            CallOnData objList = (CallOnData)CacheUtility.Get(_cacheKey, new string[] { ordersID.ToString(), state.ToString(),"GetLastDataByOrderID" });
            if (objList == null)
            {
                objList = dal.GetLastDataByOrderID(ordersID, state);
                CacheUtility.Add(objList, _cacheKey, new string[] { ordersID.ToString(), state.ToString(), "GetLastDataByOrderID" });
            }
            return objList;
        }

        /// <summary>
        /// 通过预约数据取出最新的回访记录
        /// </summary>
        /// <param name="orderId">预约ID</param>
        /// <returns>CallOnData</returns>
        public CallOnData GetNewDataByOrderID(int orderId)
        {
            CallOnData entity = (CallOnData)CacheUtility.Get(_cacheKey, new string[] { orderId.ToString(), "GetNewDataByOrderID" });
            if (entity == null)
            {
                entity = dal.GetNewDataByOrderID(orderId);
                CacheUtility.Add(entity, _cacheKey, new string[] { orderId.ToString(), "GetNewDataByOrderID" });
            }
            return entity;
        }

        ///<summary>
        /// 搜索记录
        ///</summary>
        public IList<CallOnData> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
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
            return (IList<CallOnData>)objList[0];
        }

        #endregion


        public IList<CallOnData> GetByUserIDAllCallOnData(int userId, int state, int IsValid, string section)
        {
            IList<CallOnData> objList = (IList<CallOnData>)CacheUtility.Get(_cacheKey, new string[] { userId.ToString(), state.ToString(), IsValid.ToString(), "GetByUserIDAllCallOnData" });
            if (objList == null)
            {
                objList = dal.GetByUserIDAllCallOnData(userId, state, IsValid, section);
                CacheUtility.Add(objList, _cacheKey, new string[] { userId.ToString(), state.ToString(), "GetByUserIDAllCallOnData" });
            }
            return objList;
        }
    }
}
