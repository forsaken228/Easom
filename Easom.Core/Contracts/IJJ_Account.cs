// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-18
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IJJ_Account  契约接口类
	///</summary>
	public interface IJJ_Account
	{
		int Insert(JJ_Account entity);

		void Delete(int id);

		void Update(JJ_Account entity);

		JJ_Account GetByID(int id);

		
        /// <summary>
        /// 获取该科室的所有账户
        /// </summary>
        /// <param name="sectionID">科室ID</param>
        /// <returns>账户集合</returns>
        IList<JJ_Account> GetBySectionID(int sectionID);
        /// <summary>
        /// 获取当前用户选择医院的所有科室
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hosipitalID"></param>
        /// <returns></returns>
        IList<JJ_Account> GetBySections(int userID, int hosipitalID);

        IList<JJ_Account> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);
	}
}
