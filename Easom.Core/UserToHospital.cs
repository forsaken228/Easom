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
	/// UserToHospital  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class UserToHospital
	{
		private static IUserToHospital _actor = null;
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
		/// HospitalID
		///</summary>
		public int HospitalID
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
		public static IUserToHospital Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new UserToHospitalService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
