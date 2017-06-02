// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
namespace Easom.Core
{

	///<summary>
	/// RoleToAuthority  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class RoleToAuthority
	{
		private static IRoleToAuthority _actor = null;
		private static readonly object _lockActor = new object();

		#region ����

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


		#endregion ����

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
