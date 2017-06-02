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
    /// CallOnData  �ͻ���ʵ�������
    ///</summary>
    [Serializable]
    public class CallOnData: ICloneable
    {
        private static ICallOnData _actor = null;
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
        /// OrdersID
        ///</summary>
        public int OrdersID
        {
            get;
            set;
        }

        ///<summary>
        /// IsValid
        ///</summary>
        public int IsValid
        {
            get;
            set;
        }

        ///<summary>
        /// CallOnDateTime
        ///</summary>
        public DateTime CallOnDateTime
        {
            get;
            set;
        }

        ///<summary>
        /// CallUserID
        ///</summary>
        public int CallUserID
        {
            get;
            set;
        }

        ///<summary>
        /// �ط�������
        ///</summary>
        public string CallUserName
        {
            get
            {
                UserInfo user = UserInfo.Actor.GetByID(CallUserID);
                if (user != null)
                {
                    return user.TrueName;
                }
                return null;
            }
        }

        ///<summary>
        /// IsDelete
        ///</summary>
        public int IsDelete
        {
            get;
            set;
        }

        ///<summary>
        /// Remark
        ///</summary>
        public string Remark
        {
            get;
            set;
        }

        ///<summary>
        /// IsCallOn
        ///</summary>
        public bool IsCallOn
        {
            get;
            set;
        }


        #endregion ����
        object ICloneable.Clone()
        {
            return this.Clone();
        }
        public CallOnData Clone()
        {
            return (CallOnData)this.MemberwiseClone();
        }
        ///<summary>
        /// Actor
        ///</summary>
        public static ICallOnData Actor
        {
            get
            {
                if (_actor == null)
                {
                    lock (_lockActor)
                    {
                        if (_actor == null)
                        {
                            _actor = new CallOnDataService();
                        }
                    }
                }

                return _actor;
            }
        }


    }
}
