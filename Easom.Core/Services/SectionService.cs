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
	/// SectionService  服务处理类
	///</summary>
	public class SectionService : ISection
	{
		private static readonly string _cacheKey = "Easom.Core.SectionService";
		private static readonly SectionDataAdapter dal = new SectionDataAdapter();

		#region ISection 成员

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(Section entity)
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
		public void Update(Section entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 读取一条记录
		///</summary>
		public Section GetByID(int id)
		{
			Section entity = (Section)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

        public IList<Section> GetByHospitalID(int id) 
        {
            IList<Section> entity = (IList<Section>)CacheUtility.Get(_cacheKey, new string[] { id.ToString(), "GetByHospitalID" });
            if (entity == null)
            {
                entity = dal.GetByHospitalID(id);
                CacheUtility.Add(entity, _cacheKey, new string[] { id.ToString(), "GetByHospitalID" });
            }
            return entity;
        }

        /// <summary>
        /// 根据当前用户选择唯一的hosipital，获取当前医院选择的科室，可以多选
        /// </summary>
        /// <param name="id">userid</param>
        /// <param name="isDefault">1代表sectiontouser选中</param>
        /// <param name="hosipitalID">当前医院的id</param>
        /// <returns></returns>
        public IList<Section> GetByUserID(int id, int isDefault,int hosipitalID )
        {
            IList<Section> entity = (IList<Section>)CacheUtility.Get(_cacheKey, new string[] { id.ToString(), "GetByUserIDSection", hosipitalID.ToString(), isDefault.ToString()});
            if (entity == null)
            {
                entity = dal.GetByUserID(id, isDefault, hosipitalID );
                CacheUtility.Add(entity, _cacheKey, new string[] { id.ToString(), "GetByUserIDSection", hosipitalID.ToString(), isDefault.ToString() });
            }
            return entity;
        }



        public bool InsertUserToSection(int userID, int hospitalID, bool isDefault)
        {
            if (dal.InsertUserToSection(userID, hospitalID, isDefault))
            {
                CacheUtility.Delete(_cacheKey);
                return true;
            }
            return false;
        }
		///<summary>
		/// 搜索记录
		///</summary>
		public IList<Section> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<Section>)objList[0];;
		}

		#endregion

	}
}
