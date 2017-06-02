// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com

using System.Collections.Generic;
using CHCMS.Utility;
using System.Data;
using System;
namespace Easom.Core.Contracts
{

    ///<summary>
    /// IOrders  ��Լ�ӿ���
    ///</summary>
    public interface IOrders
    {
        int Insert(Orders entity);

        void Delete(int id);

        void Update(Orders entity);

        /// <summary>
        /// �޸�Media��ID
        /// </summary>
        /// <param name="entity"></param>
        void UpdateMediaID(int hospitalID);

        Orders GetByID(int id);

        /// <summary>
        /// ���ݽ�ɫ��ȡԤԼ����
        /// </summary>
        /// <param name="role">��ɫ</param>
        /// <param name="section">����</param>
        /// <param name="countType">ͳ���˻�</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet����</returns>
        DataSet GetDepartmentData(string role, string section, int countType, DateTime beginTime, DateTime endTime);

        /// <summary>
        /// ���ݽ�ɫ��ȡԤԼ���ݵ���Excel
        /// </summary>
        /// <param name="role">��ɫ</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet����</returns>
        DataSet GetDepartmentData(string role, string section, DateTime beginTime, DateTime endTime);

        IList<Orders> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);
        IList<Orders> SearchView(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        /// <summary>
        /// ͨ������ȡ���������������Ĳ���
        /// </summary>
        /// <param name="hospitalID">����ҽԺ</param>
        /// <param name="name">��������</param>
        /// <param name="startime">��ʼʱ��</param>
        /// <returns></returns>
        Orders GetCountByExistsName(string sectionID, string name, string starttime, string endtime);

        /// <summary>
        /// ͨ���绰ȡ�����������ظ��Ĳ���
        /// </summary>
        /// <param name="hospitalID">����ҽԺ</param>
        /// <param name="telephone">���˵绰</param>
        /// <param name="startime">��ʼʱ��</param>
        /// <returns></returns>
        Orders GetCountByExistsTel(string sectionID, string telephone, string starttime, string endtime);

        /// <summary>
        /// ��ͳ�Ʊ���
        /// </summary>
        /// <param name="add">ԤԼ����</param>
        /// <param name="arrive">ʵ������</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="countType">ͳ���˻�</param>
        void GetMonthData(out int add, out int arrive, string section, System.DateTime beginTime, System.DateTime endTime, int countType);

        /// <summary>
        /// ��ͳ�Ʊ������죩
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="countType">ͳ���˻�</param>
        /// <param name="dateState">0ԤԼ1��Ժ</param>
        DataSet GetMonthDataByDay(string section, System.DateTime beginTime, System.DateTime endTime, int countType, int dateState);

        /// <summary>
        /// �Աȱ���
        /// </summary>
        /// <param name="add">ԤԼ����</param>
        /// <param name="arrive">ʵ������</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="countType">ͳ���˻�</param>
        void GetDuiBiData(out int adds, out int orders, out int arrive, System.DateTime beginTime, System.DateTime endTime, string section, int countType);

        /// <summary>
        /// ����ͳ�Ʊ���
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        DataSet GetDiseaseByHospital(string section, System.DateTime beginTime, System.DateTime endTime);
         /// <summary>
        /// ����ͳ�Ʊ���
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        DataSet GetDiseaseBySection(string section, DateTime beginTime, DateTime endTime,bool IsAddANdArrive);
        /// <summary>
        /// ������Դ����
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet����</returns>
        DataSet GetAreaHospital(string section, System.DateTime beginTime, System.DateTime endTime);

        /// <summary>
        /// ������Դ����
        /// </summary>
        /// <param name="BenDiAdds">�������</param>
        /// <param name="BenDiArrive">�����ѵ�</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        void GetBenDiAreaDate(out int BenDiAdds, out int BenDiArrive, string section, System.DateTime beginTime, System.DateTime endTime);

        /// <summary>
        /// ý��ͳ�Ʊ���
        /// </summary>
        /// <param name="sectionStr">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet</returns>
        DataSet GetMediaHospital(string sectionStr,DateTime beginTime,DateTime endTime,int hospital,int parentID);

        /// <summary>
        /// ��ҳ
        /// </summary>
        /// <param name="adddate">���</param>
        /// <param name="orderdata">ԤԼ</param>
        /// <param name="arrivedata">��Ժ</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="section">����</param>
        /// <param name="CountType">ͳ���˻�</param>
        void GetIndexData(out int adddate, out int orderdata, out int arrivedata, System.DateTime beginTime, System.DateTime endTime, string section, int CountType);

        /// <summary>
        /// ԤԼ�б����ԤԼ�ɼ�չʾ
        /// </summary>
        /// <param name="adddate">���</param>
        /// <param name="orderdata">ԤԼ</param>
        /// <param name="arrivedata">��Ժ</param>
        /// <param name="createUserID">������ID</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="section">����</param>
        /// <param name="CountType">ͳ���˻�</param>
        void GetIndexDataByUserID(out int adddate, out int orderdata, out int arrivedata,int createUserID, System.DateTime beginTime, System.DateTime endTime, string section, int CountType);

        /// <summary>
        /// ��ȡҽ������
        /// </summary>
        /// <param name="Doctor">ҽ��ID</param>
        /// <param name="starttime">��ʼʱ��</param>
        /// <param name="endtime">����ʱ��</param>
        /// <param name="sectionStr">����</param>
        /// <returns></returns>
        DataSet GetOrderByDoctor(int Doctor, DateTime starttime, DateTime endtime, string sectionStr);
    }
}
