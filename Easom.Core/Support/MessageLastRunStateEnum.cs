using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Easom.Core
{
    /// <summary>
    /// 管理员登录状态
    /// </summary>
    [Serializable]
    public enum MessageLastRunStateEnum
    {

        [Description("失败")]
        Error = -1,
        [Description("成功")]
        Success = 0,
        [Description("等待")]
        Waiting = 1

    }
}
