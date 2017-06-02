// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// ISection  ��Լ�ӿ���
	///</summary>
	public interface ISection
	{
		int Insert(Section entity);

		void Delete(int id);

		void Update(Section entity);

		Section GetByID(int id);
        /// <summary>
        /// ����HospitalID��ISDelete=0��ȡ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<Section> GetByHospitalID(int id);
        /// <summary>
        /// GetByUserID���ݵ�ǰ�û�ѡ��Ψһ��hosipital����ȡ��ǰҽԺѡ��Ŀ��ң����Զ�ѡ
        /// </summary>
        /// <param name="id">userid</param>
        /// <param name="isDefault">1����sectiontouserѡ��</param>
        /// <param name="hosipitalID">��ǰҽԺ��id</param>
        /// <returns></returns>
        IList<Section> GetByUserID(int id, int isDefault,int hosipitalID);
        /// <summary>
        /// InsertUserToSection��ǰҽԺ�������
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hospitalID"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        bool InsertUserToSection(int userID, int hospitalID, bool isDefault); 

		IList<Section> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
