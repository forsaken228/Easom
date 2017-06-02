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
	/// MessageLogDataAdapter  数据库访问类
	///</summary>
	internal class MessageLogDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_MessageLog_Insert";
		private const string SQL_DELETE = "usp_MessageLog_Delete";
		private const string SQL_UPDATE = "usp_MessageLog_Update";
		private const string SQL_GET_BY_ID = "usp_MessageLog_GetByID";
		private const string SQL_SEARCH = "usp_MessageLog_Search";
		private const string _fields="ID,ToNumber,SendState,SendDate,SendContent,UserID,HospitalID,SectionID";
        private const string SQL_GET_GETBYTELEPHONE = "usp_MessageLog_GetByToNumber";
		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private MessageLog GetByReader(IDataReader dr)
		{
			MessageLog entity = new MessageLog();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["ToNumber"];
			entity.ToNumber = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.ToNumber = Convert.ToString(obj); }
			obj = dr["SendState"];
			if (obj != null && obj != DBNull.Value) { entity.SendState = Convert.ToInt32(obj); }
			obj = dr["SendDate"];
			if (obj != null && obj != DBNull.Value) { entity.SendDate = Convert.ToDateTime(obj); }
			obj = dr["SendContent"];
			entity.SendContent = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.SendContent = Convert.ToString(obj); }
			obj = dr["UserID"];
			if (obj != null && obj != DBNull.Value) { entity.UserID = Convert.ToInt32(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			obj = dr["SectionID"];
			if (obj != null && obj != DBNull.Value) { entity.SectionID = Convert.ToInt32(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(MessageLog entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "ToNumber", DbType.String,entity.ToNumber);
			db.AddInParameter(dbc, "SendState", DbType.Int32,entity.SendState);
			db.AddInParameter(dbc, "SendDate", DbType.DateTime,entity.SendDate);
			db.AddInParameter(dbc, "SendContent", DbType.String,entity.SendContent);
			db.AddInParameter(dbc, "UserID", DbType.Int32,entity.UserID);
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
		public void Update(MessageLog entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "ToNumber", DbType.String,entity.ToNumber);
			db.AddInParameter(dbc, "SendState", DbType.Int32,entity.SendState);
			db.AddInParameter(dbc, "SendDate", DbType.DateTime,entity.SendDate);
			db.AddInParameter(dbc, "SendContent", DbType.String,entity.SendContent);
			db.AddInParameter(dbc, "UserID", DbType.Int32,entity.UserID);
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
		public MessageLog GetByID(int id)
		{
			MessageLog entity = null;
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
        /// 获取所有媒体类型
        /// </summary>
        /// <returns></returns>
        public IList<MessageLog> GetByTelephone(string telephone)
        {
            IList<MessageLog> lst = new List<MessageLog>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_GETBYTELEPHONE);
            db.AddInParameter(dbc, "ToNumber", DbType.String, telephone);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    MessageLog ent = new MessageLog();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<MessageLog> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<MessageLog> lst = new List<MessageLog>();
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
				MessageLog ent = new MessageLog();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
