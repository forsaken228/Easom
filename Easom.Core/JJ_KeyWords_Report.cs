// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-11-18
// ���ߣ�loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;

namespace Easom.Core
{

	///<summary>
	/// JJ_KeyWords_Report  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class JJ_KeyWords_Report
	{
		private static IJJ_KeyWords_Report _actor = null;
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
		/// DataTime
		///</summary>
		public DateTime DataTime
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
		/// JJ_CellID
		///</summary>
		public string JJ_Cell
		{
			get;
			set;
		}

		///<summary>
		/// JJ_KeyWordsName
		///</summary>
		public string JJ_KeyWordsName
		{
			get;
			set;
		}

		///<summary>
		/// ShowNum
		///</summary>
		public int ShowNum
		{
			get;
			set;
		}

		///<summary>
		/// ClickNum
		///</summary>
		public int ClickNum
		{
			get;
			set;
		}

		///<summary>
		/// PayNum
		///</summary>
		public decimal PayNum
		{
			get;
			set;
		}

		///<summary>
		/// Remark1
		///</summary>
		public string Remark1
		{
			get;
			set;
		}

		///<summary>
		/// Remark2
		///</summary>
		public string Remark2
		{
			get;
			set;
		}

		///<summary>
		/// Remark3
		///</summary>
		public string Remark3
		{
			get;
			set;
		}

		///<summary>
		/// TimeState
		///</summary>
		public int TimeState
		{
			get;
			set;
		}


		#endregion ����

		///<summary>
		/// Actor
		///</summary>
		public static IJJ_KeyWords_Report Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new JJ_KeyWords_ReportService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
