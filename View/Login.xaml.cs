using Customer.Controller;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Customer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginController LoginController { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoginController = new LoginController();
        }

        /**
         * 获取焦点时选中所有问题
         */
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                TextBox tbx = sender as TextBox;
                tbx.SelectAll();
                tbx.PreviewMouseDown -= new MouseButtonEventHandler(TextBox_PreviewMouseDown);

            }
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                PasswordBox passwordBox = sender as PasswordBox;
                passwordBox.SelectAll();
                passwordBox.PreviewMouseDown -= new MouseButtonEventHandler(Password_PreviewMouseDown);
            }
        }


        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                tb.Focus();
                e.Handled = true;
            }
        }

        private void Password_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (passwordBox != null)
            {
                passwordBox.Focus();
                e.Handled = true;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {

            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                tb.PreviewMouseDown += new MouseButtonEventHandler(TextBox_PreviewMouseDown);
            }
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (passwordBox != null)
            {
                passwordBox.PreviewMouseDown += new MouseButtonEventHandler(Password_PreviewMouseDown);
            }
        }

        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Passwd_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Code_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ForgetPasswd_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SavePasswd_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitLogin_Click(object sender, RoutedEventArgs e)
        {
            JObject jObject = LoginController.Submit(this.userName.Text, this.passwd.Password, this.code.Text);
            //Trace.WriteLine(ConfigurationUserLevel.None);
            if (jObject != null)
            {
                View.Index index = new View.Index();
                Application.Current.MainWindow = index;
                this.Close();
                index.Show();
            }
        }
    }
}
