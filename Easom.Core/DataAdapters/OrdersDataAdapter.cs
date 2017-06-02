// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

using CHCMS.Utility;
using Easom.Core.Support;
namespace Easom.Core.DataAdapters
{

    ///<summary>
    /// OrdersDataAdapter  数据库访问类
    ///</summary>
    internal class OrdersDataAdapter
    {
        private readonly string _dbName = "CoreDB";
        private const string SQL_INSERT = "usp_Orders_Insert";
        private const string SQL_DELETE = "usp_Orders_Delete";
        private const string SQL_UPDATE = "usp_Orders_Update";
        private const string SQL_GET_BY_ID = "usp_Orders_GetByID";
        private const string SQL_SEARCH = "usp_Orders_Search";
        private const string SQL_SEARCH_VIEW = "usp_Orders_Search_By_View";
        private const string SQL_ORDER_GETCOUNTBYEXISTSNAME = "usp_Orders_GetCountByExistsName";
        private const string SQL_ORDER_GETCOUNTBYEXISTSTEL = "usp_Orders_GetCountByExistsTel";
        private const string SQL_ORDER_GETDEPARTMENTDATA = "usp_Order_Kefu";
        private const string SQL_ORDER_GETDEPARTMENTDATA_EXCEL = "usp_Order_Kefu_Excel";
        private const string SQL_ORDER_GETMONTHDATA = "usp_Order_Month";
        private const string SQL_ORDER_GETMONTHDATA_BYDAY = "usp_Order_Month_ByDay";
        private const string SQL_ORDER_GETDUIBIDATA = "usp_Order_DuiBi";
        private const string SQL_ORDER_GETDISEASEDATA = "usp_Order_Disease_From";
        private const string SQL_ORDER_GETDISEASE = "usp_Orders_Disease";

        private const string SQL_ORDER_AREA_BENDI = "usp_Order_Area_Bendi";
        private const string SQL_ORDER_AREA = "usp_Order_Area";
        //private const string SQL_ORDER_MEDIA = "usp_Order_Meidia";
        private const string SQL_ORDER_MEDIA = "usp_Order_Media_From";
        private const string SQL_ORDER_INDEXDATA = "usp_Order_IndexData";

        private const string SQL_ORDER_DATA_BYUSERID = "usp_Order_Data_ByUserID";

        private const string SQL_ORDER_BY_DOCTOR = "usp_Order_ByDoctor";

        private const string SQL_ORDER_UPDATEMEDIA = "usp_Orders_UpdateMediaID";
        private const string _fields = "ID,OrderNumber,Name,Age,AreaSourceType,HospitalWebsiteID,HospitalID,MediaSourceID,MediaSourceExtendID,DoctorID,CreateUserID,Sex,ArriveStateType,CountType,ExpertIdentifier,Telephone,QQ,Question,ChatRecord,KeyWords,DiseaseID,Area,Remark,OrderTime,UpdateTime,AddTime,RecordTime,RecordUserID,SectionID,DRemark,OrderState,IsDelete,IsConsume,ConsumeNum";
        private const string _view_fields = "ID,OrderNumber,Name,Age,AreaSourceType,HospitalWebsiteID,HospitalID,MediaSourceID,MediaSourceExtendID,DoctorID,CreateUserID,Sex,ArriveStateType,CountType,ExpertIdentifier,Telephone,QQ,Question,KeyWords,DiseaseID,Area,Remark,OrderTime,UpdateTime,AddTime,RecordTime,RecordUserID,SectionID,DRemark,OrderState,IsDelete,RoleID,IsConsume,ConsumeNum";

        ///<summary>
        /// 从IDataReader中读取一个实体对象
        ///</summary>
        private Orders GetByReader(IDataReader dr)
        {
            Orders entity = new Orders();
            object obj = null;
            obj = dr["ID"];
            if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
            obj = dr["OrderNumber"];
            entity.OrderNumber = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.OrderNumber = Convert.ToString(obj); }
            obj = dr["Name"];
            entity.Name = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
            obj = dr["Age"];
            if (obj != null && obj != DBNull.Value) { entity.Age = Convert.ToInt32(obj); }
            obj = dr["AreaSourceType"];
            if (obj != null && obj != DBNull.Value) { entity.AreaSourceType = (AreaTypeEnum)Convert.ToInt32(obj); }
            obj = dr["HospitalWebsiteID"];
            if (obj != null && obj != DBNull.Value) { entity.HospitalWebsiteID = Convert.ToInt32(obj); }
            obj = dr["HospitalID"];
            if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
            obj = dr["MediaSourceID"];
            if (obj != null && obj != DBNull.Value) { entity.MediaSourceID = Convert.ToInt32(obj); }
            obj = dr["MediaSourceExtendID"];
            if (obj != null && obj != DBNull.Value) { entity.MediaSourceExtendID = Convert.ToInt32(obj); }
            obj = dr["DoctorID"];
            if (obj != null && obj != DBNull.Value) { entity.DoctorID = Convert.ToInt32(obj); }
            obj = dr["CreateUserID"];
            if (obj != null && obj != DBNull.Value) { entity.CreateUserID = Convert.ToInt32(obj); }
            obj = dr["Sex"];
            if (obj != null && obj != DBNull.Value) { entity.Sex = Convert.ToBoolean(obj); }
            obj = dr["ArriveStateType"];
            if (obj != null && obj != DBNull.Value) { entity.ArriveStateType = (ArriveStateEnum)Convert.ToInt32(obj); }
            obj = dr["CountType"];
            if (obj != null && obj != DBNull.Value) { entity.CountType = (CountTypeEnum)Convert.ToInt32(obj); }
            obj = dr["ExpertIdentifier"];
            entity.ExpertIdentifier = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.ExpertIdentifier = Convert.ToString(obj); }
            obj = dr["Telephone"];
            entity.Telephone = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Telephone = Convert.ToString(obj); }
            obj = dr["QQ"];
            entity.QQ = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.QQ = Convert.ToString(obj); }
            obj = dr["Question"];
            entity.Question = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Question = Convert.ToString(obj); }
            if (DBConfigUtility.IsContainsColumn(dr, "ChatRecord"))
            {
                obj = dr["ChatRecord"];
                entity.ChatRecord = Convert.ToString(obj);
                if (obj != null && obj != DBNull.Value) { entity.ChatRecord = Convert.ToString(obj); }
            }
            obj = dr["KeyWords"];
            entity.KeyWords = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.KeyWords = Convert.ToString(obj); }
            obj = dr["DiseaseID"];
            if (obj != null && obj != DBNull.Value) { entity.DiseaseID = Convert.ToInt32(obj); }
            obj = dr["Area"];
            entity.Area = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Area = Convert.ToString(obj); }
            obj = dr["Remark"];
            entity.Remark = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Remark = Convert.ToString(obj); }
            obj = dr["OrderTime"];
            if (obj != null && obj != DBNull.Value) { entity.OrderTime = Convert.ToDateTime(obj); }
            obj = dr["UpdateTime"];
            if (obj != null && obj != DBNull.Value) { entity.UpdateTime = Convert.ToDateTime(obj); }
            obj = dr["AddTime"];
            if (obj != null && obj != DBNull.Value) { entity.AddTime = Convert.ToDateTime(obj); }
            obj = dr["RecordTime"];
            if (obj != null && obj != DBNull.Value) { entity.RecordTime = Convert.ToDateTime(obj); }
            obj = dr["RecordUserID"];
            if (obj != null && obj != DBNull.Value) { entity.RecordUserID = Convert.ToInt32(obj); }
            obj = dr["SectionID"];
            if (obj != null && obj != DBNull.Value) { entity.SectionID = Convert.ToInt32(obj); }
            obj = dr["DRemark"];
            entity.DRemark = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.DRemark = Convert.ToString(obj); }
            obj = dr["OrderState"];
            if (obj != null && obj != DBNull.Value) { entity.OrderState = Convert.ToInt32(obj); }
            obj = dr["IsDelete"];
            if (obj != null && obj != DBNull.Value) { entity.IsDelete = Convert.ToInt32(obj); }
            if (DBConfigUtility.IsContainsColumn(dr, "Role"))
            {
                obj = dr["Role"];
                if (obj != null && obj != DBNull.Value) { entity.RoleID = Convert.ToInt32(obj); }
            }
            obj = dr["IsConsume"];
            if (obj != null && obj != DBNull.Value) { entity.IsConsume = Convert.ToBoolean(obj); }
            obj = dr["ConsumeNum"];
            if (obj != null && obj != DBNull.Value) { entity.ConsumeNum = Convert.ToDecimal(obj); }
            return entity;
        }

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(Orders entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddOutParameter(dbc, "ID", DbType.Int32, 4);
            db.AddInParameter(dbc, "OrderNumber", DbType.String, entity.OrderNumber);
            db.AddInParameter(dbc, "Name", DbType.String, entity.Name);
            db.AddInParameter(dbc, "Age", DbType.Int32, entity.Age);
            db.AddInParameter(dbc, "AreaSourceType", DbType.Int32, entity.AreaSourceType);
            db.AddInParameter(dbc, "HospitalWebsiteID", DbType.Int32, entity.HospitalWebsiteID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, entity.HospitalID);
            db.AddInParameter(dbc, "MediaSourceID", DbType.Int32, entity.MediaSourceID);
            db.AddInParameter(dbc, "MediaSourceExtendID", DbType.Int32, entity.MediaSourceExtendID);
            db.AddInParameter(dbc, "DoctorID", DbType.Int32, entity.DoctorID);
            db.AddInParameter(dbc, "CreateUserID", DbType.Int32, entity.CreateUserID);
            db.AddInParameter(dbc, "Sex", DbType.Boolean, entity.Sex);
            db.AddInParameter(dbc, "ArriveStateType", DbType.Int32, entity.ArriveStateType);
            db.AddInParameter(dbc, "CountType", DbType.Int32, entity.CountType);
            db.AddInParameter(dbc, "ExpertIdentifier", DbType.String, entity.ExpertIdentifier);
            db.AddInParameter(dbc, "Telephone", DbType.String, entity.Telephone);
            db.AddInParameter(dbc, "QQ", DbType.String, entity.QQ);
            db.AddInParameter(dbc, "Question", DbType.String, entity.Question);
            db.AddInParameter(dbc, "ChatRecord", DbType.String, entity.ChatRecord);
            db.AddInParameter(dbc, "KeyWords", DbType.String, entity.KeyWords);
            db.AddInParameter(dbc, "DiseaseID", DbType.Int32, entity.DiseaseID);
            db.AddInParameter(dbc, "Area", DbType.String, entity.Area);
            db.AddInParameter(dbc, "Remark", DbType.String, entity.Remark);
            db.AddInParameter(dbc, "OrderTime", DbType.DateTime, entity.OrderTime);
            db.AddInParameter(dbc, "UpdateTime", DbType.DateTime, entity.UpdateTime);
            db.AddInParameter(dbc, "RecordUserID", DbType.Int32, entity.RecordUserID);
            db.AddInParameter(dbc, "AddTime", DbType.DateTime, entity.AddTime);
            db.AddInParameter(dbc, "RecordTime", DbType.DateTime, entity.RecordTime);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, entity.SectionID);
            db.AddInParameter(dbc, "DRemark", DbType.String, entity.DRemark);
            db.AddInParameter(dbc, "OrderState", DbType.Int32, entity.OrderState);
            db.AddInParameter(dbc, "IsDelete", DbType.Int32, entity.IsDelete);
            db.AddInParameter(dbc, "IsConsume", DbType.Boolean, entity.IsConsume);
            db.AddInParameter(dbc, "ConsumeNum", DbType.Decimal, entity.ConsumeNum);
            try
            {
                db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }

            return (int)db.GetParameterValue(dbc, "ID");
        }

        ///<summary>
        /// 删除一条记录
        ///</summary>
        public void Delete(int id)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE);
            db.AddInParameter(dbc, "ID", DbType.Int32, id);

            try
            {
                db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }

        }

        ///<summary>
        /// 更新一条记录
        ///</summary>
        public void Update(Orders entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
            db.AddInParameter(dbc, "ID", DbType.Int32, entity.ID);
            db.AddInParameter(dbc, "OrderNumber", DbType.String, entity.OrderNumber);
            db.AddInParameter(dbc, "Name", DbType.String, entity.Name);
            db.AddInParameter(dbc, "Age", DbType.Int32, entity.Age);
            db.AddInParameter(dbc, "AreaSourceType", DbType.Int32, entity.AreaSourceType);
            db.AddInParameter(dbc, "HospitalWebsiteID", DbType.Int32, entity.HospitalWebsiteID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, entity.HospitalID);
            db.AddInParameter(dbc, "MediaSourceID", DbType.Int32, entity.MediaSourceID);
            db.AddInParameter(dbc, "MediaSourceExtendID", DbType.Int32, entity.MediaSourceExtendID);
            db.AddInParameter(dbc, "DoctorID", DbType.Int32, entity.DoctorID);
            db.AddInParameter(dbc, "CreateUserID", DbType.Int32, entity.CreateUserID);
            db.AddInParameter(dbc, "Sex", DbType.Boolean, entity.Sex);
            db.AddInParameter(dbc, "ArriveStateType", DbType.Int32, entity.ArriveStateType);
            db.AddInParameter(dbc, "CountType", DbType.Int32, entity.CountType);
            db.AddInParameter(dbc, "ExpertIdentifier", DbType.String, entity.ExpertIdentifier);
            db.AddInParameter(dbc, "Telephone", DbType.String, entity.Telephone);
            db.AddInParameter(dbc, "QQ", DbType.String, entity.QQ);
            db.AddInParameter(dbc, "Question", DbType.String, entity.Question);
            db.AddInParameter(dbc, "ChatRecord", DbType.String, entity.ChatRecord);
            db.AddInParameter(dbc, "KeyWords", DbType.String, entity.KeyWords);
            db.AddInParameter(dbc, "DiseaseID", DbType.Int32, entity.DiseaseID);
            db.AddInParameter(dbc, "Area", DbType.String, entity.Area);
            db.AddInParameter(dbc, "Remark", DbType.String, entity.Remark);
            db.AddInParameter(dbc, "OrderTime", DbType.DateTime, entity.OrderTime);
            db.AddInParameter(dbc, "UpdateTime", DbType.DateTime, entity.UpdateTime);
            db.AddInParameter(dbc, "AddTime", DbType.DateTime, entity.AddTime);
            db.AddInParameter(dbc, "RecordTime", DbType.DateTime, entity.RecordTime);
            db.AddInParameter(dbc, "RecordUserID", DbType.Int32, entity.RecordUserID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, entity.SectionID);
            db.AddInParameter(dbc, "DRemark", DbType.String, entity.DRemark);
            db.AddInParameter(dbc, "OrderState", DbType.Int32, entity.OrderState);
            db.AddInParameter(dbc, "IsDelete", DbType.Int32, entity.IsDelete);
            db.AddInParameter(dbc, "IsConsume", DbType.Boolean, entity.IsConsume);
            db.AddInParameter(dbc, "ConsumeNum", DbType.Decimal, entity.ConsumeNum);
            try
            {
                db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }

        }

        ///<summary>
        /// 更新一条记录
        ///</summary>
        public void UpdateMediaID(int hospitalID)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_UPDATEMEDIA);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospitalID);
            try
            {
                db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }

        }

        ///<summary>
        /// 读取一条记录
        ///</summary>
        public Orders GetByID(int id)
        {
            Orders entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ID);
            db.AddInParameter(dbc, "ID", DbType.Int32, id);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                if (dr.Read())
                {
                    entity = GetByReader(dr);
                }
            }
            return entity;
        }

        ///<summary>
        /// 搜索记录
        ///</summary>
        public IList<Orders> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<Orders> lst = new List<Orders>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_SEARCH);
            db.AddOutParameter(dbc, "pageCount", DbType.Int32, 4);
            db.AddInParameter(dbc, "pageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(dbc, "pageSize", DbType.Int32, pageSize);
            db.AddInParameter(dbc, "fields", DbType.String, _fields);
            db.AddInParameter(dbc, "where", DbType.String, where);
            db.AddInParameter(dbc, "orderField", DbType.String, orderField);
            db.AddInParameter(dbc, "isDesc", DbType.Boolean, isDesc);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Orders ent = new Orders();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }

        ///<summary>
        /// 搜索记录
        ///</summary>
        public IList<Orders> SearchView(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<Orders> lst = new List<Orders>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_SEARCH_VIEW);
            db.AddOutParameter(dbc, "pageCount", DbType.Int32, 4);
            db.AddInParameter(dbc, "pageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(dbc, "pageSize", DbType.Int32, pageSize);
            db.AddInParameter(dbc, "fields", DbType.String, _view_fields);
            db.AddInParameter(dbc, "where", DbType.String, where);
            db.AddInParameter(dbc, "orderField", DbType.String, orderField);
            db.AddInParameter(dbc, "isDesc", DbType.Boolean, isDesc);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Orders ent = new Orders();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }

        /// <summary>
        /// 通过名字取出半年所有重名的病人
        /// </summary>
        /// <param name="hospitalID">所属医院</param>
        /// <param name="name">病人名字</param>
        /// <param name="startime">开始时间</param>
        /// <returns></returns>
        public Orders GetCountByExistsName(string sectionID, string name, string startime, string endtime)
        {
            Orders entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_GETCOUNTBYEXISTSNAME);
            db.AddInParameter(dbc, "Section", DbType.String, sectionID);
            db.AddInParameter(dbc, "Name", DbType.String, name);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, startime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endtime);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                if (dr.Read())
                {
                    entity = GetByReader(dr);
                }
            }
            return entity;
        }

        /// <summary>
        /// 通过电话取出半年所有重复的病人
        /// </summary>
        /// <param name="hospitalID">所属医院</param>
        /// <param name="telephone">病人电话</param>
        /// <param name="startime">开始时间</param>
        /// <returns></returns>
        public Orders GetCountByExistsTel(string sectionID, string telephone, string startime, string endtime)
        {
            Orders entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_GETCOUNTBYEXISTSTEL);
            db.AddInParameter(dbc, "Section", DbType.String, sectionID);
            db.AddInParameter(dbc, "Telephone", DbType.String, telephone);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, startime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endtime);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                if (dr.Read())
                {
                    entity = GetByReader(dr);
                }
            }
            return entity;
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
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_GETDEPARTMENTDATA);
            db.AddInParameter(dbc, "CountType", DbType.Int32, countType);
            db.AddInParameter(dbc, "Section", DbType.String, section);

            db.AddInParameter(dbc, "Role", DbType.String, role);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
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
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_GETDEPARTMENTDATA_EXCEL);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            db.AddInParameter(dbc, "Role", DbType.String, role);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
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
        public void GetMonthData(out int add, out int arrive, string section, System.DateTime beginTime, System.DateTime endTime, int countType)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_GETMONTHDATA);
            db.AddOutParameter(dbc, "Add", DbType.Int32, 0);
            db.AddOutParameter(dbc, "Arrive", DbType.Int32, 0);
            db.AddInParameter(dbc, "beginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "endTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            db.AddInParameter(dbc, "CountType", DbType.Int32, countType);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                add = (int)db.GetParameterValue(dbc, "Add");
                arrive = (int)db.GetParameterValue(dbc, "Arrive");
            }
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
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_GETMONTHDATA_BYDAY);
            db.AddInParameter(dbc, "beginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "endTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            db.AddInParameter(dbc, "CountType", DbType.Int32, countType);
            db.AddInParameter(dbc, "DateState", DbType.Int32, dateState);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
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
        public void GetDuiBiData(out int adds, out int orders, out int arrive, DateTime beginTime, DateTime endTime, string section, int countType)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_GETDUIBIDATA);
            db.AddOutParameter(dbc, "addData", DbType.Int32, 0);
            db.AddOutParameter(dbc, "orderData", DbType.Int32, 0);
            db.AddOutParameter(dbc, "arriveData", DbType.Int32, 0);

            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            db.AddInParameter(dbc, "CountType", DbType.Int32, countType);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                adds = (int)db.GetParameterValue(dbc, "addData");
                orders = (int)db.GetParameterValue(dbc, "orderData");
                arrive = (int)db.GetParameterValue(dbc, "arriveData");
            }
        }

        //
        /// <summary>
        /// 疾病统计报表
        /// </summary>
        /// <param name="section">科室</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public DataSet GetDiseaseByHospital(string section, DateTime beginTime, DateTime endTime)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_GETDISEASEDATA);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
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
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_GETDISEASE);
            db.AddInParameter(dbc, "startTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "endTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            db.AddInParameter(dbc, "IsAddANdArrive", DbType.Boolean, IsAddANdArrive);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
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
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_AREA_BENDI);
            db.AddOutParameter(dbc, "BenDiAdds", DbType.Int32, 0);
            db.AddOutParameter(dbc, "BenDiArrive", DbType.Int32, 0);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                BenDiAdds = (int)db.GetParameterValue(dbc, "BenDiAdds");
                BenDiArrive = (int)db.GetParameterValue(dbc, "BenDiArrive");
            }
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
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_AREA);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
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
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_MEDIA);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "Section", DbType.String, sectionStr);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospital);
            db.AddInParameter(dbc, "ParentID", DbType.Int32, parentID);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
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
        internal void GetIndexData(out int adddate, out int orderdata, out int arrivedata, DateTime beginTime, DateTime endTime, string section, int countType)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_INDEXDATA);
            db.AddOutParameter(dbc, "addData", DbType.Int32, 0);
            db.AddOutParameter(dbc, "orderData", DbType.Int32, 0);
            db.AddOutParameter(dbc, "arriveData", DbType.Int32, 0);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            db.AddInParameter(dbc, "CountType", DbType.Int32, countType);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                adddate = (int)db.GetParameterValue(dbc, "addData");
                orderdata = (int)db.GetParameterValue(dbc, "orderData");
                arrivedata = (int)db.GetParameterValue(dbc, "arriveData");
            }
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
        internal void GetIndexDataByUserID(out int adddate, out int orderdata, out int arrivedata, int createUserID, System.DateTime beginTime, System.DateTime endTime, string section, int CountType)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_DATA_BYUSERID);
            db.AddOutParameter(dbc, "addData", DbType.Int32, 0);
            db.AddOutParameter(dbc, "orderData", DbType.Int32, 0);
            db.AddOutParameter(dbc, "arriveData", DbType.Int32, 0);
            db.AddInParameter(dbc, "UserID", DbType.Int32, createUserID);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            db.AddInParameter(dbc, "CountType", DbType.Int32, CountType);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                adddate = (int)db.GetParameterValue(dbc, "addData");
                orderdata = (int)db.GetParameterValue(dbc, "orderData");
                arrivedata = (int)db.GetParameterValue(dbc, "arriveData");
            }
        }


        public DataSet GetOrderByDoctor(int Doctor, DateTime starttime, DateTime endtime, string sectionStr)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_ORDER_BY_DOCTOR);
            db.AddInParameter(dbc, "DoctorID", DbType.Int32, Doctor);
            db.AddInParameter(dbc, "StartTime", DbType.DateTime, starttime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endtime);
            db.AddInParameter(dbc, "Section", DbType.String, sectionStr);

            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
        }
    }
}
