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
    /// HospitalWebsiteService  ��������
    ///</summary>
    public class HospitalWebsiteService : IHospitalWebsite
    {
        private static readonly string _cacheKey = "Easom.Core.HospitalWebsiteService";
        private static readonly HospitalWebsiteDataAdapter dal = new HospitalWebsiteDataAdapter();

        #region IHospitalWebsite ��Ա

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public int Insert(HospitalWebsite entity)
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
        public void Update(HospitalWebsite entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }

        ///<summary>
        /// ��ȡһ����¼
        ///</summary>
        public HospitalWebsite GetByID(int id)
        {
            HospitalWebsite entity = (HospitalWebsite)CacheUtility.Get(id, _cacheKey);
            if (entity == null)
            {
                entity = dal.GetByID(id);
                CacheUtility.Add(entity, id, _cacheKey);
            }
            return entity;
        }

        public HospitalWebsite GetByURL(string url)
        {
            HospitalWebsite entity = (HospitalWebsite)CacheUtility.Get(_cacheKey, new string[] { url });
            if (entity == null)
            {
                entity = dal.GetByURL(url);
                CacheUtility.Add(entity, _cacheKey, new string[] { url });
            }
            return entity;
        }

        /// <summary>
        /// ��ȡ���п����������վ
        /// </summary>
        /// <param name="sectionID">����ID</param>
        /// <returns>ҽԺ��վ</returns>
        public IList<HospitalWebsite> GetAllHospitalWebsite(int sectionID,int siteType)
        {
            IList<HospitalWebsite> entity = (IList<HospitalWebsite>)CacheUtility.Get(_cacheKey, new string[] { sectionID.ToString(), "GetAllHospitalWebsite" });
            if (entity == null)
            {
                entity = dal.GetAllHospitalWebsite(sectionID, siteType);
                CacheUtility.Add(entity, _cacheKey, new string[] { sectionID.ToString(), "GetAllHospitalWebsite" });
            }
            return entity;
        }

        /// <summary>
        /// ��ȡ��ǰ�û�ѡ��ҽԺ�����п��ҵ�ԤԼ�Һ���վ
        /// </summary>
        /// <param name="sectionID">����ID</param>
        /// <returns>ҽ������</returns>
        public IList<HospitalWebsite> GetBySections(int userID, int hosipitalID)
        {
            IList<HospitalWebsite> entity = (IList<HospitalWebsite>)CacheUtility.Get(_cacheKey, new string[] { userID.ToString(), "GetBySectionsWebsite" });
            if (entity == null)
            {
                entity = dal.GetBySections(userID, hosipitalID);
                CacheUtility.Add(entity, _cacheKey, new string[] { userID.ToString(), "GetBySectionsWebsite" });
            }
            return entity;
        }

        ///<summary>
        /// ������¼
        ///</summary>
        public IList<HospitalWebsite> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
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
            return (IList<HospitalWebsite>)objList[0]; ;
        }

        #endregion

    }
}
