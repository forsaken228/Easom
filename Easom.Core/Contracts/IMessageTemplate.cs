// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IMessageTemplate  ��Լ�ӿ���
	///</summary>
	public interface IMessageTemplate
	{
		int Insert(MessageTemplate entity);

		void Delete(int id);

		void Update(MessageTemplate entity);

		MessageTemplate GetByID(int id);

		IList<MessageTemplate> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
