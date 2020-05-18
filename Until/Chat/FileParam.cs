using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Customer.Until.Chat
{
    class FileParam
    {
        public FileParam(){ }

        public DockPanel DockPane { get; set; }

        public DockPanel instance
        {
            get{ return DockPane; }
            set
            {
                //loadFileParam();
                FileDefault.StackThird.Children.Add(FileDefault.TextFileName);
                FileDefault.StackThird.Children.Add(FileDefault.LabelSize);
                FileDefault.DockThird.Children.Add(FileDefault.StackThird);
                FileDefault.DockThird.Children.Add(FileDefault.FileImg);
                UserParam userParam = new UserParam() { Default = null }.UserParamDefault;
                userParam.StackTwo.Width = 220;
                userParam.StackTwo.Height = 105;
                userParam.StackTwo.Children.Add(FileDefault.DockThird);
                userParam.StackTwo.Children.Add(FileDefault.AppName);
                userParam.UserDock = new DockPanel();
                DockPane = userParam.UserDock;
            }
        }

        private DockPanel DockThird { get; set; }

        private StackPanel StackThird { get; set; }

        private TextBlock TextFileName { get; set; }

        private Label LabelSize { get; set; }

        private Image FileImg { get; set; }

        private Label AppName { get; set; }

        public string FileName { get; set; }

        public System.Drawing.Bitmap FileIcon { get; set; }

        public BitmapSource FileIconImg { get; set; }

        public string FileSize { get; set; }

        public string UserImg { get; set; }

        public FileParam FileDefault { get; set; }


        public FileParam Default
        {
            get { return FileDefault; }
            set
            {
                FileIconUtil = FileIcon;
                FileDefault = new FileParam()
                {
                    AppName = new Label() { Content = "    Yestar桌面版", BorderBrush = CommonUtil.GetColorBrush(222, 221, 221), BorderThickness = new Thickness(0, 1, 0, 0) },
                    DockThird = new DockPanel() { HorizontalAlignment = HorizontalAlignment.Left, Height = 82 },
                    StackThird = new StackPanel() { Width = 165 },
                    TextFileName = new TextBlock() { Padding = new Thickness(15, 15, 10, 10), TextWrapping = TextWrapping.Wrap, Text = FileName, MaxWidth = 175 },
                    LabelSize = new Label() { Content = FileSize, Margin = new Thickness(11, 0, 0, 0) },
                    FileImg = new Image() { Source = FileIconImg, Width = 45 }
                };
            }
        }

        public System.Drawing.Bitmap FileIconUtil
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
    }
}
