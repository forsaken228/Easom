using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easom.Core
{
    /// <summary>
    /// 权限共同KEY
    /// </summary>
    public class AuthorityConst
    {
        /// <summary>
        /// 首页
        /// </summary>
        public readonly static string H_HOME = "H_HOME";

        /// <summary>
        /// 首页统计
        /// </summary>
        public readonly static string H_HOME_TONGJI = "H_HOME_TONGJI";

        /// <summary>
        /// 首页回访
        /// </summary>
        public readonly static string H_HOME_HUIFANG = "H_HOME_HUIFANG";

        /// <summary>
        /// 病人预约管理
        /// </summary>
        public readonly static string A_MENU = "A_MENU";

        /// <summary>
        /// 预约管理
        /// </summary>
        public readonly static string A_YYGL = "A_YYGL";
        /// <summary>
        /// 个人资源库
        /// </summary>
        public readonly static string A_PERSON = "A_PERSON";
        /// <summary>
        /// 自助挂号
        /// </summary>
        public readonly static string A_GUOHAO = "A_GUOHAO";
        /// <summary>
        /// 预约信息添加
        /// </summary>
        public readonly static string AY_ADD = "AY_ADD";

        /// <summary>
        /// 预约信息删除
        /// </summary>
        public readonly static string AY_DELETE = "AY_DELETE";

        /// <summary>
        /// 回访
        /// </summary>
        public readonly static string AY_CALLBACK = "AY_CALLBACK";

        /// <summary>
        /// 状态
        /// </summary>
        public readonly static string AY_STATE = "AY_STATE";

        /// <summary>
        /// 编辑预约数据
        /// </summary>
        public readonly static string AY_EDIT = "AY_EDIT";

        /// <summary>
        /// 编辑其他人数据
        /// </summary>
        public readonly static string AY_EDITOTHER = "AY_EDITOTHER";

        /// <summary>
        /// 预约个人信息展示
        /// </summary>
        public readonly static string KEY_ORDER_PERSON_MESSAG = "KEY_ORDER_PERSON_MESSAG";

        /// <summary>
        /// 个人资源库添加
        /// </summary>
        public readonly static string AP_ADD = "AP_ADD";

        /// <summary>
        /// 个人资源库删除
        /// </summary>
        public readonly static string AP_DELETE = "AP_DELETE";
        /// <summary>
        /// 个人资源库修改
        /// </summary>
        public readonly static string AP_EDIT = "AP_EDIT";
        /// <summary>
        /// 查看其他人数据
        /// </summary>
        public readonly static string AP_SELECTORTER = "AP_SELECTORTER";

        /// <summary>
        /// 自助挂号改已读
        /// </summary>
        public readonly static string AG_READED = "AG_READED";


        /// <summary>
        /// 自助挂号改已读
        /// </summary>
        public readonly static string AG_DELETE = "AG_DELETE";



        /// <summary>
        /// 访客数据统计
        /// </summary>
        public readonly static string B_TONGJI = "B_TONGJI";

        /// <summary>
        /// 部门明细列表
        /// </summary>
        public readonly static string B_MINGXI = "B_MINGXI";

        /// <summary>
        /// 周月统计报表
        /// </summary>
        public readonly static string B_ZHOUYUE = "B_ZHOUYUE";

        /// <summary>
        /// 到院对比报表
        /// </summary>
        public readonly static string B_DAOYUAN = "B_DAOYUAN";

        /// <summary>
        /// 病种统计报表
        /// </summary>
        public readonly static string B_DISEASE = "B_DISEASE";

        /// <summary>
        /// 地区来源报表
        /// </summary>
        public readonly static string B_AREA = "B_AREA";

        /// <summary>
        /// 媒体来源报表
        /// </summary>
        public readonly static string B_MEDIA = "B_MEDIA";

        /// <summary>
        /// 到院信息汇总
        /// </summary>
        public readonly static string B_ALL = "B_ALL";

        /// <summary>
        /// 导出报表
        /// </summary>
        public readonly static string B_EXECL = "B_EXECL";


        /// <summary>
        /// 医院信息设置
        /// </summary>
        public readonly static string C_HOSIPITAL = "C_HOSIPITAL";


        /// <summary>
        /// 账户设置
        /// </summary>
        public readonly static string C_JJ_ACCOUNT = "C_JJ_ACCOUNT";
        /// <summary>
        /// 账户添加
        /// </summary>
        public readonly static string CJJ_A_ADD = "CJJ_A_ADD";
        /// <summary>
        /// 账户删除
        /// </summary>
        public readonly static string CJJ_A_DELETE = "CJJ_A_DELETE";
        /// <summary>
        /// 账户修改
        /// </summary>
        public readonly static string CJJ_A_EDIT = "CJJ_A_EDIT";

        /// <summary>
        /// 密码保护
        /// </summary>
        public readonly static string MiMaBaoHu = "MiMaBaoHu";


        /// <summary>
        /// 医生设置
        /// </summary>
        public readonly static string C_DOCTOR = "C_DOCTOR";
        /// <summary>
        /// 医生添加
        /// </summary>
        public readonly static string CD_ADD = "CD_ADD";
        /// <summary>
        /// 医生删除
        /// </summary>
        public readonly static string CD_DELETE = "CD_DELETE";
        /// <summary>
        /// 医生修改
        /// </summary>
        public readonly static string CD_EDIT = "CD_EDIT";

        /// <summary>
        /// 疾病设置
        /// </summary>
        public readonly static string C_DIEASE = "C_DIEASE";
        /// <summary>
        /// 疾病添加
        /// </summary>
        public readonly static string CC_ADD = "CC_ADD";
        /// <summary>
        /// 疾病删除
        /// </summary>
        public readonly static string CC_DELETE = "CC_DELETE";
        /// <summary>
        /// 疾病修改
        /// </summary>
        public readonly static string CC_EDIT = "CC_EDIT";

        /// <summary>
        /// 医院网站设置
        /// </summary>
        public readonly static string C_SITE = "C_SITE";
        /// <summary>
        /// 医院网站添加
        /// </summary>
        public readonly static string CS_ADD = "CS_ADD";

        /// <summary>
        ///  医院网站删除
        /// </summary>
        public readonly static string CS_DELETE = "CS_DELETE";

        /// <summary>
        /// 医院网站修改
        /// </summary>
        public readonly static string CS_EDIT = "CS_EDIT";

        /// <summary>
        /// 医院科室设置
        /// </summary>
        public readonly static string C_SECTION = "C_SECTION";
        /// <summary>
        /// 医院科室添加
        /// </summary>
        public readonly static string CY_ADD = "CY_ADD";
        /// <summary>
        /// 医院科室删除
        /// </summary>
        public readonly static string CY_DELETE = "CY_DELETE";

        /// <summary>
        /// 医院科室修改
        /// </summary>
        public readonly static string CY_EDIT = "CY_EDIT";




        /// <summary>
        /// 全局管理设置
        /// </summary>
        public readonly static string D_GLOBAL = "D_GLOBAL";

        /// <summary>
        /// 用户设置
        /// </summary>
        public readonly static string D_USER = "D_USER";

        /// <summary>
        /// 用户添加
        /// </summary>
        public readonly static string DU_ADD = "DU_ADD";

        /// <summary>
        /// 用户删除
        /// </summary>
        public readonly static string DU_DELETE = "DU_DELETE";

        /// <summary>
        /// 用户医院科室选择
        /// </summary>
        public readonly static string DU_SETHOSIPITAL = "DU_SETHOSIPITAL";
        /// <summary>
        /// 用户修改
        /// </summary>
        public readonly static string DU_EDIT = "DU_EDIT";
        /// <summary>
        /// 角色设置
        /// </summary>
        public readonly static string D_ROLE = "D_ROLE";
        /// <summary>
        ///  角色添加
        /// </summary>
        public readonly static string DR_ADD = "DR_ADD";

        /// <summary>
        ///  角色删除
        /// </summary>
        public readonly static string DR_DELETE = "DR_DELETE";
        /// <summary>
        /// 角色修改
        /// </summary>
        public readonly static string DR_EDIT = "DR_EDIT";
        /// <summary>
        ///  为角色设置权限
        /// </summary>
        public readonly static string DR_SETAUTHOR = "DR_SETAUTHOR";
        /// <summary>
        /// 为角色设置可以管理的其他角色
        /// </summary>
        public readonly static string DR_SETTOROLE = "DR_SETTOROLE";




        /// <summary>
        /// 权限设置
        /// </summary>
        public readonly static string D_AUTHRITY = "D_AUTHRITY";

        /// <summary>
        ///  权限添加
        /// </summary>
        public readonly static string DA_ADD = "DA_ADD";

        /// <summary>
        /// 权限删除
        /// </summary>
        public readonly static string DA_DELETE = "DA_DELETE";
        /// <summary>
        /// 权限修改
        /// </summary>
        public readonly static string DA_EDIT = "DA_EDIT";

        /// <summary>
        /// 系统服务
        /// </summary>
        public readonly static string D_SERVE = "D_SERVE";



        /// <summary>
        /// 医院设置
        /// </summary>
        public readonly static string D_HOSIPITAL = "D_HOSIPITAL";

        /// <summary>
        ///  医院添加
        /// </summary>
        public readonly static string DH_ADD = "DH_ADD";

        /// <summary>
        /// 医院删除
        /// </summary>
        public readonly static string DH_DELETE = "DH_DELETE";
        /// <summary>
        /// 医院修改
        /// </summary>
        public readonly static string DH_EDIT = "DH_EDIT";





        /// <summary>
        /// 媒体来源设置
        /// </summary>
        public readonly static string D_MEDIA = "D_MEDIA";

        /// <summary>
        ///  媒体添加
        /// </summary>
        public readonly static string DM_ADD = "DM_ADD";

        /// <summary>
        /// 媒体删除
        /// </summary>
        public readonly static string DM_DELETE = "DM_DELETE";
        /// <summary>
        /// 媒体修改
        /// </summary>
        public readonly static string DM_EDIT = "DM_EDIT";




        /// <summary>
        /// 短信中心
        /// </summary>
        public readonly static string E_MESSAGE = "E_MESSAGE";
        /// <summary>
        /// 本日新增
        /// </summary>
        public readonly static string E_ADDMESSAGE = "E_ADDMESSAGE";

        /// <summary>
        /// 明日预到
        /// </summary>
        public readonly static string E_TOMORRW = "E_TOMORRW";

        /// <summary>
        /// 短信模板
        /// </summary>
        public readonly static string E_TEMPLATE = "E_TEMPLATE";

        /// <summary>
        /// 短信模板添加
        /// </summary>
        public readonly static string EE_ADD = "EE_ADD";

        /// <summary>
        /// 短信模板删除
        /// </summary>
        public readonly static string EE_DELETE = "EE_DELETE";

        /// <summary>
        /// 短信模板编辑
        /// </summary>
        public readonly static string EE_EDIT = "EE_EDIT";

        /// <summary>
        /// 短信发送日志
        /// </summary>
        public readonly static string E_MESSAGELOG = "E_MESSAGELOG";

        /// <summary>
        /// 短信发送
        /// </summary>
        public readonly static string E_SEND = "E_SEND";

        /// <summary>
        /// 消息通知
        /// </summary>
        public readonly static string D_NOTIF = "D_NOTIF";


        /// <summary>
        /// 统计
        /// </summary>
        public readonly static string KEY_TONGJI = "KEY_TONGJI";



        /// <summary>
        /// 对话统计
        /// </summary>
        public readonly static string KEY_SWT_REPORT_TONGJI = "KEY_SWT_REPORT_TONGJI";

        /// <summary>
        /// 网络统计
        /// </summary>
        public readonly static string KEY_TONGJI_WEB = "KEY_TONGJI_WEB";

        /// <summary>
        /// 电话统计
        /// </summary>
        public readonly static string KEY_TONGJI_TEL = "KEY_TONGJI_TEL";

        /// <summary>
        /// 其他统计
        /// </summary>
        public readonly static string KEY_TONGJI_OTHER = "KEY_TONGJI_OTHER";

        /// <summary>
        /// 竞价账户管理
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_ACCOUNT = "KEY_TONGJI_JJ_ACCOUNT";


        /// <summary>
        /// 竞价账户管理添加
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_ACCOUNT_ADD = "KEY_TONGJI_JJ_ACCOUNT_ADD";

        /// <summary>
        /// -竞价账户管理编辑
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_ACCOUNT_EDIT = "KEY_TONGJI_JJ_ACCOUNT_EDIT";

        /// <summary>
        /// -竞价账户管理删除
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_ACCOUNT_DELETE = "KEY_TONGJI_JJ_ACCOUNT_DELETE";

        /// <summary>
        /// 商务通对话分析
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_SWTCHAT = "KEY_TONGJI_JJ_SWTCHAT";


        /// <summary>
        /// 后台词库管理
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_KEYWORDS = "KEY_TONGJI_JJ_KEYWORDS";

        /// <summary>
        /// 后台词库上传
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_KEYWORDS_UPLOAD = "KEY_TONGJI_JJ_KEYWORDS_UPLOAD";

        /// <summary>
        /// 后台词库删除
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_KEYWORDS_DELETE = "KEY_TONGJI_JJ_KEYWORDS_DELETE";

        /// <summary>
        /// 商务通对话数据管理
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_SWTCHAT_MANAGER = "KEY_TONGJI_JJ_SWTCHAT_MANAGER";

        /// <summary>
        /// —商务通对话上传
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_SWTCHAT_MANAGER_UPLOAD = "KEY_TONGJI_JJ_SWTCHAT_MANAGER_UPLOAD";

        /// <summary>
        /// 百度小时消费管理
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_BAIDU_HURO1 = "KEY_TONGJI_JJ_BAIDU_HURO";

        /// <summary>
        /// 百度小时消费管理
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_BAIDU_HURO_MANAGER = "KEY_TONGJI_JJ_BAIDU_HURO_MANAGER";

        /// <summary>
        /// 百度小时报表上传
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_BAIDU_HOUR_UPLOAD = "KEY_TONGJI_JJ_BAIDU_HOUR_UPLOAD";


        /// <summary>
        /// 百度小时报表删除
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_BAIDU_HOUR_DELETE = "KEY_TONGJI_JJ_BAIDU_HOUR_DELETE";

        /// <summary>
        /// 百度天消费报表上传
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_BAIDU_DAY_UPLOAD = "KEY_TONGJI_JJ_BAIDU_DAY_UPLOAD";

        /// <summary>
        /// 百度天消费报表
        /// </summary>
        public readonly static string KEY_TONGJI_JJ_BAIDU_DAY = "KEY_TONGJI_JJ_BAIDU_DAY";

        /// <summary>
        /// 操作日志
        /// </summary>
        public readonly static string KEY_OPERATION_LOG = "KEY_OPERATION_LOG";
    }
}
