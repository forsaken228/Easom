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
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Linq;

namespace Easom.Core
{

    ///<summary>
    /// Orders  客户端实体操作类
    ///</summary>
    [Serializable]
    public class Orders:ICloneable
    {
        private static IOrders _actor = null;
        private static readonly object _lockActor = new object();
        private MediaSource _mediasource;
        //private static string desCode = WebConfigUtility.GetAppSetting("encrypt");
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
        /// OrderNumber
        ///</summary>
        public string OrderNumber
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
        /// Age
        ///</summary>
        public int Age
        {
            get;
            set;
        }

        ///<summary>
        /// AreaSourceType
        ///</summary>
        public AreaTypeEnum AreaSourceType
        {
            get;
            set;
        }

        ///<summary>
        /// HospitalWebsiteID
        ///</summary>
        public int HospitalWebsiteID
        {
            get;
            set;
        }

        ///<summary>
        /// HospitalWebsiteID
        ///</summary>
        public HospitalWebsite HospitalWebsite
        {
            get
            {
                HospitalWebsite hospitalWebsite = HospitalWebsite.Actor.GetByID(HospitalWebsiteID);
                return hospitalWebsite;
            }
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
        /// MediaSourceID
        ///</summary>
        public int MediaSourceID
        {
            get;
            set;
        }

        ///<summary>
        /// MediaSourceExtendID
        ///</summary>
        public int MediaSourceExtendID
        {
            get;
            set;
        }

        /// <summary>
        /// 角色
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 媒体来源类
        /// </summary>
        public MediaSource MediaEntity
        {
            get
            {
                if (MediaSourceID > 0)
                {
                    if (_mediasource == null)
                    {
                        MediaSource entity = MediaSource.Actor.GetByID(this.MediaSourceID);
                        if (entity != null)
                        {
                            _mediasource = entity;
                            return entity;
                        }
                    }
                    else
                    {
                        return _mediasource;
                    }
                }
                return null;
            }
        }


        ///<summary>
        /// DoctorID
        ///</summary>
        public int DoctorID
        {
            get;
            set;
        }

        ///<summary>
        /// DoctorID
        ///</summary>
        public Doctor Doctor
        {
            get
            {
                Doctor doctor = Doctor.Actor.GetByID(DoctorID);
                return doctor;
            }
        }

        ///<summary>
        /// Messagelogs
        ///</summary>
        public IList<MessageLog> Messagelogs
        {
            get
            {
                if (Telephone != null)
                {
                    return MessageLog.Actor.GetByTelephone(this.Telephone);
                }
                return null;
            }
        }

        public MessageLog IsSendMessage
        {
            get
            {
                if (Messagelogs != null && Messagelogs.Count > 0)
                {
                    List<MessageLog> list = Messagelogs.ToList();
                    var entity = list.Where(i => (i.SendDate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))).ToList();
                    if (entity != null && entity.Count > 0)
                    {
                        return entity.Last();
                    }
                    return null;
                }
                return null;
            }
        }

        public MessageLog LastMessage
        {
            get
            {
                if (Messagelogs != null && Messagelogs.Count > 0)
                {
                    List<MessageLog> list = Messagelogs.ToList();
                    var entity = list.Last();
                    return entity;
                }
                return null;
            }
        }

        ///<summary>
        /// CreateUserID
        ///</summary>
        public int CreateUserID
        {
            get;
            set;
        }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreateUserName
        {
            get
            {
                UserInfo user = UserInfo.Actor.GetByID(CreateUserID);
                if (user != null)
                {
                    return user.TrueName;
                }
                return null;
            }
        }
        /// <summary>
        /// 创建人角色
        /// </summary>
        public string CreateUserRole
        {
            get
            {
                UserInfo user = UserInfo.Actor.GetByID(CreateUserID);
                if (user != null)
                {
                    return user.RoleTypeName;
                }
                return null;
            }
        }
        /// <summary>
        /// 创建人头像
        /// </summary>
        public string CreateUserPictureURL
        {
            get
            {
                UserInfo user = UserInfo.Actor.GetByID(CreateUserID);
                if (user != null)
                {
                    return user.PictureURL;
                }
                return null;
            }
        }
        ///<summary>
        /// Sex
        ///</summary>
        public bool Sex
        {
            get;
            set;
        }

        ///<summary>
        /// ArriveStateType
        ///</summary>
        public ArriveStateEnum ArriveStateType
        {
            get;
            set;
        }

        ///<summary>
        /// CountType
        ///</summary>
        public CountTypeEnum CountType
        {
            get;
            set;
        }

        ///<summary>
        /// 医生编号
        ///</summary>
        public string ExpertIdentifier
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
        /// QQ
        ///</summary>
        public string QQ
        {
            get;
            set;
        }

        ///<summary>
        /// Question
        ///</summary>
        public string Question
        {
            get;
            set;
        }

        ///<summary>
        /// ChatRecord
        ///</summary>
        public string ChatRecord
        {
            get;
            set;
        }

        ///<summary>
        /// DESTelephone
        ///</summary>
        public string DESTelephone
        {
            get
            {
                string tel = "";
                if (!string.IsNullOrEmpty(this.Telephone))
                {
                    // tel = SecurityUtility.DESDecrypt(this.Telephone, desCode);
                    tel = this.Telephone;
                    if (tel.Length == 11)//185 0089 8765
                    {
                        tel = tel.Substring(0, 3) + "****" + tel.Substring(7, 4);
                    }
                    else if (tel.Length <= 8 && tel.Length > 4)
                    {
                        tel = "****" + tel.Substring(4, 3);
                    }
                    else
                    {
                        tel = "*******";
                    }
                    return tel;
                }
                return "";
            }
        }

        ///<summary>
        /// DESQuestion
        ///</summary>
        public string DESQuestion
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Question))
                {
                    return "*********";
                }
                return "";
            }
        }


        ///<summary>
        /// DESQQ
        ///</summary>
        public string DESQQ
        {
            get
            {
                if (!string.IsNullOrEmpty(this.QQ))
                {
                    return "*********";
                }
                return "";
            }
        }

        ///<summary>
        /// DESChatRecord
        ///</summary>
        public string DESChatRecord
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ChatRecord))
                {
                    return "********";
                }
                return "";
            }
        }

        ///<summary>
        /// DECTelephone
        ///</summary>
        //public string DECTelephone
        //{
        //    get
        //    {
        //        string tel = "";
        //        if (!string.IsNullOrEmpty(this.Telephone))
        //        {
        //            try
        //            {
        //                //  tel = SecurityUtility.DESDecrypt(this.Telephone, desCode);
        //                tel = this.Telephone;
        //                return tel;
        //            }
        //            catch (Exception ex)
        //            {
        //                Log4NetUtility.WriterErrorLog(ex.ToString());
        //                throw;
        //            }
        //        }
        //        return "";
        //    }
        //}

        ///<summary>
        /// DECQuestion
        ///</summary>
        public string DECQuestion
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Question))
                {
                    string question = "";
                    question =this.Question;
                    return question;
                }
                return "";
            }
        }


        ///<summary>
        ///DECQQ
        ///</summary>
        public string DECQQ
        {
            get
            {
                if (!string.IsNullOrEmpty(this.QQ))
                {
                    string qq = "";
                    qq =this.QQ;
                    return qq;
                }
                return "";
            }
        }

        ///<summary>
        /// DECChatRecord
        ///</summary>
        public string DECChatRecord
        {
            get
            {
                if (!string.IsNullOrEmpty(this.ChatRecord))
                {
                    string chatRecord = "";
                    chatRecord = this.ChatRecord;
                    return chatRecord;
                }
                return "";
            }
        }

        ///<summary>
        /// KeyWords
        ///</summary>
        public string KeyWords
        {
            get;
            set;
        }

        ///<summary>
        /// DiseaseID
        ///</summary>
        public int DiseaseID
        {
            get;
            set;
        }

        ///<summary>
        /// 疾病类
        ///</summary>
        public Disease Disease
        {
            get
            {
                Disease disease = Disease.Actor.GetByID(DiseaseID);
                if (disease != null)
                {
                    return disease;
                }
                return null;
            }
        }

        ///<summary>
        /// Area
        ///</summary>
        public string Area
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
        /// OrderTime
        ///</summary>
        public DateTime OrderTime
        {
            get;
            set;
        }

        ///<summary>
        /// UpdateTime
        ///</summary>
        public DateTime UpdateTime
        {
            get;
            set;
        }

        ///<summary>
        /// AddTime
        ///</summary>
        public DateTime AddTime
        {
            get;
            set;
        }

        ///<summary>
        /// 到院时间
        ///</summary>
        public DateTime RecordTime
        {
            get;
            set;
        }

        ///<summary>
        /// 签到人ID
        ///</summary>
        public int RecordUserID
        {
            get;
            set;
        }

        ///<summary>
        /// 签到人姓名
        ///</summary>
        public string RecordUserName
        {
            get
            {
                UserInfo user = UserInfo.Actor.GetByID(CreateUserID);
                if (user != null)
                {
                    return user.TrueName;
                }
                return null;
            }
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
        /// 所属部门
        ///</summary>
        public Section SectionName
        {
            get
            {
                Section section = Section.Actor.GetByID(SectionID);
                return section;
            }
        }

        ///<summary>
        /// DRemark
        ///</summary>
        public string DRemark
        {
            get;
            set;
        }

        ///<summary>
        /// OrderState
        ///</summary>
        public int OrderState
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
        /// 最新的回访记录
        ///</summary>
        public CallOnData LastCallOnDada
        {
            get
            {
                CallOnData callOnData = CallOnData.Actor.GetLastDataByOrderID(this.ID, 1);
                if (callOnData != null)
                {
                    return callOnData;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion 属性

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        public Orders Clone()
        {
            return (Orders)this.MemberwiseClone();
        }

        /// <summary>
        /// 是否消费
        /// </summary>
        public bool IsConsume
        {
            get;
            set;
        }
        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal  ConsumeNum
        {
            get;
            set;
        }
        ///<summary>
        /// Actor
        ///</summary>
        public static IOrders Actor
        {
            get
            {
                if (_actor == null)
                {
                    lock (_lockActor)
                    {
                        if (_actor == null)
                        {
                            _actor = new OrdersService();
                        }
                    }
                }

                return _actor;
            }
        }

        /// <summary>
        /// 获取今日之前的周一或者周日
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetDateTime(int id)
        {
            DateTime dt = System.DateTime.Now;
            DayOfWeek doW = dt.DayOfWeek;
            switch (id)
            {
                case 1:
                    switch (doW.ToString())
                    {
                        case "Tuesday":
                            dt = dt.AddDays(6);
                            break;
                        case "Wednesday":
                            dt = dt.AddDays(5);
                            break;
                        case "Thursday":
                            dt = dt.AddDays(4);
                            break;
                        case "Friday":
                            dt = dt.AddDays(3);
                            break;
                        case "Saturday":
                            dt = dt.AddDays(2);
                            break;
                        case "Sunday":
                            dt = dt.AddDays(1);
                            break;
                    }
                    break;
                case 6:
                    switch (doW.ToString())
                    {
                        case "Mondy":
                            dt = dt.AddDays(5);
                            break;
                        case "Tuesday":
                            dt = dt.AddDays(4);
                            break;
                        case "Wednesday":
                            dt = dt.AddDays(3);
                            break;
                        case "Thursday":
                            dt = dt.AddDays(2);
                            break;
                        case "Friday":
                            dt = dt.AddDays(1);
                            break;
                        case "Sunday":
                            dt = dt.AddDays(6);
                            break;
                    }
                    break;
                case 7:
                    switch (doW.ToString())
                    {
                        case "Mondy":
                            dt = dt.AddDays(6);
                            break;
                        case "Tuesday":
                            dt = dt.AddDays(5);
                            break;
                        case "Wednesday":
                            dt = dt.AddDays(4);
                            break;
                        case "Thursday":
                            dt = dt.AddDays(3);
                            break;
                        case "Friday":
                            dt = dt.AddDays(2);
                            break;
                        case "Saturday":
                            dt = dt.AddDays(1);
                            break;
                    }
                    break;
                default:
                    break;
            }
            return dt.ToString("yyyy-MM-dd");
        }
        public static string ConvertChatToTextArea(string chatContent)
        {
            string content = string.Empty;
            string[] paras = chatContent.Split('&');
            if (paras != null && paras.Length > 0)
            {
                foreach (var item in paras)
                {
                    content += item + "\n";
                }
            }
            return content;
        }
        /// <summary>
        /// 字典类型转换为字符串类型
        /// </summary>
        /// <param name="searchData"></param>
        /// <returns></returns>
        public static string GetSearchDataString(Dictionary<string, string> searchData)
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

        /// <summary>
        /// 数字转换为字母1=A 2=B 依次类推，27表示AA
        /// </summary>
        /// <param name="num">数字</param>
        /// <returns>字母</returns>
        public static string ConvertNumToChar(Int32 num)
        {
            if (num > 1 && num <= 26)
            {
                return Char.ConvertFromUtf32(num + 64);
            }
            else if (num < 1)
            {
                return string.Empty;
            }
            else
            {
                int division = num / 26;
                int mod = num % 26;
                if (mod == 0)
                {
                    return ConvertNumToChar(division - 1) + "Z";
                }
                else
                {
                    return ConvertNumToChar(division) + char.ConvertFromUtf32(mod + 64);
                }
            }
        }

        #region 导出EXCEL报表

        #region 导出预约信息报表

        /// <summary>
        /// 导出预约信息报表
        /// </summary>
        public static bool ExportOrders(IList<Orders> orders, string title, string savePath)
        {
            System.Data.DataTable dt = ConvertOrdersToDataTable(orders);
            if (dt != null)
            {
                try
                {
                    HSSFWorkbook workbook = new HSSFWorkbook();
                    //创建3个Sheet
                    HSSFSheet sheet1 = (HSSFSheet)workbook.CreateSheet("Sheet1");
                    HSSFSheet sheet2 = (HSSFSheet)workbook.CreateSheet("Sheet2");
                    HSSFSheet sheet3 = (HSSFSheet)workbook.CreateSheet("Sheet3");

                    //保存excel
                    FileStream fileSave = new FileStream(savePath, FileMode.Create);
                    //DocumentSummaryInformation和SummaryInformation等属性知识
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company ="";
                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Subject = "" + title + "预约统计数据";
                    //赋值给工作表
                    workbook.DocumentSummaryInformation = dsi;
                    workbook.SummaryInformation = si;

                    //创建行
                    IRow row1 = sheet1.CreateRow(0);
                    //创建单元格
                    row1.CreateCell(0).SetCellValue(title);//第一行第一个标题
                    ICellStyle style = workbook.CreateCellStyle();
                    // sheet1.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");
                    //这么用有个前提，那就是第0行还没创建过，否则得这么用： sheet1.GetRow(0).CreateCell(0).SetCellValue("This is a Sample");
                    // 如果你要获得某一个已经创建的单元格对象，可以用下面的代码：  sheet1.GetRow(row_index).GetCell(column_index);
                    style.Alignment = HorizontalAlignment.CENTER;
                    // 设置字体
                    IFont font = workbook.CreateFont();
                    font.FontHeight = 20 * 20;
                    style.SetFont(font);
                    row1.GetCell(0).CellStyle = style;
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 15));
                    /**************双行样式*******************/
                    ICellStyle rowStyleS = workbook.CreateCellStyle();
                    IFont font2 = workbook.CreateFont();
                    font2.FontHeight = 15 * 15;
                    rowStyleS.SetFont(font2);
                    rowStyleS.Alignment = HorizontalAlignment.CENTER;
                    rowStyleS.BorderTop = BorderStyle.THIN;
                    rowStyleS.BorderLeft = BorderStyle.THIN;
                    rowStyleS.BorderBottom = BorderStyle.THIN;
                    rowStyleS.FillPattern = FillPatternType.SOLID_FOREGROUND;
                    rowStyleS.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_YELLOW.index;
                    /**************单行样式*******************/
                    ICellStyle rowStyleD = workbook.CreateCellStyle();
                    rowStyleD.SetFont(font2);
                    rowStyleD.Alignment = HorizontalAlignment.CENTER;
                    rowStyleD.BorderTop = BorderStyle.THIN;
                    rowStyleD.BorderLeft = BorderStyle.THIN;
                    rowStyleD.BorderBottom = BorderStyle.THIN;
                    /**************到院时间样式*******************/
                    ICellStyle rowStyleDaoTime = workbook.CreateCellStyle();
                    IFont fontRed = workbook.CreateFont();
                    fontRed.Color = NPOI.HSSF.Util.HSSFColor.RED.index;
                    rowStyleDaoTime.SetFont(fontRed);
                    rowStyleDaoTime.Alignment = HorizontalAlignment.CENTER;
                    rowStyleDaoTime.BorderTop = BorderStyle.THIN;
                    rowStyleDaoTime.BorderLeft = BorderStyle.THICK;
                    rowStyleDaoTime.BorderRight = BorderStyle.THICK;
                    rowStyleDaoTime.BorderBottom = BorderStyle.THIN;
                    rowStyleDaoTime.FillPattern = FillPatternType.SOLID_FOREGROUND;
                    rowStyleDaoTime.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_YELLOW.index;
                    /**************日期样式*******************/
                    ICellStyle Datestyle = workbook.CreateCellStyle();
                    IFont font1 = workbook.CreateFont();
                    font1.FontHeight = 17 * 17;
                    font1.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index;
                    Datestyle.SetFont(font1);
                    Datestyle.Alignment = HorizontalAlignment.CENTER;
                    Datestyle.BorderTop = BorderStyle.THIN;
                    Datestyle.BorderLeft = BorderStyle.THIN;
                    Datestyle.BorderBottom = BorderStyle.THIN;
                    Datestyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                    Datestyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_BLUE.index;
                    /*************冻结标题****************/
                    sheet1.CreateFreezePane(0, 2, 0, 2);
                    for (int i = 1; i < dt.Rows.Count + 1; i++)
                    {
                        IRow rows = sheet1.CreateRow(i);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            rows.CreateCell(j).SetCellValue(dt.Rows[i - 1][j].ToString());
                            if (i % 2 == 0)
                            {
                                rows.GetCell(j).CellStyle = rowStyleS;
                            }
                            else
                            {
                                rows.GetCell(j).CellStyle = rowStyleD;
                            }

                            if (j == 14)
                            {
                                rows.GetCell(j).CellStyle = rowStyleDaoTime;
                            }
                        }

                    }
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sheet1.GetRow(1).GetCell(j).CellStyle = Datestyle;
                    }
                    workbook.Write(fileSave);
                    fileSave.Close();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 将范型数据转换为dateTable
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        private static System.Data.DataTable ConvertOrdersToDataTable(IList<Orders> entities)
        {
            try
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("预约号");
                dt.Columns.Add("姓名");
                dt.Columns.Add("性别");
                dt.Columns.Add("年龄");
                dt.Columns.Add("电话");
                dt.Columns.Add("预约来院时间");
                dt.Columns.Add("预约方式");
                dt.Columns.Add("媒体来源");
                dt.Columns.Add("来源网站");
                dt.Columns.Add("关键词");
                dt.Columns.Add("地区来源");
                dt.Columns.Add("病种分类");
                dt.Columns.Add("预约专家");
                dt.Columns.Add("咨询医师");
                dt.Columns.Add("到院时间");
                dt.Columns.Add("登记时间");
                dt.Columns.Add("备注");

                DataRow drs1 = dt.NewRow();
                drs1[0] = "预约号";
                drs1[1] = "姓名";
                drs1[2] = "性别";
                drs1[3] = "年龄";
                drs1[4] = "电话";
                drs1[5] = "预约来院时间";
                drs1[6] = "预约方式";
                drs1[7] = "媒体来源";
                drs1[8] = "来源网站";
                drs1[9] = "关键词";
                drs1[10] = "地区来源";
                drs1[11] = "病种分类";
                drs1[12] = "预约专家";
                drs1[13] = "咨询医师";
                drs1[14] = "到院时间";
                drs1[15] = "登记时间";
                drs1[16] = "备注";
                dt.Rows.Add(drs1);
                foreach (var entity in entities)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = entity.ExpertIdentifier;
                    dr[1] = entity.Name;
                    dr[2] = entity.Sex == true ? "男" : "女";
                    dr[3] = entity.Age;
                    dr[4] = entity.Telephone;
                    dr[5] = entity.OrderTime;
                    dr[6] = entity.CountType == CountTypeEnum.Web ? "网络" : entity.CountType == CountTypeEnum.Telephone ? "电话" : "其他";

                    string media = string.Empty;
                    if (entity.MediaEntity != null)
                    {
                        media += entity.MediaEntity.Name;
                    }
                    else
                    {
                        media += "无";
                    }
                    if (entity.MediaSourceExtendID != -1)
                    {
                        MediaSource childs = MediaSource.Actor.GetByID(entity.MediaSourceExtendID);

                        if (childs != null)
                        {
                            media += " - " + childs.Name + "";
                        }
                    }

                    dr[7] = media;

                    if (entity.HospitalWebsiteID > -1)
                    {
                        HospitalWebsite hosw = HospitalWebsite.Actor.GetByID(entity.HospitalWebsiteID);
                        if (hosw != null)
                        {
                            dr[8] = hosw.URL;
                        }
                        else
                        {
                            dr[8] = "";
                        }
                    }
                    else
                    {
                        dr[8] = "";
                    }
                    dr[9] = entity.KeyWords;
                    string AareSource = string.Empty;
                    AareSource = entity.AreaSourceType == 0 ? "本地" : "外地";

                    if (!String.IsNullOrEmpty(entity.Area))
                    {
                        dr[10] = "" + AareSource + " - " + entity.Area + "";
                    }
                    else
                    {
                        dr[10] = AareSource;
                    }


                    dr[11] = entity.Disease != null ? entity.Disease.Name : "";
                    dr[12] = entity.Doctor != null ? entity.Doctor.Name : "";
                    dr[13] = entity.CreateUserName;

                    if (entity.ArriveStateType == ArriveStateEnum.Arrived)
                    {
                        dr[14] = entity.RecordTime;
                    }
                    else
                    {
                        dr[14] = string.Empty;
                    }

                    dr[15] = entity.AddTime;
                    dr[16] = entity.Remark;
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region 导出角色EXCEL报表

        public static bool ExportOrders(string role, string section, DateTime startTime, DateTime endTime, string title, string savePath)
        {
            DataTable dt = CreateDataTable(role, section, title, startTime, endTime);
            if (dt != null)
            {
                TimeSpan ts1 = new TimeSpan(startTime.Ticks);
                TimeSpan ts2 = new TimeSpan(endTime.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();

                TimeSpan ts3 = new TimeSpan(Convert.ToDateTime(startTime.ToString("yyyy-MM-dd 00:00:00")).Ticks);
                TimeSpan ts4 = new TimeSpan(Convert.ToDateTime(endTime.ToString("yyyy-MM-dd 00:00:00")).Ticks);
                TimeSpan ts5 = ts1.Subtract(ts4).Duration();

                HSSFWorkbook workbook = new HSSFWorkbook();
                //创建3个Sheet
                HSSFSheet sheet1 = (HSSFSheet)workbook.CreateSheet("Sheet1");
                HSSFSheet sheet2 = (HSSFSheet)workbook.CreateSheet("Sheet2");
                HSSFSheet sheet3 = (HSSFSheet)workbook.CreateSheet("Sheet3");

                //保存excel
                FileStream fileSave = new FileStream(savePath, FileMode.Create);
                //DocumentSummaryInformation和SummaryInformation等属性知识
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "莆田万邦科技有限公司";
                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Subject = "" + title + "预约统计数据";
                //赋值给工作表
                workbook.DocumentSummaryInformation = dsi;
                workbook.SummaryInformation = si;

                /**********************标题******************************/
                //创建行
                IRow row1 = sheet1.CreateRow(0);
                //创建单元格
                row1.CreateCell(0).SetCellValue(title);//第一行第一个标题
                ICellStyle titlestyle = workbook.CreateCellStyle();
                titlestyle.Alignment = HorizontalAlignment.CENTER;
                IFont font = workbook.CreateFont();
                font.FontHeight = 20 * 20;
                font.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index;
                titlestyle.SetFont(font);
                titlestyle.BorderRight = BorderStyle.THICK;
                titlestyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                titlestyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_BLUE.index;
                row1.GetCell(0).CellStyle = titlestyle;
                sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 16));
                /***********姓名样式***************/
                ICellStyle Namestyle = (NPOI.HSSF.UserModel.HSSFCellStyle)workbook.CreateCellStyle();
                IFont fontN = workbook.CreateFont();
                fontN.Boldweight = 30000;
                Namestyle.SetFont(fontN);
                Namestyle.Alignment = HorizontalAlignment.CENTER;
                Namestyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                Namestyle.BorderRight = BorderStyle.THICK;
                Namestyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_ORANGE.index;
                // 样式设置,在这里的样式设置主要是实现换行(即上面的”\n”) 
                Namestyle.WrapText = true;
                /************小标题文字样式************/
                ICellStyle smallTitleStyle = workbook.CreateCellStyle();

                IFont font2 = workbook.CreateFont();
                font2.Boldweight = 30000;
                font2.FontHeight = 17 * 17;
                font2.Color = NPOI.HSSF.Util.HSSFColor.GREEN.index;
                smallTitleStyle.SetFont(font2);
                smallTitleStyle.Alignment = HorizontalAlignment.CENTER;
                smallTitleStyle.BorderRight = BorderStyle.THIN;
                smallTitleStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                smallTitleStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_YELLOW.index;

                /************统计账户标题文字样式************/
                ICellStyle countTypeTitleStyle = workbook.CreateCellStyle();
                IFont font1 = workbook.CreateFont();
                font1.Boldweight = 30000;
                font1.Color = NPOI.HSSF.Util.HSSFColor.LIGHT_GREEN.index;
                font1.FontHeight = 16 * 16;
                countTypeTitleStyle.SetFont(font1);
                countTypeTitleStyle.Alignment = HorizontalAlignment.CENTER;
                countTypeTitleStyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                countTypeTitleStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index;
                countTypeTitleStyle.BorderRight = BorderStyle.THIN;

                ICellStyle countTypeTitleStyle2 = workbook.CreateCellStyle();
                countTypeTitleStyle2.SetFont(font1);
                countTypeTitleStyle2.Alignment = HorizontalAlignment.CENTER;
                countTypeTitleStyle2.FillPattern = FillPatternType.SOLID_FOREGROUND;
                countTypeTitleStyle2.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREEN.index;
                countTypeTitleStyle2.BorderRight = BorderStyle.THICK;

                /************背景颜色*****************/
                ICellStyle backgroundColor_Yellow = workbook.CreateCellStyle();//黄色
                backgroundColor_Yellow.FillPattern = FillPatternType.SOLID_FOREGROUND;
                backgroundColor_Yellow.BorderRight = BorderStyle.THIN;
                backgroundColor_Yellow.BorderBottom = BorderStyle.THIN;
                backgroundColor_Yellow.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_YELLOW.index;

                ICellStyle backgroundColor_Yellow2 = workbook.CreateCellStyle();//黄色
                backgroundColor_Yellow2.FillPattern = FillPatternType.SOLID_FOREGROUND;
                backgroundColor_Yellow2.BorderRight = BorderStyle.THICK;
                backgroundColor_Yellow2.BorderBottom = BorderStyle.THIN;
                backgroundColor_Yellow2.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_YELLOW.index;

                ICellStyle backgroundColor_Red = workbook.CreateCellStyle();//红色色
                backgroundColor_Red.BorderRight = BorderStyle.THIN;
                backgroundColor_Red.BorderBottom = BorderStyle.THIN;
                backgroundColor_Red.FillPattern = FillPatternType.SOLID_FOREGROUND;
                backgroundColor_Red.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.RED.index;


                ICellStyle backgroundColor_Red2 = workbook.CreateCellStyle();//红色色
                backgroundColor_Red2.BorderRight = BorderStyle.THICK;
                backgroundColor_Red2.BorderBottom = BorderStyle.THIN;
                backgroundColor_Red2.FillPattern = FillPatternType.SOLID_FOREGROUND;
                backgroundColor_Red2.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.RED.index;

                ICellStyle backgroundColor_Blue = workbook.CreateCellStyle();//蓝色
                IFont fontB = workbook.CreateFont();
                fontB.Boldweight = 30000;
                fontB.Color = NPOI.HSSF.Util.HSSFColor.LIGHT_GREEN.index;
                backgroundColor_Blue.SetFont(fontB);
                backgroundColor_Blue.BorderRight = BorderStyle.THIN;
                backgroundColor_Blue.BorderBottom = BorderStyle.THIN;
                backgroundColor_Blue.FillPattern = FillPatternType.SOLID_FOREGROUND;
                backgroundColor_Blue.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_BLUE.index;

                ICellStyle backgroundColor_Blue2 = workbook.CreateCellStyle();//蓝色
                backgroundColor_Blue2.SetFont(fontB);
                backgroundColor_Blue2.BorderRight = BorderStyle.THICK;
                backgroundColor_Blue2.BorderBottom = BorderStyle.THIN;
                backgroundColor_Blue2.FillPattern = FillPatternType.SOLID_FOREGROUND;
                backgroundColor_Blue2.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_BLUE.index;

                ICellStyle backgroundColor_Orange = workbook.CreateCellStyle();//橘色
                backgroundColor_Orange.FillPattern = FillPatternType.SOLID_FOREGROUND;
                backgroundColor_Orange.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_ORANGE.index;

                /*******************右边框************************/
                ICellStyle border = workbook.CreateCellStyle();
                border.BorderRight = BorderStyle.THIN;
                border.BorderBottom = BorderStyle.THIN;
                ICellStyle borderRight = workbook.CreateCellStyle();
                borderRight.BorderRight = BorderStyle.THICK;
                borderRight.BorderBottom = BorderStyle.THIN;
                /**************冻结************************/
                //sheet.CreateFreezePane(6, 10);
                /******************预约就诊小标题*************************/
                ICellStyle sTitleStyle = workbook.CreateCellStyle();
                IFont fonts = workbook.CreateFont();
                fonts.Boldweight = 30000;
                fonts.Color = NPOI.HSSF.Util.HSSFColor.BLACK.index;
                fonts.FontHeight = 14 * 14;
                sTitleStyle.SetFont(fonts);
                sTitleStyle.Alignment = HorizontalAlignment.CENTER;
                sTitleStyle.BorderRight = BorderStyle.THIN;

                ICellStyle sTitleStyle2 = workbook.CreateCellStyle();
                sTitleStyle2.SetFont(fonts);
                sTitleStyle2.Alignment = HorizontalAlignment.CENTER;
                sTitleStyle2.BorderRight = BorderStyle.THICK;
                /************************************/
                IDrawing patr = sheet1.CreateDrawingPatriarch();
                IComment comment1 = patr.CreateCellComment(new HSSFClientAnchor(0, 0, 0, 0, 3, 6, 6, 13));
                comment1.Visible = false;
                comment1.String = new HSSFRichTextString("上月同步业绩:即，上个月这一天所有预约和到诊的人数。比如今天是5月1日，上月同步业绩就是4月1日的数据");
                comment1.Author = "系统提醒：";

                for (int i = 1; i < dt.Rows.Count + 1; i++)
                {
                    IRow rows = sheet1.CreateRow(i);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        rows.CreateCell(j).SetCellValue(dt.Rows[i - 1][j].ToString());
                        rows.GetCell(j).CellStyle = border;
                    }
                    rows.GetCell(0).CellStyle = borderRight;
                    rows.GetCell(8).CellStyle = borderRight;
                    rows.GetCell(16).CellStyle = borderRight;
                }
                sheet1.GetRow(5).GetCell(0).CellComment = comment1;
                int arriyCation = (dt.Rows.Count / (ts.Days + 1));
                for (int i = 1; i < dt.Rows.Count + 1; i = i + arriyCation)/*按组循环*//*加1是为了去掉头部*/
                {
                    for (int j = 3; j < 5; j++)//上月数据，上月同步数据
                    {
                        for (int k = 9; k < 15; k++)//从第九行开始循环
                        {
                            if (k == 14)
                            {
                                sheet1.GetRow(j + i).GetCell(k).SetCellValue(Convert.ToInt32(dt.Rows[j + i - 1][k]));
                                sheet1.GetRow(j + i).GetCell(15).SetCellFormula("sum(J" + (j + i + 1) + "+L" + (j + i + 1) + "+N" + (j + i + 1) + ")");
                                sheet1.GetRow(j + i).GetCell(16).SetCellFormula("sum(K" + (j + i + 1) + "+M" + (j + i + 1) + "+O" + (j + i + 1) + ")");
                            }
                            else
                            {
                                sheet1.GetRow(j + i).GetCell(k).SetCellValue(Convert.ToInt32(dt.Rows[j + i - 1][k]));
                            }
                        }
                        //设置背景为蓝色
                        sheet1.GetRow(j + i).GetCell(0).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(1).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(2).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(3).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(4).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(5).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(6).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(7).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(8).CellStyle = backgroundColor_Blue2;
                        sheet1.GetRow(j + i).GetCell(9).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(10).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(11).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(12).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(13).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(14).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(15).CellStyle = backgroundColor_Blue;
                        sheet1.GetRow(j + i).GetCell(16).CellStyle = backgroundColor_Blue2;
                    }
                    for (int j = 0; j < 17; j++)//合计背景色
                    {
                        if (j == 8 || j == 16)
                        {
                            sheet1.GetRow(i + arriyCation - 1).GetCell(j).CellStyle = backgroundColor_Red2;
                        }
                        else
                        {
                            sheet1.GetRow(i + arriyCation - 1).GetCell(j).CellStyle = backgroundColor_Red;
                        }
                    }
                    for (int j = 5; j < 5 + arriyCation - 6; j++)//数据，减去公用的六行标题类
                    {
                        for (int k = 1; k < 7; k++)
                        {
                            if (k == 6)
                            {
                                sheet1.GetRow(j + i).GetCell(k).SetCellValue(Convert.ToInt32(dt.Rows[j + i - 1][k]));
                                sheet1.GetRow(j + i).GetCell(7).SetCellFormula("sum(B" + (j + i + 1) + "+D" + (j + i + 1) + "+F" + (j + i + 1) + ")");
                                sheet1.GetRow(j + i).GetCell(8).SetCellFormula("sum(C" + (j + i + 1) + "+E" + (j + i + 1) + "+G" + (j + i + 1) + ")");
                                if (i == 1)
                                {
                                    sheet1.GetRow(j + i).GetCell(9).SetCellFormula("B" + (j + i + 1) + "");
                                    sheet1.GetRow(j + i).GetCell(10).SetCellFormula("C" + (j + i + 1) + "");
                                    sheet1.GetRow(j + i).GetCell(11).SetCellFormula("D" + (j + i + 1) + "");
                                    sheet1.GetRow(j + i).GetCell(12).SetCellFormula("E" + (j + i + 1) + "");
                                    sheet1.GetRow(j + i).GetCell(13).SetCellFormula("F" + (j + i + 1) + "");
                                    sheet1.GetRow(j + i).GetCell(14).SetCellFormula("G" + (j + i + 1) + "");
                                    sheet1.GetRow(j + i).GetCell(15).SetCellFormula("H" + (j + i + 1) + "");
                                    sheet1.GetRow(j + i).GetCell(16).SetCellFormula("I" + (j + i + 1) + "");
                                }
                                else
                                {
                                    sheet1.GetRow(j + i).GetCell(9).SetCellFormula("sum(B" + (j + i + 1) + "+J" + ((j + i + 1) - arriyCation) + ")");
                                    sheet1.GetRow(j + i).GetCell(10).SetCellFormula("sum(C" + (j + i + 1) + "+K" + ((j + i + 1) - arriyCation) + ")");
                                    sheet1.GetRow(j + i).GetCell(11).SetCellFormula("sum(D" + (j + i + 1) + "+L" + ((j + i + 1) - arriyCation) + ")");
                                    sheet1.GetRow(j + i).GetCell(12).SetCellFormula("sum(E" + (j + i + 1) + "+M" + ((j + i + 1) - arriyCation) + ")");
                                    sheet1.GetRow(j + i).GetCell(13).SetCellFormula("sum(F" + (j + i + 1) + "+N" + ((j + i + 1) - arriyCation) + ")");
                                    sheet1.GetRow(j + i).GetCell(14).SetCellFormula("sum(G" + (j + i + 1) + "+O" + ((j + i + 1) - arriyCation) + ")");

                                    sheet1.GetRow(j + i).GetCell(15).SetCellFormula("sum(H" + (j + i + 1) + "+P" + ((j + i + 1) - arriyCation) + ")");
                                    sheet1.GetRow(j + i).GetCell(16).SetCellFormula("sum(I" + (j + i + 1) + "+Q" + ((j + i + 1) - arriyCation) + ")");
                                }

                            }
                            else
                            {
                                sheet1.GetRow(j + i).GetCell(k).SetCellValue(Convert.ToInt32(dt.Rows[j + i - 1][k]));
                            }
                        }
                    }
                    /*总结函数注入*/
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(1).SetCellFormula("SUM(B" + (7 + i - 1) + ":B" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(2).SetCellFormula("SUM(C" + (7 + i - 1) + ":C" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(3).SetCellFormula("SUM(D" + (7 + i - 1) + ":D" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(4).SetCellFormula("SUM(E" + (7 + i - 1) + ":E" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(5).SetCellFormula("SUM(F" + (7 + i - 1) + ":F" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(6).SetCellFormula("SUM(G" + (7 + i - 1) + ":G" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(7).SetCellFormula("SUM(H" + (7 + i - 1) + ":H" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(8).SetCellFormula("SUM(I" + (7 + i - 1) + ":I" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(9).SetCellFormula("SUM(J" + (7 + i - 1) + ":J" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(10).SetCellFormula("SUM(K" + (7 + i - 1) + ":K" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(11).SetCellFormula("SUM(L" + (7 + i - 1) + ":L" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(12).SetCellFormula("SUM(M" + (7 + i - 1) + ":M" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(13).SetCellFormula("SUM(N" + (7 + i - 1) + ":N" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(14).SetCellFormula("SUM(O" + (7 + i - 1) + ":O" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(15).SetCellFormula("SUM(P" + (7 + i - 1) + ":P" + (arriyCation - 1 + i) + ")");
                    //sheet1.GetRow(arriyCation - 1 + i).GetCell(16).SetCellFormula("SUM(Q" + (7 + i - 1) + ":Q" + (arriyCation - 1 + i) + ")");
                    /*合并小标题单元格*/
                    sheet1.GetRow(i).GetCell(1).CellStyle = smallTitleStyle;
                    sheet1.GetRow(i).GetCell(9).CellStyle = smallTitleStyle;


                    sheet1.AddMergedRegion(new CellRangeAddress(i, i, 1, 8));
                    sheet1.AddMergedRegion(new CellRangeAddress(i, i, 9, 16));
                    /*统计账户标题合并*/
                    sheet1.AddMergedRegion(new CellRangeAddress(i + 1, i + 1, 1, 2));
                    sheet1.AddMergedRegion(new CellRangeAddress(i + 1, i + 1, 3, 4));
                    sheet1.AddMergedRegion(new CellRangeAddress(i + 1, i + 1, 5, 6));
                    sheet1.AddMergedRegion(new CellRangeAddress(i + 1, i + 1, 7, 8));
                    sheet1.AddMergedRegion(new CellRangeAddress(i + 1, i + 1, 9, 10));
                    sheet1.AddMergedRegion(new CellRangeAddress(i + 1, i + 1, 11, 12));
                    sheet1.AddMergedRegion(new CellRangeAddress(i + 1, i + 1, 13, 14));
                    sheet1.AddMergedRegion(new CellRangeAddress(i + 1, i + 1, 15, 16));

                    for (int j = 1; j < 17; j++)
                    {
                        sheet1.GetRow(arriyCation - 1 + i).GetCell(j).SetCellFormula("SUM(" + ConvertNumToChar(j + 1) + "" + (7 + i - 1) + ":" + ConvertNumToChar(j + 1) + "" + (arriyCation - 1 + i) + ")");
                        if (j == 8 || j == 16)
                        {
                            sheet1.GetRow(i + 1).GetCell(j).CellStyle = countTypeTitleStyle2;
                            sheet1.GetRow(i + 2).GetCell(j).CellStyle = sTitleStyle2;
                        }
                        else
                        {
                            sheet1.GetRow(i + 1).GetCell(j).CellStyle = countTypeTitleStyle;
                            sheet1.GetRow(i + 2).GetCell(j).CellStyle = sTitleStyle;
                        }

                        if (j % 2 == 0)
                        {

                            if (j != 1 || j != 3 || j != 5)
                            {
                                if (j == 8 || j == 16)
                                {
                                    for (int k = 1; k < arriyCation - 5; k++)
                                    {

                                        sheet1.GetRow(i + k + 4).GetCell(j).CellStyle = backgroundColor_Yellow2;
                                    }
                                }
                                else
                                {
                                    for (int k = 1; k < arriyCation - 5; k++)
                                    {

                                        sheet1.GetRow(i + k + 4).GetCell(j).CellStyle = backgroundColor_Yellow;
                                    }
                                }

                            }
                        }

                    }

                    /*合并单元格*/
                    sheet1.GetRow(i + 1).GetCell(0).CellStyle = Namestyle;
                    sheet1.AddMergedRegion(new CellRangeAddress(i + 1, i + 2, 0, 0));//小标题合并

                    HSSFPatriarch patriarch = (NPOI.HSSF.UserModel.HSSFPatriarch)sheet1.CreateDrawingPatriarch();
                    HSSFClientAnchor a1 = new HSSFClientAnchor(0, 0, 1023, 255, 0, i + 1, 0, i + 2);
                    HSSFSimpleShape line1 = patriarch.CreateSimpleShape(a1);
                    line1.ShapeType = HSSFSimpleShape.OBJECT_TYPE_LINE;
                    line1.LineStyle = LineStyle.Solid;
                }
                workbook.Write(fileSave);
                fileSave.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        private static DataTable CreateDataTable(string role, string section, string title, DateTime startTime, DateTime endTime)
        {
            try
            {
                role += "-1";
                TimeSpan ts1 = new TimeSpan(startTime.Ticks);
                TimeSpan ts2 = new TimeSpan(endTime.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("" + title + "");
                dt.Columns.Add("1");
                dt.Columns.Add("2");
                dt.Columns.Add("3");
                dt.Columns.Add("4");
                dt.Columns.Add("5");
                dt.Columns.Add("6");
                dt.Columns.Add("7");
                dt.Columns.Add("8");
                dt.Columns.Add("9");
                dt.Columns.Add("10");
                dt.Columns.Add("11");
                dt.Columns.Add("12");
                dt.Columns.Add("13");
                dt.Columns.Add("14");
                dt.Columns.Add("15");
                dt.Columns.Add("16");
                int teladd = 0;
                int telarri = 0;
                int netadd = 0;
                int netarri = 0;
                int othadd = 0;
                int otharri = 0;
                Orders.Actor.GetMonthData(out netadd, out netarri, section, Convert.ToDateTime(startTime.AddMonths(-1).ToString("yyyy-MM-01 00:00:00")), Convert.ToDateTime(startTime.AddDays(-startTime.Day).ToString("yyyy-MM-dd 23:59:56")), (int)CountTypeEnum.Web);
                Orders.Actor.GetMonthData(out teladd, out telarri, section, Convert.ToDateTime(startTime.AddMonths(-1).ToString("yyyy-MM-01 00:00:00")), Convert.ToDateTime(startTime.AddDays(-startTime.Day).ToString("yyyy-MM-dd 23:59:56")), (int)CountTypeEnum.Telephone);
                Orders.Actor.GetMonthData(out othadd, out otharri, section, Convert.ToDateTime(startTime.AddMonths(-1).ToString("yyyy-MM-01 00:00:00")), Convert.ToDateTime(startTime.AddDays(-startTime.Day).ToString("yyyy-MM-dd 23:59:56")), (int)CountTypeEnum.Others);
                for (int i = 0; i <= ts.Days; i++)
                {
                    DataRow dr1 = dt.NewRow();//第一行
                    DataRow dr2 = dt.NewRow();//第二行
                    DataRow dr3 = dt.NewRow();//第三行
                    DataRow dr4 = dt.NewRow();//第四行:上月总数据
                    DataRow dr5 = dt.NewRow();//第五行:上月同期数据
                    dr1[0] = "日期";
                    dr1[1] = startTime.AddDays(i).ToString("MM月dd日");
                    dr1[2] = " ";
                    dr1[3] = " ";
                    dr1[4] = " ";
                    dr1[5] = " ";
                    dr1[6] = " ";
                    dr1[7] = " ";
                    dr1[8] = " ";

                    dr1[9] = startTime.AddDays(i).ToString("MM月") + "总合计";
                    dr1[10] = " ";
                    dr1[11] = " ";
                    dr1[12] = " ";
                    dr1[13] = " ";
                    dr1[14] = " ";
                    dr1[15] = " ";
                    dr1[16] = " ";
                    dt.Rows.Add(dr1);
                    dr2[0] = " 标题\n姓名 ";
                    dr2[1] = "网络";
                    dr2[2] = "";
                    dr2[3] = "电话";
                    dr2[4] = "";
                    dr2[5] = "其他";
                    dr2[6] = "";
                    dr2[7] = "合计";
                    dr2[8] = "";
                    dr2[9] = "网络";
                    dr2[10] = "";
                    dr2[11] = "电话";
                    dr2[12] = "";
                    dr2[13] = "其他";
                    dr2[14] = "";
                    dr2[15] = "合计";
                    dr2[16] = "";
                    dt.Rows.Add(dr2);

                    dr3[0] = " ";
                    dr3[1] = "预约";
                    dr3[2] = "就诊";
                    dr3[3] = "预约";
                    dr3[4] = "就诊";
                    dr3[5] = "预约";
                    dr3[6] = "就诊";
                    dr3[7] = "预约";
                    dr3[8] = "就诊";
                    dr3[9] = "预约";
                    dr3[10] = "就诊";
                    dr3[11] = "预约";
                    dr3[12] = "就诊";
                    dr3[13] = "预约";
                    dr3[14] = "就诊";
                    dr3[15] = "预约";
                    dr3[16] = "就诊";
                    dt.Rows.Add(dr3);
                    dr4[0] = "上月业绩";
                    dr4[1] = "";
                    dr4[2] = " ";
                    dr4[3] = " ";
                    dr4[4] = " ";
                    dr4[5] = " ";
                    dr4[6] = " ";
                    dr4[7] = " ";
                    dr4[8] = " ";

                    dr4[9] = netadd;
                    dr4[10] = netarri;
                    dr4[11] = teladd;
                    dr4[12] = telarri;
                    dr4[13] = othadd;
                    dr4[14] = otharri;
                    dr4[15] = "";
                    dr4[16] = "";
                    dt.Rows.Add(dr4);

                    int teladds = 0;
                    int telarris = 0;
                    int netadds = 0;
                    int netarris = 0;
                    int othadds = 0;
                    int otharris = 0;
                    Orders.Actor.GetMonthData(out netadds, out netarris, section, Convert.ToDateTime(startTime.AddMonths(-1).ToString("yyyy-MM-01 00:00:00")), Convert.ToDateTime(startTime.AddDays(i).AddMonths(-1).ToString("yyyy-MM-dd 23:59:56")), (int)CountTypeEnum.Web);
                    Orders.Actor.GetMonthData(out teladds, out telarris, section, Convert.ToDateTime(startTime.AddMonths(-1).ToString("yyyy-MM-01 00:00:00")), Convert.ToDateTime(startTime.AddDays(i).AddMonths(-1).ToString("yyyy-MM-dd 23:59:56")), (int)CountTypeEnum.Telephone);
                    Orders.Actor.GetMonthData(out othadds, out otharris, section, Convert.ToDateTime(startTime.AddMonths(-1).ToString("yyyy-MM-01 00:00:00")), Convert.ToDateTime(startTime.AddDays(i).AddMonths(-1).ToString("yyyy-MM-dd 23:59:56")), (int)CountTypeEnum.Others);
                    dr5[0] = "上月同步业绩";
                    dr5[1] = " ";
                    dr5[2] = " ";
                    dr5[3] = " ";
                    dr5[4] = " ";
                    dr5[5] = " ";
                    dr5[6] = " ";
                    dr5[7] = " ";
                    dr5[8] = " ";

                    dr5[9] = netadds;
                    dr5[10] = netarris;
                    dr5[11] = teladds;
                    dr5[12] = telarris;
                    dr5[13] = othadds;
                    dr5[14] = otharris;
                    dr5[15] = "";
                    dr5[16] = "";
                    dt.Rows.Add(dr5);

                    DataSet allCountData = Orders.Actor.GetDepartmentData(role, section, Convert.ToDateTime(startTime.AddDays(i).ToString("yyyy-MM-dd 00:00:00.000")), Convert.ToDateTime(startTime.AddDays(i).ToString("yyyy-MM-dd 23:59:56")));
                    DataTable allCountDataTA = allCountData.Tables[0];
                    int oneTaRow = allCountDataTA.Rows.Count;
                    for (int j = 0; j < oneTaRow; j++)
                    {
                        DataRow dr6 = dt.NewRow();
                        for (int k = 0; k < 7; k++)
                        {
                            dr6[k] = (allCountDataTA.Rows[j][k]).ToString();
                        }
                        for (int o = 7; o < 15; o++)
                        {
                            dr6[o] = "";
                        }
                        dt.Rows.Add(dr6);

                    }
                    DataRow dr7 = dt.NewRow();
                    dr7[0] = "合计";
                    dr7[1] = "";
                    dr7[2] = " ";
                    dr7[3] = " ";
                    dr7[4] = " ";
                    dr7[5] = " ";
                    dr7[6] = " ";
                    dr7[7] = " ";
                    dr7[8] = " ";
                    dr7[9] = "";
                    dr7[10] = "";
                    dr7[11] = "";
                    dr7[12] = "";
                    dr7[13] = "";
                    dr7[14] = "";
                    dr7[15] = "";
                    dr7[16] = "";
                    dt.Rows.Add(dr7);
                }

                return dt;
            }
            catch (Exception)
            {

                return null;
            }
        }

        #endregion

        #region 导出疾病预约报表
        /// <summary>
        /// 导出疾病预约报表
        /// </summary>
        public static bool ExportDiseaseOrders(DateTime startTime, DateTime endTime, string section, string title, string savePath)
        {
            DataTable dt = ConvertDiseaseOrdersToDataTable(startTime, endTime, section);
            TimeSpan ts1 = new TimeSpan(startTime.Ticks);
            TimeSpan ts2 = new TimeSpan(endTime.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            HSSFWorkbook workbook = new HSSFWorkbook();
            //创建3个Sheet
            HSSFSheet sheet1 = (HSSFSheet)workbook.CreateSheet("Sheet1");
            HSSFSheet sheet2 = (HSSFSheet)workbook.CreateSheet("Sheet2");
            HSSFSheet sheet3 = (HSSFSheet)workbook.CreateSheet("Sheet3");

            //保存excel
            FileStream fileSave = new FileStream(savePath, FileMode.Create);
            //DocumentSummaryInformation和SummaryInformation等属性知识
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "莆田万邦科技有限公司";
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "" + title + "预约统计数据";
            //赋值给工作表
            workbook.DocumentSummaryInformation = dsi;
            workbook.SummaryInformation = si;

            //创建行
            IRow row1 = sheet1.CreateRow(0);
            //创建单元格
            row1.CreateCell(0).SetCellValue(title);//第一行第一个标题
            ICellStyle style = workbook.CreateCellStyle();
            // sheet1.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");
            //这么用有个前提，那就是第0行还没创建过，否则得这么用： sheet1.GetRow(0).CreateCell(0).SetCellValue("This is a Sample");
            // 如果你要获得某一个已经创建的单元格对象，可以用下面的代码：  sheet1.GetRow(row_index).GetCell(column_index);
            style.Alignment = HorizontalAlignment.CENTER;
            // 设置字体
            IFont font = workbook.CreateFont();
            font.FontHeight = 20 * 20;
            style.SetFont(font);
            row1.GetCell(0).CellStyle = style;
            sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, (ts.Days + 3)));
            /**************日期样式*******************/
            ICellStyle Datestyle = workbook.CreateCellStyle();
            IFont font2 = workbook.CreateFont();
            font2.FontHeight = 15 * 15;
            font2.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index;
            Datestyle.SetFont(font2);
            Datestyle.Alignment = HorizontalAlignment.CENTER;
            Datestyle.BorderTop = BorderStyle.THIN;
            Datestyle.BorderLeft = BorderStyle.THIN;
            Datestyle.BorderBottom = BorderStyle.THIN;
            Datestyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            Datestyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_BLUE.index;
            /**************疾病名称样式*******************/
            ICellStyle DiseaseNamestyle = workbook.CreateCellStyle();
            IFont font3 = workbook.CreateFont();
            font3.FontHeight = 15 * 15;
            DiseaseNamestyle.SetFont(font3);
            DiseaseNamestyle.BorderTop = BorderStyle.THIN;
            DiseaseNamestyle.BorderRight = BorderStyle.THIN;
            DiseaseNamestyle.BorderBottom = BorderStyle.THIN;
            DiseaseNamestyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            DiseaseNamestyle.Alignment = HorizontalAlignment.CENTER;
            DiseaseNamestyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_GREEN.index;
            /**************背景样式*******************/
            ICellStyle Bstyle = workbook.CreateCellStyle();
            Bstyle.BorderTop = BorderStyle.THIN;
            Bstyle.BorderLeft = BorderStyle.THIN;
            Bstyle.BorderBottom = BorderStyle.THIN;
            Bstyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
            Bstyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_YELLOW.index;
            /**************背景样式*******************/
            ICellStyle Cstyle = workbook.CreateCellStyle();
            Cstyle.BorderTop = BorderStyle.THIN;
            Cstyle.BorderLeft = BorderStyle.THIN;
            Cstyle.BorderBottom = BorderStyle.THIN;
            for (int i = 1; i < dt.Rows.Count + 1; i++)
            {
                IRow rows = sheet1.CreateRow(i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (i > 1 && j > 1)
                    {
                        rows.CreateCell(j).SetCellValue(Convert.ToInt32(dt.Rows[i - 1][j]));
                    }
                    else
                    {
                        rows.CreateCell(j).SetCellValue(dt.Rows[i - 1][j].ToString());
                    }
                    rows.GetCell(j).CellStyle = Cstyle;
                }
                if (i >= 2)
                {
                    rows.GetCell(0).CellStyle = DiseaseNamestyle;
                }
            }
            for (int i = 1; i < dt.Rows.Count + 1; i = i + 2)
            {
                sheet1.AddMergedRegion(new CellRangeAddress(i + 1, i + 2, 0, 0));
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    sheet1.GetRow(i).GetCell(j).CellStyle = Bstyle;
                }
            }


            sheet1.CreateFreezePane(2, 2, 2, 2);


            for (int i = dt.Rows.Count + 1; i <= dt.Rows.Count + 2; i++)
            {
                IRow rows2 = sheet1.CreateRow(i);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    rows2.CreateCell(j);
                }
            }
            IDrawing patr = sheet1.CreateDrawingPatriarch();
            IComment comment1 = patr.CreateCellComment(new HSSFClientAnchor(0, 0, 0, 0, 3, dt.Rows.Count - 3, 6, dt.Rows.Count));
            comment1.Visible = true;
            comment1.String = new HSSFRichTextString("如果合计值不准确，需要按住ctrl+shift+enter三键。");
            comment1.Author = "系统提醒：";
            sheet1.GetRow(dt.Rows.Count + 1).GetCell(0).SetCellValue("合计");
            sheet1.GetRow(dt.Rows.Count + 1).GetCell(2).CellComment = comment1;
            sheet1.GetRow(dt.Rows.Count + 1).GetCell(1).SetCellValue("预约");
            sheet1.GetRow(dt.Rows.Count + 2).GetCell(1).SetCellValue("到院");

            //for (int j = 2; j < dt.Columns.Count; j++)
            //{
            //    sheet1.GetRow(dt.Rows.Count + 2).GetCell(0).SetCellValue("合计");
            //}

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                sheet1.GetRow(1).GetCell(j).CellStyle = Datestyle;
                sheet1.GetRow(dt.Rows.Count + 1).GetCell(j).CellStyle = Datestyle;
                sheet1.GetRow(dt.Rows.Count + 2).GetCell(j).CellStyle = Datestyle;
                if (j >= 2)
                {
                    sheet1.GetRow(dt.Rows.Count + 1).GetCell(j).SetCellFormula("sumproduct(mod(row(" + ConvertNumToChar(j + 1) + "3:" + ConvertNumToChar(j + 1) + "" + dt.Rows.Count + "),2)*" + ConvertNumToChar(j + 1) + "3:" + ConvertNumToChar(j + 1) + "" + dt.Rows.Count + ")");
                    sheet1.GetRow(dt.Rows.Count + 2).GetCell(j).SetCellFormula("sumproduct(mod(1-row(" + ConvertNumToChar(j + 1) + "3:" + ConvertNumToChar(j + 1) + "" + dt.Rows.Count + "),2)*" + ConvertNumToChar(j + 1) + "3:" + ConvertNumToChar(j + 1) + "" + dt.Rows.Count + ")");
                }
            }
            workbook.Write(fileSave);
            fileSave.Close();
            return true;
        }

        private static System.Data.DataTable ConvertDiseaseOrdersToDataTable(DateTime startTime, DateTime endTime, string section)
        {
            TimeSpan ts1 = new TimeSpan(startTime.Ticks);
            TimeSpan ts2 = new TimeSpan(endTime.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            System.Data.DataTable dt = new System.Data.DataTable();
            for (int i = 0; i <= ts.Days + 2; i++)
            {
                dt.Columns.Add("");
            }
            DataRow dr = dt.NewRow();
            dr[0] = "";
            dr[1] = "";
            for (int i = 0; i <= ts.Days; i++)
            {
                dr[i + 2] = "" + startTime.AddDays(i).ToString("MM月dd日") + "";
            }
            dt.Rows.Add(dr);
            IList<Disease> diseaseList = Disease.Actor.GetDiseaseBySection(section);
            DataSet addDisease = Orders.Actor.GetDiseaseBySection(section, startTime, endTime, false);
            DataSet arrDisease = Orders.Actor.GetDiseaseBySection(section, startTime, endTime, true);
            DataTable addDts = addDisease.Tables[0];
            DataTable arrDts = arrDisease.Tables[0];
            foreach (Disease dis in diseaseList)
            {
                DataRow dr2 = dt.NewRow();
                DataRow dr1 = dt.NewRow();
                dr2[0] = dis.Name;
                dr1[0] = dis.Name;
                dr2[1] = "预约";
                dr1[1] = "到院";
                int addNum = 0;
                int arrNum = 0;
                for (int i = 0; i <= ts.Days; i++)
                {
                    addNum = 0;
                    arrNum = 0;
                    var a = from row in addDts.AsEnumerable()
                            where (row.Field<int>("DiseaseID") == dis.ID && row.Field<DateTime>("AddTime").ToString("yyyy-MM-dd") == startTime.AddDays(i).ToString("yyyy-MM-dd"))
                            select row.Field<int>("DiseaseID");
                    foreach (var s in a)
                    {
                        addNum++;
                    }
                    var a2 = from row in arrDts.AsEnumerable()
                             where (row.Field<int>("DiseaseID") == dis.ID && row.Field<DateTime>("RecordTime").ToString("yyyy-MM-dd") == startTime.AddDays(i).ToString("yyyy-MM-dd"))
                             select row.Field<int>("DiseaseID");
                    foreach (var s in a2)
                    {
                        arrNum++;
                    }
                    dr2[i + 2] = addNum;
                    dr1[i + 2] = arrNum;
                }
                dt.Rows.Add(dr2);
                dt.Rows.Add(dr1);
            }
            return dt;
        }

        #endregion

        #region 导出医生预约报表

        /// <summary>
        /// 导出医生相关报表
        /// </summary>
        public static bool ExportDoctorOrders(int userID, int hospitalID, DateTime startTime, DateTime endTime, string section, string title, string savePath)
        {
            System.Data.DataTable dt = ConvertDoctorOrdersToDataTable(userID, hospitalID, startTime, endTime, section);
            if (dt != null)
            {
                //try
                //{
                HSSFWorkbook workbook = new HSSFWorkbook();
                //创建3个Sheet
                HSSFSheet sheet1 = (HSSFSheet)workbook.CreateSheet("Sheet1");
                HSSFSheet sheet2 = (HSSFSheet)workbook.CreateSheet("Sheet2");
                HSSFSheet sheet3 = (HSSFSheet)workbook.CreateSheet("Sheet3");

                //保存excel
                FileStream fileSave = new FileStream(savePath, FileMode.Create);
                //DocumentSummaryInformation和SummaryInformation等属性知识
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "莆田万邦科技有限公司";
                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Subject = "" + title + "预约统计数据";
                //赋值给工作表
                workbook.DocumentSummaryInformation = dsi;
                workbook.SummaryInformation = si;

                //创建行
                IRow row1 = sheet1.CreateRow(0);
                //创建单元格
                row1.CreateCell(0).SetCellValue(title);//第一行第一个标题
                ICellStyle style = workbook.CreateCellStyle();
                // sheet1.CreateRow(0).CreateCell(0).SetCellValue("This is a Sample");
                //这么用有个前提，那就是第0行还没创建过，否则得这么用： sheet1.GetRow(0).CreateCell(0).SetCellValue("This is a Sample");
                // 如果你要获得某一个已经创建的单元格对象，可以用下面的代码：  sheet1.GetRow(row_index).GetCell(column_index);
                style.Alignment = HorizontalAlignment.CENTER;
                // 设置字体
                IFont font = workbook.CreateFont();
                font.FontHeight = 20 * 20;
                style.SetFont(font);
                row1.GetCell(0).CellStyle = style;
                sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 3));

                /**************日期样式*******************/
                ICellStyle Datestyle = workbook.CreateCellStyle();
                IFont font2 = workbook.CreateFont();
                font2.FontHeight = 15 * 15;
                font2.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index;
                Datestyle.SetFont(font2);
                Datestyle.Alignment = HorizontalAlignment.CENTER;
                Datestyle.BorderTop = BorderStyle.THIN;
                Datestyle.BorderLeft = BorderStyle.THIN;
                Datestyle.BorderBottom = BorderStyle.THIN;
                Datestyle.FillPattern = FillPatternType.SOLID_FOREGROUND;
                Datestyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_BLUE.index;

                for (int i = 1; i < dt.Rows.Count + 1; i++)
                {
                    IRow rows = sheet1.CreateRow(i);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        rows.CreateCell(j).SetCellValue(dt.Rows[i - 1][j].ToString());
                    }
                }
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sheet1.GetRow(1).GetCell(j).CellStyle = Datestyle;
                }
                workbook.Write(fileSave);
                fileSave.Close();
                return true;
                //}
                //catch (Exception)
                //{
                //    return false;
                //}
            }
            else
            {
                return false;
            }
        }

        private static System.Data.DataTable ConvertDoctorOrdersToDataTable(int userID, int hospitalID, DateTime startTime, DateTime endTime, string section)
        {
            //try
            //{
            IList<Doctor> entities = Doctor.Actor.GetBySections(userID, hospitalID);
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("1");
            dt.Columns.Add("2");
            dt.Columns.Add("3");
            dt.Columns.Add("4");
            DataRow dr1 = dt.NewRow();
            dr1[0] = "医生姓名";
            dr1[1] = "病人名称";
            dr1[2] = "病种分类";
            dr1[3] = "就诊时间";
            dt.Rows.Add(dr1);
            foreach (var entity in entities)
            {
                DataSet dataSet = Orders.Actor.GetOrderByDoctor(entity.ID, startTime, endTime, section);
                DataTable dataTable = dataSet.Tables[0];
                int rowCount = dataTable.Rows.Count;
                for (int i = 0; i < rowCount; i++)
                {
                    string disName = "";
                    Disease dis = Disease.Actor.GetByID(Convert.ToInt32(dataTable.Rows[i][4]));
                    if (dis != null)
                    {
                        disName = dis.Name;
                    }
                    else
                    {
                        disName = "该病种不存在，或者已经删除";
                    }
                    DataRow dr = dt.NewRow();
                    dr[0] = entity.Name + "(" + rowCount + "人)";
                    dr[1] = dataTable.Rows[i][2];
                    dr[2] = disName;
                    dr[3] = dataTable.Rows[i][3];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }

        #endregion

        #endregion

    }
}
