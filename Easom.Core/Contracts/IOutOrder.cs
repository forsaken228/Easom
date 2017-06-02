// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IOutOrder  ��Լ�ӿ���
	///</summary>
	public interface IOutOrder
	{
		int Insert(OutOrder entity);

		void Delete(int id);

		void Update(OutOrder entity);

		OutOrder GetByID(int id);

		IList<OutOrder> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
