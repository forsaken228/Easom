// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-05-19
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
namespace Easom.Core.Contracts
{

	///<summary>
	/// INotifier  契约接口类
	///</summary>
	public interface INotifier
	{
        int Insert(Notifier entity);

        bool Delete(int id);

        bool Update(Notifier entity);

        Notifier GetByID(int id);

        IList<Notifier> GetAllData();

        IList<Notifier> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        IList<Notifier> NotifierNotifyLogSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);
    }
}
