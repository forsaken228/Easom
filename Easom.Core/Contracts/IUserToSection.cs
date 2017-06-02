// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IUserToSection  契约接口类
	///</summary>
	public interface IUserToSection
	{
		int Insert(UserToSection entity);

		void Delete(int id);

		void Update(UserToSection entity);

        UserToSection GetBySectionIDAndUserID(int userID, int sectionID);

        IList<UserToSection> GetByUserID(int id);

        void DeleteBySectionIDAndUserID(int userID,int sectionID);

        bool UpdateUserToSection(int userID, int sectionID, bool isDefault); 

		IList<UserToSection> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

       
	}
}
