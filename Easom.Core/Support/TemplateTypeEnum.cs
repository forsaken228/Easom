using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Easom.Core.Support
{
    [Serializable]
    public enum TemplateTypeEnum  
    {
      
        /// <summary>
        /// 个人模板
        /// </summary>
         [Description("个人模板")]
        person = 0 ,

         /// <summary>
         /// 系统模板
         /// </summary>
         [Description("新增预约默认模板")]
        today = 1,

         [Description("明日预约默认模板")]
         tomarrow = 2 
     
    }
}
