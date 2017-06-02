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
	/// DoctorDataAdapter  数据库访问类
	///</summary>
	internal class DoctorDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_Doctor_Insert";
		private const string SQL_DELETE = "usp_Doctor_Delete";
		private const string SQL_UPDATE = "usp_Doctor_Update";
		private const string SQL_GET_BY_ID = "usp_Doctor_GetByID";
        private const string SQL_GET_BY_SECTIONID = "usp_Doctor_GetBySectionID";
        private const string SQL_GET_BY_SECTIONS = "usp_Doctor_GetBySections";
		private const string SQL_SEARCH = "usp_Doctor_Search";
        private const string _fields = "ID,HospitalID,Identifier,Name,Intro,SectionID,IsDelete";

		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private Doctor GetByReader(IDataReader dr)
		{
			Doctor entity = new Doctor();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			obj = dr["Identifier"];
			entity.Identifier = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Identifier = Convert.ToString(obj); }
			obj = dr["Name"];
			entity.Name = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
			obj = dr["Intro"];
			entity.Intro = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Intro = Convert.ToString(obj); }
			obj = dr["SectionID"];
			if (obj != null && obj != DBNull.Value) { entity.SectionID = Convert.ToInt32(obj); }
            obj = dr["IsDelete"];
            if (obj != null && obj != DBNull.Value) { entity.IsDelete = Convert.ToInt32(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(Doctor entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "Identifier", DbType.String,entity.Identifier);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "Intro", DbType.String,entity.Intro);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
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

		///<summary>
		/// 更新一条记录
		///</summary>
		public void Update(Doctor entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "Identifier", DbType.String,entity.Identifier);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "Intro", DbType.String,entity.Intro);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
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
		public Doctor GetByID(int id)
		{
			Doctor entity = null;
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

        /// <summary>
        /// 获取该科室的所有医生
        /// </summary>
        /// <param name="sectionID">科室ID</param>
        /// <returns>医生集合</returns>
        public  IList<Doctor> GetBySectionID(int sectionID)
        {
            IList<Doctor> lst = new List<Doctor>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_SECTIONID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, sectionID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Doctor ent = new Doctor();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }


     /// <summary>
      /// 获取当前用户选择医院的所有科室
     /// </summary>
     /// <param name="userID"></param>
     /// <param name="hosipitalID"></param>
     /// <returns></returns>
        public IList<Doctor> GetBySections(int userID, int hosipitalID) 
        {
            IList<Doctor> lst = new List<Doctor>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_SECTIONS);
            db.AddInParameter(dbc, "ID", DbType.Int32, userID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hosipitalID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Doctor ent = new Doctor();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }
		///<summary>
		/// 搜索记录
		///</summary>
		public IList<Doctor> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<Doctor> lst = new List<Doctor>();
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
				Doctor ent = new Doctor();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
