// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-21
// 作者：loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;

namespace Easom.Core.Services
{

	///<summary>
	/// JJ_KeyWordsService  服务处理类
	///</summary>
	public class JJ_KeyWordsService : IJJ_KeyWords
	{
		private static readonly string _cacheKey = "Easom.Core.JJ_KeyWordsService";
		private static readonly JJ_KeyWordsDataAdapter dal = new JJ_KeyWordsDataAdapter();

		#region IJJ_KeyWords 成员

		///<summary>
		/// 插入一条记录
		///</summary>
		public long Insert(JJ_KeyWords entity)
		{
			long r = dal.Insert(entity);
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

        public int DeleteByAccountID(int accountID)
        {

            int returnData= dal.DeleteByAccountID(accountID);
            CacheUtility.Delete(_cacheKey);
            return returnData;
        }

		///<summary>
		/// 更新一条记录
		///</summary>
		public void Update(JJ_KeyWords entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 读取一条记录
		///</summary>
		public JJ_KeyWords GetByID(int id)
		{
			JJ_KeyWords entity = (JJ_KeyWords)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<JJ_KeyWords> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<JJ_KeyWords>)objList[0];;
		}

		#endregion

	}
}
