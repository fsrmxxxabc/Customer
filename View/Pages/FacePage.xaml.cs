using Customer.Until;
using System.Windows;
using System.Windows.Controls;

namespace Customer.View.Pages
{
    /// <summary>
    /// FacePage.xaml 的交互逻辑
    /// </summary>
    public partial class FacePage : Page
    {
        public FacePage()
        {
            InitializeComponent();
        }

        private void Face_OnDialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {

        }

        private void Face_WX_Emoji(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Index.ChatingContMsg.Focus();
            CommonUtil.SetGifImage(button.DataContext.ToString());
        }
    }
}
