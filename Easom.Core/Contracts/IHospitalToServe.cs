// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IHospitalToServe  契约接口类
	///</summary>
	public interface IHospitalToServe
	{
		int Insert(HospitalToServe entity);

		void Delete(int id);

		void Update(HospitalToServe entity);

		HospitalToServe GetByID(int id);

		IList<HospitalToServe> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        /// <summary>
        /// 根据HospitalID和服务Key获取服务数据
        /// </summary>
        /// <param name="hospitalID">医院ID</param>
        /// <param name="serverKey">服务Key</param>
        /// <returns>HospitalToServe</returns>
        HospitalToServe GetByHospitalID(int hospitalID,string serverKey);
    }
}
