using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easom.Core.Support
{
    public enum SendState
    {
        //手机发送成功0
        MobileSuccess,
        //邮件发送成功1
        EmailSuccess,
        //都发送成功2
        BothSuccess,
        //手机发送失败3
        MobileFail,
        //邮件发送失败4
        EmailFail,
        //全部失败5
        BothFail
    }
}
