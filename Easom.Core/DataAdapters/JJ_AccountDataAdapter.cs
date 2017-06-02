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
	/// JJ_AccountDataAdapter  数据库访问类
	///</summary>
	internal class JJ_AccountDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_JJ_Account_Insert";
		private const string SQL_DELETE = "usp_JJ_Account_Delete";
		private const string SQL_UPDATE = "usp_JJ_Account_Update";
		private const string SQL_GET_BY_ID = "usp_JJ_Account_GetByID";
        private const string SQL_GET_BY_SECTIONID = "usp_JJ_Account_GetBySectionID";
        private const string SQL_GET_BY_SECTIONS = "usp_JJ_Account_GetBySections";

		private const string SQL_SEARCH = "usp_JJ_Account_Search";
		private const string _fields="ID,AccountName,HospitalID,SectionID,Remark1,Remark2";

		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private JJ_Account GetByReader(IDataReader dr)
		{
			JJ_Account entity = new JJ_Account();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["AccountName"];
			entity.AccountName = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.AccountName = Convert.ToString(obj); }
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
		public int Insert(JJ_Account entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "AccountName", DbType.String,entity.AccountName);
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
		public void Update(JJ_Account entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "AccountName", DbType.String,entity.AccountName);
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
		public JJ_Account GetByID(int id)
		{
			JJ_Account entity = null;
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
        /// 获取该科室的所有帐号
        /// </summary>
        /// <param name="sectionID">科室ID</param>
        /// <returns>帐号集合</returns>
        public IList<JJ_Account> GetBySectionID(int sectionID)
        {
            IList<JJ_Account> lst = new List<JJ_Account>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_SECTIONID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, sectionID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    JJ_Account ent = new JJ_Account();
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
        public IList<JJ_Account> GetBySections(int userID, int hosipitalID)
        {
            IList<JJ_Account> lst = new List<JJ_Account>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_SECTIONS);
            db.AddInParameter(dbc, "ID", DbType.Int32, userID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hosipitalID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    JJ_Account ent = new JJ_Account();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }


		///<summary>
		/// 搜索记录
		///</summary>
		public IList<JJ_Account> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<JJ_Account> lst = new List<JJ_Account>();
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
				JJ_Account ent = new JJ_Account();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
