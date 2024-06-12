using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lanpuda.Account.UI.Login
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }


        private void PasswordBoxDemo_TextChanged(object sender, TextChangedEventArgs e)
        {
            //LoginViewModel aaa = (LoginViewModel)this.DataContext;
            //string aaaaaaa = PasswordBoxDemo.UnsafePassword;
            //string word = PasswordBoxDemo.Password;
            //HandyControl.Controls.WatermarkTextBox tt = (HandyControl.Controls.WatermarkTextBox)e.OriginalSource;
            //var bb = tt.Text;
            //aaa.Model.Password = bb;
            //;
        }
    }
}
