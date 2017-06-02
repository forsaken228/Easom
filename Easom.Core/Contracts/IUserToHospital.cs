// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IUserToHospital  契约接口类
	///</summary>
	public interface IUserToHospital
	{
		int Insert(UserToHospital entity);

		void Delete(int userID,int hospitalID);

		void Update(UserToHospital entity);

		UserToHospital GetByID(int id);

        UserToHospital GetByUserIDAndHospitalID(int userID, int hospitalID);

		IList<UserToHospital> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
