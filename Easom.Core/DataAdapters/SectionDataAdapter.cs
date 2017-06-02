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
	/// SectionDataAdapter  数据库访问类
	///</summary>
	internal class SectionDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_Section_Insert";
		private const string SQL_DELETE = "usp_Section_Delete";
		private const string SQL_UPDATE = "usp_Section_Update";
		private const string SQL_GET_BY_ID = "usp_Section_GetByID";
		private const string SQL_SEARCH = "usp_Section_Search";
        private const string _fields = "ID,HospitalID,Name,Intro,IsDelete";
        private const string SQL_GET_BY_HOSIPITALID = "usp_Section_GetByHosipitalID";
        private const string SQL_GET_BY_USERID = "usp_section_GetByUserID";
        private const string SQL_INSERT_USER_TO_SECTION = "usp_UserToSection_Insert";

		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private Section GetByReader(IDataReader dr)
		{
			Section entity = new Section();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			obj = dr["Name"];
			entity.Name = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
			obj = dr["Intro"];
			entity.Intro = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Intro = Convert.ToString(obj); }
            obj = dr["IsDelete"];
            if (obj != null && obj != DBNull.Value) { entity.IsDelete = Convert.ToInt32(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(Section entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "Intro", DbType.String,entity.Intro);
            db.AddInParameter(dbc, "IsDelete", DbType.Int32, entity.IsDelete);

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

        public IList<Section> GetByUserID(int id, int isDefault, int hosipitalID)
        {
            IList<Section> entitylist = new List<Section>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_USERID);
            db.AddInParameter(dbc, "ID", DbType.Int32, id);
            db.AddInParameter(dbc, "IsDefault", DbType.Int32, isDefault);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hosipitalID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Section entity = GetByReader(dr);
                    entitylist.Add(entity);
                }
            }
            return entitylist;
        }

		///<summary>
		/// 更新一条记录
		///</summary>
		public void Update(Section entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "Intro", DbType.String,entity.Intro);
            db.AddInParameter(dbc, "IsDelete", DbType.Int32, entity.IsDelete);

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
		public Section GetByID(int id)
		{
			Section entity = null;
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

        public IList<Section> GetByHospitalID(int id) 
        {
            IList<Section> entitylist = new List<Section>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_HOSIPITALID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, id);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Section entity = GetByReader(dr);
                    entitylist.Add(entity);
                }
            }
            return entitylist;
        }

        public bool InsertUserToSection(int userID, int hospitalID, bool isDefault) 
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT_USER_TO_SECTION);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, hospitalID);
            db.AddInParameter(dbc, "IsDefault", DbType.Boolean, isDefault);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;
        }

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<Section> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<Section> lst = new List<Section>();
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
				Section ent = new Section();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
