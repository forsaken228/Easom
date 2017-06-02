using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easom.Core.DataAdapters
{
    public class DataAdapter
    {
        private readonly string _dbName = "CoreDB";
        /// <summary>
        /// 需要实例化对象
        /// </summary>
        /// <param name="dbName"></param>
        public DataAdapter(string dbName)
        {
            _dbName = dbName;
        }
        ///<summary>
        /// 从IDataReader中读取一个实体对象
        ///</summary>
        private Authority GetByReader(IDataReader dr)
        {
            Authority entity = new Authority();
            object obj = null;
            obj = dr["ID"];
            if (obj != null && obj != DBNull.Value) { entity.ID = Convert.ToInt32(obj); }
            obj = dr["ParentID"];
            if (obj != null && obj != DBNull.Value) { entity.ParentID = Convert.ToInt32(obj); }
            obj = dr["Name"];
            entity.Name = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Name = Convert.ToString(obj); }
            obj = dr["AuthorityKey"];
            entity.AuthorityKey = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.AuthorityKey = Convert.ToString(obj); }
            obj = dr["SortNum"];
            if (obj != null && obj != DBNull.Value) { entity.SortNum = Convert.ToInt32(obj); }
            obj = dr["CroupNumber"];
            if (obj != null && obj != DBNull.Value) { entity.CroupNumber = Convert.ToInt32(obj); }
            obj = dr["Remark"];
            entity.Remark = Convert.ToString(obj);
            if (obj != null && obj != DBNull.Value) { entity.Remark = Convert.ToString(obj); }
            return entity;
        }
       
    }
}
