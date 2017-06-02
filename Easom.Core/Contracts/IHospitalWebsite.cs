// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IHospitalWebsite  ��Լ�ӿ���
	///</summary>
	public interface IHospitalWebsite
	{
		int Insert(HospitalWebsite entity);

		void Delete(int id);

		void Update(HospitalWebsite entity);

		HospitalWebsite GetByID(int id);

        HospitalWebsite GetByURL(string url);

        /// <summary>
        /// ��ȡ���п����������վ
        /// </summary>
        /// <param name="sectionID">����ID</param>
        /// <returns>ҽԺ��վ</returns>
        IList<HospitalWebsite> GetAllHospitalWebsite(int sectionID,int type);

        /// <summary>
        /// ��ȡ��ǰ�û�ѡ��ҽԺ�����п��ҵ�ԤԼ�Һ���վ
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hosipitalID"></param>
        /// <returns></returns>
        IList<HospitalWebsite> GetBySections(int userID, int hosipitalID);

		IList<HospitalWebsite> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
