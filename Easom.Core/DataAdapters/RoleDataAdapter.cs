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
    /// RoleDataAdapter  数据库访问类
    ///</summary>
    internal class RoleDataAdapter
    {
        private readonly string _dbName = "CoreDB";
        private const string SQL_INSERT = "usp_Role_Insert";
        private const string SQL_DELETE = "usp_Role_Delete";
        private const string SQL_UPDATE = "usp_Role_Update";
        private const string SQL_GET_BY_ID = "usp_Role_GetByID";
        private const string SQL_SEARCH = "usp_Role_Search";
        private const string _fields = "ID,Name,HospitalID,Remark,ToRole";
        private const string SQL_GET_BY_ROLEID = "usp_Role_GetByAuthorID";
        private const string SQL_DELETE_BY_ROLEID = "usp_RoleToAuthority_Delete";
        private const string SQL_GET_BY_USERID = "usp_Role_GetByUserID";
        private const string SQL_GET_BY_ALLDATA = "usp_Role_GetByAllRole";
        private const string SQL_GET_BY_NAME = "usp_Role_GetByName";
        ///<summary>
        /// 从IDataReader中读取一个实体对象
        ///</summary>
        private Role GetByReader(IDataReader dr)
        {
            Role entity = new Role();
            object obj = null;
            obj = dr["ID"];
            if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
            obj = dr["Name"];
            entity.Name = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
            obj = dr["HospitalID"];
            if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
            obj = dr["Remark"];
            entity.Remark = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Remark = Convert.ToString(obj); }
            obj = dr["ToRole"];
            entity.ToRole = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.ToRole = Convert.ToString(obj); }
            return entity;
        }

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(Role entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddOutParameter(dbc, "ID", DbType.Int32, 4);
            db.AddInParameter(dbc, "Name", DbType.String, entity.Name);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, entity.HospitalID);
            db.AddInParameter(dbc, "Remark", DbType.String, entity.Remark);
            db.AddInParameter(dbc, "ToRole", DbType.String, entity.ToRole);
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
        public void Update(Role entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
            db.AddInParameter(dbc, "ID", DbType.Int32, entity.ID);
            db.AddInParameter(dbc, "Name", DbType.String, entity.Name);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, entity.HospitalID);
            db.AddInParameter(dbc, "Remark", DbType.String, entity.Remark);
            db.AddInParameter(dbc, "ToRole", DbType.String, entity.ToRole);
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
        public Role GetByID(int id)
        {
            Role entity = null;
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
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public IList<Role> GetAllData()
        {
            IList<Role> list =new List<Role>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ALLDATA);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while(dr.Read())
                {
                    Role entity=new Role();
                    list.Add(GetByReader(dr));
                }
            }
            return list;
        }

        /// <summary>
        /// GetByName
        /// </summary>
        public Role GetByName(string name)
        {
            Role entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_NAME);
            db.AddInParameter(dbc, "Name", DbType.String, name);
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
        /// GetByRoleID
        /// </summary>
        public IList<int> GetByRoleID(int id)
        {
            IList<int> entity = new List<int>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ROLEID);
            db.AddInParameter(dbc, "ID", DbType.Int32, id);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    entity.Add(ConvertUtility.ToInt32(dr["AuthorID"], -1));
                }
            }
            return entity;
        }
        /// <summary>
        /// GetByUserID
        /// </summary>
        public IList<Role> GetByUserID(int id)
        {
            IList<Role> entityList = new List<Role>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_USERID);
            db.AddInParameter(dbc, "ID", DbType.Int32, id);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                if (dr.Read())
                {
                    Role entity = GetByReader(dr);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        /// <summary>
        /// DeleteByRoleID
        /// </summary>
        public bool DeleteByRoleID(int id)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE_BY_ROLEID);
            db.AddInParameter(dbc, "RoleID", DbType.Int32, id);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;
        }




        ///<summary>
        /// 搜索记录
        ///</summary>
        public IList<Role> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<Role> lst = new List<Role>();
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
                    Role ent = new Role();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }

    }
}
