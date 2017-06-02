// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IHospital  ��Լ�ӿ���
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
