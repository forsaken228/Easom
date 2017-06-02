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
    /// UserToSectionDataAdapter  数据库访问类
    ///</summary>
    internal class UserToSectionDataAdapter
    {
        private readonly string _dbName = "CoreDB";
        private const string SQL_INSERT = "usp_UserToSection_Insert";
        private const string SQL_DELETE = "usp_UserToSection_Delete";
        private const string SQL_UPDATE = "usp_UserToSection_Update";
        private const string SQL_GET_BY_ID = "usp_UserToSection_GetByID";
        private const string SQL_SEARCH = "usp_UserToSection_Search";
        private const string _fields = "UserID,SectionID,IsDefault";
        private const string SQL_DELETE_BY_USERIDANDSECTIONID = "usp_UserToSection_DeleteByUserIDAndSectionID";
        private const string SQL_GET_BY_SECTIONID_USERID = "usp_UserToSection_GetByUserIDAndSectionID";
        private const string SQL_UPDATE_IsDefault = "usp_UserToSection_UpdateIsDefault";
        ///<summary>
        /// 从IDataReader中读取一个实体对象
        ///</summary>
        private UserToSection GetByReader(IDataReader dr)
        {
            UserToSection entity = new UserToSection();
            object obj = null;
            obj = dr["UserID"];
            if (obj != null && obj != DBNull.Value) { entity.UserID = Convert.ToInt32(obj); }
            obj = dr["SectionID"];
            if (obj != null && obj != DBNull.Value) { entity.SectionID = Convert.ToInt32(obj); }
            obj = dr["IsDefault"];
            if (obj != null && obj != DBNull.Value) { entity.IsDefault = Convert.ToBoolean(obj); }
            return entity;
        }

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(UserToSection entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddInParameter(dbc, "UserID", DbType.Int32, entity.UserID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, entity.SectionID);
            db.AddInParameter(dbc, "IsDefault", DbType.Boolean, entity.IsDefault);

            try
            {
                db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }

            return -1;
        }

        public UserToSection GetBySectionIDAndUserID(int userID, int sectionID)
        {
            UserToSection entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_SECTIONID_USERID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, sectionID);
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
        /// 删除一条记录
        ///</summary>
        public void Delete(int id)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE);
            db.AddInParameter(dbc, "UserID", DbType.Int32, id);

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
        /// 删除一条记录
        ///</summary>
        public void DeleteBySectionIDAndUserID(int userID, int sectionID)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE_BY_USERIDANDSECTIONID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, sectionID);
            try
            {
                db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }

        }


        public bool UpdateUserToSection(int userID, int sectionID, bool isDefault)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE_IsDefault);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, sectionID);
            db.AddInParameter(dbc, "IsDefault", DbType.Boolean, isDefault);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;
        }


        ///<summary>
        /// 更新一条记录
        ///</summary>
        public void Update(UserToSection entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
            db.AddInParameter(dbc, "UserID", DbType.Int32, entity.UserID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, entity.SectionID);
            db.AddInParameter(dbc, "IsDefault", DbType.Boolean, entity.IsDefault);

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
        public IList<UserToSection> GetByUserID(int id)
        {
            IList<UserToSection> entitylist = new List<UserToSection>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, id);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    UserToSection entity = GetByReader(dr);
                    entitylist.Add(entity);
                }
            }
            return entitylist;
        }

        ///<summary>
        /// 搜索记录
        ///</summary>
        public IList<UserToSection> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<UserToSection> lst = new List<UserToSection>();
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
                    UserToSection ent = new UserToSection();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }

    }
}
