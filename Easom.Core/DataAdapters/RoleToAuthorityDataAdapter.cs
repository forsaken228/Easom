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
	/// RoleToAuthorityDataAdapter  ���ݿ������
	///</summary>
	internal class RoleToAuthorityDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_RoleToAuthority_Insert";
		private const string SQL_DELETE = "usp_RoleToAuthority_Delete";
		private const string SQL_UPDATE = "usp_RoleToAuthority_Update";
		private const string SQL_GET_BY_ID = "usp_RoleToAuthority_GetByID";
		private const string SQL_SEARCH = "usp_RoleToAuthority_Search";
		private const string _fields="RoleID,AuthorityID";

		///<summary>
		/// ��IDataReader�ж�ȡһ��ʵ�����
		///</summary>
		private RoleToAuthority GetByReader(IDataReader dr)
		{
			RoleToAuthority entity = new RoleToAuthority();
			object obj =null;
			obj = dr["RoleID"];
			if (obj != null && obj != DBNull.Value) { entity.RoleID = Convert.ToInt32(obj); }
			obj = dr["AuthorityID"];
			if (obj != null && obj != DBNull.Value) { entity.AuthorityID = Convert.ToInt32(obj); }
			return entity;
		}

		///<summary>
		/// ����һ����¼
		///</summary>
		public int Insert(RoleToAuthority entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
			db.AddInParameter(dbc, "RoleID", DbType.Int32,entity.RoleID);
			db.AddInParameter(dbc, "AuthorityID", DbType.Int32,entity.AuthorityID);

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
		public void Update(RoleToAuthority entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "RoleID", DbType.Int32,entity.RoleID);
			db.AddInParameter(dbc, "AuthorityID", DbType.Int32,entity.AuthorityID);

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
		public RoleToAuthority GetByID(int id)
		{
			RoleToAuthority entity = null;
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
		/// ������¼
		///</summary>
		public IList<RoleToAuthority> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<RoleToAuthority> lst = new List<RoleToAuthority>();
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
				RoleToAuthority ent = new RoleToAuthority();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}

	}
}
