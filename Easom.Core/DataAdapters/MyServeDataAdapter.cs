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
	/// MyServeDataAdapter  数据库访问类
	///</summary>
	internal class MyServeDataAdapter
	{
		private readonly string _dbName = "CoreDB";
		private const string SQL_INSERT = "usp_MyServe_Insert";
		private const string SQL_DELETE = "usp_MyServe_Delete";
		private const string SQL_UPDATE = "usp_MyServe_Update";
		private const string SQL_GET_BY_ID = "usp_MyServe_GetByID";
		private const string SQL_SEARCH = "usp_MyServe_Search";
        private const string SQL_GetAll = "usp_MyServe_GetByAll";
        private const string SQL_GET_BY_KEY = "usp_MyServe_GetByKey";
        private const string _fields = "ID,ServeName,RepertoryNum,Price,Remark,ServeKey";

		///<summary>
		/// 从IDataReader中读取一个实体对象
		///</summary>
		private MyServe GetByReader(IDataReader dr)
		{
			MyServe entity = new MyServe();
			object obj =null;
			obj = dr["ID"];
			if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
			obj = dr["ServeName"];
			entity.ServeName = Convert.ToString(obj);
			if (obj != null && obj != DBNull.Value) { entity.ServeName = Convert.ToString(obj); }
			obj = dr["RepertoryNum"];
			if (obj != null && obj != DBNull.Value) { entity.RepertoryNum = Convert.ToInt32(obj); }
            obj = dr["Price"];
            if (obj != null && obj != DBNull.Value) { entity.Price = Convert.ToDecimal(obj); }
            obj = dr["Remark"];
            if (obj != null && obj != DBNull.Value) { entity.Remark = Convert.ToString(obj); }
            obj = dr["ServeKey"];
            if (obj != null && obj != DBNull.Value) { entity.ServeKey = Convert.ToString(obj); }
			return entity;
		}

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(MyServe entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_INSERT);
            db.AddOutParameter(dbc, "ID", DbType.Int32, entity.ID);
			db.AddInParameter(dbc, "ServeName", DbType.String,entity.ServeName);
			db.AddInParameter(dbc, "RepertoryNum", DbType.Int32,entity.RepertoryNum);
            db.AddInParameter(dbc, "Price", DbType.Decimal, entity.Price);
            db.AddInParameter(dbc, "Remark", DbType.String, entity.Remark);
            db.AddInParameter(dbc, "ServeKey", DbType.String, entity.ServeKey);
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
		public void Update(MyServe entity)
		{
			Database db = DBConfigUtility.GetDatabase(_dbName);
			DbCommand dbc = db.GetStoredProcCommand(SQL_UPDATE);
			db.AddInParameter(dbc, "ID", DbType.Int32,entity.ID);
			db.AddInParameter(dbc, "ServeName", DbType.String,entity.ServeName);
			db.AddInParameter(dbc, "RepertoryNum", DbType.Int32,entity.RepertoryNum);
            db.AddInParameter(dbc, "Price", DbType.Decimal, entity.Price);
            db.AddInParameter(dbc, "Remark", DbType.String, entity.Remark);
            db.AddInParameter(dbc, "ServeKey", DbType.String, entity.ServeKey);
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
		public MyServe GetByID(int id)
		{
			MyServe entity = null;
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
		/// 搜索记录
		///</summary>
		public IList<MyServe> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			IList<MyServe> lst = new List<MyServe>();
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
				MyServe ent = new MyServe();
				ent = GetByReader(dr);
				lst.Add(ent);
				}
			}
			pageCount = (int)db.GetParameterValue(dbc, "pageCount");
			return lst;
		}


        public IList<MyServe> GetByAllServe()
        {
            IList<MyServe> lst = new List<MyServe>();
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GetAll);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                while (dr.Read())
                {
                    MyServe ent = new MyServe();
                    ent = GetByReader(dr);
                    lst.Add(ent);
                }
            }
            return lst;
        }

        internal MyServe GetByKey(string ServeKey)
        {
            MyServe entity = null;
            Database db = DBConfigUtility.GetDatabase(_dbName);
            DbCommand dbc = db.GetStoredProcCommand(SQL_GET_BY_KEY);
            db.AddInParameter(dbc, "ServeKey", DbType.String, ServeKey);
            using (IDataReader dr = db.ExecuteReader(dbc))
            {
                if (dr.Read())
                {
                    entity = GetByReader(dr);
                }
            }
            return entity;
        }

    }
}
