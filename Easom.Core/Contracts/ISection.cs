// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// ISection  契约接口类
	///</summary>
	public interface ISection
	{
		int Insert(Section entity);

		void Delete(int id);

		void Update(Section entity);

		Section GetByID(int id);
        /// <summary>
        /// 根据HospitalID，ISDelete=0读取科室
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<Section> GetByHospitalID(int id);
        /// <summary>
        /// GetByUserID根据当前用户选择唯一的hosipital，获取当前医院选择的科室，可以多选
        /// </summary>
        /// <param name="id">userid</param>
        /// <param name="isDefault">1代表sectiontouser选中</param>
        /// <param name="hosipitalID">当前医院的id</param>
        /// <returns></returns>
        IList<Section> GetByUserID(int id, int isDefault,int hosipitalID);
        /// <summary>
        /// InsertUserToSection向当前医院插入科室
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hospitalID"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        bool InsertUserToSection(int userID, int hospitalID, bool isDefault); 

		IList<Section> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
