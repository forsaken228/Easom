// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IHospitalWebsite  契约接口类
	///</summary>
	public interface IHospitalWebsite
	{
		int Insert(HospitalWebsite entity);

		void Delete(int id);

		void Update(HospitalWebsite entity);

		HospitalWebsite GetByID(int id);

        HospitalWebsite GetByURL(string url);

        /// <summary>
        /// 获取所有科室下面的网站
        /// </summary>
        /// <param name="sectionID">科室ID</param>
        /// <returns>医院网站</returns>
        IList<HospitalWebsite> GetAllHospitalWebsite(int sectionID,int type);

        /// <summary>
        /// 获取当前用户选择医院的所有科室的预约挂号网站
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hosipitalID"></param>
        /// <returns></returns>
        IList<HospitalWebsite> GetBySections(int userID, int hosipitalID);

		IList<HospitalWebsite> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
