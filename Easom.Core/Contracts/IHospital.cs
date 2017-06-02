// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IHospital  契约接口类
	///</summary>
	public interface IHospital
	{
		int Insert(Hospital entity);

		void Delete(int id);

		void Update(Hospital entity);

		Hospital GetByID(int id);

        IList<Hospital> GetByUserID(int id, int isDefault);

        IList<Hospital> GetByALLHospital();

        bool InsertUserToHospital(int userID, int hospitalID, bool isDefault);

		IList<Hospital> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

       
	}
}
