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
    /// HospitalService  ��������
    ///</summary>
    public class HospitalService : IHospital
    {
        private static readonly string _cacheKey = "Easom.Core.HospitalService";
        private static readonly HospitalDataAdapter dal = new HospitalDataAdapter();

        #region IHospital ��Ա

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public int Insert(Hospital entity)
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
        public void Update(Hospital entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }

        ///<summary>
        /// ��ȡһ����¼
        ///</summary>
        public Hospital GetByID(int id)
        {
            Hospital entity = (Hospital)CacheUtility.Get(id, _cacheKey);
            if (entity == null)
            {
                entity = dal.GetByID(id);
                CacheUtility.Add(entity, id, _cacheKey);
            }
            return entity;
        }


        public bool InsertUserToHospital(int userID, int hospitalID, bool isDefault)
        {
            if (dal.InsertUserToHospital(userID, hospitalID, isDefault))
            {
                CacheUtility.Delete(_cacheKey);
                return true;
            }
            return false;
        }

        public IList<Hospital> GetByALLHospital()
        {
            IList<Hospital> entity = (IList<Hospital>)CacheUtility.Get(_cacheKey, new string[] { "GetByALLHospital" });
            if (entity == null)
            {
                entity = dal.GetByALLHospital();
                CacheUtility.Add(entity, _cacheKey, new string[] { "GetByALLHospital" });
            }
            return entity;
        }

        public IList<Hospital> GetByUserID(int id, int isDefault)
        {
            IList<Hospital> entity = (IList<Hospital>)CacheUtility.Get(_cacheKey, new string[] { id.ToString(), isDefault.ToString() });
            if (entity == null)
            {
                entity = dal.GetByUserID(id, isDefault);
                CacheUtility.Add(entity, _cacheKey, new string[] { id.ToString(), isDefault.ToString() });
            }
            return entity;
        }



        ///<summary>
        /// ������¼
        ///</summary>
        public IList<Hospital> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
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
            return (IList<Hospital>)objList[0]; ;
        }

        #endregion

    }
}
