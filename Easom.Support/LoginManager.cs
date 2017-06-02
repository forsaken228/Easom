using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using CHCMS.Utility;
using Easom.Core;
using Easom.Core.Support;
namespace Easom.Support
{
    public class LoginManager
    {
        public static string UserCookieName
        {
            get { return "easom.user"; }
        }

        /// <summary>
        /// 取出当前登录用户Id
        /// </summary>
        /// <returns></returns>
        public static int GetUserId()
        {
            HttpCookie cookie = GetUserCookie();

            if (cookie != null)
            {
                string entryptUserID = cookie["userId"];

                if (!string.IsNullOrEmpty(entryptUserID))
                {
                    entryptUserID = entryptUserID.Replace(" ", "+");
                    string detryptUserID = SecurityUtility.DESDecrypt(entryptUserID);
                    return ConvertUtility.ToInt32(detryptUserID, -1);
                }
            }

            return -1;
        }

        /// <summary>
        /// 取出当前登录用户Cookie信息
        /// </summary>
        /// <returns></returns>
        public static HttpCookie GetUserCookie()
        {
            return RequestUtility.GetCookie(UserCookieName);
        }

        /// <summary>
        /// 是否登录
        /// </summary>
        public static bool IsLogin()
        {
            if (GetUserId() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 取出当前登录用户信息
        /// </summary>
        public static UserInfo GetUser()
        {
            int userId = GetUserId();
            if (userId > 0)
            {
                return UserInfo.Actor.GetByID(userId);
            }

            return null;
        }

        /// <summary>
        /// 登录
        /// </summary>
        public static LoginState Login(string userName, string password)
        {
            LoginState state = LoginState.Failed;
            int userId = -1;

            if (!string.IsNullOrEmpty(password))
            {
                password = SecurityUtility.DESEncrypt(password);
                state = Validate(userName, password, ref userId);

                if (state == LoginState.Success)
                {
                    HttpCookie cookie = new HttpCookie(UserCookieName);
                    cookie["userId"] = SecurityUtility.DESEncrypt(userId.ToString());
                    RequestUtility.SetResponseCookie(cookie);
                }
            }

            return state;
        }

        /// <summary>
        /// 登出
        /// </summary>
        public static void Logout()
        {
            if (IsLogin())
            {
                HttpCookie cookie = new HttpCookie(UserCookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                RequestUtility.SetResponseCookie(cookie);
            }
        }

        /// <summary>
        /// 验证用户名与密码是否合法，不合法返回的userId为-1
        /// </summary>
        private static LoginState Validate(string userName, string password, ref int userId)
        {
            UserInfo entity = UserInfo.Actor.GetByName(userName);
            userId = -1;

            if (entity != null)
            {
                if (entity.State == UserStateEnum.Disable)
                {
                    return LoginState.Failed;
                }

                if (entity.PassWord == password)
                {
                    userId = entity.ID;
                    entity.LastLoginTime = DateTime.Now;
                    entity.LastLoginIP = RequestUtility.GetClientIP();
                    entity.LoginCount++;
                    UserInfo.Actor.Update(entity);

                    return LoginState.Success;
                }
                else
                {
                    return LoginState.PasswordError;
                }
            }
            else
            {
                return LoginState.UserNameError;
            }
        }

        /// <summary>
        /// 更新用户Cookie
        /// </summary>
        /// <param name="encryptUserID"></param>
        public static void UpdateUserCookie(string cookieValue)
        {
            HttpCookie cookie = GetUserCookie();

            if (cookie == null)
            {
                cookie = new HttpCookie(UserCookieName);
            }

            cookie.Value = cookieValue;
            RequestUtility.SetResponseCookie(cookie);
        }

        /// <summary>
        /// GetCurrentHospital
        /// </summary>
        public static Hospital GetCurrentHospital()
        {

            UserInfo info = GetUser();
            if (info != null)
            {
                if (info.Hospital != null)
                {
                    return info.Hospital;
                }
            }
            return null;
        }


        /// <summary>
        /// HasAutority 判断当前登录用户是否有当前医院的权限
        /// </summary>
        public static bool HasAutority(string authorityKey)
        {
            UserInfo currentUser = LoginManager.GetUser();

            if (currentUser != null)
            {
                //判断角色是否为超级管理员
                if (currentUser.Name.ToUpper() == "ADMIN")
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(authorityKey))
                {
                    authorityKey = authorityKey.Trim().ToUpper();
                    Authority targetAuthority = Authority.Actor.GetByAuthorityKey(authorityKey);

                    if (targetAuthority != null)
                    {
                        int authorityID = targetAuthority.ID;
                        int roles = currentUser.RoleID;
                        IList<Authority> authorities = Authority.Actor.GetByRoleID(roles);

                        if (authorities != null && authorities.Count > 0)
                        {
                            foreach (var authority in authorities)
                            {
                                if (!string.IsNullOrEmpty(authority.AuthorityKey))
                                {
                                    if (authority.AuthorityKey.ToUpper().Trim() == authorityKey)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 出错了
        /// </summary>
        public static void GoErrorPage()
        {
            SiteUtility.CurResponse.Redirect("/error/index");
            SiteUtility.CurResponse.End();
        }

        /// <summary>
        /// 未授权
        /// </summary>
        public static void GoUnAccessPage()
        {
            SiteUtility.CurResponse.Redirect("/error/unaccess");
            SiteUtility.CurResponse.End();
        }

        /// <summary>
        /// 未授权
        /// </summary>
        public static void GoUnAccessPagePop()
        {
            SiteUtility.CurResponse.Redirect("/error/UnAccessPop");
            SiteUtility.CurResponse.End();
        }

        /// <summary>
        /// 拼接疾病名称字符串
        /// </summary>
        public static int SubStringDisName(int i, string Name)
        {
            int lastLength = 0;
            return lastLength = (i - Name.Length) * 12;
        }

        /// <summary>
        /// 未读的跨站预约人数
        /// </summary>
        /// <returns></returns>
        public static int UnreadData()
        {
            int pageCount = 0;
            if (GetCurrentHospital() != null)
            {
                OutOrder.Actor.Search(out pageCount, 1, 1000000, " HospitalID=" + GetCurrentHospital().ID + " AND State=0", "ID", true);
            }
            return pageCount;
        }

        /// <summary>
        /// 医院服务
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetHospitalServe(string key)
        {
            HospitalToServe hospitalToServe=GetHospitalServer(key);
            if (hospitalToServe != null && hospitalToServe.Num > 0)
            {
                if (hospitalToServe.DateType == ServeDateTypeEnum.have)
                {
                    if (DateTime.Now < hospitalToServe.ExpirationDate)
                    {
                        return true;
                    }
                }
                if (hospitalToServe.DateType == ServeDateTypeEnum.None)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 医院服务
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetHospitalMessageYuE()
        {
            HospitalToServe hospitalToServe = GetHospitalServer(ServeConst.KEY_NOTE);
            if (hospitalToServe != null && hospitalToServe.Num > 0)
            {
                if (hospitalToServe.DateType == ServeDateTypeEnum.have)
                {
                    if (DateTime.Now < hospitalToServe.ExpirationDate)
                    {
                        return hospitalToServe.Num;
                    }
                }
                if (hospitalToServe.DateType == ServeDateTypeEnum.None)
                {
                    return hospitalToServe.Num;
                }
            }
            return 0;
        }
        /// <summary>
        /// 医院服务
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static HospitalToServe GetHospitalServer(string key)
        {
            HospitalToServe hospitalToScerve = HospitalToServe.Actor.GetByHospitalID(GetUser().HospitalID, key);
            return hospitalToScerve;
        }
    }
}
