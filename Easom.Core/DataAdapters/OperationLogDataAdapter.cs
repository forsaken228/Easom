// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2017-01-14
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
	/// OperationLogDataAdapter  数据库访问类
	///</summary>
	internal class OperationLogDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_OperationLog_Insert";
		private const string SQL_DELETE = "usp_OperationLog_Delete";
		private const string SQL_UPDATE = "usp_OperationLog_Update";
		private const string SQL_GET_BY_ID = "usp_OperationLog_GetByID";
		private const string SQL_SEARCH = "usp_OperationLog_Search";
		private const string _fields="ID,UserID,CreateTime,OperationInfo,TableName,OperationType,LinkID,HospitalID,SectionID";

		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private OperationLog GetByReader(IDataReader dr)
		{
			OperationLog entity = new OperationLog();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt64(obj); }
			obj = dr["UserID"];
			if (obj != null && obj != DBNull.Value) { entity.UserID = Convert.ToInt32(obj); }
			obj = dr["CreateTime"];
			if (obj != null && obj != DBNull.Value) { entity.CreateTime = Convert.ToDateTime(obj); }
			obj = dr["OperationInfo"];
			entity.OperationInfo = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.OperationInfo = Convert.ToString(obj); }
			obj = dr["TableName"];
			entity.TableName = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.TableName = Convert.ToString(obj); }
			obj = dr["OperationType"];
            if (obj != null && obj != DBNull.Value) { entity.OperationType = (OperationTypeEnum)Convert.ToInt32(obj); }
            obj = dr["LinkID"];
			if (obj != null && obj != DBNull.Value) { entity.LinkID = Convert.ToInt32(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			obj = dr["SectionID"];
			if (obj != null && obj != DBNull.Value) { entity.SectionID = Convert.ToInt32(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public Int64 Insert(OperationLog entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int64,8);
			db.AddInParameter(dbc, "UserID", DbType.Int32,entity.UserID);
			db.AddInParameter(dbc, "CreateTime", DbType.DateTime,entity.CreateTime);
			db.AddInParameter(dbc, "OperationInfo", DbType.String,entity.OperationInfo);
			db.AddInParameter(dbc, "TableName", DbType.String,entity.TableName);
			db.AddInParameter(dbc, "OperationType", DbType.Int32,entity.OperationType);
			db.AddInParameter(dbc, "LinkID", DbType.Int32,entity.LinkID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);

			try
			 {
				db.ExecuteNonQuery(dbc);
			}
			catch
			{
				throw;
			}

			return (Int64)db.GetParameterValue(dbc, "ID");
		}

		///<summary>
		/// 删除一条记录
		///</summary>
		public void Delete(int id)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE);
			db.AddInParameter(dbc, "ID", DbType.Int64,id);

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
		public void Update(OperationLog entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int64,entity.ID);
			db.AddInParameter(dbc, "UserID", DbType.Int32,entity.UserID);
			db.AddInParameter(dbc, "CreateTime", DbType.DateTime,entity.CreateTime);
			db.AddInParameter(dbc, "OperationInfo", DbType.String,entity.OperationInfo);
			db.AddInParameter(dbc, "TableName", DbType.String,entity.TableName);
			db.AddInParameter(dbc, "OperationType", DbType.Int32,entity.OperationType);
			db.AddInParameter(dbc, "LinkID", DbType.Int32,entity.LinkID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);

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
		public OperationLog GetByID(int id)
		{
			OperationLog entity = null;
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_ID);
			db.AddInParameter(dbc, "ID", DbType.Int64,id);
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
		public IList<OperationLog> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<OperationLog> lst = new List<OperationLog>();
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
				OperationLog ent = new OperationLog();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
