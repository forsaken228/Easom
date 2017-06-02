// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-18
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
using System.Data;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IJJ_KeyWords_Report  ��Լ�ӿ���
	///</summary>
	public interface IJJ_KeyWords_Report
	{
		long Insert(JJ_KeyWords_Report entity);

		void Delete(long id);

		void Update(JJ_KeyWords_Report entity);

		JJ_KeyWords_Report GetByID(int id);

		IList<JJ_KeyWords_Report> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        DataSet GetHourData(int accountID, System.DateTime beginTime, System.DateTime endTime);
        /// <summary>
        /// �������ƻ�ȡ�����Ѿ��ϴ�ĳ���ؼ��ʵ���������֮��
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="plan"></param>
        /// <param name="cell"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        JJ_KeyWords_Report GetbaiduHourSumByKeywordName(int accountID, System.DateTime begintime, System.DateTime endtime, string plan, string cell, string keyword);
        /// <summary>
        /// ���ݹؼ��ʣ��ƻ����ƣ���Ԫ���ƻ�ȡ�ùؼ��ʵ��������
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="plan"></param>
        /// <param name="cell"></param>
        /// <param name="keyword"></param>
        /// <param name="timestate"></param>
        /// <returns></returns>
        JJ_KeyWords_Report GetByKeywordName(int accountID, System.DateTime begintime, System.DateTime endtime, string plan, string cell, string keyword, int timestate);

        /// <summary>
        /// ɾ��һ��Сʱ���ϴ��Ĺؼ���
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <param name="timestate"></param>
        void DeleteDataByHour(int accountID, System.DateTime begintime, System.DateTime endtime, int timestate);
	}
}
