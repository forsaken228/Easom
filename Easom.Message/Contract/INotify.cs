// 创建日期：2012-08-23
// 作者：heise321890234@163.com

using System.Collections.Generic;

namespace Easom.Message.Contracts 
{
    /// <summary>
    ///INotify 契约接口 
    /// </summary>
    public interface INotify
    {
        int HttpNotifyBatchSend(string Mobile, string Content, string Cell, string SendTime);

        int HttpOverage();
    }
}