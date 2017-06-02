// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
using System.Collections.Generic;
namespace Easom.Core
{

	///<summary>
	/// Authority  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class Authority
	{
		private static IAuthority _actor = null;
		private static readonly object _lockActor = new object();
        private IList<Authority> channelList = null;
		#region ����

		///<summary>
		/// ID
		///</summary>
		public int ID
		{
			get;
			set;
		}

		///<summary>
		/// ParentID
		///</summary>
		public int ParentID
		{
			get;
			set;
		}

		///<summary>
		/// Name
		///</summary>
		public string Name
		{
			get;
			set;
		}

		///<summary>
		/// AuthorityKey
		///</summary>
		public string AuthorityKey
		{
			get;
			set;
		}

		///<summary>
		/// SortNum
		///</summary>
		public int SortNum
		{
			get;
			set;
		}

		///<summary>
		/// CroupNumber
		///</summary>
		public int CroupNumber
		{
			get;
			set;
		}

		///<summary>
		/// Remark
		///</summary>
		public string Remark
		{
			get;
			set;
		}

        public IList<Authority> ChannelList
        {
            get
            {
                if (channelList == null)
                {
                    channelList = GetByParentID(string.Format("ParentID={0}", this.ID));
                }

                return channelList;
            }
        }

        public IList<Authority> ChildrenChannel
        {
            get
            {
                return ChannelList;
            }
        }


		#endregion ����

		///<summary>
		/// Actor
		///</summary>
		public static IAuthority Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new AuthorityService();
						}
					}
				}

				return _actor;
			}
		}


        public static IList<dynamic> ConvertToArray(out int pageCount, int pageIndex, int pageSize, string where, string orderFields, bool isDesc)
        {
            IList<dynamic> sourceData = new List<dynamic>();
            IList<Authority> entityList = Authority.Actor.Search(out pageCount, pageIndex, pageSize, where, orderFields, isDesc);
            if (entityList != null && entityList.Count > 0)
            {
                foreach (var item in entityList)
                {
                    sourceData.Add(new dynamic[] { item.ID, item.Name, item.AuthorityKey, item.CroupNumber, item.ID });
                }
            }
            return sourceData;
        }
        public static string ConvertGroupNumberToString(int id)
        {
            string returnData = string.Empty;
            switch (id)
            {
                case 100:

                    returnData = "����ԤԼ����";
                    break;
                case 200:
                    returnData = "�ÿ�����ͳ��";
                    break;
                case 300:
                    returnData = "ҽԺ��Ϣ����";
                    break;
                case 400:
                    returnData = "ȫ�ֹ�������";
                    break;
                case 500:
                    returnData = "��������";
                    break;
                default:
                    break;
            }
            return returnData;
        }
        public static IList<Authority> GetByParentID(string where)
        {
            int pageCount = 0;
            return Actor.Search(out pageCount, 1, -1, where, "ID", false);
        }

	}
}
