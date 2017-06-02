// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-18
// 作者：loskiv@gmail.com
      
using System;
using CHCMS.Utility;
using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;

namespace Easom.Core
{

	///<summary>
	/// JJ_Swt_Report  客户端实体操作类
	///</summary>
	[Serializable]
	public class JJ_Swt_Report
	{
		private static IJJ_Swt_Report _actor = null;
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
		/// ChatState
		///</summary>
		public int ChatState
		{
			get;
			set;
		}

		///<summary>
		/// Title
		///</summary>
		public string Title
		{
			get;
			set;
		}
        

        ///<summary>
        /// BeginTime
		///</summary>
        public DateTime BeginTime
		{
			get;
			set;
		}

		///<summary>
		/// Equipment
		///</summary>
        public string Equipment
		{
			get;
			set;
		}

		///<summary>
		/// Disease
		///</summary>
		public string Disease
		{
			get;
			set;
		}

		///<summary>
		/// BeginUrl
		///</summary>
		public string BeginUrl
		{
			get;
			set;
		}

		///<summary>
		/// ChatUrl
		///</summary>
		public string ChatUrl
		{
			get;
			set;
		}

		///<summary>
		/// FromUrl
		///</summary>
		public string FromUrl
		{
			get;
			set;
		}

		///<summary>
		/// KeyWords
		///</summary>
		public string KeyWords
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
		/// SectionID
		///</summary>
		public int SectionID
		{
			get;
			set;
		}

		///<summary>
		/// Reamrk1
		///</summary>
		public string Reamrk1
		{
			get;
			set;
		}

		///<summary>
		/// Reamrk2
		///</summary>
		public string Reamrk2
		{
			get;
			set;
		}

		///<summary>
		/// Reamrk3
		///</summary>
		public string Reamrk3
		{
			get;
			set;
		}


		#endregion 属性

		///<summary>
		/// Actor
		///</summary>
		public static IJJ_Swt_Report Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new JJ_Swt_ReportService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
