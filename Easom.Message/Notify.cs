using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easom.Message.Contracts;
using Easom.Message.Service;
using CHCMS.Utility;
using System.Threading;
using Easom.Core;
using Easom.Core.Support;

namespace Easom.Message
{
    public class Notify
    {
        private static INotify _actor = null;
        private static readonly object _lockActor = new object();
        private static Timer timer;
        private static TimerCallback _timerCallback = null;

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


        #endregion 属性

        /// <summary>
        /// Notify
        /// </summary>
        public static INotify Actor
        {
            get
            {
                if (_actor == null)
                {
                    lock (_lockActor)
                    {
                        if (_actor == null)
                        {
                            _actor = new NotifyService();
                        }
                    }
                }

                return _actor;
            }
        }

        /// <summary>
        /// 是否可以发送
        /// </summary>
        /// <param name="userID">用户名</param>
        /// <param name="date">时间(如果时间为空立即发送)</param>
        /// <param name="notifyCycleEnum">发送类型</param>
        /// <returns></returns>
        public bool IsNeedSend(int userID, DateTime date, NotifyCycleEnum notifyCycleEnum)
        {
            if (userID > 0)
            {
                if (notifyCycleEnum == Easom.Core.Support.NotifyCycleEnum.Day)
                {
                    NotifyLog entity = NotifyLog.Actor.TopGetByUserID(userID);

                    if (entity != null)
                    {
                        //如果日志最后一天等于发送时间，说明已经发送过了。
                        if (entity.SendDate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 推动任务总方法
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="sendtype"></param>
        public void NotifyRun(int userID, NotifyTypeEnum sendtype)
        {
            NotifyCycleEnum notifyCycleEnum = new NotifyCycleEnum();
            NotifyLog notify = new NotifyLog();
            UserInfo info = UserInfo.Actor.GetByID(userID);//ID
            bool result = IsNeedSend(userID, DateTime.Now, notifyCycleEnum);//是否可以发送
            if (result == true)
            {
                string note = CreateMsg(userID);
                string note1 = string.Empty;//第一部分
                string note2 = string.Empty;//加上份数的最终结果
                int subIndex = 270;//截取字符数
                int entity = -999;//返回状态
                int entity3 = -999;
                if (sendtype == Easom.Core.Support.NotifyTypeEnum.Mobile)
                {

                    if (note.Length > subIndex)
                    {
                        int sum = note.Length / subIndex;
                        if (note.Length % subIndex > 0)
                        {
                            sum++;
                        }
                        for (int i = 0; i < sum; i++)
                        {
                            if (i == sum - 1)
                            {
                                note2 = string.Empty;
                                note1 = note.Substring(i * subIndex, note.Length % subIndex);
                                note2 += "(" + (i + 1) + "/" + sum + ")" + note1;
                            }
                            else
                            {
                                note2 = string.Empty;
                                note1 = note.Substring(i * subIndex, subIndex);
                                note2 += "(" + (i + 1) + "/" + sum + ")" + note1;
                            }
                            MyServe myserve = MyServe.Actor.GetByKey("KEY_NOTE");
                            if (myserve.RepertoryNum > 0)
                            {
                                entity = Notify.Actor.HttpNotifyBatchSend(info.Telephone, note2.ToString(), null, null);
                                if (entity == 0)
                                {
                                    myserve.RepertoryNum = (myserve.RepertoryNum - 1);
                                    MyServe.Actor.Update(myserve);
                                    CreateNotifyLog(userID, (int)Easom.Core.Support.SendState.MobileSuccess);//创建手机成功日志
                                }
                                else
                                {
                                    CreateNotifyLog(userID, (int)Easom.Core.Support.SendState.MobileFail);//创建手机失败日志
                                }
                            }
                        }
                    }
                    else//不超过270个字一次就能发送
                    {
                        entity3 = Notify.Actor.HttpNotifyBatchSend(info.Telephone, note.ToString(), null, null);
                        Console.Write(note + "\n");
                        Console.ReadLine();
                        if (entity3 == 0)//0表示成功
                        {
                            CreateNotifyLog(userID, (int)Easom.Core.Support.SendState.MobileSuccess);//创建手机成功日志
                        }
                        else
                        {
                            CreateNotifyLog(userID, (int)Easom.Core.Support.SendState.MobileFail);//创建手机失败日志
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 短信/邮件生成
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public string CreateMsg(int userID)
        {
            int adddate = 0;
            int orderdata = 0;

            int dayWebArrivedata = 0;
            int dayTelArrivedata = 0;
            int dayAntArrivedata = 0;
            int mothWebArrivedata = 0;
            int mothTelArrivedata = 0;
            int mothAntArrivedata = 0;

            IList<Hospital> allhospital = UserInfo.GetHospitalByUserID(userID, -1);
            StringBuilder note = new StringBuilder();
            if (allhospital != null)
            {
                note.Append(string.Format("【{0}】", DateTime.Now.ToString("yyyy-MM-dd")));
                foreach (Hospital item in allhospital)
                {
                    IList<Section> section = Section.Actor.GetByUserID(userID, 0, item.ID);
                    string sectionStr = "";
                    foreach (Section i in section)
                    {
                        sectionStr += "" + i.ID + ",";
                    }
                    sectionStr += "-1";
                    DateTime begintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000"));
                    DateTime endtimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:56.999"));
                    DateTime monthBegintimes = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-01 00:00:00.000"));
                    DateTime monthEndtimes = Convert.ToDateTime(DateTime.Now.AddDays(-DateTime.Now.Day).AddMonths(1).ToString("yyyy-MM-dd 23:59:56.999"));
                    /****************网络********************/
                    Orders.Actor.GetIndexData(out adddate, out orderdata, out dayWebArrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Web);
                    Orders.Actor.GetIndexData(out adddate, out orderdata, out mothWebArrivedata, monthBegintimes, monthEndtimes, sectionStr, (int)CountTypeEnum.Web);
                    /****************电话********************/
                    Orders.Actor.GetIndexData(out adddate, out orderdata, out dayTelArrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Telephone);
                    Orders.Actor.GetIndexData(out adddate, out orderdata, out mothTelArrivedata, monthBegintimes, monthEndtimes, sectionStr, (int)CountTypeEnum.Telephone);
                    /****************其他********************/
                    Orders.Actor.GetIndexData(out adddate, out orderdata, out dayAntArrivedata, begintimes, endtimes, sectionStr, (int)CountTypeEnum.Others);
                    Orders.Actor.GetIndexData(out adddate, out orderdata, out mothAntArrivedata, monthBegintimes, monthEndtimes, sectionStr, (int)CountTypeEnum.Others);
                    if ((dayWebArrivedata + dayTelArrivedata + dayAntArrivedata) > 0 || (mothWebArrivedata + mothTelArrivedata + mothAntArrivedata) > 0)
                    {
                        note.Append(string.Format("{0}", item.Name));
                    }
                    if ((dayWebArrivedata + dayTelArrivedata + dayAntArrivedata) > 0)
                    {
                        note.Append(string.Format("今日网络{0}，电话{1}，其他{2}，共{3}；", dayWebArrivedata, dayTelArrivedata, dayAntArrivedata, (dayWebArrivedata + dayTelArrivedata + dayAntArrivedata)));
                    }
                    if ((mothWebArrivedata + mothTelArrivedata + mothAntArrivedata) > 0)
                    {
                        note.Append(string.Format("本月网络{0}，电话{1}，其他{2}，共{3}；", mothWebArrivedata, mothTelArrivedata, mothAntArrivedata, (mothWebArrivedata + mothTelArrivedata + mothAntArrivedata)));
                    }
                }
            }
            return note.ToString();
        }

        /// <summary>
        /// 添加发送日志
        /// </summary>
        /// <param name="UserID">用户名</param>
        /// <param name="entity">成功or失败</param>
        /// <param name="result"></param>
        public void CreateNotifyLog(int UserID, int entity)
        {
            NotifyLog notifylog = new NotifyLog();
            notifylog.SendDate = DateTime.Now;
            notifylog.SendState = entity;
            notifylog.UserID = UserID;
            NotifyLog.Actor.Insert(notifylog);
        }

        /// <summary>
        /// 发送短息启动方法！查找Notifier表客户
        /// </summary>
        public void NotifyBegin()
        {
            IList<Notifier> list = Notifier.Actor.GetAllData();
            foreach (Notifier item in list)
            {
                if (item.UserID > 0)
                {
                    NotifyRun(item.UserID, item.NotifyType);
                }
            }
        }

        public void StartService()
        {
            Thread thread = new Thread(new ThreadStart(ThreadStartRun));
            thread.Start();
        }

        private void ThreadStartRun()
        {
            Run(null);
        }

        private void Run(object obj)
        {
            lock (_lockActor)
            {
                DateTime nowDate = DateTime.Now;

                if (nowDate.Hour == ScheduleHour)
                {
                    NotifyBegin();
                }

                if (_timerCallback == null)
                {
                    _timerCallback = new TimerCallback(Run);
                }

                timer = new Timer(_timerCallback, null, 1000 * 60 * IntervalMinutes, 1000 * 60 * IntervalMinutes);
            }
        }

        /// <summary>
        /// IntervalMinutes
        /// </summary>
        public static int IntervalMinutes
        {
            get
            {
                string strIntervalMinutes = WebConfigUtility.GetAppSetting("IntervalMinutes");

                return ConvertUtility.ToInt32(strIntervalMinutes, 5);
            }
        }

        /// <summary>
        /// ScheduleHour
        /// </summary>
        public static int ScheduleHour
        {
            get
            {
                string strScheduleHour = WebConfigUtility.GetAppSetting("ScheduleHour");

                return ConvertUtility.ToInt32(strScheduleHour, 21);
            }
        }

    }
}
