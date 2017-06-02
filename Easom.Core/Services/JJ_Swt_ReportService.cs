// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-18
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
	/// JJ_Swt_ReportService  ��������
	///</summary>
	public class JJ_Swt_ReportService : IJJ_Swt_Report
	{
		private static readonly string _cacheKey = "Easom.Core.JJ_Swt_ReportService";
		private static readonly JJ_Swt_ReportDataAdapter dal = new JJ_Swt_ReportDataAdapter();

		#region IJJ_Swt_Report ��Ա

		///<summary>
		/// ����һ����¼
		///</summary>
		public int Insert(JJ_Swt_Report entity)
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
		public void Update(JJ_Swt_Report entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// ��ȡһ����¼
		///</summary>
		public JJ_Swt_Report GetByID(int id)
		{
			JJ_Swt_Report entity = (JJ_Swt_Report)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

        /// <summary>
        /// ��ȡ��������ͨ
        /// </summary>
        /// <returns></returns>
        public IList<JJ_Swt_Report> GetAllJJ_Swt_Report(int hospitalID)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { hospitalID.ToString(), "GetAllJJ_Swt_Report" });
            if (objList == null)
            {
                objList = new object[1];
                objList[0] = dal.GetAllJJ_Swt_Report(hospitalID);
                CacheUtility.Add(objList, _cacheKey, new string[] { hospitalID.ToString(), "GetAllJJ_Swt_Report" });
            }
            return (IList<JJ_Swt_Report>)objList[0];
        }
        /// <summary>
        /// ����ͨ��sectionID��ȡ����ͨ
        /// </summary>
        /// <param name="prentid"></param>
        /// <returns></returns>
        public IList<JJ_Swt_Report> GetAllJJ_Swt_Report(int hospitalID, int sectionID)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { hospitalID.ToString(), sectionID.ToString(), "GetAllJJ_Swt_Report" });
            if (objList == null)
            {
                objList = new object[1];
                objList[0] = dal.GetAllJJ_Swt_Report(hospitalID, sectionID);
                CacheUtility.Add(objList, _cacheKey, new string[] { hospitalID.ToString(), sectionID.ToString(), "GetAllJJ_Swt_Report" });
            }
            return (IList<JJ_Swt_Report>)objList[0];
        }

		///<summary>
		/// ������¼
		///</summary>
		public IList<JJ_Swt_Report> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<JJ_Swt_Report>)objList[0];;
		}



        /// <summary>
        /// �Ի�ͳ�Ʊ���
        /// </summary>
        /// <param name="SectionID">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet</returns>
        public DataSet GetJJ_Swt_ReportHospital(DateTime beginTime, DateTime endTime, int hospital)
        {
            return dal.GetJJ_Swt_ReportHospital(beginTime, endTime, hospital);
        }

        public DataSet GetSwtTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string SectionID, int tableSort, int chatState)
        {
            return dal.GetSwtTableSortStatistics(beginTime, endTime, hospital,SectionID,tableSort,chatState);
        }
        #endregion


        public DataSet GetBaiduPlanTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, string tableSort, string chatState)
        {
            return dal.GetBaiduPlanTableSortStatistics(beginTime, endTime, hospital, Section, tableSort, chatState);
        }


        public DataSet GetBaiduKeyWordsTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, string tableSort, string chatState)
        {
            return dal.GetBaiduKeyWordsTableSortStatistics(beginTime, endTime, hospital, Section, tableSort, chatState);
        }
    }
}
