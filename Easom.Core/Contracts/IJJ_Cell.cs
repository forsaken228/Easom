// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-18
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IJJ_Cell  契约接口类
	///</summary>
	public interface IJJ_Cell
	{
		int Insert(JJ_Cell entity);

		void Delete(int id);

		void Update(JJ_Cell entity);

		JJ_Cell GetByID(int id);

		IList<JJ_Cell> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
