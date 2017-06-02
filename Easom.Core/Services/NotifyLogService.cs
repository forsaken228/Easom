// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-05-19
// ���ߣ�loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;

namespace Easom.Core.Services
{

	///<summary>
	/// NotifyLogService  ��������
	///</summary>
	public class NotifyLogService : INotifyLog
	{
		private static readonly string _cacheKey = "Easom.Core.NotifyLogService";
		private static readonly NotifyLogDataAdapter dal = new NotifyLogDataAdapter();

        #region INotifyLog ��Ա

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public int Insert(NotifyLog entity)
        {
            int r = dal.Insert(entity);
            CacheUtility.Delete(_cacheKey);
            return r;
        }

        ///<summary>
        /// ɾ��һ����¼
        ///</summary>
        public void Delete(int id)
        {
            dal.Delete(id);
            CacheUtility.Delete(_cacheKey);
        }

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public void Update(NotifyLog entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }
        /// <summary>
        /// ��ȡһ����¼
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
        /// ��ȡһ�����µļ�¼
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
        /// ��ȡ���м�¼
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
        /// ������¼
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
