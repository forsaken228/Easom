// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IUserToHospital  ��Լ�ӿ���
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
