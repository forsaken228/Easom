// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com

using System;

using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;
using CHCMS.Utility;
using System.Data;
namespace Easom.Core.Services
{

    ///<summary>
    /// OrdersService  服务处理类
    ///</summary>
    public class OrdersService : IOrders
    {
        private static readonly string _cacheKey = "Easom.Core.OrdersService";
        private static readonly OrdersDataAdapter dal = new OrdersDataAdapter();

        #region IOrders 成员

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(Orders entity)
        {
            int r = dal.Insert(entity);
            CacheUtility.Delete(_cacheKey);
            return r;
        }

        ///<summary>
        /// 删除一条记录
        ///</summary>
        public void Delete(int id)
        {
            dal.Delete(id);
            CacheUtility.Delete(_cacheKey);
        }

        ///<summary>
        /// 更新一条记录
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
        /// 读取一条记录
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
        /// 搜索记录
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
        /// 搜索视图
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
        /// 通过名字取出半年所有重名的病人
        /// </summary>
        /// <param name="hospitalID">所属医院</param>
        /// <param name="name">病人名字</param>
        /// <param name="startime">开始时间</param>
        /// <returns>返回病人数据</returns>
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
        /// 通过名字取出半年所有重名的病人
        /// </summary>
        /// <param name="hospitalID">所属医院</param>
        /// <param name="name">病人名字</param>
        /// <param name="startime">开始时间</param>
        /// <returns>返回病人数据</returns>
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
        /// 根据角色获取预约数据
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="section">科室</param>
        /// <param name="countType">统计账户</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet集合</returns>
        public DataSet GetDepartmentData(string role, string section, int countType, DateTime beginTime, DateTime endTime)
        {
            return dal.GetDepartmentData(role, section, countType, beginTime, endTime);
        }

        /// <summary>
        /// 根据角色获取预约数据导出Excel
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet集合</returns>
        public DataSet GetDepartmentData(string role, string section, DateTime beginTime, DateTime endTime)
        {
            return dal.GetDepartmentData(role, section, beginTime, endTime);
        }


        /// <summary>
        /// 月统计报表
        /// </summary>
        /// <param name="add">预约人数</param>
        /// <param name="arrive">实到人数</param>
        /// <param name="section">部门</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="countType">统计账户</param>
        public void GetMonthData(out int add, out int arrive, string section, DateTime beginTime, DateTime endTime, int countType)
        {
            dal.GetMonthData(out add, out arrive, section, beginTime, endTime, countType);
        }
         /// <summary>
        /// 月统计报表（按天）
        /// </summary>
        /// <param name="add">预约人数</param>
        /// <param name="arrive">实到人数</param>
        /// <param name="section">部门</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="countType">统计账户</param>
        public DataSet GetMonthDataByDay(string section, System.DateTime beginTime, System.DateTime endTime, int countType, int dateState)
        {
           return dal.GetMonthDataByDay(section,beginTime,endTime,countType,dateState);
        }

        public void GetDuiBiData(out int adds, out int orders, out int arrive, DateTime beginTime, DateTime endTime, string section, int countType)
        {
            dal.GetDuiBiData(out adds, out orders, out arrive, beginTime, endTime, section, countType);
        }

        /// <summary>
        /// 疾病统计报表
        /// </summary>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public DataSet GetDiseaseByHospital(string section, DateTime beginTime, DateTime endTime)
        {
            return dal.GetDiseaseByHospital(section, beginTime, endTime);
        }
        /// <summary>
        /// 疾病统计报表
        /// </summary>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public DataSet GetDiseaseBySection(string section, DateTime beginTime, DateTime endTime, bool IsAddANdArrive)
        {
            return dal.GetDiseaseBySection(section, beginTime, endTime, IsAddANdArrive);
        }
        /// <summary>
        /// 地区来源报表
        /// </summary>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet集合</returns>
        public DataSet GetAreaHospital(string section, DateTime beginTime, DateTime endTime)
        {
            return dal.GetAreaHospital(section, beginTime, endTime);
        }

        /// <summary>
        /// 本地来源报表
        /// </summary>
        /// <param name="BenDiAdds">本地添加</param>
        /// <param name="BenDiArrive">本地已到</param>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public void GetBenDiAreaDate(out int BenDiAdds, out int BenDiArrive, string section, DateTime beginTime, DateTime endTime)
        {
            dal.GetBenDiAreaDate(out BenDiAdds, out BenDiArrive, section, beginTime, endTime);
        }

        /// <summary>
        /// 媒体统计报表
        /// </summary>
        /// <param name="sectionStr">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet</returns>
        public DataSet GetMediaHospital(string sectionStr, DateTime beginTime, DateTime endTime, int hospital, int parentID)
        {
            return dal.GetMediaHospital(sectionStr, beginTime, endTime, hospital, parentID);
        }



        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="adddate">添加</param>
        /// <param name="orderdata">预约</param>
        /// <param name="arrivedata">到院</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="section">科室</param>
        /// <param name="CountType">统计账户</param>
        public void GetIndexData(out int adddate, out int orderdata, out int arrivedata, DateTime beginTime, DateTime endTime, string section, int countType)
        {
            dal.GetIndexData(out adddate, out orderdata, out arrivedata, beginTime, endTime, section, countType);
        }


        /// <summary>
        /// 预约列表个人预约成绩展示
        /// </summary>
        /// <param name="adddate">添加</param>
        /// <param name="orderdata">预约</param>
        /// <param name="arrivedata">到院</param>
        /// <param name="createUserID">创建人ID</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="section">科室</param>
        /// <param name="CountType">统计账户</param>
        public void GetIndexDataByUserID(out int adddate, out int orderdata, out int arrivedata, int createUserID, System.DateTime beginTime, System.DateTime endTime, string section, int CountType)
        {
            dal.GetIndexDataByUserID(out adddate, out orderdata, out arrivedata, createUserID, beginTime, endTime, section, CountType);
        }

        /// <summary>
        /// 获取医生数据
        /// </summary>
        /// <param name="Doctor">医生ID</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="sectionStr">科室</param>
        /// <returns></returns>
        public DataSet GetOrderByDoctor(int Doctor, DateTime starttime, DateTime endtime, string sectionStr)
        {
            return dal.GetOrderByDoctor(Doctor, starttime, endtime, sectionStr);
        }



    }
}
