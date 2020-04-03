using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Until
{
    /// <summary>
    /// 消息体所需参数
    /// </summary>
    class CustParam
    {
        public String Url { set; get; }

        public String Time { set; get; }

        public String UrlIcon { set; get; }

        public String Content { set; get; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public String UserImage { get; set; }

        /// <summary>
        /// send time log
        /// </summary>
        public String UserTime { get; set; }

        /// <summary>
        /// send content log
        /// </summary>
        public String MsgCont { get; set; }

        /// <summary>
        /// 自适应RichTextBox的宽度
        /// </summary>
        public int RichTextBoxWidth { get; set; }
    }
}
