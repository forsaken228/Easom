// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com

using System;

using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;
using CHCMS.Utility;
namespace Easom.Core.Services
{

    ///<summary>
    /// CallOnDataService  ��������
    ///</summary>
    public class CallOnDataService : ICallOnData
    {
        private static readonly string _cacheKey = "Easom.Core.CallOnDataService";
        private static readonly CallOnDataDataAdapter dal = new CallOnDataDataAdapter();

        #region ICallOnData ��Ա

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public int Insert(CallOnData entity)
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

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns>CallOnData</returns>
        public void DeleteRamind(int ordersID, int callUserID, DateTime datetime)
        {
            dal.DeleteRamind(ordersID, callUserID, datetime);
            CacheUtility.Delete(_cacheKey);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns>CallOnData</returns>
        public void DeleteRamind(int ordersID)
        {
            dal.DeleteRamind(ordersID);
            CacheUtility.Delete(_cacheKey);
        }

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public void Update(CallOnData entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }

        ///<summary>
        /// ��ȡһ����¼
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
        /// ͨ��ԤԼ����ȡ�����лطü�¼
        /// </summary>
        /// <param name="oderId">ԤԼID</param>
        /// <returns>CallOnData����</returns>
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
        /// �����û�ȡ�����µ�����
        /// </summary>
        /// <param name="UserID">�û�ID</param>
        /// <param name="state">���ѻ�����ͨԤԼ0��Ϊ���ѣ�1Ϊ��ͨ</param>
        /// <returns>CallOnData���ͼ���</returns>
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
        /// ȡ�����µ�����
        /// </summary>
        /// <param name="ordersID">ԤԼID</param>
        /// <param name="state">���ѻ�����ͨԤԼ0��Ϊ���ѣ�1Ϊ��ͨ</param>
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
        /// ͨ��ԤԼ����ȡ�����µĻطü�¼
        /// </summary>
        /// <param name="orderId">ԤԼID</param>
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
        /// ������¼
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
