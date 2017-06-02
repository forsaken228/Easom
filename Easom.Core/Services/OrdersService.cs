// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
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
    /// OrdersService  ��������
    ///</summary>
    public class OrdersService : IOrders
    {
        private static readonly string _cacheKey = "Easom.Core.OrdersService";
        private static readonly OrdersDataAdapter dal = new OrdersDataAdapter();

        #region IOrders ��Ա

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public int Insert(Orders entity)
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
        public void Update(Orders entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }

        public void UpdateMediaID(int hospitalID)
        {
            dal.UpdateMediaID(hospitalID);
            CacheUtility.Delete(_cacheKey);
        }
        ///<summary>
        /// ��ȡһ����¼
        ///</summary>
        public Orders GetByID(int id)
        {
            Orders entity = (Orders)CacheUtility.Get(id, _cacheKey);
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
        public IList<Orders> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
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
            return (IList<Orders>)objList[0]; ;
        }
        ///<summary>
        /// ������ͼ
        ///</summary>
        public IList<Orders> SearchView(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderField.ToString(), isDesc.ToString(), "SearchView" });
            if (objList == null)
            {
                objList = new object[2];
                objList[0] = dal.SearchView(out pageCount, pageIndex, pageSize, where, orderField, isDesc);
                objList[1] = pageCount;
                CacheUtility.Add(objList, _cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderField.ToString(), isDesc.ToString(), "SearchView" });
            }
            pageCount = Convert.ToInt32(objList[1]);
            return (IList<Orders>)objList[0]; ;
        }

        #endregion

        /// <summary>
        /// ͨ������ȡ���������������Ĳ���
        /// </summary>
        /// <param name="hospitalID">����ҽԺ</param>
        /// <param name="name">��������</param>
        /// <param name="startime">��ʼʱ��</param>
        /// <returns>���ز�������</returns>
        public Orders GetCountByExistsName(string sectionID, string name, string startTime, string endtime)
        {
            Orders orders = (Orders)CacheUtility.Get(_cacheKey, new string[] { sectionID.ToString(), name.ToString(), startTime.ToString(), endtime.ToString(), "GetCountByExistsName" });
            if (orders == null)
            {
                orders = dal.GetCountByExistsName(sectionID, name, startTime, endtime);
                CacheUtility.Add(orders, _cacheKey, new string[] { sectionID.ToString(), name.ToString(), startTime.ToString(), endtime.ToString(), "GetCountByExistsName" });
            }
            return orders;
        }
        /// <summary>
        /// ͨ������ȡ���������������Ĳ���
        /// </summary>
        /// <param name="hospitalID">����ҽԺ</param>
        /// <param name="name">��������</param>
        /// <param name="startime">��ʼʱ��</param>
        /// <returns>���ز�������</returns>
        public Orders GetCountByExistsTel(string sectionID, string telephone, string startTime, string endtime)
        {
            Orders orders = (Orders)CacheUtility.Get(_cacheKey, new string[] { sectionID.ToString(), telephone.ToString(), startTime.ToString(), endtime.ToString(), "GetCountByExistsTel" });
            if (orders == null)
            {
                orders = dal.GetCountByExistsTel(sectionID, telephone, startTime, endtime);
                CacheUtility.Add(orders, _cacheKey, new string[] { sectionID.ToString(), telephone.ToString(), startTime.ToString(), endtime.ToString(), "GetCountByExistsTel" });
            }
            return orders;
        }


        /// <summary>
        /// ���ݽ�ɫ��ȡԤԼ����
        /// </summary>
        /// <param name="role">��ɫ</param>
        /// <param name="section">����</param>
        /// <param name="countType">ͳ���˻�</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet����</returns>
        public DataSet GetDepartmentData(string role, string section, int countType, DateTime beginTime, DateTime endTime)
        {
            return dal.GetDepartmentData(role, section, countType, beginTime, endTime);
        }

        /// <summary>
        /// ���ݽ�ɫ��ȡԤԼ���ݵ���Excel
        /// </summary>
        /// <param name="role">��ɫ</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet����</returns>
        public DataSet GetDepartmentData(string role, string section, DateTime beginTime, DateTime endTime)
        {
            return dal.GetDepartmentData(role, section, beginTime, endTime);
        }


        /// <summary>
        /// ��ͳ�Ʊ���
        /// </summary>
        /// <param name="add">ԤԼ����</param>
        /// <param name="arrive">ʵ������</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="countType">ͳ���˻�</param>
        public void GetMonthData(out int add, out int arrive, string section, DateTime beginTime, DateTime endTime, int countType)
        {
            dal.GetMonthData(out add, out arrive, section, beginTime, endTime, countType);
        }
         /// <summary>
        /// ��ͳ�Ʊ������죩
        /// </summary>
        /// <param name="add">ԤԼ����</param>
        /// <param name="arrive">ʵ������</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="countType">ͳ���˻�</param>
        public DataSet GetMonthDataByDay(string section, System.DateTime beginTime, System.DateTime endTime, int countType, int dateState)
        {
           return dal.GetMonthDataByDay(section,beginTime,endTime,countType,dateState);
        }

        public void GetDuiBiData(out int adds, out int orders, out int arrive, DateTime beginTime, DateTime endTime, string section, int countType)
        {
            dal.GetDuiBiData(out adds, out orders, out arrive, beginTime, endTime, section, countType);
        }

        /// <summary>
        /// ����ͳ�Ʊ���
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns></returns>
        public DataSet GetDiseaseByHospital(string section, DateTime beginTime, DateTime endTime)
        {
            return dal.GetDiseaseByHospital(section, beginTime, endTime);
        }
        /// <summary>
        /// ����ͳ�Ʊ���
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns></returns>
        public DataSet GetDiseaseBySection(string section, DateTime beginTime, DateTime endTime, bool IsAddANdArrive)
        {
            return dal.GetDiseaseBySection(section, beginTime, endTime, IsAddANdArrive);
        }
        /// <summary>
        /// ������Դ����
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet����</returns>
        public DataSet GetAreaHospital(string section, DateTime beginTime, DateTime endTime)
        {
            return dal.GetAreaHospital(section, beginTime, endTime);
        }

        /// <summary>
        /// ������Դ����
        /// </summary>
        /// <param name="BenDiAdds">�������</param>
        /// <param name="BenDiArrive">�����ѵ�</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        public void GetBenDiAreaDate(out int BenDiAdds, out int BenDiArrive, string section, DateTime beginTime, DateTime endTime)
        {
            dal.GetBenDiAreaDate(out BenDiAdds, out BenDiArrive, section, beginTime, endTime);
        }

        /// <summary>
        /// ý��ͳ�Ʊ���
        /// </summary>
        /// <param name="sectionStr">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet</returns>
        public DataSet GetMediaHospital(string sectionStr, DateTime beginTime, DateTime endTime, int hospital, int parentID)
        {
            return dal.GetMediaHospital(sectionStr, beginTime, endTime, hospital, parentID);
        }



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
        public void GetIndexData(out int adddate, out int orderdata, out int arrivedata, DateTime beginTime, DateTime endTime, string section, int countType)
        {
            dal.GetIndexData(out adddate, out orderdata, out arrivedata, beginTime, endTime, section, countType);
        }


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
        public void GetIndexDataByUserID(out int adddate, out int orderdata, out int arrivedata, int createUserID, System.DateTime beginTime, System.DateTime endTime, string section, int CountType)
        {
            dal.GetIndexDataByUserID(out adddate, out orderdata, out arrivedata, createUserID, beginTime, endTime, section, CountType);
        }

        /// <summary>
        /// ��ȡҽ������
        /// </summary>
        /// <param name="Doctor">ҽ��ID</param>
        /// <param name="starttime">��ʼʱ��</param>
        /// <param name="endtime">����ʱ��</param>
        /// <param name="sectionStr">����</param>
        /// <returns></returns>
        public DataSet GetOrderByDoctor(int Doctor, DateTime starttime, DateTime endtime, string sectionStr)
        {
            return dal.GetOrderByDoctor(Doctor, starttime, endtime, sectionStr);
        }



    }
}
