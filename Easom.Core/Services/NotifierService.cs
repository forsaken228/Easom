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
    /// NotifierService  ��������
    ///</summary>
    public class NotifierService : INotifier
    {
        private static readonly string _cacheKey = "Easom.Core.NotifierService";
        private static readonly NotifierDataAdapter dal = new NotifierDataAdapter();

        #region INotifier ��Ա

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public int Insert(Notifier entity)
        {
            int r = dal.Insert(entity);
            CacheUtility.Delete(_cacheKey);
            return r;
        }

        ///<summary>
        /// ɾ��һ����¼
        ///</summary>
        public bool Delete(int id)
        {
            if (dal.Delete(id))
            {
                CacheUtility.Delete(_cacheKey);
                return true;
            }
            return false;
        }

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public bool Update(Notifier entity)
        {
            if (dal.Update(entity))
            {
                CacheUtility.Delete(_cacheKey);
                return true;
            }
            return false;
        }

        ///<summary>
        /// ��ȡһ����¼
        ///</summary>
        public Notifier GetByID(int id)
        {
            Notifier entity = (Notifier)CacheUtility.Get(id, _cacheKey);
            if (entity == null)
            {
                entity = dal.GetByID(id);
                CacheUtility.Add(entity, id, _cacheKey);
            }
            return entity;
        }

        ///<summary>
        /// ������¼
        ///</summary>
        public IList<Notifier> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
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
            return (IList<Notifier>)objList[0]; ;
        }

        ///<summary>
        /// ������¼
        ///</summary>
        public IList<Notifier> NotifierNotifyLogSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            return dal.NotifierNotifyLogSearch(out pageCount, pageIndex, pageSize, where, orderField, isDesc);
        }

        /// <summary>
        /// ��ȡ������Ҫ����ŵĿͻ�
        /// </summary>
        /// <returns></returns>
        public IList<Notifier> GetAllData()
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey);
            if (objList == null)
            {
                objList = new object[1];
                objList[0] = dal.GetAllData();
                CacheUtility.Add(objList, _cacheKey);
            }
            return (IList<Notifier>)objList[0];
        }
        #endregion

    }
}
