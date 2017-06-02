using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Easom.Core.Support
{
    [Serializable]
    public enum OrderStateEnum
    {
        [Description("预约信息")]
        OrdersData = 0,
        [Description("客户资源")]
        MySelfOrdersData = 1
    }
}
