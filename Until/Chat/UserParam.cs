using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Customer.Until.Chat
{
    class UserParam
    {
        public DockPanel DockOne { get; set; }

        public StackPanel StackOne { get; set; }

        public Label Labe { get; set; }

        public DockPanel DockTwo { get; set; }

        public StackPanel StackTwo { get; set; }

        public Image Img { get; set; }

        public Image Avatar { get; set; }

        public UserParam UserParamDefault { get; set; }

        public string Default
        {
            set
            {
                this.UserParamDefault = new UserParam()
                {
                    DockOne = new DockPanel() { HorizontalAlignment = System.Windows.HorizontalAlignment.Right, Margin = new System.Windows.Thickness(0, 10, 8, 0) },
                    StackOne = new StackPanel() { VerticalAlignment = System.Windows.VerticalAlignment.Top },
                    Labe = new Label() { HorizontalAlignment = System.Windows.HorizontalAlignment.Right, FontSize = 14, VerticalContentAlignment = System.Windows.VerticalAlignment.Top, Foreground = CommonUtil.GetColorBrush(102, 102, 102), Content = FormattableString.Invariant($"{DateTime.Now:HH:mm:ss}") },
                    DockTwo = new DockPanel() { VerticalAlignment = System.Windows.VerticalAlignment.Stretch, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                    StackTwo = new StackPanel() { Width = 300, Background = System.Windows.Media.Brushes.White, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, VerticalAlignment = System.Windows.VerticalAlignment.Stretch },
                    Img = new Image() { Width = 8, Height = 8, Margin = new System.Windows.Thickness(-1, 8, 0, 0), Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Resources/chat_desktop_triangle_icon.png")), VerticalAlignment = System.Windows.VerticalAlignment.Top, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                    Avatar = new Image() { Width = 41, Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("https://video.yestar.com/chat_desktop_kf_icon.png")), VerticalAlignment = System.Windows.VerticalAlignment.Top }
                };
            }
        }

        public DockPanel UserDock
        {
            get { return this.DockOne; }
            set
            {
                DockTwo.Children.Add(StackTwo);
                DockTwo.Children.Add(Img);
                StackOne.Children.Add(Labe);
                StackOne.Children.Add(DockTwo);
                DockOne.Children.Add(StackOne);
                DockOne.Children.Add(Avatar);
            }
        }

        public string DefaultLeft
        {
            set
            {
                this.UserParamDefault = new UserParam()
                {
                    DockOne = new DockPanel() { HorizontalAlignment = System.Windows.HorizontalAlignment.Left, Margin = new System.Windows.Thickness(8, 10, 0, 0) },
                    StackOne = new StackPanel() { VerticalAlignment = System.Windows.VerticalAlignment.Top },
                    Labe = new Label() { HorizontalAlignment = System.Windows.HorizontalAlignment.Left, FontSize = 14, VerticalContentAlignment = System.Windows.VerticalAlignment.Top, Foreground = CommonUtil.GetColorBrush(102, 102, 102), Content = FormattableString.Invariant($"{DateTime.Now:HH:mm:ss}") },
                    DockTwo = new DockPanel() { VerticalAlignment = System.Windows.VerticalAlignment.Bottom, HorizontalAlignment = System.Windows.HorizontalAlignment.Left },
                    StackTwo = new StackPanel() { Width = 300, Background = System.Windows.Media.Brushes.White, HorizontalAlignment = System.Windows.HorizontalAlignment.Center, VerticalAlignment = System.Windows.VerticalAlignment.Stretch },
                    Img = new Image() { Width = 8, Height = 8, Margin = new System.Windows.Thickness(-1, 8, 0, 0), Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Resources/chat_desktop_left_icon.png")), VerticalAlignment = System.Windows.VerticalAlignment.Top, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                    Avatar = new Image() { Width = 41, Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("https://video.yestar.com/chat_desktop_customer_avatr_img.png")), VerticalAlignment = System.Windows.VerticalAlignment.Top }
                };
            }
        }

        public DockPanel UserDockLeft
        {
            get { return this.DockOne; }
            set
            {
                DockTwo.Children.Add(Img);
                DockTwo.Children.Add(StackTwo);
                StackOne.Children.Add(Labe);
                StackOne.Children.Add(DockTwo);
                DockOne.Children.Add(Avatar);
                DockOne.Children.Add(StackOne);
            }
        }
    }
}
