// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IDisease  契约接口类
	///</summary>
	public interface IDisease
	{
		int Insert(Disease entity);

		void Delete(int id);

		void Update(Disease entity);

		Disease GetByID(int id);

        IList<Disease> GetAllDisease(int hospitalid);

        IList<Disease> GetDiseaseBySection(string section);

		IList<Disease> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
