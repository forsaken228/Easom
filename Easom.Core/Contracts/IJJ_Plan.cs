// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-18
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IJJ_Plan  ��Լ�ӿ���
	///</summary>
	public interface IJJ_Plan
	{
		int Insert(JJ_Plan entity);

		void Delete(int id);

		void Update(JJ_Plan entity);

		JJ_Plan GetByID(int id);

		IList<JJ_Plan> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
