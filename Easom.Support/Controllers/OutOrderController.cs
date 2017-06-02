using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easom.Support.ActionFilters;
using System.Web.Mvc;
using Easom.Support.App_Start;
using Easom.Support;
using CHCMS.Utility;
using Easom.Core;
using System.Security.Cryptography;

namespace Easom.Support.Controllers 
{
    public  class OutOrderController:Controller
    {
        /// <summary>
        /// 添加预约
        /// </summary>
        /// <returns></returns>
        public ActionResult OutOrderAdd()
        {
            HospitalWebsite currentEntity = null;
            string url = RequestUtility.GetQueryString("url");
            bool returnhData = Tool<object>.IsSubmitByOkWay(url, out currentEntity);
            string callbackName = RequestUtility.GetQueryString("callbackname");
            if (returnhData)
            {
                OutOrder entity = new OutOrder();
                entity.UserName = RequestUtility.GetQueryString("name");
                entity.Sex = RequestUtility.GetQueryString("sex") == "男" ? true : false;
                entity.Age = RequestUtility.GetQueryString("age");
                entity.Telephone = RequestUtility.GetQueryString("tel");
                entity.OrderTime =RequestUtility.GetQueryDateTime("ordertime");
                entity.CreateTime = System.DateTime.Now;
                entity.Description = RequestUtility.GetQueryString("description");
                entity.State = false;
                if (currentEntity != null)
                {
                    entity.OutSiteToHospiatlID = currentEntity.ID;
                    entity.HospitalID = currentEntity.HospitalID;
                    entity.SectionID = currentEntity.SectionID;
                }
                int returnData = OutOrder.Actor.Insert(entity);
                return new Tool<object>(new { success = (returnData > 0 ? true : false) }, callbackName);
            }
            return new Tool<object>(new { success = false }, callbackName);
        }

        public ActionResult OutOrderWxID()
        {
            HospitalWebsite currentEntity = null;
            string wxID = RequestUtility.GetQueryString("wxID");
            string sKey = WebConfigUtility.GetAppSetting("encrypt");
            string enCode = Decrypt(wxID, sKey);
            bool returnhData = Tool<object>.IsSubmitByOkWay(enCode, out currentEntity);
            string callbackName = RequestUtility.GetQueryString("callbackname");
            if (returnhData)
            {
                OutOrder entity = new OutOrder();
                entity.UserName = RequestUtility.GetQueryString("name");
                entity.Sex = RequestUtility.GetQueryString("sex") == "男" ? true : false;
                entity.Age = RequestUtility.GetQueryString("age");
                entity.Telephone = RequestUtility.GetQueryString("tel");
                entity.OrderTime = RequestUtility.GetQueryDateTime("ordertime");
                entity.CreateTime = System.DateTime.Now;
                entity.Description = RequestUtility.GetQueryString("description");
                entity.State = false;
                if (currentEntity != null)
                {
                    entity.OutSiteToHospiatlID = currentEntity.ID;
                    entity.HospitalID = currentEntity.HospitalID;
                    entity.SectionID = currentEntity.SectionID;
                }
                int returnData = OutOrder.Actor.Insert(entity);
                return new Tool<object>(new { success = (returnData > 0 ? true : false) }, callbackName);
            }
            return new Tool<object>(new { success = false }, callbackName);
        }

        public string Decrypt(string pToDecrypt, string sKey)
        {
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }


        [HttpPost]
        public JsonResult OutOrderAdd(FormCollection form)
        {
            HospitalWebsite currentEntity = null;
            string url = GetFormString(form,"url");
            bool returnhData = Tool<object>.IsSubmitByOkWay(url, out currentEntity);
            string callbackName = RequestUtility.GetQueryString("callbackname");
            if (returnhData)
            {
                OutOrder entity = new OutOrder();
                entity.UserName = GetFormString(form, "name");
                entity.Sex = GetFormString(form, "sex") == "男" ? true : false;
                entity.Age = GetFormString(form, "age");
                entity.Telephone = GetFormString(form, "tel");
                entity.OrderTime = GetFormDate(form, "ordertime");
                entity.CreateTime = System.DateTime.Now;
                entity.Description = GetFormString(form, "description");
                entity.State = false;
                if (currentEntity != null)
                {
                    entity.OutSiteToHospiatlID = currentEntity.ID;
                    entity.HospitalID = currentEntity.HospitalID;
                    entity.SectionID = currentEntity.SectionID;
                }
                int returnData = OutOrder.Actor.Insert(entity);
                return  Json(new {flag="success",mssage="挂号成功。"});
            }
            return Json(new { flag = "error", mssage = "未授权的网站。" });
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
    }
}
