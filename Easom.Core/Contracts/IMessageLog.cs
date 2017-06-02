// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IMessageLog  ��Լ�ӿ���
	///</summary>
	public interface IMessageLog
	{
		int Insert(MessageLog entity);

		void Delete(int id);

		void Update(MessageLog entity);

		MessageLog GetByID(int id);

        IList<MessageLog> GetByTelephone(string telephone); 

		IList<MessageLog> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
