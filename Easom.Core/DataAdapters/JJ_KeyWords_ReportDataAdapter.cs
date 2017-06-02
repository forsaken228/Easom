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
	/// JJ_KeyWords_ReportDataAdapter  数据库访问类
	///</summary>
	internal class JJ_KeyWords_ReportDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_JJ_KeyWords_Report_Insert";
		private const string SQL_DELETE = "usp_JJ_KeyWords_Report_Delete";
		private const string SQL_UPDATE = "usp_JJ_KeyWords_Report_Update";
		private const string SQL_GET_BY_ID = "usp_JJ_KeyWords_Report_GetByID";
		private const string SQL_SEARCH = "usp_JJ_KeyWords_Report_Search";
        private const string _fields = "ID,DataTime,JJ_AccountID,JJ_Plan,JJ_Cell,JJ_KeyWordsName,ShowNum,ClickNum,PayNum,Remark1,Remark2,Remark3,TimeState";

        private const string SQL_JJ_KeyWords_Report_GetByKeywordName = "SQL_JJ_KeyWords_Report_GetByKeywordName";
        private const string SQL_DELETEByTimeState = "usp_JJ_KeyWords_Report_Delete_ByTimeState";
        private const string SQL_JJ_KeyWords_Report_GETSumDATAByKeyWord = "usp_JJ_KeyWords_Report_GetSumDataByKeyWord"; 
        private const string SQL_JJ_KeyWords_Report_GETHOURDATA = "usp_JJ_KeyWords_Report_GetHourData"; 
		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private JJ_KeyWords_Report GetByReader(IDataReader dr)
		{
			JJ_KeyWords_Report entity = new JJ_KeyWords_Report();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt64(obj); }
			obj = dr["DataTime"];
			if (obj != null && obj != DBNull.Value) { entity.DataTime = Convert.ToDateTime(obj); }
			obj = dr["JJ_AccountID"];
			if (obj != null && obj != DBNull.Value) { entity.JJ_AccountID = Convert.ToInt32(obj); }
			obj = dr["JJ_Plan"];
			entity.JJ_Plan = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.JJ_Plan = Convert.ToString(obj); }
            obj = dr["JJ_Cell"];
			entity.JJ_Cell = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.JJ_Cell = Convert.ToString(obj); }
			obj = dr["JJ_KeyWordsName"];
			entity.JJ_KeyWordsName = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.JJ_KeyWordsName = Convert.ToString(obj); }
			obj = dr["ShowNum"];
			if (obj != null && obj != DBNull.Value) { entity.ShowNum = Convert.ToInt32(obj); }
			obj = dr["ClickNum"];
			if (obj != null && obj != DBNull.Value) { entity.ClickNum = Convert.ToInt32(obj); }
			obj = dr["PayNum"];
			if (obj != null && obj != DBNull.Value) { entity.PayNum = Convert.ToDecimal(obj); }
			obj = dr["Remark1"];
			entity.Remark1 = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Remark1 = Convert.ToString(obj); }
			obj = dr["Remark2"];
			entity.Remark2 = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Remark2 = Convert.ToString(obj); }
			obj = dr["Remark3"];
			entity.Remark3 = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Remark3 = Convert.ToString(obj); }
			obj = dr["TimeState"];
			if (obj != null && obj != DBNull.Value) { entity.TimeState = Convert.ToInt32(obj); }
			return entity;
		}

        private JJ_KeyWords_Report GetBySpecialReader(IDataReader dr) 
        {
            JJ_KeyWords_Report entity = new JJ_KeyWords_Report();
            object obj = null;
            obj = dr["JJ_Plan"];
            entity.JJ_Plan = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.JJ_Plan = Convert.ToString(obj); }
            obj = dr["JJ_Cell"];
            entity.JJ_Cell = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.JJ_Cell = Convert.ToString(obj); }
            obj = dr["JJ_KeyWordsName"];
            entity.JJ_KeyWordsName = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.JJ_KeyWordsName = Convert.ToString(obj); }
            obj = dr["ShowNum"];
            if (obj != null && obj != DBNull.Value) { entity.ShowNum = Convert.ToInt32(obj); }
            obj = dr["ClickNum"];
            if (obj != null && obj != DBNull.Value) { entity.ClickNum = Convert.ToInt32(obj); }
            obj = dr["PayNum"];
            if (obj != null && obj != DBNull.Value) { entity.PayNum = Convert.ToDecimal(obj); }
            return entity;
        }

		///<summary>
		/// 插入一条记录
		///</summary>
		public long Insert(JJ_KeyWords_Report entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int64,8);
			db.AddInParameter(dbc, "DataTime", DbType.DateTime,entity.DataTime);
			db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32,entity.JJ_AccountID);
			db.AddInParameter(dbc, "JJ_Plan", DbType.String,entity.JJ_Plan);
			db.AddInParameter(dbc, "JJ_Cell", DbType.String,entity.JJ_Cell);
			db.AddInParameter(dbc, "JJ_KeyWordsName", DbType.String,entity.JJ_KeyWordsName);
			db.AddInParameter(dbc, "ShowNum", DbType.Int32,entity.ShowNum);
			db.AddInParameter(dbc, "ClickNum", DbType.Int32,entity.ClickNum);
			db.AddInParameter(dbc, "PayNum", DbType.Decimal,entity.PayNum);
			db.AddInParameter(dbc, "Remark1", DbType.String,entity.Remark1);
			db.AddInParameter(dbc, "Remark2", DbType.String,entity.Remark2);
			db.AddInParameter(dbc, "Remark3", DbType.String,entity.Remark3);
			db.AddInParameter(dbc, "TimeState", DbType.Int32,entity.TimeState);

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
		public void Delete(long id)
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


        public void DeleteDataByHour(int accountID, System.DateTime beginTime, System.DateTime endTime, int timestate)
        {

            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_DELETEByTimeState);

            db.AddInParameter(dbc, "beginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "endTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32, accountID);
            db.AddInParameter(dbc, "TimeState", DbType.Int32, timestate);

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
		public void Update(JJ_KeyWords_Report entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int64,entity.ID);
			db.AddInParameter(dbc, "DataTime", DbType.DateTime,entity.DataTime);
			db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32,entity.JJ_AccountID);
			db.AddInParameter(dbc, "JJ_Plan", DbType.String,entity.JJ_Plan);
			db.AddInParameter(dbc, "JJ_Cell", DbType.String,entity.JJ_Cell);
			db.AddInParameter(dbc, "JJ_KeyWordsName", DbType.String,entity.JJ_KeyWordsName);
			db.AddInParameter(dbc, "ShowNum", DbType.Int32,entity.ShowNum);
			db.AddInParameter(dbc, "ClickNum", DbType.Int32,entity.ClickNum);
			db.AddInParameter(dbc, "PayNum", DbType.Decimal,entity.PayNum);
			db.AddInParameter(dbc, "Remark1", DbType.String,entity.Remark1);
			db.AddInParameter(dbc, "Remark2", DbType.String,entity.Remark2);
			db.AddInParameter(dbc, "Remark3", DbType.String,entity.Remark3);
			db.AddInParameter(dbc, "TimeState", DbType.Int32,entity.TimeState);

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
		public JJ_KeyWords_Report GetByID(int id)
		{
			JJ_KeyWords_Report entity = null;
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

        public DataSet GetHourData(int accountID, System.DateTime beginTime, System.DateTime endTime)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_JJ_KeyWords_Report_GETHOURDATA);
           
            db.AddInParameter(dbc, "beginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "endTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32, accountID);

      
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
        }

       
            
		///<summary>
		/// 读取关键词读取一条汇总记录
		///</summary>
        public JJ_KeyWords_Report GetbaiduHourSumByKeywordName(int accountID, System.DateTime beginTime, System.DateTime endTime, string plan, string cell, string keyword)
		{
			JJ_KeyWords_Report entity = null;
			Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_JJ_KeyWords_Report_GETSumDATAByKeyWord);

            db.AddInParameter(dbc, "beginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "endTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32, accountID);
            db.AddInParameter(dbc, "JJ_KeyWordsName", DbType.String, keyword);
            db.AddInParameter(dbc, "JJ_Cell", DbType.String, cell);
            db.AddInParameter(dbc, "JJ_Plan", DbType.String, plan);

			using (IDataReader dr = db.ExecuteReader(dbc))
			{
				if (dr.Read())
				{
                    entity = GetBySpecialReader(dr);
				}
			}
			return entity;
		}

        ///<summary>
        /// 读取同一个小时内传两次的问题
        ///</summary>
        public JJ_KeyWords_Report GetByKeywordName(int accountID, System.DateTime beginTime, System.DateTime endTime, string plan, string cell, string keyword, int timestate) 
        {
            JJ_KeyWords_Report entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_JJ_KeyWords_Report_GetByKeywordName); 

            db.AddInParameter(dbc, "beginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "endTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "JJ_AccountID", DbType.Int32, accountID);
            db.AddInParameter(dbc, "JJ_Cell", DbType.String, cell);
            db.AddInParameter(dbc, "JJ_KeyWordsName", DbType.String, keyword);
            db.AddInParameter(dbc, "TimeState", DbType.Int32, timestate);
            db.AddInParameter(dbc, "JJ_Plan", DbType.String, plan);
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
		public IList<JJ_KeyWords_Report> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<JJ_KeyWords_Report> lst = new List<JJ_KeyWords_Report>();
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
				JJ_KeyWords_Report ent = new JJ_KeyWords_Report();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
