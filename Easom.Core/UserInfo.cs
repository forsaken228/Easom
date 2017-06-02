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
using System.Collections.Generic;
using System.Linq;
namespace Easom.Core
{

	///<summary>
	/// UserInfo  客户端实体操作类
	///</summary>
	[Serializable]
	public class UserInfo
	{
		private static IUserInfo _actor = null;
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
		/// State
		///</summary>
        public UserStateEnum State
		{
			get;
			set;
		}

		///<summary>
		/// LoginCount
		///</summary>
		public int LoginCount
		{
			get;
			set;
		}

		///<summary>
		/// TrueName
		///</summary>
		public string TrueName
		{
			get;
			set;
		}

		///<summary>
		/// PassWord
		///</summary>
		public string PassWord
		{
			get;
			set;
		}

		///<summary>
		/// LastLoginIP
		///</summary>
		public string LastLoginIP
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
		/// LastLoginTime
		///</summary>
		public DateTime LastLoginTime
		{
			get;
			set;
		}

		///<summary>
		/// CreateTime
		///</summary>
		public DateTime CreateTime
		{
			get;
			set;
		}

		///<summary>
		/// Telephone
		///</summary>
		public string Telephone
		{
			get;
			set;
		}

		///<summary>
		/// Email
		///</summary>
		public string Email
		{
			get;
			set;
		}

		///<summary>
		/// SafeCode
		///</summary>
		public string SafeCode
		{
			get;
			set;
		}

		///<summary>
		/// IsOnLine
		///</summary>
		public bool IsOnLine
		{
			get;
			set;
		}

		///<summary>
		/// PictureURL
		///</summary>
		public string PictureURL
		{
			get;
			set;
		}

		///<summary>
		/// RoleID
		///</summary>
		public int RoleID
		{
			get;
			set;
		}

        ///<summary>
        /// RoleID
        ///</summary>
        public Role UserRole   
        {
            get
            {
                if (RoleID>0)
                {
                    return Role.Actor.GetByID(this.RoleID);
                }
                return null;
            }
        }

		#endregion 属性

		///<summary>
		/// Actor
		///</summary>
		public static IUserInfo Actor
		{
			get
			{
				if (_actor == null)
				{
					lock (_lockActor)
					{
						if (_actor == null)
						{
							 _actor = new UserInfoService();
						}
					}
				}

				return _actor;
			}
		}
     

        /// <summary>
        /// Hospital
        /// </summary>
        public Hospital Hospital
        {
            get
            {
                if (ID > 0)
                {
                    Hospital entity = Hospital.Actor.GetByID(this.HospitalID);
                    if (entity != null)
                    {
                        return entity;
                    }
                }
                return null;
            }
        }


        public string SectionStr
        {
            get
            {
                if (UserSectionList!=null && UserSectionList.Count>0)
                {
                    string returnData = string.Empty;
                    foreach (var item in UserSectionList)
                        {
                            returnData += item.Name+"|";
                        }
                    returnData =returnData.Trim('|');

                    return returnData;
                }
                return string.Empty;
            }

        }
        
      

        public IList<Section> UserSectionList
        {
            get
            {
                if (ID > 0 && HospitalID>0)
                {
                    ////获取当前用户所有的科室IList<Section> allSection = Section.Actor.GetByUserID(CurrentUser.ID, -1, CurrentUser.HospitalID);
                    IList<Section> entitySection = Section.Actor.GetByUserID(ID, 1,HospitalID);
                    if (entitySection != null)
                    {
                        return entitySection;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// HospitalID
        /// </summary>
        public int HospitalID
        {
            get
            {
                if (ID > 0)
                {
                    int entity = UserInfo.Actor.GetIsDefaluteByID(this.ID);
                    if (entity > 0)
                    {
                        return entity;
                    }
                }

                return 0;
            }
        }


        /// <summary>
        /// RoleType
        /// </summary>
        public string RoleTypeName 
        {
            get
            {
               
                if (ID > 0)
                {
                    Role entity = Role.Actor.GetByID(this.RoleID);
                    if (entity != null)
                    {
                        return entity.Name;
                    }
                }
                return string.Empty;
            }
        }

        ///// <summary>
        ///// RoleType
        ///// </summary>
        //public RoleType RoleType
        //{
        //    get
        //    {
        //        RoleType cn = new RoleType();
        //        if (ID > 0)
        //        {
        //            Role entity = Role.Actor.GetByID(this.RoleID);
        //            if (entity != null)
        //            {
        //                  cn = (RoleType)entity.ID;
   
        //            }
        //        }
        //        return cn;
        //    }
           
        //}


        public static IList<Hospital> GetHospitalByUserID(int id, int isDefault)
        {
            IList<Hospital> entityList = Hospital.Actor.GetByUserID(id, isDefault);
            return entityList;
        }

        public static bool IsChecked(IList<Hospital> entityList, int id)
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

        public static bool IsCheckedRole(IList<int> entityList, int id)
        {
            bool returnData = false;
            if (entityList != null && entityList.Count > 0)
            {
                var entity = from role in entityList where role == id select role;
                if (entity != null && entity.Count() > 0)
                {
                    returnData = true;
                }
            }
            return returnData;
        }

        public static bool CheckIsHospital(IList<Hospital> currentHospital, Hospital entity)
        {
            bool returnData = false;

            if (currentHospital == null || currentHospital.Count <= 0)
            {
                return returnData;
            }
            foreach (var item in currentHospital)
            {
                Hospital selectEntity = item as Hospital;
                if (entity.ID == selectEntity.ID)
                {
                    return returnData = true;
                }
            }
            return returnData;
        }

        public static int UpdateUserTOHospital(int userID, int hospitalID)
        {
            bool returnData = UserInfo.Actor.UpdateCurrentHospital(userID, hospitalID);
            int id = 0;
            if (returnData)
            {
                id = UserInfo.Actor.GetIsDefaluteByID(userID);
                if (id > 0)
                {
                    return id;
                }
            }
            return id;
        }

        public static string GetHospitalName(IList<Hospital> entity)
        {
            string returnData = string.Empty;
            if (entity != null && entity.Count > 0)
            {
                foreach (var item in entity)
                {
                    returnData += item.Name + "｜";
                }
            }

            returnData = returnData.Trim('｜');

            return returnData;
        }

        public static string GetHospitalName(UserInfo user)
        {
            string returnData = string.Empty;
            if (user != null)
            {
                IList<Hospital> entity = Hospital.Actor.GetByUserID(user.ID, -1);
                if (entity != null && entity.Count > 0)
                {
                    foreach (var item in entity)
                    {
                        returnData += item.Name + "｜";
                    }
                }
            }

            returnData = returnData.Trim('｜');

            return returnData;
        }

        public static string GetRoleNameByUserID(int id)
        {
            string returnData = string.Empty;
            IList<string> entity = UserInfo.Actor.GetByNameUserID(id);
            if (entity != null && entity.Count > 0)
            {
                foreach (var item in entity)
                {
                    returnData += item + " ";
                }
                returnData = returnData.Length > 15 ? StringUtility.CutString(returnData, 15, true) : returnData;
            }
            return returnData;
        }



	}
}
