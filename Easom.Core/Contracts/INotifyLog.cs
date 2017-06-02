// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-05-19
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
namespace Easom.Core.Contracts
{

	///<summary>
	/// INotifyLog  ��Լ�ӿ���
	///</summary>
	public interface INotifyLog
	{
        int Insert(NotifyLog entity);

        void Delete(int id);

        void Update(NotifyLog entity);

        IList<NotifyLog> GetByID(int userID);

        NotifyLog GetByUserID(int userID);

        NotifyLog TopGetByUserID(int userID);

        IList<NotifyLog> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);
    }
}
