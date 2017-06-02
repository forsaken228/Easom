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
    /// UserInfoService  ��������
    ///</summary>
    public class UserInfoService : IUserInfo
    {
        private static readonly string _cacheKey = "Easom.Core.UserInfoService";
        private static readonly UserInfoDataAdapter dal = new UserInfoDataAdapter();

        #region IUserInfo ��Ա

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public int Insert(UserInfo entity)
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
        public void Update(UserInfo entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }

        public UserInfo GetByName(string name)
        {
            UserInfo entity = (UserInfo)CacheUtility.Get(_cacheKey, new string[] { name });
            if (entity == null)
            {
                entity = dal.GetByName(name);
                CacheUtility.Add(entity, _cacheKey, new string[] { name });
            }
            return entity;
        }

        ///<summary>
        /// ��ȡһ����¼
        ///</summary>
        public UserInfo GetByID(int id)
        {
            UserInfo entity = (UserInfo)CacheUtility.Get(id, _cacheKey);
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
        public IList<UserInfo> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
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
            return (IList<UserInfo>)objList[0]; ;
        }


        ///<summary>
        /// ������¼
        ///</summary>
        public IList<UserInfo> ViewSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderField.ToString(), isDesc.ToString() });
            if (objList == null)
            {
                objList = new object[2];
                objList[0] = dal.ViewSearch(out pageCount, pageIndex, pageSize, where, orderField, isDesc);
                objList[1] = pageCount;
                CacheUtility.Add(objList, _cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderField.ToString(), isDesc.ToString() });
            }
            pageCount = Convert.ToInt32(objList[1]);
            return (IList<UserInfo>)objList[0]; ;
        }

        #endregion

        /// <summary>
        /// ���µ�ǰҽԺ
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hospitalID"></param>
        /// <returns></returns>
        public bool UpdateCurrentHospital(int userID, int hospitalID)
        {
            if (dal.UpdateCurrentHospital(userID, hospitalID))
            {
                CacheUtility.Delete(_cacheKey);
                return true;
            }
            return false;
        }

        /// <summary>
        /// ���µ�ǰҽԺ
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hospitalID"></param>
        /// <returns></returns>
        public bool UpdateUserToSection(int userID, int sectionid)
        {
            if (dal.UpdateUserToSection(userID, sectionid))
            {
                CacheUtility.Delete(_cacheKey);
                return true;
            }
            return false;
        }

        public IList<string> GetByNameUserID(int id)
        {
            IList<string> entity = (IList<string>)CacheUtility.Get(_cacheKey, new string[] { id.ToString(), "GetByNameUserID" });
            if (entity == null)
            {
                entity = dal.GetByNameUserID(id);
                CacheUtility.Add(entity, _cacheKey, new string[] { id.ToString(), "GetByNameUserID" });
            }
            return entity;
        }

        public int GetIsDefaluteByID(int id)
        {
            int r = dal.GetIsDefaluteByID(id);
            CacheUtility.Delete(_cacheKey);
            return r;
        }

        public bool DeleteByUserID(int id)
        {
            if (dal.DeleteByUserID(id))
            {
                CacheUtility.Delete(_cacheKey);
                return true;
            }
            return false;
        }




        /// <summary>
        /// ͨ����ɫ�Ϳ��һ�ȡ����
        /// </summary>
        /// <param name="roleID">��ɫID</param>
        /// <param name="sectionID">����ID</param>
        /// <returns>�û�����</returns>
        public IList<UserInfo> GetByRoleAndSection(int roleID, int sectionID)
        {
            IList<UserInfo> entity = (IList<UserInfo>)CacheUtility.Get(_cacheKey, new string[] { roleID.ToString(), sectionID.ToString(), "GetByRoleAndSection" });
            if (entity == null)
            {
                entity = dal.GetByRoleAndSection(roleID, sectionID);
                CacheUtility.Add(entity, _cacheKey, new string[] { roleID.ToString(), sectionID.ToString(), "GetByRoleAndSection" });
            }
            return entity;
        }


        public IList<UserInfo> UserInfoNotInNotifierSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderFields.ToString(), isDesc.ToString(), "UserToRoleToHospitalSearch" });
            if (objList == null)
            {
                objList = new object[2];
                objList[0] = dal.UserInfoNotInNotifierSearch(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
                objList[1] = pageCount;
                CacheUtility.Add(objList, _cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderFields.ToString(), isDesc.ToString(), "UserToRoleToHospitalSearch" });
            }
            pageCount = Convert.ToInt32(objList[1]);
            return (IList<UserInfo>)objList[0];
        }
    }
}
