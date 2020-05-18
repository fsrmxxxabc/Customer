using Customer.Until.Chat;
using Newtonsoft.Json.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Customer.Until
{
    class IndexUtil
    {

        private String StartCont { get; set; }

        private Label label;

        private JObject JObjects { get; set; }

        /// <summary>
        /// 开始聊天前头部的消息体
        /// </summary>
        /// <param name="startCont"></param>
        public IndexUtil(String startCont)
        {
            this.StartCont = startCont;
            this.Label = new Label();
        }

        public IndexUtil(Label label)
        {
            this.label = label;
            ContUtil contUtil = new ContUtil(this.label.Content.ToString());
            JObjects = contUtil.ContParam;
        }

        public static void SendData(RichTextBoxUtil richTextBoxUtil)
        {
            string str = richTextBoxUtil.GetRichTextBoxToString;
            string result = HtmlFromXamlConverter.ConvertXamlToHtml(str);
            WebSocketUtil.WebSockets.Send(new ContUtil(result).ContParam.ToString());
            //int width = CommonUtil.GetRichTextBoxWidth(richTextBoxUtil.GetRichTextBoxToString, richTextBoxUtil.GetRichTextBoxCont);

        }

        /// <summary>
        /// 发送消息的重载（发送抖屏）
        /// </summary>
        public void SendData()
        {
            App.Current.Dispatcher.Invoke((Action)(() => { View.Index.Chatingmsg.Children.Add(this.label);WebSocketUtil.WebSockets.Send(JObjects.ToString()); }));
        }

        public JObject Jobject
        {
            get { return JObjects; }
        }

        /// <summary>
        /// 组装消息体结构
        /// </summary>
        private class ContUtil
        {
            public String type = "chatmessage", content;

            public JObject data, mine, to, param;

            public ContUtil(String content)
            {
                this.content = content;
                ContParam = new JObject();
            }

            public JObject ContParam
            {
                get { return param; }
                set
                {
                    param = new JObject();
                    Data = new JObject();
                    param.Add("type", type);
                    param.Add("data", Data);
                }
            }

            public JObject Data
            {
                get { return data; }
                set
                {
                    data = new JObject();
                    Mine = new JObject();
                    To = new JObject();
                    data.Add("mine", Mine);
                    data.Add("to", To);
                }
            }

            /// <summary>
            /// 消息发送者信息组装
            /// </summary>
            public JObject Mine
            {
                get { return mine; }
                set
                {
                    mine = value;
                    mine.Add("avatar", "https://video.yestar.com/chat_desktop_kf_icon.png");
                    mine.Add("username", "admin");
                    mine.Add("id", "1");
                    mine.Add("content", content);
                }
            }

            /// <summary>
            /// 消息接收者信息组装
            /// </summary>
            public JObject To
            {
                get { return to; }
                set
                {
                    to = value;
                    to.Add("type", "customer_service");
                }
            }
        }

        /// <summary>
        /// 开始聊天的组装体
        /// </summary>
        public Label Label
        {
            get { return label; }
            set
            {
                try
                {
                    label = value;
                    label.HorizontalAlignment = HorizontalAlignment.Center;
                    label.Margin = new Thickness(0, 10, 0, 5);
                    label.Background = Brushes.White;
                    label.Width = 240;
                    label.HorizontalContentAlignment.Equals("Center");
                    label.Foreground = Brushes.LightGray;
                    label.Content = StartCont;
                }
                catch (ArgumentNullException)
                {

                }
            }
        }
    }
}
