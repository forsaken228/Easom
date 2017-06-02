// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IHospitalToServe  ��Լ�ӿ���
	///</summary>
	public interface IHospitalToServe
	{
		int Insert(HospitalToServe entity);

		void Delete(int id);

		void Update(HospitalToServe entity);

		HospitalToServe GetByID(int id);

		IList<HospitalToServe> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        /// <summary>
        /// ����HospitalID�ͷ���Key��ȡ��������
        /// </summary>
        /// <param name="hospitalID">ҽԺID</param>
        /// <param name="serverKey">����Key</param>
        /// <returns>HospitalToServe</returns>
        HospitalToServe GetByHospitalID(int hospitalID,string serverKey);
    }
}
