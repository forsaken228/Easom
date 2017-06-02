// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-21
// 作者：loskiv@gmail.com

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using CHCMS.Utility;
using System.Data.SqlClient;
namespace Easom.Core.DataAdapters
{

	///<summary>
	/// JJ_KeyWordsDataAdapter  数据库访问类
	///</summary>
	internal class JJ_KeyWordsDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_JJ_KeyWords_Insert";
		private const string SQL_DELETE = "usp_JJ_KeyWords_Delete";
        private const string SQL_DELETEByAccountID = "usp_JJ_KeyWords_DeleteByAccountID";

		private const string SQL_UPDATE = "usp_JJ_KeyWords_Update";
		private const string SQL_GET_BY_ID = "usp_JJ_KeyWords_GetByID";
		private const string SQL_SEARCH = "usp_JJ_KeyWords_Search";
		private const string _fields="ID,JJ_AccountID,JJ_Plan,JJ_Cell,Name,PCUrl,WAPUrl";

		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private JJ_KeyWords GetByReader(IDataReader dr)
		{
			JJ_KeyWords entity = new JJ_KeyWords();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt64(obj); }
			obj = dr["JJ_AccountID"];
			if (obj != null && obj != DBNull.Value) { entity.JJ_AccountID = Convert.ToInt32(obj); }
			obj = dr["JJ_Plan"];
			entity.JJ_Plan = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.JJ_Plan = Convert.ToString(obj); }
			obj = dr["JJ_Cell"];
			entity.JJ_Cell = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.JJ_Cell = Convert.ToString(obj); }
			obj = dr["Name"];
			entity.Name = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
			obj = dr["PCUrl"];
			entity.PCUrl = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.PCUrl = Convert.ToString(obj); }
			obj = dr["WAPUrl"];
			entity.WAPUrl = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.WAPUrl = Convert.ToString(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public long Insert(JJ_KeyWords entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int64,8);
			db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32,entity.JJ_AccountID);
			db.AddInParameter(dbc, "JJ_Plan", DbType.String,entity.JJ_Plan);
			db.AddInParameter(dbc, "JJ_Cell", DbType.String,entity.JJ_Cell);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "PCUrl", DbType.String,entity.PCUrl);
			db.AddInParameter(dbc, "WAPUrl", DbType.String,entity.WAPUrl);

			try
			 {
				db.ExecuteNonQuery(dbc);
			}
			catch
			{
				throw;
			}

			return (long)db.GetParameterValue(dbc, "ID");
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
        /// 删除该账户所有的关键词库
        ///</summary>
        public int DeleteByAccountID(int accountID )
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETEByAccountID); 
            db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32, accountID);

            try
            {
                return db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }

        }


		///<summary>
		/// 更新一条记录
		///</summary>
		public void Update(JJ_KeyWords entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int64,entity.ID);
			db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32,entity.JJ_AccountID);
			db.AddInParameter(dbc, "JJ_Plan", DbType.String,entity.JJ_Plan);
			db.AddInParameter(dbc, "JJ_Cell", DbType.String,entity.JJ_Cell);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "PCUrl", DbType.String,entity.PCUrl);
			db.AddInParameter(dbc, "WAPUrl", DbType.String,entity.WAPUrl);

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
		public JJ_KeyWords GetByID(int id)
		{
			JJ_KeyWords entity = null;
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



        /// <summary>
        /// 统计每天各小时的数据
        /// </summary>

      


		///<summary>
		/// 搜索记录
		///</summary>
		public IList<JJ_KeyWords> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<JJ_KeyWords> lst = new List<JJ_KeyWords>();
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
				JJ_KeyWords ent = new JJ_KeyWords();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
