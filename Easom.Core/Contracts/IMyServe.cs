// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IMyServe  ��Լ�ӿ���
	///</summary>
	public interface IMyServe
	{
		int Insert(MyServe entity);

		void Delete(int id);

		void Update(MyServe entity);

		MyServe GetByID(int id);

		IList<MyServe> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

        /// <summary>
        /// ��ȡ���з���
        /// </summary>
        /// <returns>���񼯺�</returns>
        IList<MyServe> GetByAllServe();

        MyServe GetByKey(string ServeKey);
    }
}
