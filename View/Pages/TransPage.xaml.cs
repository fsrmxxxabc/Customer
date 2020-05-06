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
using Newtonsoft.Json.Linq;

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
            string str = Index.ChatingContMsg.Selection.Text;
            if (!String.IsNullOrEmpty(str))
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)this.ToLanguage.SelectedItem;
                string ret = BaiduTrans.Trans(str, comboBoxItem.DataContext.ToString());
                JObject jObject = JObject.Parse(ret);
                Index.ChatingContMsg.Selection.Text = jObject["trans_result"][0]["dst"].ToString();
            }
        }
    }
}
