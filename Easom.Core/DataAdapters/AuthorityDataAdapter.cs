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
	/// AuthorityDataAdapter  数据库访问类
	///</summary>
	internal class AuthorityDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_Authority_Insert";
		private const string SQL_DELETE = "usp_Authority_Delete";
		private const string SQL_UPDATE = "usp_Authority_Update";
		private const string SQL_GET_BY_ID = "usp_Authority_GetByID";
		private const string SQL_SEARCH = "usp_Authority_Search";
		private const string _fields="ID,ParentID,Name,AuthorityKey,SortNum,CroupNumber,Remark";
        private const string SQL_GET_BY_KEY = "usp_Authority_GetByAuthorityKey";
        private const string SQL_GET_ALL_AUTHOR = "usp_Get_All_Author";
        private const string SQL_INSERT_ROLE_TO_AUTHOR = "usp_RoleToAuthority_Insert";
        private const string SQL_GET_BY_ROLEID = "usp_Authority_GetByRoleID";
        private const string SQL_DELETE_ByParentID = "usp_Authority_DeleteByParentID";
		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private Authority GetByReader(IDataReader dr)
		{
			Authority entity = new Authority();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["ParentID"];
			if (obj != null && obj != DBNull.Value) { entity.ParentID = Convert.ToInt32(obj); }
			obj = dr["Name"];
			entity.Name = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
			obj = dr["AuthorityKey"];
			entity.AuthorityKey = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.AuthorityKey = Convert.ToString(obj); }
			obj = dr["SortNum"];
			if (obj != null && obj != DBNull.Value) { entity.SortNum = Convert.ToInt32(obj); }
			obj = dr["CroupNumber"];
			if (obj != null && obj != DBNull.Value) { entity.CroupNumber = Convert.ToInt32(obj); }
			obj = dr["Remark"];
			entity.Remark = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Remark = Convert.ToString(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(Authority entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "ParentID", DbType.Int32,entity.ParentID);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "AuthorityKey", DbType.String,entity.AuthorityKey);
			db.AddInParameter(dbc, "SortNum", DbType.Int32,entity.SortNum);
			db.AddInParameter(dbc, "CroupNumber", DbType.Int32,entity.CroupNumber);
			db.AddInParameter(dbc, "Remark", DbType.String,entity.Remark);

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
			db.AddInParameter(dbc, "ID", DbType.Int32,id);

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
        ///根据ParentID删除数据
        ///</summary>
        public void DeleteByParentID(int ParentID)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE_ByParentID);
            db.AddInParameter(dbc, "ParentID", DbType.Int32, ParentID);

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
		public void Update(Authority entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "ParentID", DbType.Int32,entity.ParentID);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "AuthorityKey", DbType.String,entity.AuthorityKey);
			db.AddInParameter(dbc, "SortNum", DbType.Int32,entity.SortNum);
			db.AddInParameter(dbc, "CroupNumber", DbType.Int32,entity.CroupNumber);
			db.AddInParameter(dbc, "Remark", DbType.String,entity.Remark);

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
		public Authority GetByID(int id)
		{
			Authority entity = null;
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ID);
			db.AddInParameter(dbc, "ID", DbType.Int32,id);
			using (IDataReader dr = db.ExecuteReader(dbc))
			{
				if (dr.Read())
				{
				entity = GetByReader(dr);
				}
			}
			return entity;
		}

        public Authority GetByAuthorityKey(string authorityKey)
        {
            Authority entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_KEY);
            db.AddInParameter(dbc, "AuthorityKey", DbType.String, authorityKey);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                if (dr.Read())
                {
                    entity = GetByReader(dr);
                }
            }
            return entity;
        }

        public IList<Authority> GetAllAuthor()
        {
            IList<Authority> entitylist = new List<Authority>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_ALL_AUTHOR);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Authority entity = GetByReader(dr);
                    entitylist.Add(entity);
                }
            }
            return entitylist;
        }


        /// <summary>
        /// GetByRoleID
        /// </summary>
        public IList<Authority> GetByRoleID(int id)
        {
            IList<Authority> entitylist = new List<Authority>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ROLEID);
            db.AddInParameter(dbc, "ID", DbType.Int32, id);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Authority entity = GetByReader(dr);
                    entitylist.Add(entity);
                }
            }
            return entitylist;
        }

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<Authority> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<Authority> lst = new List<Authority>();
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
				Authority ent = new Authority();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}


        /// <summary>
        /// AddAuthors
        /// </summary>
        public bool AddAuthors(int roleID, int authorID)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT_ROLE_TO_AUTHOR);
            db.AddInParameter(dbc, "RoleID", DbType.Int32, roleID);
            db.AddInParameter(dbc, "AuthorityID", DbType.Int32, authorID);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;
        }
	}
}
