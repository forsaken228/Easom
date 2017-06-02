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
	/// HospitalWebsiteDataAdapter  数据库访问类
	///</summary>
	internal class HospitalWebsiteDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_HospitalWebsite_Insert";
		private const string SQL_DELETE = "usp_HospitalWebsite_Delete";
		private const string SQL_UPDATE = "usp_HospitalWebsite_Update";
		private const string SQL_GET_BY_ID = "usp_HospitalWebsite_GetByID";
        private const string SQL_GET_BY_SECTIONID = "usp_HospitalWebsite_GetBySectionID";
		private const string SQL_SEARCH = "usp_HospitalWebsite_Search";
		private const string _fields="ID,HospitalID,Title,URL,SectionID,SiteType";
        private const string SQL_GET_BY_URL = "usp_HospitalWebsite_GetByURL";
        private const string SQL_GET_BY_SECTIONS = "usp_HospitalWebsite_GetBySections";
		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private HospitalWebsite GetByReader(IDataReader dr)
		{
			HospitalWebsite entity = new HospitalWebsite();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			obj = dr["Title"];
			entity.Title = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Title = Convert.ToString(obj); }
			obj = dr["URL"];
			entity.URL = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.URL = Convert.ToString(obj); }
			obj = dr["SectionID"];
			if (obj != null && obj != DBNull.Value) { entity.SectionID = Convert.ToInt32(obj); }
			obj = dr["SiteType"];
			if (obj != null && obj != DBNull.Value) { entity.SiteType = Convert.ToInt32(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(HospitalWebsite entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "Title", DbType.String,entity.Title);
			db.AddInParameter(dbc, "URL", DbType.String,entity.URL);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
			db.AddInParameter(dbc, "SiteType", DbType.Int32,entity.SiteType);

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
		public void Update(HospitalWebsite entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "Title", DbType.String,entity.Title);
			db.AddInParameter(dbc, "URL", DbType.String,entity.URL);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
			db.AddInParameter(dbc, "SiteType", DbType.Int32,entity.SiteType);

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
		public HospitalWebsite GetByID(int id)
		{
			HospitalWebsite entity = null;
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
        /// 读取一条记录
        ///</summary>
        public HospitalWebsite GetByURL(string url)
        {
            HospitalWebsite entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_URL);
            db.AddInParameter(dbc, "URL", DbType.String, url);
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
        /// 获取所有科室下面的网站
        /// </summary>
        /// <param name="sectionID">科室ID</param>
        /// <returns>医院网站</returns>
        public IList<HospitalWebsite> GetAllHospitalWebsite(int sectionID,int type)
        {
            IList<HospitalWebsite> lst = new List<HospitalWebsite>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_SECTIONID);
            db.AddInParameter(dbc, "SectionID", DbType.Int32, sectionID);
            db.AddInParameter(dbc, "SiteType", DbType.Int32, type);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    HospitalWebsite entity = new HospitalWebsite();
                    entity = GetByReader(dr);
                    lst.Add(entity);
                }
            }
            return lst;
        }


        /// <summary>
        /// 获取当前用户选择医院的所有科室的预约挂站
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hosipitalID"></param>
        /// <returns></returns>
        public IList<HospitalWebsite> GetBySections(int userID, int hosipitalID)
        {
            IList<HospitalWebsite> lst = new List<HospitalWebsite>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_SECTIONS);
            db.AddInParameter(dbc, "ID", DbType.Int32, userID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hosipitalID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    HospitalWebsite ent = new HospitalWebsite();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<HospitalWebsite> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<HospitalWebsite> lst = new List<HospitalWebsite>();
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
				HospitalWebsite ent = new HospitalWebsite();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
