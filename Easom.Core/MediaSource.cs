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
namespace Easom.Core
{

	///<summary>
	/// MediaSource  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class MediaSource
	{
		private static IMediaSource _actor = null;
		private static readonly object _lockActor = new object();

		#region ����

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


		#endregion ����

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
