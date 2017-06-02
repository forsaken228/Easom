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
    /// RoleService  服务处理类
    ///</summary>
    public class RoleService : IRole
    {
        private static readonly string _cacheKey = "Easom.Core.RoleService";
        private static readonly RoleDataAdapter dal = new RoleDataAdapter();

        #region IRole 成员

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(Role entity)
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
        public void Update(Role entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }


        public Role GetByName(string name)
        {
            Role entity = (Role)CacheUtility.Get(_cacheKey, new string[] { name });
            if (entity == null)
            {
                entity = dal.GetByName(name);
                CacheUtility.Add(entity, _cacheKey, new string[] { name });
            }
            return entity;
        }

        ///<summary>
        /// 读取一条记录
        ///</summary>
        public Role GetByID(int id)
        {
            Role entity = (Role)CacheUtility.Get(id, _cacheKey);
            if (entity == null)
            {
                entity = dal.GetByID(id);
                CacheUtility.Add(entity, id, _cacheKey);
            }
            return entity;
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public IList<Role> GetAllData()
        {
            IList<Role> entity = (IList<Role>)CacheUtility.Get(_cacheKey, new string[] { "GetAllData" });
            if (entity == null)
            {
                entity = dal.GetAllData();
                CacheUtility.Add(entity, _cacheKey, new string[] { "GetAllData" });
            }
            return entity;
        }

        /// <summary>
        /// GetByRoleID
        /// </summary>
        public IList<int> GetByRoleID(int id)
        {
            IList<int> entitylist = (IList<int>)CacheUtility.Get(_cacheKey, new string[] { id.ToString(), "GetByRoleID" });
            if (entitylist != null && entitylist.Count > 0)
            {
                return entitylist;
            }
            else
            {
                entitylist = dal.GetByRoleID(id);
                CacheUtility.Add(entitylist, _cacheKey, new string[] { id.ToString(), "GetByRoleID" });
            }
            return entitylist;
        }

        /// <summary>
        /// GetByUserID
        /// </summary>
        public IList<Role> GetByUserID(int id)
        {
            IList<Role> entitylist = (IList<Role>)CacheUtility.Get(_cacheKey, new string[] { id.ToString(), "GetByUserID" });
            if (entitylist != null && entitylist.Count > 0)
            {
                return entitylist;
            }
            else
            {
                entitylist = dal.GetByUserID(id);
                CacheUtility.Add(entitylist, _cacheKey, new string[] { id.ToString(), "GetByUserID" });
            }
            return entitylist;
        }

        /// <summary>
        /// DeleteByRoleID
        /// </summary>
        public bool DeleteByRoleID(int id)
        {
            if (dal.DeleteByRoleID(id))
            {
                CacheUtility.Delete(_cacheKey);
                return true;
            }
            return false;
        }



        ///<summary>
        /// 搜索记录
        ///</summary>
        public IList<Role> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
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
            return (IList<Role>)objList[0]; ;
        }

        #endregion

    }
}
