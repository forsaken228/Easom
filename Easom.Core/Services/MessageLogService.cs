// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System;

using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;
using CHCMS.Utility;
namespace Easom.Core.Services
{

	///<summary>
	/// MessageLogService  ��������
	///</summary>
	public class MessageLogService : IMessageLog
	{
		private static readonly string _cacheKey = "Easom.Core.MessageLogService";
		private static readonly MessageLogDataAdapter dal = new MessageLogDataAdapter();

		#region IMessageLog ��Ա

		///<summary>
		/// ����һ����¼
		///</summary>
		public int Insert(MessageLog entity)
		{
			int r = dal.Insert(entity);
			CacheUtility.Delete(_cacheKey);
			return r;
		}

		///<summary>
		/// ɾ��һ����¼
		///</summary>
		public void Delete(int id)
		{
			dal.Delete(id);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// ����һ����¼
		///</summary>
		public void Update(MessageLog entity)
		{
			dal.Update(entity);
			CacheUtility.Delete(_cacheKey);
		}

		///<summary>
		/// ��ȡһ����¼
		///</summary>
		public MessageLog GetByID(int id)
		{
			MessageLog entity = (MessageLog)CacheUtility.Get(id, _cacheKey);
			if (entity == null)
			{
				entity = dal.GetByID(id);
				CacheUtility.Add(entity, id, _cacheKey);
			}
			return entity;
		}

        public IList<MessageLog> GetByTelephone(string telephone)
        {
            IList<MessageLog> entity = (IList<MessageLog>)CacheUtility.Get(_cacheKey, new string[] { telephone, "GetByTelephone" });
            if (entity == null)
            {
                entity = dal.GetByTelephone(telephone);
                CacheUtility.Add(entity, _cacheKey, new string[] { telephone, "GetByTelephone" });
            }
            return entity;

        }
		///<summary>
		/// ������¼
		///</summary>
		public IList<MessageLog> Search(out int pageCount,int pageIndex,int pageSize,string where,string orderField,bool isDesc)
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
			return (IList<MessageLog>)objList[0];;
		}

		#endregion

	}
}
