using Easom.Core.Contracts;
using Easom.Core.DataAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easom.Core.Services
{
    public class DataService<T>
    { 
        public T obj;
        public DataService(T obj)
        {
            this.obj = obj;
        }
        public int Insert(T entity)
        {
            return 0;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public CallOnData GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public IList<CallOnData> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            throw new NotImplementedException();
        }
    }
}
