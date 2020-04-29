using Customer.Until;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Customer.Until.Chat;
using Microsoft.Win32;
using Customer.Until.Qiniu;
using Newtonsoft.Json.Linq;
using Customer.until;
using SpRecognition;
using System.Windows.Documents;
using Customer.View.Pages;

namespace Customer.View
{
    /// <summary>
    /// Index.xaml
    /// </summary>
    public partial class Index : Window
    {

        public static StackPanel Chatingmsg { get; set; }

        public static RichTextBox ChatingContMsg { get; set; }

        private static RichTextBoxUtil RichTextBoxUtils { get; set; }

        public static RichTextBox MsgRichTextBoxTemps { get; set; }

        private SpRecognition.SpRecognition Recognition = new SpRecognition.SpRecognition();

        private bool boolRec = false;

        private Paragraph paragraph = new Paragraph();

        private readonly FacePage Face = new FacePage();

        private readonly TransPage BdTrans = new TransPage();

        public Index()
        {
            InitializeComponent();

            Chatingmsg = this.ChatingContent;

            ChatingContMsg = this.ChatingContentMsg;

            MsgRichTextBoxTemps = this.MsgRichTextBoxTemp;

            RichTextBoxUtils = new RichTextBoxUtil(this.ChatingContentMsg);

            Recognition.GetAutoComment += AutoResult;

            LoadChatingInfo();
        }

        public void LoadChatingInfo()
        {
            _ = new WebSocketUtil();

            this.ChatingContent.Children.Clear();

            this.ChatingContent.Children.Add(new IndexUtil(FormattableString.Invariant($"{DateTime.Now}") + " " + ConfigUntil.GetSettingString("userName") + "正在为您服务").Label);
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

        private CustParam SetCustparam(string content, int width)
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
                _ = new RtfToHtmlUtil("../../../Resources/Content/Content.rtf");
                int width = CommonUtil.GetRichTextBoxWidth(RichTextBoxUtils.GetRichTextBoxToString, RichTextBoxUtils.GetRichTextBoxCont);
                IndexUtil.SendData(SetCustparam(RichTextBoxUtils.GetRichTextBoxToString, width));
            }
        }

        private void Face_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Face.FaceDialogHost.IsOpen.Equals(false))
            {
                Face.FaceDialogHost.IsOpen = true;
            }
            else
            {
                Face.FaceDialogHost.IsOpen = false;
            }
            this.ButClick.Content = Face;
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

                string ret = qiniuUtil.ChunkUpload("chat_" + CommonUtil.GetTimeSecond() + "_msg." + CommonUtil.GetFileExtra(openFileDialog.SafeFileName), openFileDialog.FileName);

                JObject jObject = JObject.Parse(ret);

                CommonUtil.SetImage(QiniuUtil.Domain + jObject["key"]);
            }
        }

        /// <summary>
        /// 截屏事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Screen(object sender, RoutedEventArgs e)
        {
            Interaction.StartScreenShot();
        }

        /// <summary>
        /// 你懂得
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e != null)
            {
                base.OnKeyDown(e);
                if (e.KeyboardDevice.Modifiers.HasFlag(ModifierKeys.Control) && e.Key == Key.Y)
                {
                    CommonUtil.Clicked(this.PrintScreen);
                }
            }

        }

        private void Send_Shake_Click(object sender, RoutedEventArgs e)
        {
            new IndexUtil(new LabelShake().GetLabels).SendData();
        }

        private void AutoResult(object sender, StringEventArgs e)
        {
            paragraph.ContentStart.Paragraph.Inlines.Add(new Run(e.Str));
            _ = new InlineUIContainer(new TextBlock() { Text=e.Str}, this.ChatingContentMsg.Selection.Start);
            //this.ChatingContentMsg.Document.Blocks.Add(paragraph);
            this.ChatingContentMsg.ScrollToEnd();
            this.ChatingContentMsg.Focus();
        }

        private void Auto_Click(object sender, RoutedEventArgs e)
        {
            Recognition.instance();

            if (!boolRec)
            {
                Recognition.BeginRec();
                boolRec = true;
            }
            else
            {
                Recognition.CloseRec();
                boolRec = false;
            }
            
        }

        private void Baidu_Trans(object sender, RoutedEventArgs e)
        {
            BdTrans.TransDialogHost.IsOpen = BdTrans.TransDialogHost.IsOpen ? false : true;
            this.ButClick.Content = BdTrans;
        }
    }
}
