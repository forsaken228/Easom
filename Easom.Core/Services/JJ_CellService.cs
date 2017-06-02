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
	/// JJ_CellService  ��������
	///</summary>
	public class JJ_CellService : IJJ_Cell
	{
		private static readonly string _cacheKey = "Easom.Core.JJ_CellService";
		private static readonly JJ_CellDataAdapter dal = new JJ_CellDataAdapter();

		#region IJJ_Cell ��Ա

		///<summary>
		/// ����һ����¼
		///</summary>
		public int Insert(JJ_Cell entity)
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
		public void Update(JJ_Cell entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// ��ȡһ����¼
		///</summary>
		public JJ_Cell GetByID(int id)
		{
			JJ_Cell entity = (JJ_Cell)CacheUtility.Get(id, _cacheKey);
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
		public IList<JJ_Cell> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<JJ_Cell>)objList[0];;
		}

		#endregion

	}
}
