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
namespace Easom.Core.DataAdapters
{

    ///<summary>
    /// CallOnDataDataAdapter  数据库访问类
    ///</summary>
    internal class CallOnDataDataAdapter
    {
        private readonly string _dbName = "CoreDB";
        private const string SQL_INSERT = "usp_CallOnData_Insert";
        private const string SQL_DELETE = "usp_CallOnData_Delete";
        private const string SQL_UPDATE = "usp_CallOnData_Update";
        private const string SQL_GET_BY_ID = "usp_CallOnData_GetByID";
        private const string SQL_GET_BY_ORDER_ID = "usp_CallOnData_GetByOrderID";
        private const string SQL_GET_NEW_BY_ORDER_ID = "usp_CallOnData_GetNewDataByOrderID";
        private const string SQL_GET_LASTDATA = "usp_CallOnData_GetByLastData";
        private const string SQL_SEARCH = "usp_CallOnData_Search";
        private const string SQL_DELETE_REMIND = "usp_CallOnData_Delete_Remind";
        private const string SQL_DELETE_ORDERSID = "usp_CallOnData_Delete_OrdersID";
        private const string SQL_GET_BY_USER_ID = "usp_CallOnData_ByUserID";
        private const string SQL_GET_BY_USER_ID_ALLDATA = "usp_CallOnData_ByAllData";
        private const string _fields = "ID,OrdersID,IsValid,CallOnDateTime,CallUserID,IsDelete,Remark,IsCallOn";

        ///<summary>
        /// 从IDataReader中读取一个实体对象
        ///</summary>
        private CallOnData GetByReader(IDataReader dr)
        {
            CallOnData entity = new CallOnData();
            object obj = null;
            obj = dr["ID"];
            if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
            obj = dr["OrdersID"];
            if (obj != null && obj != DBNull.Value) { entity.OrdersID = Convert.ToInt32(obj); }
            obj = dr["IsValid"];
            if (obj != null && obj != DBNull.Value) { entity.IsValid = Convert.ToInt32(obj); }
            obj = dr["CallOnDateTime"];
            if (obj != null && obj != DBNull.Value) { entity.CallOnDateTime = Convert.ToDateTime(obj); }
            obj = dr["CallUserID"];
            if (obj != null && obj != DBNull.Value) { entity.CallUserID = Convert.ToInt32(obj); }
            obj = dr["IsDelete"];
            if (obj != null && obj != DBNull.Value) { entity.IsDelete = Convert.ToInt32(obj); }
            obj = dr["Remark"];
            entity.Remark = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Remark = Convert.ToString(obj); }
            obj = dr["IsCallOn"];
            if (obj != null && obj != DBNull.Value) { entity.IsCallOn = Convert.ToBoolean(obj); }
            return entity;
        }

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(CallOnData entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddOutParameter(dbc, "ID", DbType.Int32, entity.ID);
            db.AddInParameter(dbc, "OrdersID", DbType.Int32, entity.OrdersID);
            db.AddInParameter(dbc, "IsValid", DbType.Int32, entity.IsValid);
            db.AddInParameter(dbc, "CallOnDateTime", DbType.DateTime, entity.CallOnDateTime);
            db.AddInParameter(dbc, "CallUserID", DbType.Int32, entity.CallUserID);
            db.AddInParameter(dbc, "IsDelete", DbType.Int32, entity.IsDelete);
            db.AddInParameter(dbc, "Remark", DbType.String, entity.Remark);
            db.AddInParameter(dbc, "IsCallOn", DbType.Boolean, entity.IsCallOn);
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

        /// <summary>
        /// 删除提醒
        /// </summary>
        /// <param name="ordersID">预约ID</param>
        /// <param name="callUserID">回访用户ID</param>
        /// <param name="datetime">时间</param>
        /// <param name="IsValid">0表示预约数据，1表示个人资源数据</param>
        public void DeleteRamind(int ordersID, int callUserID, DateTime datetime)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE_REMIND);
            db.AddInParameter(dbc, "OrdersID", DbType.Int32, ordersID);
            db.AddInParameter(dbc, "CallUserID", DbType.Int32, callUserID);
            db.AddInParameter(dbc, "DateTime", DbType.DateTime, datetime);
            try
            {
                db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除提醒
        /// </summary>
        /// <param name="ordersID">预约ID</param>
        public void DeleteRamind(int ordersID)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE_ORDERSID);
            db.AddInParameter(dbc, "OrdersID", DbType.Int32, ordersID);
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
        public void Update(CallOnData entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
            db.AddInParameter(dbc, "ID", DbType.Int32, entity.ID);
            db.AddInParameter(dbc, "OrdersID", DbType.Int32, entity.OrdersID);
            db.AddInParameter(dbc, "IsValid", DbType.Int32, entity.IsValid);
            db.AddInParameter(dbc, "CallOnDateTime", DbType.DateTime, entity.CallOnDateTime);
            db.AddInParameter(dbc, "CallUserID", DbType.Int32, entity.CallUserID);
            db.AddInParameter(dbc, "IsDelete", DbType.Int32, entity.IsDelete);
            db.AddInParameter(dbc, "Remark", DbType.String, entity.Remark);
            db.AddInParameter(dbc, "IsCallOn", DbType.Boolean, entity.IsCallOn);

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
        public CallOnData GetByID(int id)
        {
            CallOnData entity = null;
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

        /// <summary>
        /// 通过预约数据取出所有回访记录
        /// </summary>
        /// <param name="oderId">预约ID</param>
        /// <returns>CallOnData集合</returns>
        public IList<CallOnData> GetByOrderID(int ordersId, int state)
        {
            IList<CallOnData> lst = new List<CallOnData>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ORDER_ID);
            db.AddInParameter(dbc, "OrdersID", DbType.Int32, ordersId);
            db.AddInParameter(dbc, "IsCallOn", DbType.Boolean, state);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    CallOnData ent = new CallOnData();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }

        /// <summary>
        /// 通过预约数据取出最新的回访记录
        /// </summary>
        /// <param name="orderId">预约ID</param>
        /// <returns>CallOnData</returns>
        public CallOnData GetNewDataByOrderID(int ordersId)
        {
            CallOnData entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_NEW_BY_ORDER_ID);
            db.AddInParameter(dbc, "OrdersID", DbType.Int32, ordersId);
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
        /// 取出预约数据最新的回访数据
        /// </summary>
        /// <param name="ordersID">预约ID</param>
        /// <param name="state">提醒还是普通预约0，为提醒，1为普通</param>
        /// <returns>CallOnData</returns>
        public CallOnData GetLastDataByOrderID(int ordersID, int state)
        {
            CallOnData entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_LASTDATA);
            db.AddInParameter(dbc, "OrdersID", DbType.Int32, ordersID);
            db.AddInParameter(dbc, "IsCallOn", DbType.Boolean, state);
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
        /// 根据用户取出最新的数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="state">提醒还是普通预约0，为提醒，1为普通</param>
        /// <returns>CallOnData泛型集合</returns>
        public IList<CallOnData> GetByUserIDAndState(int userID, int state, int IsValid,string section)
        {
            IList<CallOnData> lst = new List<CallOnData>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_USER_ID);
            db.AddInParameter(dbc, "CallUserID", DbType.Int32, userID);
            db.AddInParameter(dbc, "IsCallOn", DbType.Boolean, state);
            db.AddInParameter(dbc, "IsValid", DbType.Int32, IsValid);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    CallOnData ent = new CallOnData();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }

        /// <summary>
        /// 根据用户取出最新的数据
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="state">提醒还是普通预约0，为提醒，1为普通</param>
        /// <returns>CallOnData泛型集合</returns>
        public IList<CallOnData> GetByUserIDAllCallOnData(int userID, int state, int IsValid, string section)
        {
            IList<CallOnData> lst = new List<CallOnData>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_USER_ID_ALLDATA);
            db.AddInParameter(dbc, "CallUserID", DbType.Int32, userID);
            db.AddInParameter(dbc, "IsCallOn", DbType.Boolean, state);
            db.AddInParameter(dbc, "IsValid", DbType.Int32, IsValid);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    CallOnData ent = new CallOnData();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }


        ///<summary>
        /// 搜索记录
        ///</summary>
        public IList<CallOnData> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<CallOnData> lst = new List<CallOnData>();
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
                    CallOnData ent = new CallOnData();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }

    }
}
