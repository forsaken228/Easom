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
namespace Easom.Core
{

	///<summary>
	/// MediaSource  客户端实体操作类
	///</summary>
	[Serializable]
	public class MediaSource
	{
		private static IMediaSource _actor = null;
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
		/// ParentID
		///</summary>
		public int ParentID
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
		/// HospitalID
		///</summary>
		public int HospitalID
		{
			get;
			set;
		}


		#endregion 属性

        public IList<MediaSource> ChildrenMediaSource
        {
            get
            {
                int pageCount = 0;
                return MediaSource.Actor.Search(out pageCount, 1, -1, string.Format("ParentID={0}", this.ID), "ID", false);
            }
        }


		///<summary>
		/// Actor
		///</summary>
		public static IMediaSource Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new MediaSourceService();
						}
					}
				}

				return _actor;
			}
		}


	}
}
