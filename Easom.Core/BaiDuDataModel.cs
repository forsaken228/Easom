// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com
      
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
	/// Doctor  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class BaiDuDataModel
	{
		//private static IDoctor _actor = null;
		private static readonly object _lockActor = new object();

		#region ����

		

		///<summary>
        /// TimeState  ʱ���
		///</summary>
		public string TimeState 
		{
			get;
			set;
		}

		///<summary>
        /// ShowNum   չ����
		///</summary>
		public string ShowNum 
		{
			get;
			set;
		}

		///<summary>
        /// ClickNum  �����
		///</summary>
		public string ClickNum 
		{
			get;
			set;
		}


        ///<summary>
        /// PayNum  ����
        ///</summary>
        public string PayNum
        { 
            get;
            set;
        }
      


     
		#endregion ����

	


	}
}
