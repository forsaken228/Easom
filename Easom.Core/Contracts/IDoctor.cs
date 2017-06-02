// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IDoctor  ��Լ�ӿ���
	///</summary>
	public interface IDoctor
	{
		int Insert(Doctor entity);

		void Delete(int id);

		void Update(Doctor entity);

		Doctor GetByID(int id);

        /// <summary>
        /// ��ȡ�ÿ��ҵ�����ҽ��
        /// </summary>
        /// <param name="sectionID">����ID</param>
        /// <returns>ҽ������</returns>
        IList<Doctor> GetBySectionID(int sectionID);
        /// <summary>
        /// ��ȡ��ǰ�û�ѡ��ҽԺ�����п���
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="hosipitalID"></param>
        /// <returns></returns>
        IList<Doctor> GetBySections(int userID, int hosipitalID);

		IList<Doctor> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
