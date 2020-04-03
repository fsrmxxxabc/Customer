using Customer.Until;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Customer.Until.Chat;
using Microsoft.Win32;
using Customer.Until.Qiniu;
using Newtonsoft.Json.Linq;
using System.Windows.Media.Imaging;
using System.IO;

namespace Customer.View
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : Window
    {

        public static StackPanel Chatingmsg { get; set; }

        public static RichTextBox ChatingContMsg { get; set; }

        private static RichTextBoxUtil RichTextBoxUtils { get; set; }

        public static RichTextBox MsgRichTextBoxTemps { get; set; }

        private readonly RisCaptureLib.ScreenCaputre screenCaputre = new RisCaptureLib.ScreenCaputre();

        private Size? lastSize;

        public Index()
        {
            InitializeComponent();
            screenCaputre.ScreenCaputred += OnScreenCaputred;
            screenCaputre.ScreenCaputreCancelled += OnScreenCaputreCancelled;
            Chatingmsg = this.ChatingContent;
            ChatingContMsg = this.ChatingContentMsg;
            MsgRichTextBoxTemps = this.MsgRichTextBoxTemp;
            RichTextBoxUtils = new RichTextBoxUtil(this.ChatingContentMsg);
            LoadChatingInfo();
        }

        public void LoadChatingInfo()
        {
            _ = new WebSocketUtil();
            this.ChatingContent.Children.Clear();
            this.ChatingContent.Children.Add(new IndexUtil(FormattableString.Invariant($"{DateTime.Now}") + " admin正在为您服务").Label);
        }

        private void DockPanel_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DockPanel dockPanel = (DockPanel)sender;
            Trace.WriteLine(dockPanel.Uid);
        }

        /// <summary>
        /// 发送消息的点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IndexUtil.SendData(SetCustparam(RichTextBoxUtils.GetRichTextBoxCont));
        }

        /// <summary>
        /// 回复消息的消息体
        /// </summary>
        /// <param name="token"></param>

        private CustParam SetCustparam(String content)
        {
            return new CustParam()
            {
                Url = "https://video.yestar.com/chat_desktop_kf_icon.png",
                Time = DateTime.Now.ToLongTimeString(),
                UrlIcon = "pack://application:,,,/Resources/chat_desktop_triangle_icon.png",
                Content = content,
            };
        }

        private CustParam SetCustparam(string content,int width)
        {
            return new CustParam()
            {
                UserImage = "https://video.yestar.com/chat_desktop_kf_icon.png",
                UserTime = DateTime.Now.ToLongTimeString(),
                MsgCont = content,
                RichTextBoxWidth = width
            };
        }


        /// <summary>
        /// 发送消息的回车事件即按下enter键就可以发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChatingContentMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                //RichTextBoxUtils.SaveRichTextBoxContent = "../../../Resources/Content/Content.rtf";
                //_= new RtfToHtmlUtil("../../../Resources/Content/Content.rtf");
                int width = CommonUtil.GetRichTextBoxWidth(RichTextBoxUtils.GetRichTextBoxToString,RichTextBoxUtils.GetRichTextBoxCont);
                IndexUtil.SendData(SetCustparam(RichTextBoxUtils.GetRichTextBoxToString, width));
            }
        }

        private void Face_OnDialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {

        }

        private void Test_Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.FaceDialogHost.IsOpen.Equals(false))
            {
                CommonUtil.Clicked(this.FaceHappy);
            }
            else
            {
                this.FaceDialogHost.IsOpen = false;
            }
        }

        private void Face_WX_Emoji(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            this.ChatingContentMsg.Focus();
            CommonUtil.SetGifImage(button.DataContext.ToString());
            /*if (image != null)
            {
                _ = new InlineUIContainer(image, this.ChatingContentMsg.Selection.Start);
            }*/
        }

        /// <summary>
        /// 图片点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Upload_Image_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "图像文件|*.jpg;*.png;*.jpeg;*.bmp;*.gif|所有文件|*.*"
            };

            if ((bool)openFileDialog.ShowDialog())
            {
                QiniuUtil qiniuUtil = new QiniuUtil()
                {
                    ConfigUtil = new Qiniu.Storage.Config(),
                    TokenUtil = null
                };

                string ret = qiniuUtil.ChunkUpload("chat_"+CommonUtil.GetTimeSecond()+"_msg."+CommonUtil.GetFileExtra(openFileDialog.SafeFileName),openFileDialog.FileName);

                JObject jObject = JObject.Parse(ret);

                CommonUtil.SetImage(QiniuUtil.Domain+jObject["key"]);
            }
        }

        private void Print_Screen(object sender, RoutedEventArgs e)
        {
            Hide();
            Thread.Sleep(300);
            screenCaputre.StartCaputre(30, lastSize);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if ( e.KeyboardDevice.Modifiers.HasFlag(ModifierKeys.Control) && e.Key ==  Key.Y)
            {
                CommonUtil.Clicked(this.PrintScreen);
            }
        }

        private void OnScreenCaputreCancelled(object sender, System.EventArgs e)
        {
            Show();
            Focus();
        }

        private void OnScreenCaputred(object sender, RisCaptureLib.ScreenCaputredEventArgs e)
        {
            //set last size
            lastSize = new Size(e.Bmp.Width, e.Bmp.Height);

            QiniuUtil qiniuUtil = new QiniuUtil()
            {
                ConfigUtil = new Qiniu.Storage.Config(),
                TokenUtil = null,
                BitMapToStream = screenCaputre.GetBitMap,
            };

            string ret = qiniuUtil.StreamUpload("chat_screen_caputre" + CommonUtil.GetTimeSecond() + "_msg" );

            JObject jObject = JObject.Parse(ret);

            CommonUtil.SetImage(QiniuUtil.Domain + jObject["key"]);

            Show();
        }
    }
}
