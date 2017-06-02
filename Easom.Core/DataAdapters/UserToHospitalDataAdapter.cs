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
	/// UserToHospitalDataAdapter  ���ݿ������
	///</summary>
	internal class UserToHospitalDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_UserToHospital_Insert";
		private const string SQL_DELETE = "usp_UserToHospital_Delete";
		private const string SQL_UPDATE = "usp_UserToHospital_Update";
		private const string SQL_GET_BY_ID = "usp_UserToHospital_GetByID";
        private const string SQL_GET_BY_USERID_HOSPITALID = "usp_UserToHospital_GetByUserIDAndHospitalID";
		private const string SQL_SEARCH = "usp_UserToHospital_Search";
		private const string _fields="UserID,HospitalID,IsDefault";

		///<summary>
		/// ��IDataReader�ж�ȡһ��ʵ�����
		///</summary>
		private UserToHospital GetByReader(IDataReader dr)
		{
			UserToHospital entity = new UserToHospital();
			object obj =null;
			obj = dr["UserID"];
			if (obj != null && obj != DBNull.Value) { entity.UserID = Convert.ToInt32(obj); }
			obj = dr["HospitalID"];
			if (obj != null && obj != DBNull.Value) { entity.HospitalID = Convert.ToInt32(obj); }
			obj = dr["IsDefault"];
			if (obj != null && obj != DBNull.Value) { entity.IsDefault = Convert.ToBoolean(obj); }
			return entity;
		}

		///<summary>
		/// ����һ����¼
		///</summary>
		public int Insert(UserToHospital entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddInParameter(dbc, "UserID", DbType.Int32,entity.UserID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "IsDefault", DbType.Boolean,entity.IsDefault);

			try
			 {
				db.ExecuteNonQuery(dbc);
			}
			catch
			{
				throw;
			}

			return (int)db.GetParameterValue(dbc, "");
		}

		///<summary>
		/// ɾ��һ����¼
		///</summary>
		public void Delete(int userID,int hospitalID)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_DELETE);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userID);
            db.AddInParameter(dbc, "HospitalID", DbType.Int32, hospitalID);
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
		public void Update(UserToHospital entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "UserID", DbType.Int32,entity.UserID);
			db.AddInParameter(dbc, "HospitalID", DbType.Int32,entity.HospitalID);
			db.AddInParameter(dbc, "IsDefault", DbType.Boolean,entity.IsDefault);

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
		public UserToHospital GetByID(int id)
		{
			UserToHospital entity = null;
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
        /// ��ȡһ����¼
        ///</summary>
        public UserToHospital GetByUserIDAndHospitalID(int userId,int hospitalID)
        {
            UserToHospital entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_USERID_HOSPITALID);
            db.AddInParameter(dbc, "UserID", DbType.Int32, userId);
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


		///<summary>
		/// ������¼
		///</summary>
		public IList<UserToHospital> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<UserToHospital> lst = new List<UserToHospital>();
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
				UserToHospital ent = new UserToHospital();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
