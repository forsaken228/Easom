// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2017-01-14
// 作者：loskiv@gmail.com

using System;
using System.Collections.Generic;

namespace Easom.Core.Contracts
{

	///<summary>
	/// IOperationLog  契约接口类
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
