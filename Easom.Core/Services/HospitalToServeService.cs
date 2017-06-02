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
	/// HospitalToServeService  ��������
	///</summary>
	public class HospitalToServeService : IHospitalToServe
	{
		private static readonly string _cacheKey = "Easom.Core.HospitalToServeService";
		private static readonly HospitalToServeDataAdapter dal = new HospitalToServeDataAdapter();

		#region IHospitalToServe ��Ա

		///<summary>
		/// ����һ����¼
		///</summary>
		public int Insert(HospitalToServe entity)
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
		public void Update(HospitalToServe entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// ��ȡһ����¼
		///</summary>
		public HospitalToServe GetByID(int id)
		{
			HospitalToServe entity = (HospitalToServe)CacheUtility.Get(id, _cacheKey);
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
		public IList<HospitalToServe> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<HospitalToServe>)objList[0];;
		}

        /// <summary>
        /// ��ȡһ��ҽԺ������Ϣ
        /// </summary>
        /// <param name="hospital">ҽԺID</param>
        /// <param name="serverID">����ID</param>
        /// <returns>�÷���������Ϣ</returns>
        public HospitalToServe GetByHospitalID(int hospital,string serverKey)
        {
            HospitalToServe entity = (HospitalToServe)CacheUtility.Get(_cacheKey, new string[] { hospital.ToString(), serverKey.ToString(), "GetByHospitalID" });
            if (entity == null)
            {
                entity = dal.GetByHospitalID(hospital, serverKey);
                CacheUtility.Add(entity, _cacheKey, new string[] { hospital.ToString(), serverKey.ToString(), "GetByKey" });
            }
            return entity;
        }

		#endregion

	}
}
