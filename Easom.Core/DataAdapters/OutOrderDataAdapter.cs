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
	/// OutOrderDataAdapter  数据库访问类
	///</summary>
	internal class OutOrderDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_OutOrder_Insert";
		private const string SQL_DELETE = "usp_OutOrder_Delete";
		private const string SQL_UPDATE = "usp_OutOrder_Update";
		private const string SQL_GET_BY_ID = "usp_OutOrder_GetByID";
		private const string SQL_SEARCH = "usp_OutOrder_Search";
        private const string _fields = "ID,CatID,OutSiteToHospiatlID,HospitalID,Sex,State,OrderTime,Age,UserName,Telephone,Description,SectionID,CreateTime";

		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private OutOrder GetByReader(IDataReader dr)
		{
			OutOrder entity = new OutOrder();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["CatID"];
			if (obj != null && obj != DBNull.Value) { entity.CatID = Convert.ToInt32(obj); }
			obj = dr["OutSiteToHospiatlID"];
			if (obj != null && obj != DBNull.Value) { entity.OutSiteToHospiatlID = Convert.ToInt32(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			obj = dr["Sex"];
			if (obj != null && obj != DBNull.Value) { entity.Sex = Convert.ToBoolean(obj); }
			obj = dr["State"];
			if (obj != null && obj != DBNull.Value) { entity.State = Convert.ToBoolean(obj); }
			obj = dr["OrderTime"];
			if (obj != null && obj != DBNull.Value) { entity.OrderTime = Convert.ToDateTime(obj); }
			obj = dr["Age"];
			entity.Age = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Age = Convert.ToString(obj); }
			obj = dr["UserName"];
			entity.UserName = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.UserName = Convert.ToString(obj); }
			obj = dr["Telephone"];
			entity.Telephone = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Telephone = Convert.ToString(obj); }
			obj = dr["Description"];
			entity.Description = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Description = Convert.ToString(obj); }
			obj = dr["SectionID"];
			if (obj != null && obj != DBNull.Value) { entity.SectionID = Convert.ToInt32(obj); }
            obj = dr["CreateTime"];
            if (obj != null && obj != DBNull.Value) { entity.CreateTime = Convert.ToDateTime(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(OutOrder entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddOutParameter(dbc, "ID", DbType.Int32, 4);
			db.AddInParameter(dbc, "CatID", DbType.Int32,entity.CatID);
			db.AddInParameter(dbc, "OutSiteToHospiatlID", DbType.Int32,entity.OutSiteToHospiatlID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "Sex", DbType.Boolean,entity.Sex);
			db.AddInParameter(dbc, "State", DbType.Boolean,entity.State);
			db.AddInParameter(dbc, "OrderTime", DbType.DateTime,entity.OrderTime);
			db.AddInParameter(dbc, "Age", DbType.String,entity.Age);
			db.AddInParameter(dbc, "UserName", DbType.String,entity.UserName);
			db.AddInParameter(dbc, "Telephone", DbType.String,entity.Telephone);
			db.AddInParameter(dbc, "Description", DbType.String,entity.Description);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
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
		public void Update(OutOrder entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "CatID", DbType.Int32,entity.CatID);
			db.AddInParameter(dbc, "OutSiteToHospiatlID", DbType.Int32,entity.OutSiteToHospiatlID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "Sex", DbType.Boolean,entity.Sex);
			db.AddInParameter(dbc, "State", DbType.Boolean,entity.State);
			db.AddInParameter(dbc, "OrderTime", DbType.DateTime,entity.OrderTime);
			db.AddInParameter(dbc, "Age", DbType.String,entity.Age);
			db.AddInParameter(dbc, "UserName", DbType.String,entity.UserName);
			db.AddInParameter(dbc, "Telephone", DbType.String,entity.Telephone);
			db.AddInParameter(dbc, "Description", DbType.String,entity.Description);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
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
		public OutOrder GetByID(int id)
		{
			OutOrder entity = null;
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

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<OutOrder> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<OutOrder> lst = new List<OutOrder>();
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
				OutOrder ent = new OutOrder();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
