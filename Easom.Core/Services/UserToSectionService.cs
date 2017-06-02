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
    /// UserToSectionService  服务处理类
    ///</summary>
    public class UserToSectionService : IUserToSection
    {
        private static readonly string _cacheKey = "Easom.Core.UserToSectionService";
        private static readonly UserToSectionDataAdapter dal = new UserToSectionDataAdapter();

        #region IUserToSection 成员

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(UserToSection entity)
        {
            int r = dal.Insert(entity);
            CacheUtility.Delete(_cacheKey);
            CacheUtility.Delete("Easom.Core.SectionService");
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
        public UserToSection GetBySectionIDAndUserID(int userID, int sectionID)
        {
            UserToSection entity = (UserToSection)CacheUtility.Get(_cacheKey, new string[] { userID.ToString(), sectionID.ToString(), "GetBySectionIDAndUserID" });
            if (entity == null)
            {
                entity = dal.GetBySectionIDAndUserID(userID, sectionID);
                CacheUtility.Add(entity, _cacheKey, new string[] { userID.ToString(), sectionID.ToString(), "GetBySectionIDAndUserID" });
            }
            return entity;
        }
        ///<summary>
        /// 删除一条记录
        ///</summary>
        public void DeleteBySectionIDAndUserID(int userID, int sectionID)
        {
            dal.DeleteBySectionIDAndUserID(userID, sectionID);
            CacheUtility.Delete(_cacheKey);
        }

        public bool UpdateUserToSection(int userID, int sectionID, bool isDefault)
        {
            if (dal.UpdateUserToSection(userID, sectionID, isDefault))
            {
                CacheUtility.Delete(_cacheKey);
                CacheUtility.Delete("Easom.Core.SectionService");
                return true;
            }
            return false;
        }


        ///<summary>
        /// 更新一条记录
        ///</summary>
        public void Update(UserToSection entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }

        ///<summary>
        /// 读取一条记录
        ///</summary>
        public IList<UserToSection> GetByUserID(int id)
        {
            IList<UserToSection> entity = (IList<UserToSection>)CacheUtility.Get(id, _cacheKey);
            if (entity == null)
            {
                entity = dal.GetByUserID(id);
                CacheUtility.Add(entity, id, _cacheKey);
            }
            return entity;
        }

        ///<summary>
        /// 搜索记录
        ///</summary>
        public IList<UserToSection> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
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
            return (IList<UserToSection>)objList[0]; ;
        }

        #endregion

    }
}
