using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Customer.Until.Chat
{
    /// <summary>
    /// 访客发送的消息体
    /// </summary>
    class CustInfo
    {
        private CustParam CustParams { get; }

        public CustUi CustUis { get; }

        public CustInfo(CustParam custParam)
        {
            this.CustParams = custParam;

            CustUis = new CustUi()
            {
                UserImage = GetUserImage,
                StackPanelf = GetStackPanelf,
                UserTime = GetUserTime,
                DockPanelt = GetDockPanelt,
                ArrowIcon = GetArrowIcon,
                UserContent = GetUserContent
            };
        }

        public Image GetUserImage
        {
            get
            {
                return new Image()
                {
                    Width = 41,
                    Source = new BitmapImage(new Uri(CustParams.UserImage)),
                    VerticalAlignment = VerticalAlignment.Top
                };
            }
           
        }

        public static StackPanel GetStackPanelf
        {
            get
            {
                return new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Top
                };
            }
        }

        public Label GetUserTime
        {
            get
            {
                return new Label()
                {
                    Content = CustParams.UserTime,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Foreground = CommonUtil.GetColorBrush(102, 102, 102),
                    FontSize = 14,
                    VerticalContentAlignment = VerticalAlignment.Top
                };
            }
        }

        public static DockPanel GetDockPanelt
        {
            get
            {
                return new DockPanel()
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    Margin = new Thickness(-5, 0, 0, 0)
                };
            }
        }

        public static Image GetArrowIcon
        {
            get
            {
                return new Image()
                {
                    Width = 32,
                    Margin = new Thickness(0, -7, 0, 0),
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/chat_desktop_triangle_icon.png")),
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
            }
        }

        public RichTextBox GetUserContent
        {
            get
            {
                DockPanel dock = new DockPanel();
                RichTextBox richTextBox =  new RichTextBox()
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    AllowDrop = true,
                    Padding = new Thickness(2, 2, 0, 2),
                    Margin = new Thickness(-18, 0, 0, 0),
                    Background = Brushes.White,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    FontSize = 14,
                    Foreground = CommonUtil.GetColorBrush(43, 43, 43),
                    IsReadOnly = true,
                    Width = CustParams.RichTextBoxWidth,
                    Template = View.Index.MsgRichTextBoxTemps.Template
                };
                new RichTextBoxUtil(richTextBox).AddRichTextBoxCont = CustParams.MsgCont;
                //new RichTextBoxUtil(CustUis.UserContent).AddRichTextBoxFromRtf(CustParams.MsgCont);
                return richTextBox;
            }
        }

        /// <summary>
        /// 访客头像组装
        /// </summary>
        public Image CustImage
        {
            get { return CustUis.CustImage; }
            set
            {
                CustUis.CustImage.Source = new BitmapImage(new Uri(CustParams.Url));
                CustUis.CustImage.Width = 41;
            }
        }

        /// <summary>
        /// 消息体箭头组装
        /// </summary>
        public Image IconImage
        {
            get { return CustUis.IconImage; }
            set
            {
                CustUis.IconImage.Width = 32;
                CustUis.IconImage.Source = new BitmapImage(new Uri(CustParams.UrlIcon));
                CustUis.IconImage.VerticalAlignment = VerticalAlignment.Bottom;
                CustUis.IconImage.HorizontalAlignment = HorizontalAlignment.Left;
                CustUis.IconImage.Margin = new Thickness(-65, 0, 0, 12);
            }
        }

        /// <summary>
        /// 时间组装
        /// </summary>
        public Label TimeLabel
        {
            get { return CustUis.TimeLabel; }
            set
            {
                CustUis.TimeLabel.Content = CustParams.Time;
                CustUis.TimeLabel.Margin = new Thickness(0, 0, 0, 14);
                CustUis.TimeLabel.Height = 47;
                CustUis.TimeLabel.Foreground = Brushes.DimGray;
                CustUis.TimeLabel.FontSize = 14;
            }
        }

        /// <summary>
        /// 消息内容组装
        /// </summary>
        public Label ContLabel
        {
            get { return CustUis.ContLabel; }
            set
            {
                CustUis.ContLabel.Content = CustParams.Content;
                CustUis.ContLabel.Height = 30;
                CustUis.ContLabel.VerticalAlignment = VerticalAlignment.Bottom;
                CustUis.ContLabel.Background = Brushes.White;
                CustUis.ContLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
                CustUis.ContLabel.Padding = new Thickness(9, 5, 9, 5);
                CustUis.ContLabel.Margin = new Thickness(-50, 0, 3, 8);
                CustUis.ContLabel.VerticalContentAlignment = VerticalAlignment.Center;
                CustUis.ContLabel.FontSize = 14;
                CustUis.ContLabel.Foreground = Brushes.Black;
            }
        }
    }
}
