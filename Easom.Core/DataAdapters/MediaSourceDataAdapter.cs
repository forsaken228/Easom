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
	/// MediaSourceDataAdapter  数据库访问类
	///</summary>
	internal class MediaSourceDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_MediaSource_Insert";
        private const string SQL_INSERT_ALLMEDIA = "usp_MediaSource_InsertMediaSource";
        private const string SQL_INSERT_ALLMEDIA_NEW = "usp_MediaSource_InsertMediaSource_New";
		private const string SQL_DELETE = "usp_MediaSource_Delete";
		private const string SQL_UPDATE = "usp_MediaSource_Update";
		private const string SQL_GET_BY_ID = "usp_MediaSource_GetByID";
		private const string SQL_SEARCH = "usp_MediaSource_Search";
		private const string _fields="ID,ParentID,Name,HospitalID";
        private const string SQL_PARENT_DELETE = "usp_MediaSource_Parent_Delete";
        private const string SQL_GETALL_MEDIASOURCE = "usp_MediaSoutce_GetALL";
        private const string SQL_GET_BY_PARENT = "usp_MediaSoutce_GetALLBYParentID";
        private const string SQL_GET_BY_NAME = "usp_MediaSource_GetByName";


		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private MediaSource GetByReader(IDataReader dr)
		{
			MediaSource entity = new MediaSource();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["ParentID"];
			if (obj != null && obj != DBNull.Value) { entity.ParentID = Convert.ToInt32(obj); }
			obj = dr["Name"];
			entity.Name = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(MediaSource entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "ParentID", DbType.Int32,entity.ParentID);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);

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
        /// 此为以前的医院
        ///</summary>
        public int InsertAllMedia(int hoapitalID)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT_ALLMEDIA);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hoapitalID);
            try
            {
                db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }
            return 1;
        }

        ///<summary>
        /// 此为新医院
        ///</summary>
        public int InsertAllMediaNew(int hoapitalID)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT_ALLMEDIA_NEW);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hoapitalID);
            try
            {
                db.ExecuteNonQuery(dbc);
            }
            catch
            {
                throw;
            }
            return 1;
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
		public void Update(MediaSource entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "ParentID", DbType.Int32,entity.ParentID);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);

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
		public MediaSource GetByID(int id)
		{
			MediaSource entity = null;
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

        public MediaSource GetByName(int hospitalID,string name)
        {
            MediaSource entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_NAME);
            db.AddInParameter(dbc, "Name", DbType.String, name);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospitalID);
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
        public IList<MediaSource> GetAllMediaSource(int hospitalID)
        {
            IList<MediaSource> lst = new List<MediaSource>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GETALL_MEDIASOURCE);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospitalID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    MediaSource ent = new MediaSource();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }
        /// <summary>
        /// 返回通过parentid获取媒体类型
        /// </summary>
        /// <returns></returns>
        public IList<MediaSource> GetAllMediaSource(int hospitalID,int parentid)
        {
            IList<MediaSource> lst = new List<MediaSource>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_PARENT);
            db.AddInParameter(dbc, "ParentID", DbType.Int32, parentid);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospitalID);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    MediaSource ent = new MediaSource();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }


        /// <summary>
        /// 删除子项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteParentID(int id)
        {
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_PARENT_DELETE);
            db.AddInParameter(dbc, "ParentID", DbType.Int32, id);
            int num = db.ExecuteNonQuery(dbc);
            return num > 0 ? true : false;

        }

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<MediaSource> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<MediaSource> lst = new List<MediaSource>();
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
				MediaSource ent = new MediaSource();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
