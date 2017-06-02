// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IRole  契约接口类
	///</summary>
	public interface IRole
	{
		int Insert(Role entity);

		void Delete(int id);

		void Update(Role entity);

		Role GetByID(int id);

        Role GetByName(string name);

        IList<Role> GetByUserID(int id);

		IList<Role> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        bool DeleteByRoleID(int id);

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        IList<Role> GetAllData();

        IList<int> GetByRoleID(int id);
	}
}
