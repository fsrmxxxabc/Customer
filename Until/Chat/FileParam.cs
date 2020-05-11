using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace Customer.Until.Chat
{
    class FileParam
    {
        public FileParam(){ }

        public void loadFileParam()
        {
            FileIconUtil = FileIcon;
            DockThirdUtil = DockpanelUtil = DockSecondUtil = new DockPanel();
            StackSecondUtil = StackThirdUtil = StackpanelUtil = new StackPanel();
            LabelSizeUtil = LabelsUtil = new Label();
            AppName = new Label() { Content = "    Yestar桌面版", BorderBrush = CommonUtil.GetColorBrush(222, 221, 221), BorderThickness = new Thickness(0, 1, 0, 0) };
            TextFileNameUtil = new TextBlock();
            FileImgUtil = ImgIconUtil = AvarUtil = new Image();
        }

        public DockPanel instance()
        {
            loadFileParam();
            StackThird.Children.Add(TextFileName);
            StackThird.Children.Add(LabelSize);
            DockThird.Children.Add(StackThird);
            DockThird.Children.Add(FileImg);
            StackSecond.Children.Add(DockThird);
            StackSecond.Children.Add(AppName);
            DockSecond.Children.Add(StackSecond);
            DockSecond.Children.Add(ImgIcon);
            Stackpanel.Children.Add(Labels);
            Stackpanel.Children.Add(DockSecond);
            Dockpanel.Children.Add(Stackpanel);
            Dockpanel.Children.Add(Avar);
            return Dockpanel;
        }

        private DockPanel Dockpanel { get; set; }

        private StackPanel Stackpanel { get; set; }

        private Label Labels { get; set; }

        private DockPanel DockSecond { get; set; }

        private StackPanel StackSecond { get; set; }

        private DockPanel DockThird { get; set; }

        private StackPanel StackThird { get; set; }

        private TextBlock TextFileName { get; set; }

        private Label LabelSize { get; set; }

        /*private RichTextBox RichTextBox { get; set; }

        private FlowDocument FlowDocuments { get; set; }

        private Paragraph Paragraph { get; set; }

        private Run Runtop { get; set; }*/

        private Image FileImg { get; set; }

        //private Run Runbottom { get; set; }

        private Label AppName { get; set; }

        private Image ImgIcon { get; set; }

        private Image Avar { get; set; }

        public string FileName { get; set; }

        public System.Drawing.Bitmap FileIcon { get; set; }

        public BitmapSource FileIconImg { get; set; }

        public string FileSize { get; set; }

        public string UserImg { get; set; }

        private DockPanel DockpanelUtil
        {
            set { Dockpanel = new DockPanel() { Cursor = System.Windows.Input.Cursors.Hand, HorizontalAlignment = System.Windows.HorizontalAlignment.Right, Margin = new System.Windows.Thickness(0, 10, 8, 0) }; }
        }

        private StackPanel StackpanelUtil
        {
            set { Stackpanel = new StackPanel() { VerticalAlignment = System.Windows.VerticalAlignment.Top }; }
        }

        private Label LabelsUtil
        {
            set { Labels = new Label() { HorizontalAlignment = System.Windows.HorizontalAlignment.Right, Foreground = CommonUtil.GetColorBrush(102, 102, 102), FontSize = 14, VerticalContentAlignment = System.Windows.VerticalAlignment.Top }; }
        }

        private DockPanel DockSecondUtil
        {
            set { DockSecond = new DockPanel() { VerticalAlignment = System.Windows.VerticalAlignment.Stretch, HorizontalAlignment = System.Windows.HorizontalAlignment.Right, Margin = new System.Windows.Thickness(0, 0, 0, 0) }; }
        }

        private StackPanel StackSecondUtil
        {
            set { StackSecond = new StackPanel() { Width = 220, Height = 105, Background = System.Windows.Media.Brushes.White, Margin = new Thickness(0, 0, -17, 0), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center }; }
        }

        private DockPanel DockThirdUtil
        {
            set { DockThird = new DockPanel() { HorizontalAlignment = HorizontalAlignment.Left, Height = 82 }; }
        }

        private StackPanel StackThirdUtil
        {
            set { StackThird = new StackPanel() { Width = 165 }; }
        }

        private TextBlock TextFileNameUtil
        {
            set { TextFileName = new TextBlock() { Padding = new Thickness(15, 15, 10, 10), TextWrapping = TextWrapping.Wrap, Text = FileName, MaxWidth = 175 }; }
        }

        private Label LabelSizeUtil
        {
            set { LabelSize = new Label() { Content = FileSize, Margin = new Thickness(11, 0, 0, 0) }; }
        }

        /*private RichTextBox RichTextBoxUtil
        {
            set { RichTextBox = new RichTextBox() { MaxWidth = 200, Template = View.Index.MsgRichTextBoxTemps.Template, VerticalAlignment = System.Windows.VerticalAlignment.Center, Margin = new System.Windows.Thickness(0, 0, -20, 0), AutoWordSelection = false, Padding = new System.Windows.Thickness(2, 2, 2, 2), IsDocumentEnabled = false, Background = System.Windows.Media.Brushes.White, HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right, FontSize = 14, Foreground = CommonUtil.GetColorBrush(43, 43, 43), IsReadOnly = true, HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch }; }
        }

        private FlowDocument FlowDocumentsUtil
        {
            set { FlowDocuments = new FlowDocument(); }
        }

        private Paragraph ParagraphUtil
        {
            set { Paragraph = new Paragraph(); }
        }

        private Run RuntopUtil
        {
            set { Runtop = new Run() { Text = FileName, Language = System.Windows.Markup.XmlLanguage.GetLanguage("zh-cn") }; }
        }*/

        private System.Drawing.Bitmap FileIconUtil
        {
            set
            {
                if(value != null)
                {
                    IntPtr ip = value.GetHbitmap();
                    FileIconImg = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty,
    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                    value.Dispose();
                }
            }
        }

        private Image FileImgUtil
        {
            set { FileImg = new Image() { Source = FileIconImg, Width = 45 }; }
        }

        /*private Run RunBottomUtil
        {
            set { Runbottom = new Run() { Text = FileSize, Language = System.Windows.Markup.XmlLanguage.GetLanguage("zh-cn") }; }
        }*/

        private Image ImgIconUtil
        {
            set { ImgIcon = new Image() { Width = 32, Margin = new System.Windows.Thickness(0, -5, -5, 0), Source = new BitmapImage(new Uri("pack://application:,,,/Resources/chat_desktop_triangle_icon.png")), VerticalAlignment = System.Windows.VerticalAlignment.Top, HorizontalAlignment = System.Windows.HorizontalAlignment.Right }; }
        }

        private Image AvarUtil
        {
            set { Avar = new Image() { Width = 41, VerticalAlignment = System.Windows.VerticalAlignment.Top, Source = new BitmapImage(new Uri(UserImg)) }; }
        }
    }
}
