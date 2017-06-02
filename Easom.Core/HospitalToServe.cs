// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com

using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
using Easom.Core.Support;
namespace Easom.Core
{

    ///<summary>
    /// HospitalToServe  �ͻ���ʵ�������
    ///</summary>
    [Serializable]
    public class HospitalToServe
    {
        private static IHospitalToServe _actor = null;
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
        /// HospitalID
        ///</summary>
        public int HospitalID
        {
            get;
            set;
        }

        /// <summary>
        /// ҽԺ
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
        /// ����Key
        ///</summary>
        public string ServeKey
        {
            get;
            set;
        }

        /// <summary>
        /// ������ϸ��Ϣ
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
        /// ��������
        ///</summary>
        public int Num
        {
            get;
            set;
        }

        ///<summary>
        /// ������ʱ��
        ///</summary>
        public DateTime ExpirationDate
        {
            get;
            set;
        }
        ///<summary>
        /// ����ʱ������ 0��ʾ�е���ʱ�䣬1��ʾ������Ч
        ///</summary>
        public ServeDateTypeEnum DateType
        {
            get;
            set;
        }
        ///<summary>
        /// ����ʱ��
        ///</summary>
        public DateTime CreateTime
        {
            get;
            set;
        }
        #endregion ����

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
