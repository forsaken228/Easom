// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-18
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
using System.Data;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IJJ_KeyWords  ��Լ�ӿ���
	///</summary>
	public interface IJJ_KeyWords
	{
        long Insert(JJ_KeyWords entity);

        void Delete(int id);

        int DeleteByAccountID(int accountID );

        void Update(JJ_KeyWords entity);

        JJ_KeyWords GetByID(int id);

        IList<JJ_KeyWords> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
