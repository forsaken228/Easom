using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easom.Message.Contracts;
using CHCMS.Utility;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using System.Xml.Linq;

namespace Easom.Message.Service
{
    public class NotifyService : INotify
    {
        /// <summary>
        /// 群发送短信
        /// </summary>
        /// <param name="CorpID">账号</param>
        /// <param name="Pwd">密码</param>
        /// <param name="Mobile">发送的手机号码(多个号码以逗号分隔,最多支持600个号码)</param>
        /// <param name="Content">内容</param>
        /// <param name="Cell">子号（可为空）</param>
        /// <param name="SendTime">定时发送时间（固定14位长度字符串，比如：20060912152435代表2006年9月12日15时24分35秒，为空表示立即发送）</param>
        /// <returns>0，成功。1，失败。</returns>
        public int HttpNotifyBatchSend(string mobile, string content, string cell, string sendTime)
        {
            string corpID = WebConfigUtility.GetAppSetting("corpID");
            string pwd = WebConfigUtility.GetAppSetting("passWord");
            string userID = WebConfigUtility.GetAppSetting("userID");
            string url = "http://dx.ipyy.net/sms.aspx?action=send&userid=" + userID + "&account=" + corpID + "&password=" + pwd + "&mobile=" + mobile + "&content=" + content + "&sendTime=" + sendTime + "&extno=";
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            //<? xml version = "1.0" encoding = "utf-8" ?>
            //< returnsms >
            //    < returnstatus > Faild </ returnstatus >
            //    < message > 需要签名 </ message >
            //    < remainpoint > 0 </ remainpoint >
            //    < taskID ></ taskID >
            //    < successCounts > 0 </ successCounts >
            //</ returnsms >
            XElement root = XElement.Load(url);
            XElement xele = root.Element("returnstatus");
            if (xele.Value == "Success")
            {
                return 0;
            }
            if (xele.Value == "Faild")
            {
                return 1;
            }
            return 1;
        }

        public int HttpOverage()
        {
            string corpID = WebConfigUtility.GetAppSetting("corpID");
            string pwd = WebConfigUtility.GetAppSetting("passWord");
            string userID = WebConfigUtility.GetAppSetting("userID");
            string url = "http://dx.ipyy.net/sms.aspx?action=overage&userid=" + userID + "&account=" + corpID + "&password=" + pwd + "";
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            //<? xml version = "1.0" encoding = "utf-8" ?>
            //< returnsms >
            //< returnstatus > status </ returnstatus > -------返回状态值：成功返回Sucess 失败返回：Faild
            //<message> message</ message > --------------返回信息提示：见下表
            //<payinfo> payinfo</ payinfo > --------------返回支付方式  后付费，预付费
            //<overage> overage</ overage > -------------返回余额
            //< sendTotal > sendTotal </ sendTotal > ----返回总点数  当支付方式为预付费是返回总充值点数
            //</ returnsms >

            XElement root = XElement.Load(url);
            XElement xele = root.Element("returnstatus");
            if (xele.Value == "Success")
            {
                XElement overage = root.Element("overage");
                return Convert.ToInt32(overage.Value);
            }
            //else if (xele.Value == "Faild")
            //{
            //    return 0;
            //}
            else
            {
                return 0;
            }
        }
    }
}