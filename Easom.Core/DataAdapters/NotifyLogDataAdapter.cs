// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-05-19
// ���ߣ�loskiv@gmail.com

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CHCMS.Utility;

namespace Easom.Core.DataAdapters
{

    ///<summary>
    /// NotifyLogDataAdapter  ���ݿ������
    ///</summary>
    internal class NotifyLogDataAdapter
    {
        private readonly string _dbName = "CoreDB";
        private const string SQL_INSERT = "usp_NotifyLog_Insert";
        private const string SQL_DELETE = "usp_NotifyLog_Delete";
        private const string SQL_UPDATE = "usp_NotifyLog_Update";
        private const string SQL_GET_BY_ID = "usp_NotifyLog_GetByID";
        private const string SQL_SEARCH = "usp_NotifyLog_Search";
        private const string SQL_GET_BY_USERID = "usp_Notifier_GetByUserID";
        private const string SQL_GET_BY_TOPGETBYID = "usp_NotifyLog_TopGetByID";
        private const string _fields = "ID,UserID,SendState,SendDate";

        ///<summary>
        /// ��IDataReader�ж�ȡһ��ʵ�����
        ///</summary>
        private NotifyLog GetByReader(IDataReader dr)
        {
            NotifyLog entity = new NotifyLog();
            object obj = null;
            obj = dr["ID"];
            if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
            obj = dr["UserID"];
            if (obj != null && obj != DBNull.Value) { entity.UserID = Convert.ToInt32(obj); }
            obj = dr["SendState"];
            if (obj != null && obj != DBNull.Value) { entity.SendState = Convert.ToInt32(obj); }
            obj = dr["SendDate"];
            if (obj != null && obj != DBNull.Value) { entity.SendDate = Convert.ToDateTime(obj); }
            return entity;
        }

        ///<summary>
        /// ����һ����¼
        ///</summary>
        public int Insert(NotifyLog entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddOutParameter(dbc, "ID", DbType.Int32, entity.ID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, entity.UserID);
            db.AddInParameter(dbc, "SendState", DbType.Int32, entity.SendState);
            db.AddInParameter(dbc, "SendDate", DbType.DateTime, entity.SendDate);

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
        public void Update(NotifyLog entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
            db.AddInParameter(dbc, "ID", DbType.Int32, entity.ID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, entity.UserID);
            db.AddInParameter(dbc, "SendState", DbType.Int32, entity.SendState);
            db.AddInParameter(dbc, "SendDate", DbType.DateTime, entity.SendDate);

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
        /// ��ȡ���м�¼
        ///</summary>
        public IList<NotifyLog> GetByID(int userID)
        {
            IList<NotifyLog> lst = new List<NotifyLog>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    NotifyLog ent = new NotifyLog();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }
        /// <summary>
        /// ��ȡһ����¼
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public NotifyLog GetByUserID(int userID)
        {
            NotifyLog entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_USERID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    entity = GetByReader(dr);
                }
            }
            return entity;
        }

        /// <summary>
        /// ��ȡһ�����µļ�¼
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public NotifyLog TopGetByUserID(int userID)
        {
            NotifyLog entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_TOPGETBYID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    entity = GetByReader(dr);
                }
            }
            return entity;
        }

        ///<summary>
        /// ������¼
        ///</summary>
        public IList<NotifyLog> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<NotifyLog> lst = new List<NotifyLog>();
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
                    NotifyLog ent = new NotifyLog();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }
    }
}
