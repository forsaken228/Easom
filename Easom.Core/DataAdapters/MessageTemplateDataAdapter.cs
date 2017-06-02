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
	/// MessageTemplateDataAdapter  数据库访问类
	///</summary>
	internal class MessageTemplateDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_MessageTemplate_Insert";
		private const string SQL_DELETE = "usp_MessageTemplate_Delete";
		private const string SQL_UPDATE = "usp_MessageTemplate_Update";
		private const string SQL_GET_BY_ID = "usp_MessageTemplate_GetByID";
		private const string SQL_SEARCH = "usp_MessageTemplate_Search";
		private const string _fields="ID,CreateUserID,Content,State,Name";

		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private MessageTemplate GetByReader(IDataReader dr)
		{
			MessageTemplate entity = new MessageTemplate();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["CreateUserID"];
			if (obj != null && obj != DBNull.Value) { entity.CreateUserID = Convert.ToInt32(obj); }
			obj = dr["Content"];
			entity.Content = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Content = Convert.ToString(obj); }
            obj = dr["Name"];
            entity.Name = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
			obj = dr["State"];
            if (obj != null && obj != DBNull.Value) { entity.State = (TemplateTypeEnum)Convert.ToInt32(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(MessageTemplate entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "CreateUserID", DbType.Int32,entity.CreateUserID);
			db.AddInParameter(dbc, "Content", DbType.String,entity.Content);
            db.AddInParameter(dbc, "Name", DbType.String, entity.Name);
			db.AddInParameter(dbc, "State", DbType.Int32,entity.State);

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
		public void Update(MessageTemplate entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "CreateUserID", DbType.Int32,entity.CreateUserID);
			db.AddInParameter(dbc, "Content", DbType.String,entity.Content);
            db.AddInParameter(dbc, "Name", DbType.String, entity.Name);
			db.AddInParameter(dbc, "State", DbType.Int32,entity.State);

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
		public MessageTemplate GetByID(int id)
		{
			MessageTemplate entity = null;
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
		public IList<MessageTemplate> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<MessageTemplate> lst = new List<MessageTemplate>();
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
				MessageTemplate ent = new MessageTemplate();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
