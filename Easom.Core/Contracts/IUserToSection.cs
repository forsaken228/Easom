// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IUserToSection  ��Լ�ӿ���
	///</summary>
	public interface IUserToSection
	{
		int Insert(UserToSection entity);

		void Delete(int id);

		void Update(UserToSection entity);

        UserToSection GetBySectionIDAndUserID(int userID, int sectionID);

        IList<UserToSection> GetByUserID(int id);

        void DeleteBySectionIDAndUserID(int userID,int sectionID);

        bool UpdateUserToSection(int userID, int sectionID, bool isDefault); 

		IList<UserToSection> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

       
	}
}
