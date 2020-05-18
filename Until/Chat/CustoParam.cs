using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Customer.Until.Chat
{
    class CustoParam
    {
        public CustoParam() { }

        public RichTextBox RichTextBox { get; set; }

        public DockPanel Dock { get; set; }

        public ControlTemplate Contpl { get; set; }

        public int Width { get; set; }

        public FlowDocument Document { get; set; }

        public DockPanel Default
        {
            get { return Dock; }
            set
            {
                UserParam userParam = new UserParam() { Default = null }.UserParamDefault;
                userParam.StackTwo.Width = Width;
                userParam.StackTwo.Children.Add(RichTextBox);
                userParam.UserDock = new DockPanel();
                Dock = userParam.UserDock;
            }
        }

        public RichTextBox RTB
        {
            set
            {
                RichTextBox =  new RichTextBox()
                {
                    VerticalAlignment = VerticalAlignment.Top,
                    AllowDrop = true,
                    Padding = new Thickness(2, 2, 0, 2),
                    Background = CommonUtil.GetColorBrush(176, 226, 141),
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    FontSize = 14,
                    Foreground = CommonUtil.GetColorBrush(43, 43, 43),
                    IsReadOnly = true,
                    Template = Contpl,
                    Document = Document
                };
            }
        }
    }
}
