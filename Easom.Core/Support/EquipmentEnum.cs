using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Easom.Core.Support
{
    /// <summary>
    /// 设备来源
    /// </summary>
    public enum EquipmentEnum
    {
        [Description("Windows")]
        Windows = 0,
        [Description("iPhone")]
        iPhone = 1,
        [Description("iPad")]
        iPad = 2,
        [Description("Android")]
        Android = 3,
        [Description("Uc浏览器")]
        UC = 4,
        [Description("其他")]
        Other = 5
    }
}
