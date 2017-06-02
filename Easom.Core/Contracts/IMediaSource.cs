// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com
      
using System.Collections.Generic;
using CHCMS.Utility;
namespace Easom.Core.Contracts
{

	///<summary>
	/// IMediaSource  契约接口类
	///</summary>
	public interface IMediaSource
	{
		int Insert(MediaSource entity);

        int InsertAllMedia(int hospital);

        int InsertAllMediaNew(int Hospital);

		void Delete(int id);

		void Update(MediaSource entity);

        bool DeleteParentID(int id);

        MediaSource GetByName(int hospitalID,string name);

		MediaSource GetByID(int id);

        IList<MediaSource> GetAllMediaSource(int hospitalID);

        IList<MediaSource> GetAllMediaSource(int hospitalID,int prentid);

		IList<MediaSource> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc);

	}
}
