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
    /// HospitalDataAdapter  数据库访问类
    ///</summary>
    internal class HospitalDataAdapter
    {
        private readonly string _dbName = "CoreDB";
        private const string SQL_INSERT = "usp_Hospital_Insert";
        private const string SQL_DELETE = "usp_Hospital_Delete";
        private const string SQL_UPDATE = "usp_Hospital_Update";
        private const string SQL_GET_BY_ID = "usp_Hospital_GetByID";
        private const string SQL_SEARCH = "usp_Hospital_Search";
        private const string _fields = "ID,AreaID,Name,Intro,OrderNumber,HSafeCode,PassWord,IsOpenPassWord";
        private const string SQL_GET_BY_USERID = "usp_Hospital_GetByUserID";
        private const string SQL_GET_BY_ALLHOSPITAL = "usp_Hospital_GetALL";
        private const string SQL_INSERT_USER_TO_HOSPITAL = "usp_UserToHospital_Insert";
        ///<summary>
        /// 从IDataReader中读取一个实体对象
        ///</summary>
        private Hospital GetByReader(IDataReader dr)
        {
            Hospital entity = new Hospital();
            object obj = null;
            obj = dr["ID"];
            if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
            obj = dr["AreaID"];
            if (obj != null && obj != DBNull.Value) { entity.AreaID = Convert.ToInt32(obj); }
            obj = dr["Name"];
            entity.Name = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
            obj = dr["Intro"];
            entity.Intro = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Intro = Convert.ToString(obj); }
            obj = dr["OrderNumber"];
            entity.OrderNumber = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.OrderNumber = Convert.ToString(obj); }
            obj = dr["HSafeCode"];
            entity.HSafeCode = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.HSafeCode = Convert.ToString(obj); }
            obj = dr["PassWord"];
            entity.PassWord = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.PassWord = Convert.ToString(obj); }
            obj = dr["IsOpenPassWord"];
            if (obj != null && obj != DBNull.Value) { entity.IsOpenPassWord = Convert.ToInt32(obj); }
            return entity;
        }

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(Hospital entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddOutParameter(dbc, "ID", DbType.Int32, 4);
            db.AddInParameter(dbc, "AreaID", DbType.Int32, entity.AreaID);
            db.AddInParameter(dbc, "Name", DbType.String, entity.Name);
            db.AddInParameter(dbc, "Intro", DbType.String, entity.Intro);
            db.AddInParameter(dbc, "OrderNumber", DbType.String, entity.OrderNumber);
            db.AddInParameter(dbc, "HSafeCode", DbType.String, entity.HSafeCode);
            db.AddInParameter(dbc, "PassWord", DbType.String, entity.PassWord);
            db.AddInParameter(dbc, "IsOpenPassWord", DbType.Int32, entity.IsOpenPassWord);
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
        public void Update(Hospital entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
            db.AddInParameter(dbc, "ID", DbType.Int32, entity.ID);
            db.AddInParameter(dbc, "AreaID", DbType.Int32, entity.AreaID);
            db.AddInParameter(dbc, "Name", DbType.String, entity.Name);
            db.AddInParameter(dbc, "Intro", DbType.String, entity.Intro);
            db.AddInParameter(dbc, "OrderNumber", DbType.String, entity.OrderNumber);
            db.AddInParameter(dbc, "HSafeCode", DbType.String, entity.HSafeCode);
            db.AddInParameter(dbc, "PassWord", DbType.String, entity.PassWord);
            db.AddInParameter(dbc, "IsOpenPassWord", DbType.Int32, entity.IsOpenPassWord);
            try
            {
                db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }

        }

        public IList<Hospital> GetByALLHospital()
        {
            IList<Hospital> entitylist = new List<Hospital>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ALLHOSPITAL);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Hospital entity = GetByReader(dr);
                    entitylist.Add(entity);
                }
            }
            return entitylist;

        }
        public IList<Hospital> GetByUserID(int id, int isDefault)
        {
            IList<Hospital> entitylist = new List<Hospital>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_USERID);
            db.AddInParameter(dbc, "ID", DbType.Int32, id);
            db.AddInParameter(dbc, "IsDefault", DbType.Int32, isDefault);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Hospital entity = GetByReader(dr);
                    entitylist.Add(entity);
                }
            }
            return entitylist;
        }

        ///<summary>
        /// 读取一条记录
        ///</summary>
        public Hospital GetByID(int id)
        {
            Hospital entity = null;
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
        public IList<Hospital> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            return Search(out pageCount, _fields, pageIndex, pageSize, where, orderField, isDesc);
        }
        ///<summary>
        /// 搜索记录,可自由选择字段值【ID,AreaID,Name,Intro,OrderNumber,HSafeCode,PassWord】
        ///</summary>
        public IList<Hospital> Search(out int pageCount,string fields, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<Hospital> lst = new List<Hospital>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_SEARCH);
            db.AddOutParameter(dbc, "pageCount", DbType.Int32, 4);
            db.AddInParameter(dbc, "pageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(dbc, "pageSize", DbType.Int32, pageSize);
            db.AddInParameter(dbc, "fields", DbType.String, fields);
            db.AddInParameter(dbc, "where", DbType.String, where);
            db.AddInParameter(dbc, "orderField", DbType.String, orderField);
            db.AddInParameter(dbc, "isDesc", DbType.Boolean, isDesc);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Hospital ent = new Hospital();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }

        public bool InsertUserToHospital(int userID, int hospitalID, bool isDefault)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT_USER_TO_HOSPITAL);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospitalID);
            db.AddInParameter(dbc, "IsDefault", DbType.Boolean, isDefault);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;
        }

    }
}
