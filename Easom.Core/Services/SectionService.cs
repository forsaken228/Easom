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
	/// SectionService  ��������
	///</summary>
	public class SectionService : ISection
	{
		private static readonly string _cacheKey = "Easom.Core.SectionService";
		private static readonly SectionDataAdapter dal = new SectionDataAdapter();

		#region ISection ��Ա

		///<summary>
		/// ����һ����¼
		///</summary>
		public int Insert(Section entity)
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
		public void Update(Section entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// ��ȡһ����¼
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
        /// ���ݵ�ǰ�û�ѡ��Ψһ��hosipital����ȡ��ǰҽԺѡ��Ŀ��ң����Զ�ѡ
        /// </summary>
        /// <param name="id">userid</param>
        /// <param name="isDefault">1����sectiontouserѡ��</param>
        /// <param name="hosipitalID">��ǰҽԺ��id</param>
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
		/// ������¼
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
