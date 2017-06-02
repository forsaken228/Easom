// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IDisease  ��Լ�ӿ���
	///</summary>
	public interface IDisease
	{
		int Insert(Disease entity);

		void Delete(int id);

		void Update(Disease entity);

		Disease GetByID(int id);

        IList<Disease> GetAllDisease(int hospitalid);

        IList<Disease> GetDiseaseBySection(string section);

		IList<Disease> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}