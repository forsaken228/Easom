// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IRole  ��Լ�ӿ���
	///</summary>
	public interface IRole
	{
		int Insert(Role entity);

		void Delete(int id);

		void Update(Role entity);

		Role GetByID(int id);

        Role GetByName(string name);

        IList<Role> GetByUserID(int id);

		IList<Role> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        bool DeleteByRoleID(int id);

        /// <summary>
        /// ��ȡ���н�ɫ
        /// </summary>
        /// <returns></returns>
        IList<Role> GetAllData();

        IList<int> GetByRoleID(int id);
	}
}
