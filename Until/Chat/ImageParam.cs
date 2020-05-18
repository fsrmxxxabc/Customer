using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Customer.Until.Chat
{
    class ImageParam
    {
        public ImageParam() { }

        public Image Image { get; set; }

        public Border Border { get; set; }

        public DockPanel Dock { get; set; }

        public DockPanel Default
        {
            get { return Dock; }
            set
            {
                UserParam userParam = new UserParam() { Default = null }.UserParamDefault;
                userParam.StackTwo.Width = 200;
                userParam.StackTwo.Background = Brushes.White;
                userParam.Img.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("pack://application:,,,/Resources/chat_desktop_right_triangle_icon.png"));
                userParam.StackTwo.Children.Add(Border);
                userParam.UserDock = new DockPanel();
                Dock = userParam.UserDock;
            }
        }

        public string Bord
        {
            set
            {
                if(value != null)
                {
                    Border = new Border()
                    {
                        CornerRadius = new System.Windows.CornerRadius(5),
                        Background = new ImageBrush() { ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(value)) },
                    };
                    Border.Child = Image;
                }
            }
        }
    }
}
