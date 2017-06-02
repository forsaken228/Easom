// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2017-01-14
// ���ߣ�loskiv@gmail.com

using System;
using System.Collections.Generic;

namespace Easom.Core.Contracts
{

	///<summary>
	/// IOperationLog  ��Լ�ӿ���
	///</summary>
	public interface IOperationLog
	{
        Int64 Insert(OperationLog entity);

		void Delete(int id);

		void Update(OperationLog entity);

		OperationLog GetByID(int id);

		IList<OperationLog> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
