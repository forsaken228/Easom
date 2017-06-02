// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-18
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IJJ_Account  ��Լ�ӿ���
	///</summary>
	public interface IJJ_Account
	{
		int Insert(JJ_Account entity);

		void Delete(int id);

		void Update(JJ_Account entity);

		JJ_Account GetByID(int id);

		
        /// <summary>
        /// ��ȡ�ÿ��ҵ������˻�
        /// </summary>
        /// <param name="sectionID">����ID</param>
        /// <returns>�˻�����</returns>
        IList<JJ_Account> GetBySectionID(int sectionID);
        /// <summary>
        /// ��ȡ��ǰ�û�ѡ��ҽԺ�����п���
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hosipitalID"></param>
        /// <returns></returns>
        IList<JJ_Account> GetBySections(int userID, int hosipitalID);

        IList<JJ_Account> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);
	}
}
