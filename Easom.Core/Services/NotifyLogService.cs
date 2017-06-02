// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-05-19
// 作者：loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;

namespace Easom.Core.Services
{

	///<summary>
	/// NotifyLogService  服务处理类
	///</summary>
	public class NotifyLogService : INotifyLog
	{
		private static readonly string _cacheKey = "Easom.Core.NotifyLogService";
		private static readonly NotifyLogDataAdapter dal = new NotifyLogDataAdapter();

        #region INotifyLog 成员

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(NotifyLog entity)
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
        public void Update(NotifyLog entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }
        /// <summary>
        /// 读取一条记录
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public NotifyLog GetByUserID(int userID)
        {
            NotifyLog entity = (NotifyLog)CacheUtility.Get(_cacheKey, new string[] { userID.ToString(), "GetByUserID" });
            if (entity == null)
            {
                entity = dal.GetByUserID(userID);
                CacheUtility.Add(entity, _cacheKey, new string[] { userID.ToString(), "GetByUserID" });
            }
            return entity;
        }

        /// <summary>
        /// 读取一条最新的记录
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public NotifyLog TopGetByUserID(int userID)
        {
            NotifyLog entity = (NotifyLog)CacheUtility.Get(_cacheKey, new string[] { userID.ToString(), "TopGetByUserID" });
            if (entity == null)
            {
                entity = dal.GetByUserID(userID);
                CacheUtility.Add(entity, _cacheKey, new string[] { userID.ToString(), "TopGetByUserID" });
            }
            return entity;
        }

        ///<summary>
        /// 读取所有记录
        ///</summary>
        public IList<NotifyLog> GetByID(int id)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { id.ToString(), "GetByID" });
            if (objList == null)
            {
                objList = new object[2];
                objList[0] = dal.GetByID(id);
                CacheUtility.Add(objList, _cacheKey, new string[] { id.ToString(), "GetByID" });
            }
            return (IList<NotifyLog>)objList[0]; ;
        }

        ///<summary>
        /// 搜索记录
        ///</summary>
        public IList<NotifyLog> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
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
            return (IList<NotifyLog>)objList[0]; ;
        }

        #endregion
    }
}
