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
    /// UserInfoDataAdapter  数据库访问类
    ///</summary>
    internal class UserInfoDataAdapter
    {
        private readonly string _dbName = "CoreDB";
        private const string SQL_INSERT = "usp_UserInfo_Insert";
        private const string SQL_DELETE = "usp_UserInfo_Delete";
        private const string SQL_UPDATE = "usp_UserInfo_Update";
        private const string SQL_GET_BY_ID = "usp_UserInfo_GetByID";
        private const string SQL_SEARCH = "usp_UserInfo_Search";
        private const string _fields = "ID,State,LoginCount,TrueName,PassWord,LastLoginIP,Name,LastLoginTime,CreateTime,Telephone,Email,SafeCode,IsOnLine,PictureURL,RoleID";
        private const string SQL_GET_BY_NAME = "usp_UserInfo_GetByName";
        private const string SQL_GET_ISDEFALUTE_BY_USERID = "usp_User_GetIsDefalutByUserID";
        private const string SQL_USERTOHOSPITAL_HOSPITALADMIN = "usp_UserInfo_HospitalAdmin_Search";
        private const string _usertohospitaltoroleFields = "ID,RoleID,HospitalID,State,LoginCount,TrueName,PassWord,LastLoginIP,Name,LastLoginTime,CreateTime,Telephone,Email";
        private const string SQL_UPDATE_HOSPITALTOUSER = "usp_UserToHospital_Update";
        private const string SQL_UPDATE_SECTIONTOUSER = "usp_UserToSection_Update";
        private const string SQL_GET_NAME_BY_USERID = "usp_User_GetNameByUserID";
        private const string SQL_DELETE_BY_USERID = "usp_UserToHospital_Delete";
        private const string SQL_USERINFONOTINNOTIFIER = "usp_Notifier_UserInfo_search";
        private const string SQL_VIEW_USERINFO_SEARCH = "usp_nv_UserInfo_HospitalInfo_Search";
        private const string _searchFields = "ID,State,LoginCount,TrueName,PassWord,LastLoginIP,Name,LastLoginTime,CreateTime,Telephone,Email";

        private const string SQL_GET_BY_ROLEID_SECTIONID = "usp_UserInfo_RoleID_SectionID";
        ///<summary>
        /// 从IDataReader中读取一个实体对象
        ///</summary>
        private UserInfo GetByReader(IDataReader dr)
        {
            UserInfo entity = new UserInfo();
            object obj = null;
            obj = dr["ID"];
            if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
            obj = dr["State"];
            if (obj != null && obj != DBNull.Value) { entity.State = (UserStateEnum)Convert.ToInt32(obj); }
            obj = dr["LoginCount"];
            if (obj != null && obj != DBNull.Value) { entity.LoginCount = Convert.ToInt32(obj); }
            obj = dr["TrueName"];
            entity.TrueName = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.TrueName = Convert.ToString(obj); }
            obj = dr["PassWord"];
            entity.PassWord = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.PassWord = Convert.ToString(obj); }
            obj = dr["LastLoginIP"];
            entity.LastLoginIP = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.LastLoginIP = Convert.ToString(obj); }
            obj = dr["Name"];
            entity.Name = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
            obj = dr["LastLoginTime"];
            if (obj != null && obj != DBNull.Value) { entity.LastLoginTime = Convert.ToDateTime(obj); }
            obj = dr["CreateTime"];
            if (obj != null && obj != DBNull.Value) { entity.CreateTime = Convert.ToDateTime(obj); }
            obj = dr["Telephone"];
            entity.Telephone = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Telephone = Convert.ToString(obj); }
            obj = dr["Email"];
            entity.Email = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Email = Convert.ToString(obj); }
            if (DBConfigUtility.IsContainsColumn(dr, "SafeCode"))
            {
                obj = dr["SafeCode"];
                entity.SafeCode = Convert.ToString(obj);
                if (obj != null && obj != DBNull.Value) { entity.SafeCode = Convert.ToString(obj); }
            }
            if (DBConfigUtility.IsContainsColumn(dr, "IsOnLine"))
            {
                obj = dr["IsOnLine"];
                if (obj != null && obj != DBNull.Value) { entity.IsOnLine = Convert.ToBoolean(obj); }
            }
            if (DBConfigUtility.IsContainsColumn(dr, "PictureURL"))
            {
                obj = dr["PictureURL"];
                entity.PictureURL = Convert.ToString(obj);
                if (obj != null && obj != DBNull.Value) { entity.PictureURL = Convert.ToString(obj); }
            }
            if (DBConfigUtility.IsContainsColumn(dr, "RoleID"))
            {
                obj = dr["RoleID"];
                if (obj != null && obj != DBNull.Value) { entity.RoleID = Convert.ToInt32(obj); }
            }
            return entity;
        }

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(UserInfo entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddOutParameter(dbc, "ID", DbType.Int32, 4);
            db.AddInParameter(dbc, "State", DbType.Int32, entity.State);
            db.AddInParameter(dbc, "LoginCount", DbType.Int32, entity.LoginCount);
            db.AddInParameter(dbc, "TrueName", DbType.String, entity.TrueName);
            db.AddInParameter(dbc, "PassWord", DbType.String, entity.PassWord);
            db.AddInParameter(dbc, "LastLoginIP", DbType.String, entity.LastLoginIP);
            db.AddInParameter(dbc, "Name", DbType.String, entity.Name);
            db.AddInParameter(dbc, "LastLoginTime", DbType.DateTime, entity.LastLoginTime);
            db.AddInParameter(dbc, "CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(dbc, "Telephone", DbType.String, entity.Telephone);
            db.AddInParameter(dbc, "Email", DbType.String, entity.Email);
            db.AddInParameter(dbc, "SafeCode", DbType.String, entity.SafeCode);
            db.AddInParameter(dbc, "IsOnLine", DbType.Boolean, entity.IsOnLine);
            db.AddInParameter(dbc, "PictureURL", DbType.String, entity.PictureURL);
            db.AddInParameter(dbc, "RoleID", DbType.Int32, entity.RoleID);

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
        /// DeleteByUserID
        /// </summary>
        public bool DeleteByUserID(int id)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE_BY_USERID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, id);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;
        }

        /// <summary>
        /// GetByNameUserID
        /// </summary>
        public IList<string> GetByNameUserID(int id)
        {
            IList<string> entity = new List<string>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_NAME_BY_USERID);
            db.AddInParameter(dbc, "ID", DbType.Int32, id);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    entity.Add(ConvertUtility.ToString(dr["RoleName"]));
                }
            }
            return entity;
        }


        /// <summary>
        /// GetByName
        /// </summary>
        public UserInfo GetByName(string name)
        {
            UserInfo entity = null;
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
        /// GetIsDefaluteByID
        /// </summary>
        public int GetIsDefaluteByID(int id)
        {
            int entity = 0;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_ISDEFALUTE_BY_USERID);
            db.AddInParameter(dbc, "ID", DbType.Int32, id);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                if (dr.Read())
                {
                    entity = ConvertUtility.ToInt32(dr["HospitalID"], 0);
                }
            }
            return entity;
        }

        ///<summary>
        /// 更新一条记录
        ///</summary>
        public void Update(UserInfo entity)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
            db.AddInParameter(dbc, "ID", DbType.Int32, entity.ID);
            db.AddInParameter(dbc, "State", DbType.Int32, entity.State);
            db.AddInParameter(dbc, "LoginCount", DbType.Int32, entity.LoginCount);
            db.AddInParameter(dbc, "TrueName", DbType.String, entity.TrueName);
            db.AddInParameter(dbc, "PassWord", DbType.String, entity.PassWord);
            db.AddInParameter(dbc, "LastLoginIP", DbType.String, entity.LastLoginIP);
            db.AddInParameter(dbc, "Name", DbType.String, entity.Name);
            db.AddInParameter(dbc, "LastLoginTime", DbType.DateTime, entity.LastLoginTime);
            db.AddInParameter(dbc, "CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(dbc, "Telephone", DbType.String, entity.Telephone);
            db.AddInParameter(dbc, "Email", DbType.String, entity.Email);
            db.AddInParameter(dbc, "SafeCode", DbType.String, entity.SafeCode);
            db.AddInParameter(dbc, "IsOnLine", DbType.Boolean, entity.IsOnLine);
            db.AddInParameter(dbc, "PictureURL", DbType.String, entity.PictureURL);
            db.AddInParameter(dbc, "RoleID", DbType.Int32, entity.RoleID);

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
        public UserInfo GetByID(int id)
        {
            UserInfo entity = null;
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
        public IList<UserInfo> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<UserInfo> lst = new List<UserInfo>();
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
                    UserInfo ent = new UserInfo();
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
        public IList<UserInfo> ViewSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<UserInfo> lst = new List<UserInfo>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_VIEW_USERINFO_SEARCH);
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
                    UserInfo ent = new UserInfo();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }
        /// <summary>
        /// 更新当前医院
        /// </summary>
        public bool UpdateCurrentHospital(int userID, int hospitalID)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE_HOSPITALTOUSER);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospitalID);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;
        }

        /// <summary>
        /// 更新当前医院
        /// </summary>
        public bool UpdateUserToSection(int userid, int sectionid)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE_SECTIONTOUSER);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userid);
            db.AddInParameter(dbc, "sectionid", DbType.Int32, sectionid);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;
        }

        /// <summary>
        /// 通过角色和科室获取数据
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="sectionID">科室ID</param>
        /// <returns>用户集合</returns>
        public IList<UserInfo> GetByRoleAndSection(int roleID, int sectionID)
        {
            IList<UserInfo> lst = new List<UserInfo>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ROLEID_SECTIONID);
            db.AddInParameter(dbc, "RoleID", DbType.Int32, roleID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, sectionID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    UserInfo ent = new UserInfo();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }

        ///<summary>
        /// 视图搜索记录
        ///</summary>
        public IList<UserInfo> UserInfoNotInNotifierSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            IList<UserInfo> lst = new List<UserInfo>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_USERINFONOTINNOTIFIER);
            db.AddOutParameter(dbc, "pageCount", DbType.Int32, 4);
            db.AddInParameter(dbc, "pageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(dbc, "pageSize", DbType.Int32, pageSize);
            db.AddInParameter(dbc, "fields", DbType.String, _searchFields);
            db.AddInParameter(dbc, "where", DbType.String, where);
            db.AddInParameter(dbc, "orderField", DbType.String, orderField);
            db.AddInParameter(dbc, "isDesc", DbType.Boolean, isDesc);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    UserInfo ent = new UserInfo();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            pageCount = (int)db.GetParameterValue(dbc, "pageCount");
            return lst;
        }
    }
}
