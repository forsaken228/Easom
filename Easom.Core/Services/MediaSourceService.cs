// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com

using System;

using System.Collections.Generic;
using Easom.Core.Contracts;
using Easom.Core.DataAdapters;
using CHCMS.Utility;
namespace Easom.Core.Services
{

    ///<summary>
    /// MediaSourceService  服务处理类
    ///</summary>
    public class MediaSourceService : IMediaSource
    {
        private static readonly string _cacheKey = "Easom.Core.MediaSourceService";
        private static readonly MediaSourceDataAdapter dal = new MediaSourceDataAdapter();

        #region IMediaSource 成员

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int Insert(MediaSource entity)
        {
            int r = dal.Insert(entity);
            CacheUtility.Delete(_cacheKey);
            return r;
        }

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int InsertAllMedia(int hoapitalID)
        {
            int r = dal.InsertAllMedia(hoapitalID);
            CacheUtility.Delete(_cacheKey);
            CacheUtility.Delete("Easom.Core.OrdersService");
            return r;
        }

        ///<summary>
        /// 插入一条记录
        ///</summary>
        public int InsertAllMediaNew(int hoapitalID)
        {
            int r = dal.InsertAllMediaNew(hoapitalID);
            CacheUtility.Delete(_cacheKey);
            return r;
        }

        ///<summary>
        /// 删除一条记录
        ///</summary>
        public void Delete(int id)
        {
            dal.Delete(id);
            CacheUtility.Delete(_cacheKey);
        }

        ///<summary>
        /// 更新一条记录
        ///</summary>
        public void Update(MediaSource entity)
        {
            dal.Update(entity);
            CacheUtility.Delete(_cacheKey);
        }
        public bool DeleteParentID(int id)
        {
            if (dal.DeleteParentID(id))
            {
                CacheUtility.Delete(_cacheKey);
                return true;
            }
            return false;
        }

        ///<summary>
        /// 读取一条记录
        ///</summary>
        public MediaSource GetByName(int hospitalID, string Name)
        {
            MediaSource entity = (MediaSource)CacheUtility.Get(_cacheKey, new string[] { Name, "GetByName", hospitalID.ToString() });
            if (entity == null)
            {
                entity = dal.GetByName(hospitalID, Name);
                CacheUtility.Add(entity, _cacheKey, new string[] { Name, "GetByName",hospitalID.ToString() });
            }
            return entity;
        }

        ///<summary>
        /// 读取一条记录
        ///</summary>
        public MediaSource GetByID(int id)
        {
            MediaSource entity = (MediaSource)CacheUtility.Get(id, _cacheKey);
            if (entity == null)
            {
                entity = dal.GetByID(id);
                CacheUtility.Add(entity, id, _cacheKey);
            }
            return entity;
        }

        /// <summary>
        /// 获取所有媒体
        /// </summary>
        /// <returns></returns>
        public IList<MediaSource> GetAllMediaSource(int hospitalID)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { hospitalID.ToString(), "GetAllMediaSource" });
            if (objList == null)
            {
                objList = new object[1];
                objList[0] = dal.GetAllMediaSource(hospitalID);
                CacheUtility.Add(objList, _cacheKey, new string[]{hospitalID.ToString(),"GetAllMediaSource"});
            }
            return (IList<MediaSource>)objList[0];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prentid"></param>
        /// <returns></returns>
        public IList<MediaSource> GetAllMediaSource(int hospitalID, int prentid)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { hospitalID.ToString(), prentid.ToString(), "GetAllMediaSource" });
            if (objList == null)
            {
                objList = new object[1];
                objList[0] = dal.GetAllMediaSource(hospitalID, prentid);
                CacheUtility.Add(objList, _cacheKey, new string[] { hospitalID.ToString(), prentid.ToString(), "GetAllMediaSource" });
            }
            return (IList<MediaSource>)objList[0];
        }

        ///<summary>
        /// 搜索记录
        ///</summary>
        public IList<MediaSource> Search(out int pageCount, int pageIndex, int pageSize, string where, string orderField, bool isDesc)
        {
            object[] objList = (object[])CacheUtility.Get(_cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderField.ToString(), isDesc.ToString() });
            if (objList == null)
            {
                objList = new object[2];
                objList[0] = dal.Search(out pageCount, pageIndex, pageSize, where, orderField, isDesc);
                objList[1] = pageCount;
                CacheUtility.Add(objList, _cacheKey, new string[] { pageIndex.ToString(), pageSize.ToString(), where, orderField.ToString(), isDesc.ToString() });
            }
            pageCount = Convert.ToInt32(objList[1]);
            return (IList<MediaSource>)objList[0]; ;
        }

        #endregion

    }
}
