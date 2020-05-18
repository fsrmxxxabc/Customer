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
        public string Url { set; get; }

        public string Time { set; get; }

        public string UrlIcon { set; get; }

        public string Content { set; get; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserImage { get; set; }

        /// <summary>
        /// send time log
        /// </summary>
        public string UserTime { get; set; }

        /// <summary>
        /// send content log
        /// </summary>
        public string MsgCont { get; set; }

        /// <summary>
        /// 自适应RichTextBox的宽度
        /// </summary>
        public int RichTextBoxWidth { get; set; }

        public string HtmlCont { get; set; }

    }
}
