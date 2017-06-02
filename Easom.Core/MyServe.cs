// MiniCoder ver 0.1 powered by loskiv (c) CopyRight
// �ô����ɴ����������Զ����� 
// �������ڣ�2014-04-17
// ���ߣ�loskiv@gmail.com

using System;

using Easom.Core.Services;
using Easom.Core.Contracts;
using System.Runtime.Serialization;
using CHCMS.Utility;
namespace Easom.Core
{

    ///<summary>
    /// MyServe  �ͻ���ʵ�������
    ///</summary>
    [Serializable]
    public class MyServe
    {
        private static IMyServe _actor = null;
        private static readonly object _lockActor = new object();

        #region ����

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
        /// ��ע
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public decimal Price { get; set; }


        public string ServeKey { get; set; }
        #endregion ����

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
