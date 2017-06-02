using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Easom.Core.Support
{
    /// <summary>
    /// 商务通对话分析表种类
    /// </summary>
    [Serializable]
    public enum SwtTableSortEnum
    {
        [Description("着陆页")]
        Zheluye = 0,
        [Description("病种")]
        Disease = 1,
        [Description("设备")]
        Equipment = 2,
        [Description("来源")]
        FromUrl = 3,
        [Description("关键词")]
        Keywords = 4,
        [Description("百度计划消费分析")]
        BaiduPlan = 5,
        [Description("百度关键词消费分析")]
        BaiduKeywords = 6

        /*7聊天页面,
         * 8Reamrk1,
         * 9Reamrk2,
         * 10Reamrk3*/
    }
}
