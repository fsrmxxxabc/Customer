using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Customer.Until;
using BaiDuTrans;

namespace Customer.View.Pages
{
    /// <summary>
    /// TransPage.xaml 的交互逻辑
    /// </summary>
    public partial class TransPage : Page
    {
        public TransPage()
        {
            InitializeComponent();
        }

        private void Face_OnDialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {

        }

        private void Trans_Click(object sender, RoutedEventArgs e)
        {
            RichTextBoxUtil richTextBoxUtil = new RichTextBoxUtil(Index.ChatingContMsg);
            string cont = richTextBoxUtil.GetRichTextBoxCont;
            ComboBoxItem comboBoxItem  = (ComboBoxItem)this.ToLanguage.SelectedItem;
            string ret = BaiduTrans.Trans(cont,comboBoxItem.DataContext.ToString());
            MessageBox.Show(ret);
        }
    }
}
