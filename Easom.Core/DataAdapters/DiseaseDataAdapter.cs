// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;

using CHCMS.Utility;
namespace Easom.Core.DataAdapters
{

	///<summary>
	/// DiseaseDataAdapter  ���ݿ������
	///</summary>
	internal class DiseaseDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_Disease_Insert";
		private const string SQL_DELETE = "usp_Disease_Delete";
		private const string SQL_UPDATE = "usp_Disease_Update";
		private const string SQL_GET_BY_ID = "usp_Disease_GetByID";
        private const string SQL_GET_BY_SECTION = "usp_Disease_GetBySection";
		private const string SQL_SEARCH = "usp_Disease_Search";
		private const string _fields="ID,Priority,HospitalID,IsMain,CreatTime,Cure,Name,Intro,SectionID,IsDelete";
        private const string SQL_GET_ALLDISEASE_BY_HOSPITAL = "usp_Disease_GetAllDisease";
		///<summary>
		/// ��IDataReader�ж�ȡһ��ʵ�����
		///</summary>
		private Disease GetByReader(IDataReader dr)
		{
			Disease entity = new Disease();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["Priority"];
			if (obj != null && obj != DBNull.Value) { entity.Priority = Convert.ToInt32(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			obj = dr["IsMain"];
			if (obj != null && obj != DBNull.Value) { entity.IsMain = Convert.ToBoolean(obj); }
			obj = dr["CreatTime"];
			if (obj != null && obj != DBNull.Value) { entity.CreatTime = Convert.ToDateTime(obj); }
			obj = dr["Cure"];
			entity.Cure = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Cure = Convert.ToString(obj); }
			obj = dr["Name"];
			entity.Name = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
			obj = dr["Intro"];
			entity.Intro = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.Intro = Convert.ToString(obj); }
			obj = dr["SectionID"];
			if (obj != null && obj != DBNull.Value) { entity.SectionID = Convert.ToInt32(obj); }
			obj = dr["IsDelete"];
			if (obj != null && obj != DBNull.Value) { entity.IsDelete = Convert.ToInt32(obj); }
			return entity;
		}

        /// <summary>
        /// ���ص�ǰ�û����м���
        /// </summary>
        /// <param name="hospitalid">��ǰҽԺ</param>
        /// <returns>���м���</returns>
        public IList<Disease> GetAllDisease(int hospitalid)
        {
            IList<Disease> lst = new List<Disease>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_ALLDISEASE_BY_HOSPITAL);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospitalid);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    Disease ent = new Disease();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }


		///<summary>
		/// ����һ����¼
		///</summary>
		public int Insert(Disease entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddOutParameter(dbc, "ID", DbType.Int32,4);
			db.AddInParameter(dbc, "Priority", DbType.Int32,entity.Priority);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "IsMain", DbType.Boolean,entity.IsMain);
			db.AddInParameter(dbc, "CreatTime", DbType.DateTime,entity.CreatTime);
			db.AddInParameter(dbc, "Cure", DbType.String,entity.Cure);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "Intro", DbType.String,entity.Intro);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
			db.AddInParameter(dbc, "IsDelete", DbType.Int32,entity.IsDelete);

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
		/// ɾ��һ����¼
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
		/// ����һ����¼
		///</summary>
		public void Update(Disease entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "Priority", DbType.Int32,entity.Priority);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "IsMain", DbType.Boolean,entity.IsMain);
			db.AddInParameter(dbc, "CreatTime", DbType.DateTime,entity.CreatTime);
			db.AddInParameter(dbc, "Cure", DbType.String,entity.Cure);
			db.AddInParameter(dbc, "Name", DbType.String,entity.Name);
			db.AddInParameter(dbc, "Intro", DbType.String,entity.Intro);
			db.AddInParameter(dbc, "SectionID", DbType.Int32,entity.SectionID);
			db.AddInParameter(dbc, "IsDelete", DbType.Int32,entity.IsDelete);

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
		/// ��ȡһ����¼
		///</summary>
		public Disease GetByID(int id)
		{
			Disease entity = null;
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
        /// ��ȡ�����������м���
        ///</summary>
        public IList<Disease> GetDiseaseBySection(string section)
        {
            IList<Disease> lst = new List<Disease>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_SECTION);
            db.AddInParameter(dbc, "Section", DbType.String, section);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while(dr.Read())
                {
                    Disease entity = new Disease();
                    lst.Add(GetByReader(dr));
                }
            }
            return lst;
        }

        
		///<summary>
		/// ������¼
		///</summary>
		public IList<Disease> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<Disease> lst = new List<Disease>();
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
				Disease ent = new Disease();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
