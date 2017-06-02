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
	/// UserToHospitalService  服务处理类
	///</summary>
	public class UserToHospitalService : IUserToHospital
	{
		private static readonly string _cacheKey = "Easom.Core.UserToHospitalService";
		private static readonly UserToHospitalDataAdapter dal = new UserToHospitalDataAdapter();

		#region IUserToHospital 成员

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(UserToHospital entity)
		{
			int r = dal.Insert(entity);
			CacheUtility.Delete(_cacheKey);
            CacheUtility.Delete("Easom.Core.HospitalService");
			return r;
		}

		///<summary>
		/// 删除一条记录
		///</summary>
		public void Delete(int userID,int hospitalID)
		{
            dal.Delete(userID, hospitalID);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 更新一条记录
		///</summary>
		public void Update(UserToHospital entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 读取一条记录
		///</summary>
		public UserToHospital GetByID(int id)
		{
			UserToHospital entity = (UserToHospital)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

        ///<summary>
		/// 读取一条记录
		///</summary>
        public UserToHospital GetByUserIDAndHospitalID(int userId, int hospitalID)
		{
            UserToHospital entity = (UserToHospital)CacheUtility.Get(_cacheKey, new string[] { userId.ToString(), hospitalID.ToString() });
			if (entity == null)
			{
                entity = dal.GetByUserIDAndHospitalID(userId, hospitalID);
				CacheUtility.Add(entity, _cacheKey,new string[] { userId.ToString(), hospitalID.ToString() });
			}
			return entity;
		}

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<UserToHospital> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<UserToHospital>)objList[0];;
		}

		#endregion

	}
}
