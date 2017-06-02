// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-18
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
using System.Data;
using System;

namespace Easom.Core.Contracts
{

	///<summary>
	/// IJJ_Swt_Report  ��Լ�ӿ���
	///</summary>
	public interface IJJ_Swt_Report
	{
		int Insert(JJ_Swt_Report entity);

		void Delete(int id);

		void Update(JJ_Swt_Report entity);

		JJ_Swt_Report GetByID(int id);

        IList<JJ_Swt_Report> GetAllJJ_Swt_Report(int hospitalID);

        IList<JJ_Swt_Report> GetAllJJ_Swt_Report(int hospitalID, int sectionID);

		IList<JJ_Swt_Report> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);


        /// <summary>
        /// �Ի�ͳ�Ʊ���
        /// </summary>
        /// <param name="SectionID">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet</returns>
        DataSet GetJJ_Swt_ReportHospital(DateTime beginTime, DateTime endTime, int hospital);


        /// <summary>
        /// ��ȡ����ͨ�����ֶξۺ��б�
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="hospital">ҽԺID</param>
        /// <param name="SectionID">����ID</param>
        /// <param name="tableSort">�ֶ����</param>
        /// <param name="chatState">�Ի�����</param>
        /// <returns>DataSet</returns>
        DataSet GetSwtTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, int tableSort, int chatState);

        /// <summary>
        /// �ٶȼƻ�����
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="hospital">ҽԺID</param>
        /// <param name="SectionID">����ID</param>
        /// <param name="tableSort">�ֶ����</param>
        /// <param name="chatState">�Ի�����</param>
        /// <returns>DataSet</returns>
        DataSet GetBaiduPlanTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, string tableSort, string chatState);

        /// <summary>
        /// �ٶȹؼ��ʷ���
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="hospital">ҽԺID</param>
        /// <param name="SectionID">����ID</param>
        /// <param name="tableSort">�ֶ����</param>
        /// <param name="chatState">�Ի�����</param>
        /// <returns>DataSet</returns>
        DataSet GetBaiduKeyWordsTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, string tableSort, string chatState);

	}
}
