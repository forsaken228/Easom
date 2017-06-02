// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-18
// ���ߣ�loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;
namespace Easom.Core.Services
{

	///<summary>
	/// JJ_AccountService  ��������
	///</summary>
	public class JJ_AccountService : IJJ_Account
	{
		private static readonly string _cacheKey = "Easom.Core.JJ_AccountService";
		private static readonly JJ_AccountDataAdapter dal = new JJ_AccountDataAdapter();

		#region IJJ_Account ��Ա

		///<summary>
		/// ����һ����¼
		///</summary>
		public int Insert(JJ_Account entity)
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
		public void Update(JJ_Account entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// ��ȡһ����¼
		///</summary>
		public JJ_Account GetByID(int id)
		{
			JJ_Account entity = (JJ_Account)CacheUtility.Get(id, _cacheKey);
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
        public IList<JJ_Account> GetBySectionID(int sectionID)
        {
            IList<JJ_Account> entity = (IList<JJ_Account>)CacheUtility.Get(_cacheKey, new string[] { sectionID.ToString(), "GetBySectionID" });
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
        public IList<JJ_Account> GetBySections(int userID, int hosipitalID)
        {
            IList<JJ_Account> entity = (IList<JJ_Account>)CacheUtility.Get(_cacheKey, new string[] { userID.ToString(), "GetBySections", hosipitalID.ToString() });
            if (entity == null)
            {
                entity = dal.GetBySections(userID, hosipitalID);
                CacheUtility.Add(entity, _cacheKey, new string[] { userID.ToString(), "GetBySections", hosipitalID.ToString() });
            }
            return entity;
        }

		///<summary>
		/// ������¼
		///</summary>
		public IList<JJ_Account> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<JJ_Account>)objList[0];;
		}

		#endregion

	}
}
