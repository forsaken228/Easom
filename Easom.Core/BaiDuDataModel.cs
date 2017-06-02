// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
using System.Collections.Generic;
using System.Linq;
namespace Easom.Core
{

	///<summary>
	/// Doctor  客户端实体操作类
	///</summary>
	[Serializable]
	public class BaiDuDataModel
	{
		//private static IDoctor _actor = null;
		private static readonly object _lockActor = new object();

		#region 属性

		

		///<summary>
        /// TimeState  时间端
		///</summary>
		public string TimeState 
		{
			get;
			set;
		}

		///<summary>
        /// ShowNum   展现量
		///</summary>
		public string ShowNum 
		{
			get;
			set;
		}

		///<summary>
        /// ClickNum  点击量
		///</summary>
		public string ClickNum 
		{
			get;
			set;
		}


        ///<summary>
        /// PayNum  消费
        ///</summary>
        public string PayNum
        { 
            get;
            set;
        }
      


     
		#endregion 属性

	


	}
}
