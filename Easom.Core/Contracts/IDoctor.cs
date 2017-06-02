// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IDoctor  契约接口类
	///</summary>
	public interface IDoctor
	{
		int Insert(Doctor entity);

		void Delete(int id);

		void Update(Doctor entity);

		Doctor GetByID(int id);

        /// <summary>
        /// 获取该科室的所有医生
        /// </summary>
        /// <param name="sectionID">科室ID</param>
        /// <returns>医生集合</returns>
        IList<Doctor> GetBySectionID(int sectionID);
        /// <summary>
        /// 获取当前用户选择医院的所有科室
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hosipitalID"></param>
        /// <returns></returns>
        IList<Doctor> GetBySections(int userID, int hosipitalID);

		IList<Doctor> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
