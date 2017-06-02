// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-18
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
	/// JJ_PlanDataAdapter  数据库访问类
	///</summary>
	internal class JJ_PlanDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_JJ_Plan_Insert";
		private const string SQL_DELETE = "usp_JJ_Plan_Delete";
		private const string SQL_UPDATE = "usp_JJ_Plan_Update";
		private const string SQL_GET_BY_ID = "usp_JJ_Plan_GetByID";
		private const string SQL_SEARCH = "usp_JJ_Plan_Search";
		private const string _fields="ID,Name,JJ_AccountID,HospitalID,SectionID,Remark1,Remark2";

		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private JJ_Plan GetByReader(IDataReader dr)
		{
			JJ_Plan entity = new JJ_Plan();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["Name"];
			entity.Name = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
			obj = dr["JJ_AccountID"];
			if (obj != null && obj != DBNull.Value) { entity.JJ_AccountID = Convert.ToInt32(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			obj = dr["SectionID"];
			if (obj != null && obj != DBNull.Value) { entity.SectionID = Convert.ToInt32(obj); }
			obj = dr["Remark1"];
			entity.Remark1 = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Remark1 = Convert.ToString(obj); }
			obj = dr["Remark2"];
			entity.Remark2 = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Remark2 = Convert.ToString(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(JJ_Plan entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32,entity.JJ_AccountID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
			db.AddInParameter(dbc, "Remark1", DbType.String,entity.Remark1);
			db.AddInParameter(dbc, "Remark2", DbType.String,entity.Remark2);

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
		public void Update(JJ_Plan entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32,entity.JJ_AccountID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
			db.AddInParameter(dbc, "Remark1", DbType.String,entity.Remark1);
			db.AddInParameter(dbc, "Remark2", DbType.String,entity.Remark2);

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
		public JJ_Plan GetByID(int id)
		{
			JJ_Plan entity = null;
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
		public IList<JJ_Plan> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<JJ_Plan> lst = new List<JJ_Plan>();
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
				JJ_Plan ent = new JJ_Plan();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
