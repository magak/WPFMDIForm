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
using System.Windows.Shapes;

namespace WPFMDIForm
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        const String AuthorisedLogin = "User1";
        const String AuthorisedPassword = "123";

        public WindowLogin()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text == AuthorisedLogin)
            {
                if (password.Password == AuthorisedPassword)
                    Close(); //ok
                else
                    MessageBox.Show("Неправильный пароль");
            }
            else
                MessageBox.Show("Неправильное имя пользователя");
        }
    }
}
