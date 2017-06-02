// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
using Easom.Core.Support;
namespace Easom.Core
{

	///<summary>
	/// MessageTemplate  客户端实体操作类
	///</summary>
	[Serializable]
	public class MessageTemplate
	{
		private static IMessageTemplate _actor = null;
		private static readonly object _lockActor = new object();

		#region 属性

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


		#endregion 属性

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
