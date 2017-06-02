// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-25
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
using System.Data;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IJJ_DayKeyWords_Report  契约接口类
	///</summary>
	public interface IJJ_DayKeyWords_Report
	{
		long Insert(JJ_DayKeyWords_Report entity);

		void Delete(int id);

		void Update(JJ_DayKeyWords_Report entity);

		JJ_DayKeyWords_Report GetByID(int id);

		IList<JJ_DayKeyWords_Report> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);


        int DeleteDataByDay(int accountID, System.DateTime begintime, System.DateTime endtime); 
	}
}
