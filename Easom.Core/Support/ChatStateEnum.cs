using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Easom.Core.Support
{
    /// <summary>
    /// 对话 类型
    /// </summary>
    [Serializable]
    public enum ChatStateEnum
    {
        [Description("预约对话")]
        YuYue = 0,
        [Description("有效对话")]
        YouXiao = 1,
        [Description("一般对话")]
        YiBan = 2,
        [Description("无对话")]
        None = 3,
    }
}
