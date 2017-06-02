// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
namespace Easom.Core
{

	///<summary>
	/// RoleToAuthority  客户端实体操作类
	///</summary>
	[Serializable]
	public class RoleToAuthority
	{
		private static IRoleToAuthority _actor = null;
		private static readonly object _lockActor = new object();

		#region 属性

		///<summary>
		/// RoleID
		///</summary>
		public int RoleID
		{
			get;
			set;
		}

		///<summary>
		/// AuthorityID
		///</summary>
		public int AuthorityID
		{
			get;
			set;
		}


		#endregion 属性

		///<summary>
		/// Actor
		///</summary>
		public static IRoleToAuthority Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new RoleToAuthorityService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
