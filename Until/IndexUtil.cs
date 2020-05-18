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

        private CustInfo CustInfos { get; set; }

        private Label label;

        private DockPanel dockPanel;

        private DockPanel UserDockPane;

        private JObject JObjects { get; set; }

        public IndexUtil() { }

        public IndexUtil(String startCont, CustInfo custInfo)
        {
            this.StartCont = startCont;
            this.CustInfos = custInfo;
            this.Label = new Label();
            this.DockPanel = new DockPanel();
        }

        /// <summary>
        /// 客服发送消息方法体
        /// </summary>
        /// <param name="custInfo"></param>
        public IndexUtil(CustInfo custInfo)
        {
            if (custInfo != null) 
            {
                this.CustInfos = custInfo;
                this.DockPanelUser = new DockPanel();
                ContUtil contUtil = new ContUtil(custInfo.ContLabel.Content.ToString());
                JObjects = contUtil.ContParam;
            }
            //Jobject = new JObject();
        }

        public IndexUtil(CustInfo custInfo,string content)
        {
            if(custInfo != null )
            {
                this.CustInfos = custInfo;
                this.DockPaneUserNew = new DockPanel();
                ContUtil contUtil = new ContUtil(content);
                JObjects = contUtil.ContParam;
            }
        }

        /// <summary>
        /// 访客发送消息方法体
        /// </summary>
        /// <param name="custInfo"></param>
        /// <param name="type"></param>
        public IndexUtil(CustInfo custInfo,DockPanel dockPanel)
        {
            this.CustInfos = custInfo;
            this.DockPanel = dockPanel;
        }

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

        /// <summary>
        /// 客服发送消息方法体，此方法体可以直接发送消息给访客
        /// </summary>
        /// <param name="cust"></param>
        public static void SendData(CustParam cust)
        {
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                IndexUtil indexUtil = new IndexUtil(new CustInfo(cust),cust.HtmlCont);
                View.Index.Chatingmsg.Children.Add(indexUtil.DockPaneUserNew);
                WebSocketUtil.WebSockets.Send(indexUtil.Jobject.ToString());
                View.Index.ChatingContMsg.Document.Blocks.Clear();
            }));
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

        /// <summary>
        /// 初始化在线用户列表
        /// </summary>
        public void LoadUserList()
        {
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                UserListStyle = new DockPanel();
            }));
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

        /// <summary>
        /// 访客发送的消息体
        /// </summary>
        public DockPanel DockPanel
        {
            get { return DockPaneStyle; }
            set
            {
                /*DockPaneStyle = value;
                // CustInfo custInfo = new CustInfo("https://video.yestar.com/chat_desktop_customer_avatr_img.png", "18:13:30", "pack://application:,,,/Resources/chat_desktop_triangle_icon.png", "成功了吗？");
                DockPaneStyle.Children.Add(CustInfos.CustImage);
                DockPaneStyle.Children.Add(CustInfos.TimeLabel);
                DockPaneStyle.Children.Add(CustInfos.IconImage);
                DockPaneStyle.Children.Add(CustInfos.ContLabel);*/
                DockPaneStyle = value;
                this.CustInfos.CustUis.DockPanelt.Children.Add(this.CustInfos.CustUis.UserContent);
                this.CustInfos.CustUis.DockPanelt.Children.Add(this.CustInfos.CustUis.ArrowIcon);
                this.CustInfos.CustUis.StackPanelf.Children.Add(this.CustInfos.CustUis.UserTime);
                this.CustInfos.CustUis.StackPanelf.Children.Add(this.CustInfos.CustUis.DockPanelt);
                DockPaneStyle.Children.Add(this.CustInfos.CustUis.StackPanelf);
                DockPaneStyle.Children.Add(this.CustInfos.CustUis.UserImage);
            }
        }

        /// <summary>
        /// 客服回复的消息体
        /// </summary>
        public DockPanel DockPanelUser
        {
            get { return DockPanelUserStyle; }
            set
            {
                DockPanelUserStyle = value;
                UserInfo userInfo = new UserInfo(CustInfos);
                DockPanelUserStyle.Children.Add(userInfo.ContLabel);
                DockPanelUserStyle.Children.Add(userInfo.IconImage);
                DockPanelUserStyle.Children.Add(userInfo.TimeLabe);
                DockPanelUserStyle.Children.Add(userInfo.CustImage);
            }
        }

        public DockPanel DockPaneUserNew
        {
            get { return DockPanelUserStyle; }
            set
            {
                DockPanelUserStyle = value;
                UserInfo userInfo = new UserInfo(this.CustInfos);
                userInfo.DockPanelT.Children.Add(userInfo.UserContent);
                userInfo.DockPanelT.Children.Add(userInfo.ArrowIcon);
                userInfo.StackPanelf.Children.Add(userInfo.UserTime);
                userInfo.StackPanelf.Children.Add(userInfo.DockPanelT);
                DockPanelUserStyle.Children.Add(userInfo.StackPanelf);
                DockPanelUserStyle.Children.Add(userInfo.UserImage);
                /*this.CustInfos.DockPanelt.Children.Add(this.CustInfos.UserContent);
                this.CustInfos.DockPanelt.Children.Add(this.CustInfos.ArrowIcon);
                this.CustInfos.StackPanelf.Children.Add(this.CustInfos.UserTime);
                this.CustInfos.StackPanelf.Children.Add(this.CustInfos.DockPanelt);
                DockPanelUserStyle.Children.Add(this.CustInfos.StackPanelf);
                DockPanelUserStyle.Children.Add(this.CustInfos.UserImage);*/ 
            }
        }

        private DockPanel DockPanelUserStyle
        {
            get { return dockPanel; }
            set
            {
                dockPanel = value;
                dockPanel.HorizontalAlignment = HorizontalAlignment.Right;
                DockPanel.Margin = new Thickness(0, 10, 8, 10);
            }
        }

        private DockPanel DockPaneStyle
        {
            get { return dockPanel; }
            set
            {
                dockPanel = value;
                dockPanel.HorizontalAlignment = HorizontalAlignment.Left;
                dockPanel.Margin = new Thickness(5, 10, 0, 0);
            }
        }

        /// <summary>
        /// 客服发送的消息体
        /// </summary>
        private class UserInfo
        {
            private CustInfo CustInfot { get; set; }

            /*public UserInfo(CustInfo cust)
            {
                CustInfot = cust;
                IconImage = new Image();
                TimeLabe = ContLabel = new Label();
            }*/

            public UserInfo(CustInfo custInfo)
            {
                CustInfot = custInfo;
                UserTime = new Label();
                DockPanelT = new DockPanel();
                ArrowIcon = new Image();
                UserContent = new RichTextBox();
            }

            public StackPanel StackPanelf
            {
                get { return CustInfot.CustUis.StackPanelf; }
            }

            public Image UserImage
            {
                get { return CustInfot.CustUis.UserImage; }
            }

            public Label UserTime
            {
                get { return CustInfot.CustUis.UserTime; }
                set
                {
                    CustInfot.CustUis.UserTime.HorizontalAlignment = HorizontalAlignment.Right;
                }
            }

            public DockPanel DockPanelT
            {
                get { return CustInfot.CustUis.DockPanelt; }
                set
                {
                    CustInfot.CustUis.DockPanelt.HorizontalAlignment = HorizontalAlignment.Right;
                    CustInfot.CustUis.DockPanelt.Margin = new Thickness(0, 0, 0, 0);
                }
            }

            public Image ArrowIcon
            {
                get { return CustInfot.CustUis.ArrowIcon; }
                set
                {
                    CustInfot.CustUis.ArrowIcon.Margin = new Thickness(0,-5,-5,0);
                    CustInfot.CustUis.ArrowIcon.HorizontalAlignment = HorizontalAlignment.Right;
                }
            }

            public RichTextBox UserContent
            {
                get { return CustInfot.CustUis.UserContent; }
                set
                {
                    CustInfot.CustUis.UserContent.Margin = new Thickness(0,0,-20,0);
                    CustInfot.CustUis.UserContent.HorizontalContentAlignment = HorizontalAlignment.Right;
                }
            }


            /// <summary>
            /// 用户头像消息体
            /// </summary>
            public Image CustImage
            {
                get { return CustInfot.CustImage; }
            }

            /// <summary>
            /// 消息体箭头图标
            /// </summary>
            public Image IconImage
            {
                get { return CustInfot.IconImage; }
                set
                {
                    CustInfot.IconImage.HorizontalAlignment = HorizontalAlignment.Right;
                    CustInfot.IconImage.Margin = new Thickness(17, 5, 0, 0);
                    CustInfot.IconImage.Width = 12;
                }
            }

            /// <summary>
            /// 消息体时间组装
            /// </summary>
            public Label TimeLabe
            {
                get { return CustInfot.TimeLabel; }
                set
                {
                    CustInfot.TimeLabel.HorizontalAlignment = HorizontalAlignment.Center;
                    CustInfot.TimeLabel.VerticalAlignment = VerticalAlignment.Center;
                    CustInfot.TimeLabel.Margin = new Thickness(0, 0, 0, 30);
                    CustInfot.TimeLabel.Height = 25;
                }
            }

            /// <summary>
            /// 客服回复的内容组装
            /// </summary>
            public Label ContLabel
            {
                get { return CustInfot.ContLabel; }
                set
                {
                    CustInfot.ContLabel = value;
                    CustInfot.ContLabel.Margin = new Thickness(0, 0, -55, 0);
                    CustInfot.ContLabel.HorizontalAlignment = HorizontalAlignment.Right;
                }
            }
        }

        private class UserList
        {

            public UserListUtil UserListUtilx { get; set; }

            public class UserListUtil
            {
                public Image UserIcon { set; get; }
                public Label UserName { get; set; }
                public Label UserCont { get; set; }
                public Label UserTime { get; set; }
            }

            public UserList()
            {
                UserListUtilx = new UserListUtil()
                {
                    UserCont = new Label(),
                    UserTime = new Label(),
                    UserName = new Label(),
                    UserIcon = new Image()

                };
            }

            public Label UserName
            {
                get { return UserListUtilx.UserName; }
                set
                {
                    UserListUtilx.UserName.HorizontalAlignment = HorizontalAlignment.Stretch;
                    UserListUtilx.UserName.VerticalAlignment = VerticalAlignment.Top;
                    UserListUtilx.UserName.FontSize = 12;
                    UserListUtilx.UserName.Foreground = CommonUtil.GetColorBrush(79, 79, 79);
                    UserListUtilx.UserName.Width = 71;
                    UserListUtilx.UserName.Margin = new Thickness(0, 0, -70, 0);
                }
            }

            public Label UserTime
            {
                get { return UserListUtilx.UserTime; }
                set
                {
                    UserListUtilx.UserTime.FontSize = 10;
                    UserListUtilx.UserTime.Width = 33;
                    UserListUtilx.UserTime.Foreground = CommonUtil.GetColorBrush(147, 147, 147);
                }
            }

            public Label UserCont
            {
                get { return UserListUtilx.UserCont; }
                set
                {
                    UserListUtilx.UserCont.VerticalAlignment = VerticalAlignment.Bottom;
                    UserListUtilx.UserCont.HorizontalAlignment = HorizontalAlignment.Center;
                    UserListUtilx.UserCont.FontSize = 10;
                    UserListUtilx.UserCont.Background = CommonUtil.GetColorBrush(0, 184, 184, 184);
                    UserListUtilx.UserCont.Foreground = CommonUtil.GetColorBrush(1, 184, 184, 184);
                    UserListUtilx.UserCont.Margin = new Thickness(0, 0, -17, 0);
                    UserListUtilx.UserCont.Width = 118;
                }
            }

            public Image UserIcon
            {
                get { return UserListUtilx.UserIcon; }
                set { }
            }
        }

        public DockPanel UserListStyle
        {
            get { return UserDockPane; }
            set
            {
                UserDockPane = value;
                UserDockPane.Height = 35;
            }

        }
    }
}
