using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace Customer.Until.Chat
{
    class VisitorImgParam
    {
        public VisitorImgParam() { }

        public Border Border { get; set; }

        public Image Image { get; set; }

        public DockPanel Dock { get; set; }

        public DockPanel Default
        {
            get { return Dock; }
            set
            {
                UserParam userParam = new UserParam() { DefaultLeft = null }.UserParamDefault;
                userParam.StackTwo.Width = 200;
                userParam.StackTwo.Children.Add(Border);
                userParam.UserDockLeft = new DockPanel();
                Dock = userParam.UserDockLeft;
            }
        }

        public String Bord
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
