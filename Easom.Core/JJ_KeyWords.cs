// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-21
// ���ߣ�loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;

namespace Easom.Core
{

	///<summary>
	/// JJ_KeyWords  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class JJ_KeyWords
	{
		private static IJJ_KeyWords _actor = null;
		private static readonly object _lockActor = new object();

		#region ����

		///<summary>
		/// ID
		///</summary>
		public long ID
		{
			get;
			set;
		}

		///<summary>
		/// JJ_AccountID
		///</summary>
		public int JJ_AccountID
		{
			get;
			set;
		}

		///<summary>
		/// JJ_Plan
		///</summary>
		public string JJ_Plan
		{
			get;
			set;
		}

		///<summary>
		/// JJ_Cell
		///</summary>
		public string JJ_Cell
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
		/// PCUrl
		///</summary>
		public string PCUrl
		{
			get;
			set;
		}

		///<summary>
		/// WAPUrl
		///</summary>
		public string WAPUrl
		{
			get;
			set;
		}


		#endregion ����

		///<summary>
		/// Actor
		///</summary>
		public static IJJ_KeyWords Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new JJ_KeyWordsService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
