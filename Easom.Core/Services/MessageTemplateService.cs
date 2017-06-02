// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System;

using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;
using CHCMS.Utility;
namespace Easom.Core.Services
{

	///<summary>
	/// MessageTemplateService  服务处理类
	///</summary>
	public class MessageTemplateService : IMessageTemplate
	{
		private static readonly string _cacheKey = "Easom.Core.MessageTemplateService";
		private static readonly MessageTemplateDataAdapter dal = new MessageTemplateDataAdapter();

		#region IMessageTemplate 成员

		///<summary>
		/// 插入一条记录
		///</summary>
		public int Insert(MessageTemplate entity)
		{
			int r = dal.Insert(entity);
			CacheUtility.Delete(_cacheKey);
			return r;
		}

		///<summary>
		/// 删除一条记录
		///</summary>
		public void Delete(int id)
		{
			dal.Delete(id);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 更新一条记录
		///</summary>
		public void Update(MessageTemplate entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// 读取一条记录
		///</summary>
		public MessageTemplate GetByID(int id)
		{
			MessageTemplate entity = (MessageTemplate)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

		///<summary>
		/// 搜索记录
		///</summary>
		public IList<MessageTemplate> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
		{
			object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderField.ToString(), isDesc.ToString() });
			if (objList == null)
			{
				objList = new object[2];
				objList[0] = dal.Search(out pageCount, pageIndex, pageSize, where, orderField, isDesc);
				objList[1] = pageCount;
				CacheUtility.Add(objList, _cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderField.ToString(), isDesc.ToString() });
			}
			pageCount = Convert.ToInt32(objList[1]);
			return (IList<MessageTemplate>)objList[0];;
		}

		#endregion

	}
}
