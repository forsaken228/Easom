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
	/// JJ_Swt_ReportDataAdapter  数据库访问类
	///</summary>
	internal class JJ_Swt_ReportDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_JJ_Swt_Report_Insert";
		private const string SQL_DELETE = "usp_JJ_Swt_Report_Delete";
		private const string SQL_UPDATE = "usp_JJ_Swt_Report_Update";
		private const string SQL_GET_BY_ID = "usp_JJ_Swt_Report_GetByID";
        private const string SQL_GETALL_JJ_Swt_Report = "usp_JJ_Swt_Report_GetALL";
        private const string SQL_GET_BY_SECTIONID = "usp_JJ_Swt_Report_GetALLBYSectionID";
        private const string SQL_JJ_Swt_Report = "usp_JJ_Swt_Report_From";

        private const string SQL_JJ_Swt_Report_BAIDU_PLAN = "usp_JJ_Baidu_Plan";
        private const string SQL_JJ_Swt_Report_BAIDU_KEYWORDS = "usp_JJ_Baidu_Keywords";

        private const string SQL_JJ_Swt_Report_Statistics = "usp_JJ_Swt_Report_Statistics";

		private const string SQL_SEARCH = "usp_JJ_Swt_Report_Search";
        private const string _fields = "ID,ChatState,Title,BeginTime,Equipment,Disease,BeginUrl,ChatUrl,FromUrl,KeyWords,HospitalID,SectionID,Reamrk1,Reamrk2,Reamrk3";

		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private JJ_Swt_Report GetByReader(IDataReader dr)
		{
			JJ_Swt_Report entity = new JJ_Swt_Report();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["ChatState"];
			if (obj != null && obj != DBNull.Value) { entity.ChatState = Convert.ToInt32(obj); }
			obj = dr["Title"];
			entity.Title = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Title = Convert.ToString(obj); }

            obj = dr["BeginTime"];
            if (obj != null && obj != DBNull.Value) { entity.BeginTime = Convert.ToDateTime(obj); }

			obj = dr["Equipment"];
			if (obj != null && obj != DBNull.Value) { entity.Equipment = Convert.ToString(obj); }
			obj = dr["Disease"];
			entity.Disease = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Disease = Convert.ToString(obj); }
			obj = dr["BeginUrl"];
			entity.BeginUrl = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.BeginUrl = Convert.ToString(obj); }
			obj = dr["ChatUrl"];
			entity.ChatUrl = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.ChatUrl = Convert.ToString(obj); }
			obj = dr["FromUrl"];
			entity.FromUrl = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.FromUrl = Convert.ToString(obj); }
			obj = dr["KeyWords"];
			entity.KeyWords = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.KeyWords = Convert.ToString(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			obj = dr["SectionID"];
			if (obj != null && obj != DBNull.Value) { entity.SectionID = Convert.ToInt32(obj); }
			obj = dr["Reamrk1"];
			entity.Reamrk1 = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Reamrk1 = Convert.ToString(obj); }
			obj = dr["Reamrk2"];
			entity.Reamrk2 = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Reamrk2 = Convert.ToString(obj); }
			obj = dr["Reamrk3"];
			entity.Reamrk3 = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Reamrk3 = Convert.ToString(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(JJ_Swt_Report entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "ChatState", DbType.Int32,entity.ChatState);
			db.AddInParameter(dbc, "Title", DbType.String,entity.Title);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, entity.BeginTime);
			db.AddInParameter(dbc, "Equipment", DbType.String,entity.Equipment);
			db.AddInParameter(dbc, "Disease", DbType.String,entity.Disease);
			db.AddInParameter(dbc, "BeginUrl", DbType.String,entity.BeginUrl);
			db.AddInParameter(dbc, "ChatUrl", DbType.String,entity.ChatUrl);
			db.AddInParameter(dbc, "FromUrl", DbType.String,entity.FromUrl);
			db.AddInParameter(dbc, "KeyWords", DbType.String,entity.KeyWords);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
			db.AddInParameter(dbc, "Reamrk1", DbType.String,entity.Reamrk1);
			db.AddInParameter(dbc, "Reamrk2", DbType.String,entity.Reamrk2);
			db.AddInParameter(dbc, "Reamrk3", DbType.String,entity.Reamrk3);

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
		public void Update(JJ_Swt_Report entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "ChatState", DbType.Int32,entity.ChatState);
			db.AddInParameter(dbc, "Title", DbType.String,entity.Title);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, entity.BeginTime);
			db.AddInParameter(dbc, "Equipment", DbType.String,entity.Equipment);
			db.AddInParameter(dbc, "Disease", DbType.String,entity.Disease);
			db.AddInParameter(dbc, "BeginUrl", DbType.String,entity.BeginUrl);
			db.AddInParameter(dbc, "ChatUrl", DbType.String,entity.ChatUrl);
			db.AddInParameter(dbc, "FromUrl", DbType.String,entity.FromUrl);
			db.AddInParameter(dbc, "KeyWords", DbType.String,entity.KeyWords);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
			db.AddInParameter(dbc, "Reamrk1", DbType.String,entity.Reamrk1);
			db.AddInParameter(dbc, "Reamrk2", DbType.String,entity.Reamrk2);
			db.AddInParameter(dbc, "Reamrk3", DbType.String,entity.Reamrk3);

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
		public JJ_Swt_Report GetByID(int id)
		{
			JJ_Swt_Report entity = null;
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
        /// 获取所有商务通
        /// </summary>
        /// <returns></returns>
        public IList<JJ_Swt_Report> GetAllJJ_Swt_Report(int hospitalID)
        {
            IList<JJ_Swt_Report> lst = new List<JJ_Swt_Report>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GETALL_JJ_Swt_Report);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospitalID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    JJ_Swt_Report ent = new JJ_Swt_Report();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }
        /// <summary>
        /// 返回通过sectionID获取商务通
        /// </summary>
        /// <returns></returns>
        public IList<JJ_Swt_Report> GetAllJJ_Swt_Report(int hospitalID, int sectionID)
        {
            IList<JJ_Swt_Report> lst = new List<JJ_Swt_Report>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_SECTIONID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, sectionID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospitalID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    JJ_Swt_Report ent = new JJ_Swt_Report();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<JJ_Swt_Report> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<JJ_Swt_Report> lst = new List<JJ_Swt_Report>();
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
				JJ_Swt_Report ent = new JJ_Swt_Report();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}


        /// <summary>
        /// 对话统计报表
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>DataSet</returns>
        public DataSet GetJJ_Swt_ReportHospital(DateTime beginTime, DateTime endTime, int hospital)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_JJ_Swt_Report);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospital);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
        }

        /// <summary>
        /// 获取商务通各个字段聚合列表
        /// </summary>
        public DataSet GetSwtTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string SectionID, int tableSort, int chatState)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_JJ_Swt_Report_Statistics);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "SectionStr", DbType.String, SectionID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospital);
            db.AddInParameter(dbc, "chatState", DbType.Int32, chatState);
            db.AddInParameter(dbc, "tableSort", DbType.Int32, tableSort);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
        }

        internal DataSet GetBaiduPlanTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, string account, string chatState)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_JJ_Swt_Report_BAIDU_PLAN);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "SectionStr", DbType.String, Section);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospital);
            db.AddInParameter(dbc, "ChatStateStr", DbType.String, chatState);
            db.AddInParameter(dbc, "JJ_AccountStr", DbType.String, account);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
        }

        internal DataSet GetBaiduKeyWordsTableSortStatistics(DateTime beginTime, DateTime endTime, int hospital, string Section, string account, string chatState)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_JJ_Swt_Report_BAIDU_KEYWORDS);
            db.AddInParameter(dbc, "BeginTime", DbType.DateTime, beginTime);
            db.AddInParameter(dbc, "EndTime", DbType.DateTime, endTime);
            db.AddInParameter(dbc, "SectionStr", DbType.String, Section);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospital);
            db.AddInParameter(dbc, "ChatStateStr", DbType.String, chatState);
            db.AddInParameter(dbc, "JJ_AccountStr", DbType.String, account);
            using (DataSet dr = db.ExecuteDataSet(dbc))
            {
                return dr;
            }
        }
    }
}
