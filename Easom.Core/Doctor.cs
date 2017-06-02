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
using System.Linq;
namespace Easom.Core
{

	///<summary>
	/// Doctor  �ͻ���ʵ�������
	///</summary>
	[Serializable]
	public class Doctor
	{
		private static IDoctor _actor = null;
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

		///<summary>
		/// Identifier
		///</summary>
		public string Identifier
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
		/// SectionID
		///</summary>
		public int SectionID
		{
			get;
			set;
		}

        ///<summary>
        /// IsDelete
        ///</summary>
        public int IsDelete
        {
            get;
            set;
        }


        /// <summary>
        /// ��������
        /// </summary>
        public string SectionName  
        {
            get
            {
                if (SectionID > 0)
                {
                    Section entity = Section.Actor.GetByID(this.SectionID);
                    if (entity != null)
                    {
                        return entity.Name;
                    }
                }
                return null;
            }
        }

		#endregion ����

		///<summary>
		/// Actor
		///</summary>
		public static IDoctor Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new DoctorService();
						}
					}
				}

				return _actor;
			}
		}


        public static bool GetDoctorAuthority(IList<Doctor> entityList, int id)  
        {
            bool returnData = false;
            if (entityList != null && entityList.Count > 0)
            {
                var entity = from hospital in entityList where hospital.ID == id select hospital;
                if (entity != null && entity.Count() > 0)
                {
                    returnData = true;
                }
            }
            return returnData;
        }


	}
}
