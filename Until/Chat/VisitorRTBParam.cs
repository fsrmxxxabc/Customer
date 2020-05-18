using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Customer.Until.Chat
{
    class VisitorRTBParam
    {
        public VisitorRTBParam() { }

        public int Width { get; set; }

        public DockPanel Dock { get; set; }

        public FlowDocument Flow { get; set; }

        public RichTextBox RichTextBox { get; set; }

        public ControlTemplate Contpl { get; set; }

        public DockPanel Default
        {
            get { return Dock; }
            set
            {
                UserParam userParam = new UserParam() { DefaultLeft = null }.UserParamDefault;
                userParam.StackTwo.Width = Width;
                userParam.StackTwo.Children.Add(RichTextBox);
                userParam.UserDockLeft = new DockPanel();
                Dock = userParam.UserDockLeft;
            }
        }

        public RichTextBox RTB
        {
            set
            {
                RichTextBox = new RichTextBox()
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    AllowDrop = true,
                    Padding = new Thickness(2, 2, 0, 2),
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    FontSize = 14,
                    Foreground = CommonUtil.GetColorBrush(43, 43, 43),
                    IsReadOnly = true,
                    Template = Contpl,
                    Document = Flow
                };
            }
        }
    }
}
