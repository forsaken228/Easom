// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
using Easom.Core.Support;
namespace Easom.Core
{

	///<summary>
	/// MessageTemplate  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class MessageTemplate
	{
		private static IMessageTemplate _actor = null;
		private static readonly object _lockActor = new object();

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
		/// CreateUserID
		///</summary>
		public int CreateUserID
		{
			get;
			set;
		}

		///<summary>
		/// Content
		///</summary>
		public string Content
		{
			get;
			set;
		}

		///<summary>
		/// State
		///</summary>
        public TemplateTypeEnum State
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


		#endregion ����

		///<summary>
		/// Actor
		///</summary>
		public static IMessageTemplate Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new MessageTemplateService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
