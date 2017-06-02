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
	/// UserToSection  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class UserToSection
	{
		private static IUserToSection _actor = null;
		private static readonly object _lockActor = new object();

		#region ����

		///<summary>
		/// UserID
		///</summary>
		public int UserID
		{
			get;
			set;
		}

		///<summary>
		/// SectionID
		///</summary>
		public int SectionID
		{
			get;
			set;
		}

		///<summary>
		/// IsDefault
		///</summary>
		public bool IsDefault
		{
			get;
			set;
		}


		#endregion ����

		///<summary>
		/// Actor
		///</summary>
		public static IUserToSection Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new UserToSectionService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
