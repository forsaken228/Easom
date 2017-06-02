// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com

using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
namespace Easom.Core
{

    ///<summary>
    /// MyServe  客户端实体操作类
    ///</summary>
    [Serializable]
    public class MyServe
    {
        private static IMyServe _actor = null;
        private static readonly object _lockActor = new object();

        #region 属性

        ///<summary>
        /// ID
        ///</summary>
        public int ID { get; set; }
        ///<summary>
        /// ServeName
        ///</summary>
        public string ServeName { get; set; }
        ///<summary>
        /// RepertoryNum
        ///</summary>
        public int RepertoryNum { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }


        public string ServeKey { get; set; }
        #endregion 属性

        ///<summary>
        /// Actor
        ///</summary>
        public static IMyServe Actor
        {
            get
            {
                if (_actor == null)
                {
                    lock (_lockActor)
                    {
                        if (_actor == null)
                        {
                            _actor = new MyServeService();
                        }
                    }
                }

                return _actor;
            }
        }
    }
}
