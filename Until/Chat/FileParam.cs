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
            DockpanelUtil = DockSecondUtil = new DockPanel();
            StackpanelUtil = new StackPanel();
            LabelsUtil = new Label();
            RichTextBoxUtil = new RichTextBox();
            ParagraphUtil = new Paragraph();
            RuntopUtil = RunBottomUtil = new Run();
            FileImgUtil = ImgIconUtil = AvarUtil = new Image();
            FlowDocumentsUtil = new FlowDocument();
        }

        public DockPanel instance()
        {
            loadFileParam();
            Paragraph.Inlines.Add(Runtop);
            Paragraph.Inlines.Add(FileImg);
            Paragraph.Inlines.Add(Runbottom);
            FlowDocuments.Blocks.Add(Paragraph);
            RichTextBox.Document = FlowDocuments;
            DockSecond.Children.Add(RichTextBox);
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

        private RichTextBox RichTextBox { get; set; }

        private FlowDocument FlowDocuments { get; set; }

        private Paragraph Paragraph { get; set; }

        private Run Runtop { get; set; }

        private Image FileImg { get; set; }

        private Run Runbottom { get; set; }

        private Image ImgIcon { get; set; }

        private Image Avar { get; set; }

        public string FileName { get; set; }

        public System.Drawing.Bitmap FileIcon { get; set; }

        public BitmapSource FileIconImg { get; set; }

        public string FileSize { get; set; }

        public string UserImg { get; set; }

        private DockPanel DockpanelUtil
        {
            set { Dockpanel = new DockPanel() { HorizontalAlignment = System.Windows.HorizontalAlignment.Right, Margin = new System.Windows.Thickness(0, 10, 8, 0) }; }
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

        private RichTextBox RichTextBoxUtil
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
        }

        private System.Drawing.Bitmap FileIconUtil
        {
            set
            {
                if(value != null)
                {
                    IntPtr ip = value.GetHbitmap();
                    FileIconImg = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty,
    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                }
            }
        }

        private Image FileImgUtil
        {
            set { FileImg = new Image() { Source = FileIconImg, Width = 32 }; }
        }

        private Run RunBottomUtil
        {
            set { Runbottom = new Run() { Text = FileSize, Language = System.Windows.Markup.XmlLanguage.GetLanguage("zh-cn") }; }
        }

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
