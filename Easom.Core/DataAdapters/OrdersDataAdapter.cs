// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com

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
    /// OrdersDataAdapter  ���ݿ������
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
        /// ��IDataReader�ж�ȡһ��ʵ�����
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
        /// ����һ����¼
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
        /// ɾ��һ����¼
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
        /// ����һ����¼
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
        /// ����һ����¼
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
        /// ��ȡһ����¼
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
        /// ������¼
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
        /// ������¼
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
        /// ͨ������ȡ���������������Ĳ���
        /// </summary>
        /// <param name="hospitalID">����ҽԺ</param>
        /// <param name="name">��������</param>
        /// <param name="startime">��ʼʱ��</param>
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
        /// ͨ���绰ȡ�����������ظ��Ĳ���
        /// </summary>
        /// <param name="hospitalID">����ҽԺ</param>
        /// <param name="telephone">���˵绰</param>
        /// <param name="startime">��ʼʱ��</param>
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
        /// ���ݽ�ɫ��ȡԤԼ���ݵ���Excel
        /// </summary>
        /// <param name="role">��ɫ</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet����</returns>
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
        /// ��ͳ�Ʊ���
        /// </summary>
        /// <param name="add">ԤԼ����</param>
        /// <param name="arrive">ʵ������</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="countType">ͳ���˻�</param>
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
        /// ��ͳ�Ʊ���
        /// </summary>
        /// <param name="add">ԤԼ����</param>
        /// <param name="arrive">ʵ������</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="countType">ͳ���˻�</param>
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
        /// ����ͳ�Ʊ���
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
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
        /// ����ͳ�Ʊ���
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
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
        /// ������Դ����
        /// </summary>
        /// <param name="BenDiAdds">�������</param>
        /// <param name="BenDiArrive">�����ѵ�</param>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
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
        /// ������Դ����
        /// </summary>
        /// <param name="section">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>DataSet����</returns>
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
        /// ý��ͳ�Ʊ���
        /// </summary>
        /// <param name="sectionStr">����</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
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
        /// ��ҳ
        /// </summary>
        /// <param name="adddate">���</param>
        /// <param name="orderdata">ԤԼ</param>
        /// <param name="arrivedata">��Ժ</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="section">����</param>
        /// <param name="CountType">ͳ���˻�</param>
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
