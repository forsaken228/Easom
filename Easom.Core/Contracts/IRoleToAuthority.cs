// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IRoleToAuthority  ��Լ�ӿ���
	///</summary>
	public interface IRoleToAuthority
	{
		int Insert(RoleToAuthority entity);

		void Delete(int id);

		void Update(RoleToAuthority entity);

		RoleToAuthority GetByID(int id);

		IList<RoleToAuthority> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
