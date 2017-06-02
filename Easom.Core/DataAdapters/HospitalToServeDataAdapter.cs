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
    /// HospitalToServeDataAdapter  数据库访问类
    ///</summary>
    internal class HospitalToServeDataAdapter
    {
        private readonly string _dbName = "CoreDB";
        private const string SQL_INSERT = "usp_HospitalToServe_Insert";
        private const string SQL_DELETE = "usp_HospitalToServe_Delete";
        private const string SQL_UPDATE = "usp_HospitalToServe_Update";
        private const string SQL_GET_BY_ID = "usp_HospitalToServe_GetByID";
        private const string SQL_SEARCH = "usp_HospitalToServe_Search";
        private const string SQL_GET_BY_HOSPITALID_SERVERID = "usp_HospitalToServe_GetByHospitalIdAndServeID";
        private const string _fields = "ID,HospitalID,ServeKey,DateType,Num,ExpirationDate,CreateTime";

        ///<summary>
        /// 从IDataReader中读取一个实体对象
        ///</summary>
        private HospitalToServe GetByReader(IDataReader dr)
        {
            HospitalToServe entity = new HospitalToServe();
            object obj = null;
            obj = dr["ID"];
            if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
            obj = dr["HospitalID"];
            if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
            obj = dr["ServeKey"];
            if (obj != null && obj != DBNull.Value) { entity.ServeKey = Convert.ToString(obj); }
            obj = dr["DateType"];
            if (obj != null && obj != DBNull.Value) { entity.DateType = (ServeDateTypeEnum)Convert.ToInt32(obj); }
            obj = dr["Num"];
            if (obj != null && obj != DBNull.Value) { entity.Num = Convert.ToInt32(obj); }
            obj = dr["ExpirationDate"];
            if (obj != null && obj != DBNull.Value) { entity.ExpirationDate = Convert.ToDateTime(obj); }
            obj = dr["CreateTime"];
            if (obj != null && obj != DBNull.Value) { entity.CreateTime = Convert.ToDateTime(obj); }
            return entity;
        }

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(HospitalToServe entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddOutParameter(dbc, "ID", DbType.Int32, 4);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, entity.HospitalID);
            db.AddInParameter(dbc, "ServeKey", DbType.String, entity.ServeKey);
            db.AddInParameter(dbc, "DateType", DbType.Int32, (int)entity.DateType);
            db.AddInParameter(dbc, "Num", DbType.Int32, entity.Num);
            db.AddInParameter(dbc, "ExpirationDate", DbType.DateTime, entity.ExpirationDate);
            db.AddInParameter(dbc, "CreateTime", DbType.DateTime, entity.CreateTime);

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
        public void Update(HospitalToServe entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
            db.AddInParameter(dbc, "ID", DbType.Int32, entity.ID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, entity.HospitalID);
            db.AddInParameter(dbc, "ServeKey", DbType.String, entity.ServeKey);
            db.AddInParameter(dbc, "DateType", DbType.Int32, (int)entity.DateType);
            db.AddInParameter(dbc, "Num", DbType.Int32, entity.Num);
            db.AddInParameter(dbc, "ExpirationDate", DbType.DateTime, entity.ExpirationDate);
            db.AddInParameter(dbc, "CreateTime", DbType.DateTime, entity.CreateTime);

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
        public HospitalToServe GetByID(int id)
        {
            HospitalToServe entity = null;
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
        public IList<HospitalToServe> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<HospitalToServe> lst = new List<HospitalToServe>();
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
                    HospitalToServe ent = new HospitalToServe();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }

        /// <summary>
        /// 获取一条医院服务信息
        /// </summary>
        /// <param name="hospital">医院ID</param>
        /// <param name="serverID">服务ID</param>
        /// <returns>该服务其他信息</returns>
        public HospitalToServe GetByHospitalID(int hospital, string serverKey)
        {
            HospitalToServe entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_HOSPITALID_SERVERID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospital);
            db.AddInParameter(dbc, "ServeKey", DbType.String, serverKey);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                if (dr.Read())
                {
                    entity = GetByReader(dr);
                }
            }
            return entity;
        }

    }
}
