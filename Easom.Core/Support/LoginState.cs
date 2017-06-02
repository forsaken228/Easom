using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easom.Core
{
    /// <summary>
    /// 管理员登录状态
    /// </summary>
    [Serializable]
    public enum LoginState
    {

        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError = 1,

        /// <summary>
        /// 用户名错误
        /// </summary>
        UserNameError = 2,

        /// <summary>
        /// 登录成功
        /// </summary>
        Success = 3,

        /// <summary>
        /// 登录失败
        /// </summary>
        Failed = 4

    }
}
