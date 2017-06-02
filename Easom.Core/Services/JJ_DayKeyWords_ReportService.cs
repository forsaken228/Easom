// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-25
// ���ߣ�loskiv@gmail.com
      
using System;
using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;
using CHCMS.Utility;
using System.Data;
namespace Easom.Core.Services
{

	///<summary>
	/// JJ_DayKeyWords_ReportService  ��������
	///</summary>
	public class JJ_DayKeyWords_ReportService : IJJ_DayKeyWords_Report
	{
		private static readonly string _cacheKey = "Easom.Core.JJ_DayKeyWords_ReportService";
		private static readonly JJ_DayKeyWords_ReportDataAdapter dal = new JJ_DayKeyWords_ReportDataAdapter();

		#region IJJ_DayKeyWords_Report ��Ա

		///<summary>
		/// ����һ����¼
		///</summary>
		public long Insert(JJ_DayKeyWords_Report entity)
		{
			long r = dal.Insert(entity);
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
		public void Update(JJ_DayKeyWords_Report entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

        public int DeleteDataByDay(int accountID, System.DateTime beginTime, System.DateTime endTime)
        {
            CacheUtility.Delete(_cacheKey);
            return dal.DeleteDataByDay(accountID, beginTime, endTime); 
          
        }

		///<summary>
		/// ��ȡһ����¼
		///</summary>
		public JJ_DayKeyWords_Report GetByID(int id)
		{
			JJ_DayKeyWords_Report entity = (JJ_DayKeyWords_Report)CacheUtility.Get(id, _cacheKey);
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
		public IList<JJ_DayKeyWords_Report> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<JJ_DayKeyWords_Report>)objList[0];;
		}

		#endregion

	}
}
