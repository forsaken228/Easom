// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-05-19
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
    /// NotifierDataAdapter  数据库访问类
    ///</summary>
    internal class NotifierDataAdapter
    {
        private readonly string _dbName = "CoreDB";
        private const string SQL_INSERT = "usp_Notifier_Insert";
        private const string SQL_DELETE = "usp_Notifier_Delete";
        private const string SQL_UPDATE = "usp_Notifier_Update";
        private const string SQL_GET_BY_ID = "usp_Notifier_GetByID";
        private const string SQL_GETALLDATA = "usp_Notifier_GetAllData";
        private const string SQL_SEARCH = "usp_Notifier_Search";
        private const string SQL_NOTIFIER_GETALL = "usp_Notifier_GetAllNotifier";
        private const string SQL_NOTIFIERNOTIFYLOG_SEARCH = "usp_nv_Notifier_UserInfo_NotifyLog_Search";
        private const string _fields = "ID,UserID,Name,TrueName,NotifyType,SendState,NotifyCycle,Disable,SendDate,CreateDate";
        private const string _searchfields = "ID,UserID,NotifyType,NotifyCycle,Disable,CreateDate";
        ///<summary>
        /// 从IDataReader中读取一个实体对象
        ///</summary>
        private Notifier GetByReader(IDataReader dr)
        {
            Notifier entity = new Notifier();
            object obj = null;
            obj = dr["ID"];
            if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
            obj = dr["UserID"];
            if (obj != null && obj != DBNull.Value) { entity.UserID = Convert.ToInt32(obj); }
            if (ReaderExists(dr, "Name"))
            {
                obj = dr["Name"];
                if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
            }
            if (ReaderExists(dr, "TrueName"))
            {
                obj = dr["TrueName"];
                if (obj != null && obj != DBNull.Value) { entity.TrueName = Convert.ToString(obj); }
            }
            obj = dr["NotifyType"];
            if (obj != null && obj != DBNull.Value) { entity.NotifyType = (NotifyTypeEnum)Convert.ToInt32(obj); }
            if (ReaderExists(dr, "SendState"))
            {
                obj = dr["SendState"];
                if (obj != null && obj != DBNull.Value) { entity.SendState = (SendState)Convert.ToInt32(obj); }
            }
            obj = dr["NotifyCycle"];
            if (obj != null && obj != DBNull.Value) { entity.NotifyCycle = Convert.ToInt32(obj); }
            obj = dr["Disable"];
            if (obj != null && obj != DBNull.Value) { entity.Disable = Convert.ToBoolean(obj); }
            if (ReaderExists(dr, "SendDate"))
            {
                obj = dr["SendDate"];
                if (obj != null && obj != DBNull.Value) { entity.SendDate = Convert.ToDateTime(obj); }
            }
            obj = dr["CreateDate"];
            if (obj != null && obj != DBNull.Value) { entity.CreateDate = Convert.ToDateTime(obj); }
            return entity;
        }

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(Notifier entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddOutParameter(dbc, "ID", DbType.Int32, 4);
            db.AddInParameter(dbc, "UserID", DbType.Int32, entity.UserID);
            db.AddInParameter(dbc, "NotifyType", DbType.Int32, entity.NotifyType);
            db.AddInParameter(dbc, "NotifyCycle", DbType.Int32, entity.NotifyCycle);
            db.AddInParameter(dbc, "Disable", DbType.Boolean, entity.Disable);
            db.AddInParameter(dbc, "CreateDate", DbType.DateTime, entity.CreateDate);

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
        public bool Delete(int id)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE);
            db.AddInParameter(dbc, "ID", DbType.Int32, id);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;
        }

        ///<summary>
        /// 更新一条记录
        ///</summary>
        public bool Update(Notifier entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
            db.AddInParameter(dbc, "ID", DbType.Int32, entity.ID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, entity.UserID);
            db.AddInParameter(dbc, "NotifyType", DbType.Int32, entity.NotifyType);
            db.AddInParameter(dbc, "NotifyCycle", DbType.Int32, entity.NotifyCycle);
            db.AddInParameter(dbc, "Disable", DbType.Boolean, entity.Disable);
            db.AddInParameter(dbc, "CreateDate", DbType.DateTime, entity.CreateDate);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;

        }

        ///<summary>
        /// 读取一条记录
        ///</summary>
        public Notifier GetByID(int id)
        {
            Notifier entity = null;
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
        public IList<Notifier> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<Notifier> lst = new List<Notifier>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_SEARCH);
            db.AddOutParameter(dbc, "pageCount", DbType.Int32, 4);
            db.AddInParameter(dbc, "pageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(dbc, "pageSize", DbType.Int32, pageSize);
            db.AddInParameter(dbc, "fields", DbType.String, _searchfields);
            db.AddInParameter(dbc, "where", DbType.String, where);
            db.AddInParameter(dbc, "orderField", DbType.String, orderField);
            db.AddInParameter(dbc, "isDesc", DbType.Boolean, isDesc);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Notifier ent = new Notifier();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }
        ///<summary>
        /// 视图搜索记录
        ///</summary>
        public IList<Notifier> NotifierNotifyLogSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<Notifier> lst = new List<Notifier>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_NOTIFIERNOTIFYLOG_SEARCH);
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
                    Notifier ent = new Notifier();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }

        public bool ReaderExists(IDataReader dr, string columnName)
        {
            dr.GetSchemaTable().DefaultView.RowFilter = string.Format("ColumnName='{0}'", columnName);
            return (dr.GetSchemaTable().DefaultView.Count > 0);
        }

        /// <summary>
        /// 获取当前所有的信息用户
        /// </summary>
        /// <returns></returns>
        public IList<Notifier> GetAllData()
        {
            IList<Notifier> lst = new List<Notifier>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GETALLDATA);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Notifier ent = new Notifier();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }
    }
}
