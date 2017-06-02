// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IAuthority  ��Լ�ӿ���
	///</summary>
	public interface IAuthority
	{
		int Insert(Authority entity);

		void Delete(int id);

        /// <summary>
        /// ����ParentIDɾ������
        /// </summary>
        /// <param name="ParentID"></param>
        void DeleteByParentID(int ParentID); 

		void Update(Authority entity);

		Authority GetByID(int id);
        /// <summary>
        /// ����authorityKey�Ƿ����
        /// </summary>
        /// <param name="authorityKey"></param>
        /// <returns></returns>
        Authority GetByAuthorityKey(string authorityKey);
       
        IList<Authority> GetAllAuthor();
       /// <summary>
        /// �������RoleToAuthority
       /// </summary>
       /// <param name="roleID"></param>
       /// <param name="authorID"></param>
       /// <returns></returns>
        bool AddAuthors(int roleID, int authorID);

        IList<Authority> GetByRoleID(int id);

		IList<Authority> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
