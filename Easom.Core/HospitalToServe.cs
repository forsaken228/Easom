// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// 该代表由代码生成器自动生成 
// 创建日期：2014-04-17
// 作者：loskiv@gmail.com

using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
using Easom.Core.Support;
namespace Easom.Core
{

    ///<summary>
    /// HospitalToServe  客户端实体操作类
    ///</summary>
    [Serializable]
    public class HospitalToServe
    {
        private static IHospitalToServe _actor = null;
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
        /// HospitalID
        ///</summary>
        public int HospitalID
        {
            get;
            set;
        }

        /// <summary>
        /// 医院
        /// </summary>
        public Hospital Hospital
        {
            get
            {
                Hospital hospital = Hospital.Actor.GetByID(HospitalID);
                return hospital;
            }
        }

        ///<summary>
        /// 服务Key
        ///</summary>
        public string ServeKey
        {
            get;
            set;
        }

        /// <summary>
        /// 服务详细信息
        /// </summary>
        public MyServe Sever
        {
            get
            {
                MyServe myserve = MyServe.Actor.GetByKey(ServeKey);
                return myserve;
            }
        }

        ///<summary>
        /// 服务数量
        ///</summary>
        public int Num
        {
            get;
            set;
        }

        ///<summary>
        /// 服务到期时间
        ///</summary>
        public DateTime ExpirationDate
        {
            get;
            set;
        }
        ///<summary>
        /// 服务时间类型 0表示有到期时间，1表示永久有效
        ///</summary>
        public ServeDateTypeEnum DateType
        {
            get;
            set;
        }
        ///<summary>
        /// 创建时间
        ///</summary>
        public DateTime CreateTime
        {
            get;
            set;
        }
        #endregion 属性

        ///<summary>
        /// Actor
        ///</summary>
        public static IHospitalToServe Actor
        {
            get
            {
                if (_actor == null)
                {
                    lock (_lockActor)
                    {
                        if (_actor == null)
                        {
                            _actor = new HospitalToServeService();
                        }
                    }
                }

                return _actor;
            }
        }


    }
}
