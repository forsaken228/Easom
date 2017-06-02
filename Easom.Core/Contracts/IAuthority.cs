// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IAuthority  契约接口类
	///</summary>
	public interface IAuthority
	{
		int Insert(Authority entity);

		void Delete(int id);

        /// <summary>
        /// 根据ParentID删除数据
        /// </summary>
        /// <param name="ParentID"></param>
        void DeleteByParentID(int ParentID); 

		void Update(Authority entity);

		Authority GetByID(int id);
        /// <summary>
        /// 检验authorityKey是否存在
        /// </summary>
        /// <param name="authorityKey"></param>
        /// <returns></returns>
        Authority GetByAuthorityKey(string authorityKey);
       
        IList<Authority> GetAllAuthor();
       /// <summary>
        /// 添加数据RoleToAuthority
       /// </summary>
       /// <param name="roleID"></param>
       /// <param name="authorID"></param>
       /// <returns></returns>
        bool AddAuthors(int roleID, int authorID);

        IList<Authority> GetByRoleID(int id);

		IList<Authority> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
