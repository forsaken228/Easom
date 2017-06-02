// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IUserInfo  契约接口类
	///</summary>
	public interface IUserInfo
	{
		int Insert(UserInfo entity);

		void Delete(int id);

		void Update(UserInfo entity);

		UserInfo GetByID(int id);

        bool DeleteByUserID(int id);

        UserInfo GetByName(string name);

        IList<string> GetByNameUserID(int id);

        int GetIsDefaluteByID(int id);

		IList<UserInfo> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        IList<UserInfo> ViewSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);
        
        /// <summary>
        /// 通过角色和科室获取数据
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="sectionID">科室ID</param>
        /// <returns>用户集合</returns>
        IList<UserInfo> GetByRoleAndSection(int roleID,int sectionID);

        bool UpdateCurrentHospital(int userid, int hospitalid);

        bool UpdateUserToSection(int userid, int sectionid);

        IList<UserInfo> UserInfoNotInNotifierSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc);
    }
}
