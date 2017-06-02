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
	/// DoctorService  ��������
	///</summary>
	public class DoctorService : IDoctor
	{
		private static readonly string _cacheKey = "Easom.Core.DoctorService";
		private static readonly DoctorDataAdapter dal = new DoctorDataAdapter();

		#region IDoctor ��Ա

		///<summary>
		/// ����һ����¼
		///</summary>
		public int Insert(Doctor entity)
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
		public void Update(Doctor entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// ��ȡһ����¼
		///</summary>
		public Doctor GetByID(int id)
		{
			Doctor entity = (Doctor)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

        /// <summary>
        /// ��ȡ�ÿ��ҵ�����ҽ��
        /// </summary>
        /// <param name="sectionID">����ID</param>
        /// <returns>ҽ������</returns>
        public IList<Doctor> GetBySectionID(int sectionID)
        {
            IList<Doctor> entity = (IList<Doctor>)CacheUtility.Get(_cacheKey, new string[] { sectionID.ToString(), "GetBySectionID" });
            if (entity == null)
            {
                entity = dal.GetBySectionID(sectionID);
                CacheUtility.Add(entity, _cacheKey, new string[] { sectionID.ToString(), "GetBySectionID" });
            }
            return entity;
        }

        /// <summary>
        /// ��ȡ��ǰ�û�ѡ��ҽԺ�����п���
        /// </summary>
        /// <param name="sectionID">����ID</param>
        /// <returns>ҽ������</returns>
        public IList<Doctor> GetBySections(int userID, int hosipitalID) 
        {
            IList<Doctor> entity = (IList<Doctor>)CacheUtility.Get(_cacheKey, new string[] { userID.ToString(), "GetBySections" });
            if (entity == null)
            {
                entity = dal.GetBySections(userID,  hosipitalID) ;
                CacheUtility.Add(entity, _cacheKey, new string[] { userID.ToString(), "GetBySections" });
            }
            return entity;
        }

		///<summary>
		/// ������¼
		///</summary>
		public IList<Doctor> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<Doctor>)objList[0];;
		}

		#endregion

	}
}
