// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-11-18
// 作者：loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
namespace Easom.Core
{

	///<summary>
	/// JJ_Plan  客户端实体操作类
	///</summary>
	[Serializable]
	public class JJ_Plan
	{
		private static IJJ_Plan _actor = null;
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
		/// Name
		///</summary>
		public string Name
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


		#endregion 属性

		///<summary>
		/// Actor
		///</summary>
		public static IJJ_Plan Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new JJ_PlanService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
