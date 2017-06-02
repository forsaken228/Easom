// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
using System;
namespace Easom.Core.Contracts
{

	///<summary>
	/// ICallOnData  ��Լ�ӿ���
	///</summary>
	public interface ICallOnData
	{
		int Insert(CallOnData entity);

		void Delete(int id);

		void Update(CallOnData entity);

		CallOnData GetByID(int id);

        /// <summary>
        /// ͨ��ԤԼ����ȡ�����лطü�¼
        /// </summary>
        /// <param name="oderId">ԤԼID</param>
        /// <param name="state">���ѻ�����ͨԤԼ0��Ϊ���ѣ�1Ϊ��ͨ</param>
        /// <returns>CallOnData����</returns>
        IList<CallOnData> GetByOrderID(int orderId,int state);

        /// <summary>
        /// ȡ�����û�����������ʱ�䲻��������Ļطü�¼
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="state">���ѻ�����ͨԤԼ0��Ϊ���ѣ�1Ϊ��ͨ</param>
        /// <returns>CallOnData����</returns>
        IList<CallOnData> GetByUserIDAndState(int userId, int state, int IsValid,string section);

        /// <summary>
        /// ȡ�����û������лطü�¼
        /// </summary>
        /// <param name="userId">�û�ID</param>
        /// <param name="state">���ѻ�����ͨԤԼ0��Ϊ���ѣ�1Ϊ��ͨ</param>
        /// <returns>CallOnData����</returns>
        IList<CallOnData> GetByUserIDAllCallOnData(int userId, int state, int IsValid, string section);

        ///// <summary>
        ///// ͨ��ԤԼ����ȡ�����µĻطü�¼
        ///// </summary>
        ///// <param name="orderId">ԤԼID</param>
        ///// <returns>CallOnData</returns>
        //CallOnData GetNewDataByOrderID(int orderId);

        /// <summary>
        /// ɾ������
        /// </summary>
        void DeleteRamind(int ordersID, int callUserID, DateTime datetime);

        /// <summary>
        /// ͨ��ordersIDɾ����������
        /// </summary>
        void DeleteRamind(int ordersID);

        /// <summary>
        /// ȡ�����µ�����
        /// </summary>
        /// <param name="ordersID">ԤԼID</param>
        /// <param name="state">���ѻ�����ͨԤԼ0��Ϊ���ѣ�1Ϊ��ͨ</param>
        /// <param name="IsValid">0��ΪԤԼ���ݣ�1Ϊ�������Ͽ�</param>
        /// <returns>CallOnData</returns>
        CallOnData GetLastDataByOrderID(int ordersID, int state);

		IList<CallOnData> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
