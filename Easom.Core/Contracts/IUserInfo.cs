// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IUserInfo  ��Լ�ӿ���
	///</summary>
	public interface IUserInfo
	{
		int Insert(UserInfo entity);

		void Delete(int id);

		void Update(UserInfo entity);

		UserInfo GetByID(int id);

        bool DeleteByUserID(int id);

        UserInfo GetByName(string name);

        IList<string> GetByNameUserID(int id);

        int GetIsDefaluteByID(int id);

		IList<UserInfo> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        IList<UserInfo> ViewSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);
        
        /// <summary>
        /// ͨ����ɫ�Ϳ��һ�ȡ����
        /// </summary>
        /// <param name="roleID">��ɫID</param>
        /// <param name="sectionID">����ID</param>
        /// <returns>�û�����</returns>
        IList<UserInfo> GetByRoleAndSection(int roleID,int sectionID);

        bool UpdateCurrentHospital(int userid, int hospitalid);

        bool UpdateUserToSection(int userid, int sectionid);

        IList<UserInfo> UserInfoNotInNotifierSearch(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc);
    }
}
