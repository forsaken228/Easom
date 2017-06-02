// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com

using System.Collections.Generic;
using CHCMS.Utility;
using System.Data;
using System;
namespace Easom.Core.Contracts
{

    ///<summary>
    /// IOrders  契约接口类
    ///</summary>
    public interface IOrders
    {
        int Insert(Orders entity);

        void Delete(int id);

        void Update(Orders entity);

        /// <summary>
        /// 修改Media的ID
        /// </summary>
        /// <param name="entity"></param>
        void UpdateMediaID(int hospitalID);

        Orders GetByID(int id);

        /// <summary>
        /// 根据角色获取预约数据
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="section">科室</param>
        /// <param name="countType">统计账户</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet集合</returns>
        DataSet GetDepartmentData(string role, string section, int countType, DateTime beginTime, DateTime endTime);

        /// <summary>
        /// 根据角色获取预约数据导出Excel
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet集合</returns>
        DataSet GetDepartmentData(string role, string section, DateTime beginTime, DateTime endTime);

        IList<Orders> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);
        IList<Orders> SearchView(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        /// <summary>
        /// 通过名字取出半年所有重名的病人
        /// </summary>
        /// <param name="hospitalID">所属医院</param>
        /// <param name="name">病人名字</param>
        /// <param name="startime">开始时间</param>
        /// <returns></returns>
        Orders GetCountByExistsName(string sectionID, string name, string starttime, string endtime);

        /// <summary>
        /// 通过电话取出半年所有重复的病人
        /// </summary>
        /// <param name="hospitalID">所属医院</param>
        /// <param name="telephone">病人电话</param>
        /// <param name="startime">开始时间</param>
        /// <returns></returns>
        Orders GetCountByExistsTel(string sectionID, string telephone, string starttime, string endtime);

        /// <summary>
        /// 月统计报表
        /// </summary>
        /// <param name="add">预约人数</param>
        /// <param name="arrive">实到人数</param>
        /// <param name="section">部门</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="countType">统计账户</param>
        void GetMonthData(out int add, out int arrive, string section, System.DateTime beginTime, System.DateTime endTime, int countType);

        /// <summary>
        /// 月统计报表（按天）
        /// </summary>
        /// <param name="section">部门</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="countType">统计账户</param>
        /// <param name="dateState">0预约1到院</param>
        DataSet GetMonthDataByDay(string section, System.DateTime beginTime, System.DateTime endTime, int countType, int dateState);

        /// <summary>
        /// 对比报表
        /// </summary>
        /// <param name="add">预约人数</param>
        /// <param name="arrive">实到人数</param>
        /// <param name="section">部门</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="countType">统计账户</param>
        void GetDuiBiData(out int adds, out int orders, out int arrive, System.DateTime beginTime, System.DateTime endTime, string section, int countType);

        /// <summary>
        /// 疾病统计报表
        /// </summary>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        DataSet GetDiseaseByHospital(string section, System.DateTime beginTime, System.DateTime endTime);
         /// <summary>
        /// 疾病统计报表
        /// </summary>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        DataSet GetDiseaseBySection(string section, DateTime beginTime, DateTime endTime,bool IsAddANdArrive);
        /// <summary>
        /// 地区来源报表
        /// </summary>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet集合</returns>
        DataSet GetAreaHospital(string section, System.DateTime beginTime, System.DateTime endTime);

        /// <summary>
        /// 本地来源报表
        /// </summary>
        /// <param name="BenDiAdds">本地添加</param>
        /// <param name="BenDiArrive">本地已到</param>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        void GetBenDiAreaDate(out int BenDiAdds, out int BenDiArrive, string section, System.DateTime beginTime, System.DateTime endTime);

        /// <summary>
        /// 媒体统计报表
        /// </summary>
        /// <param name="sectionStr">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet</returns>
        DataSet GetMediaHospital(string sectionStr,DateTime beginTime,DateTime endTime,int hospital,int parentID);

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
        void GetIndexData(out int adddate, out int orderdata, out int arrivedata, System.DateTime beginTime, System.DateTime endTime, string section, int CountType);

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
        void GetIndexDataByUserID(out int adddate, out int orderdata, out int arrivedata,int createUserID, System.DateTime beginTime, System.DateTime endTime, string section, int CountType);

        /// <summary>
        /// 获取医生数据
        /// </summary>
        /// <param name="Doctor">医生ID</param>
        /// <param name="starttime">开始时间</param>
        /// <param name="endtime">结束时间</param>
        /// <param name="sectionStr">科室</param>
        /// <returns></returns>
        DataSet GetOrderByDoctor(int Doctor, DateTime starttime, DateTime endtime, string sectionStr);
    }
}
