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
	/// AuthorityService  服务处理类
	///</summary>
	public class AuthorityService : IAuthority
	{
		private static readonly string _cacheKey = "Easom.Core.AuthorityService";
		private static readonly AuthorityDataAdapter dal = new AuthorityDataAdapter();

		#region IAuthority 成员

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(Authority entity)
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
        /// 删除一条记录
        ///</summary>
        public void DeleteByParentID(int ParentID)
        {
            dal.DeleteByParentID(ParentID);
            CacheUtility.Delete(_cacheKey);
        }

		///<summary>
		/// 更新一条记录
		///</summary>
		public void Update(Authority entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 读取一条记录
		///</summary>
		public Authority GetByID(int id)
		{
			Authority entity = (Authority)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

        /// <summary>
        /// GetByAuthorityKey
        /// </summary>
        /// <param name="authorityKey"></param>
        /// <returns></returns>
        public Authority GetByAuthorityKey(string authorityKey)
        {
            Authority entity = (Authority)CacheUtility.Get(_cacheKey, new string[] { authorityKey });
            if (entity == null)
            {
                entity = dal.GetByAuthorityKey(authorityKey);
                CacheUtility.Add(entity, _cacheKey, new string[] { authorityKey });
            }
            return entity;
        }

        /// <summary>
        /// GetAllAuthor
        /// </summary>
        /// <returns></returns>
        public IList<Authority> GetAllAuthor()
        {
            IList<Authority> entity = (IList<Authority>)CacheUtility.Get(_cacheKey, new string[] { "GetAllAuthor" });
            if (entity != null && entity.Count > 0)
            {
                return entity;
            }
            else
            {
                entity = dal.GetAllAuthor();
                CacheUtility.Add(entity, _cacheKey, new string[] { "GetAllAuthor" });
            }
            return entity;
        }

        /// <summary>
        /// AddAuthors
        /// </summary>
        public bool AddAuthors(int roleID, int authorID)
        {
            if (dal.AddAuthors(roleID, authorID))
            {
                CacheUtility.Delete(_cacheKey);
                return true;
            }
            return false;
        }

        /// <summary>
        /// GetByRoleID
        /// </summary>
        public IList<Authority> GetByRoleID(int id)
        {
            IList<Authority> entitylist = (IList<Authority>)CacheUtility.Get(_cacheKey, new string[] { id.ToString(), "GetByRoleID" });
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

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<Authority> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<Authority>)objList[0];;
		}

		#endregion

	}
}
