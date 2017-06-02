using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using CHCMS.Utility;
using Easom.Core;
using Easom.Core.Support;

namespace Easom.Support.App_Start
{
    public class SysAdminBaseController : Controller
    {
        private UserInfo _currentUser = null;

        /// <summary>
        /// 当前登录用户
        /// </summary>
        protected virtual UserInfo CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    _currentUser = LoginManager.GetUser();
                }
                //if (_currentUser == null)
                //{
                //    Response.Redirect("~/login");
                //}
                return _currentUser;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string flashuploadkey = RequestUtility.GetFormString("flashuploadkey");

            if (!string.IsNullOrEmpty(flashuploadkey))
            {
                LoginManager.UpdateUserCookie(flashuploadkey);
            }

            if (!LoginManager.IsLogin())
            {
                filterContext.Result = RedirectToRoute("Login", new { Controller = "Login", Action = "Login" });
            }

            base.OnActionExecuting(filterContext);
        }

        protected virtual Dictionary<string, string> GetKVDic(string kv)
        {
            //kv = "key-value,key-value"
            Dictionary<string, string> dc = null;

            string[] argArray = StringUtility.Split(kv, ',');

            if (argArray != null && argArray.Length > 0)
            {
                foreach (var arg in argArray)
                {
                    string[] keyArr = StringUtility.Split(arg, '-');

                    if (keyArr != null)
                    {
                        if (dc == null)
                        {
                            dc = new Dictionary<string, string>();
                        }

                        if (keyArr.Length == 2)
                        {
                            dc.Add(keyArr[0].ToUpper(), keyArr[1]);
                        }

                        if (keyArr.Length == 3)
                        {
                            dc.Add(keyArr[0].ToUpper(), "-" + keyArr[2]);
                        }
                    }
                }
            }

            return dc;
        }

        protected virtual string GetKeyValue(Dictionary<string, string> dic, string key)
        {
            if (dic != null)
            {
                key = key.ToUpper();

                if (dic.ContainsKey(key))
                {
                    return dic[key].Replace("'", "");
                }
            }

            return null;
        }

        protected virtual string GetFormString(FormCollection form, string name)
        {
            return GetFormString(form, name, true);
        }

        protected virtual string GetFormString(FormCollection form, string name, bool trim)
        {
            string r = form[name];

            if (string.IsNullOrEmpty(r))
            {
                return string.Empty;
            }

            if (r.ToLower() == "undefined")
            {
                r = string.Empty;
            }

            if (trim)
            {
                return r.Trim();
            }
            else
            {
                return r;
            }
        }

        protected virtual int GetFormInt(FormCollection form, string name, int defaultValue)
        {
            string r = GetFormString(form, name);
            return ConvertUtility.ToInt32(r, defaultValue);
        }

        protected virtual DateTime GetFormDate(FormCollection form, string name)
        {
            string r = GetFormString(form, name);
            return ConvertUtility.ToDateTime(r);
        }

        protected virtual decimal GetFormDecimal(FormCollection form, string name, decimal defaultValue)
        {
            string r = GetFormString(form, name);
            return ConvertUtility.ToDecimal(r, defaultValue);
        }

        protected virtual bool GetFormBool(FormCollection form, string name)
        {
            string r = form[name];

            if (r != null && r.ToLower() == "true")
            {
                return true;
            }

            if (r != null && r.ToLower() == "1")
            {
                return true;
            }
            return false;
        }

        protected virtual string GetSearchDataString(Dictionary<string, string> searchData)
        {
            string dataString = "[";
            if (searchData != null && searchData.Count > 0)
            {
                foreach (var item in searchData)
                {
                    dataString += "{";
                    dataString += string.Format("name:'{0}',value:'{1}'", item.Key, item.Value);
                    dataString += "},";
                }

                dataString = dataString.Trim(',');
            }

            return dataString += "]";
        }
        protected virtual string GetNewSearchDataString(Dictionary<string, string> searchData)
        {
            string dataString = "[";
            if (searchData != null && searchData.Count > 0)
            {
                foreach (var item in searchData)
                {
                    dataString += "{";
                    dataString += string.Format("id:'{0}',value:'{1}'", item.Key, item.Value);
                    dataString += "},";
                }

                dataString = dataString.Trim(',');
            }

            return dataString += "]";
        }
        /// <summary>
        /// 未读的回访人数
        /// </summary>
        /// <returns></returns>
        public static int UnreadCallonData(int isVaild)
        {
            int pageCount = 0;
            if (LoginManager.GetUserId() > 0)
            {
                IList<Section> sectionList = LoginManager.GetUser().UserSectionList;
                string section = "-100";
                if (sectionList != null)
                {
                    foreach (Section sItem in sectionList)
                    {
                        section += "," + sItem.ID + "";
                    }
                }
                IList<CallOnData> list = CallOnData.Actor.GetByUserIDAndState(LoginManager.GetUserId(), 0, isVaild, section);
                string where = string.Empty;
                string gGwhere="IsDelete=0 AND OrderState=" + (int)OrderStateEnum.OrdersData + " AND ArriveStateType="+(int)ArriveStateEnum.Wait;
                where += " AND ID in (";
                int num = 0;
                foreach (CallOnData cal in list)
                {
                    if (num > 20)
                    {
                        break;
                    }
                    else
                    {
                        where += "" + cal.OrdersID + ",";
                    }
                    num++;
                }
                where += "-100)";
                Orders.Actor.SearchView(out pageCount, 1,10000000, gGwhere + where, " UpdateTime ", true);
            }
            return pageCount;
        }


        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public static bool CreateOperationLogs(OperationLog logs)
        {
            logs.CreateTime = DateTime.Now;
            logs.UserID = LoginManager.GetUserId();
            if (OperationLog.Actor.Insert(logs) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
