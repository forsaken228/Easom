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
    /// Hospital  �ͻ���ʵ�������
    ///</summary>
    [Serializable]
    public class Hospital
    {
        private static IHospital _actor = null;
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
        /// AreaID
        ///</summary>
        public int AreaID
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
        /// Intro
        ///</summary>
        public string Intro
        {
            get;
            set;
        }

        ///<summary>
        /// OrderNumber
        ///</summary>
        public string OrderNumber
        {
            get;
            set;
        }

        public int IsDelete { get; set; }

        public string HSafeCode { get; set; }

        public string PassWord { get; set; }

        public IList<Section> GetSectionList()
        {
            IList<Section> entityList = Section.Actor.GetByHospitalID(this.ID);
            return entityList;
        }

        public string GetSectionNames
        {
            get
            {

                if (ID > 0)
                {
                    IList<Section> entity = GetSectionList();
                    string returnData = string.Empty;
                    if (entity != null && entity.Count > 0)
                    {
                        foreach (var item in entity)
                        {
                            returnData += item.Name + "��";
                        }
                    }

                    returnData = returnData.Trim('��');

                    return returnData;
                }
                return string.Empty;

            }
        }
        public string GetSelectSectionNames
        {
            get
            {

                if (ID > 0)
                {
                    IList<Section> entity = GetSectionList();
                    string returnData = string.Empty;
                    if (entity != null && entity.Count > 0)
                    {
                        foreach (var item in entity)
                        {
                           
                            returnData += item.Name + "��";
                        }
                    }
                    returnData = returnData.Trim('��');

                    return returnData;
                }
                return string.Empty;

            }
        }

        #endregion ����

            ///<summary>
            /// Actor
            ///</summary>
        public static IHospital Actor
        {
            get
            {
                if (_actor == null)
                {
                    lock (_lockActor)
                    {
                        if (_actor == null)
                        {
                            _actor = new HospitalService();
                        }
                    }
                }

                return _actor;
            }
        }





        public int IsOpenPassWord { get; set; }
    }
}
