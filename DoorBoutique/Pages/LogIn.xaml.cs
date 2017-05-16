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
using System.IO;
using System.Security.Cryptography;

namespace DoorBoutique
{
    /// <summary>
    /// Логика взаимодействия для LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        List<LoginData> logins = new List<LoginData>();
        public LogIn()
        {
            InitializeComponent();
            loginTextBox.Focus();
            using (StreamReader sr = new StreamReader("login.txt"))
            {
                logins = LoginList(sr);
            }
        }

        private string HashFunction(string password)
        {
            MD5 md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        public class LoginData
        {
            private string _loginName;

            public string LoginName
            {
                get { return _loginName; }
                set { _loginName = value; }
            }
            private string _loginPassword;

            public string LoginPassword
            {
                get { return _loginPassword; }
                set { _loginPassword = value; }
            }

            public LoginData(string loginName, string loginPassword)
            {
                _loginName = loginName;
                _loginPassword = loginPassword;
            }


        }

        public List<LoginData> LoginList(StreamReader sr)
        {
            List<LoginData> _loginlist = new List<LoginData>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string loginName = line.Split()[0];
                string hashPassword = line.Split()[1];
                _loginlist.Add(new LoginData(loginName, hashPassword));
            }
            return _loginlist;
        }


        int c = 0;
        string userName;

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in logins)
            {
                if ((item.LoginName == loginTextBox.Text) && HashFunction(passwordBox.Password) == item.LoginPassword)
                {
                    c++;
                    userName = item.LoginName;
                }
            }
            if (c > 0)
            {
                c = 0;
                NavigationService.Navigate(Pages.MainPage);
                Pages.MainPage.DoorPageButton.Visibility = Visibility.Visible;
                Pages.MainPage.ShopPageButton.Visibility = Visibility.Visible;
                Pages.MainPage.loginAccount.Text = userName;
                Pages.DoorPage.loginAccount.Text = userName;
                Pages.ShopPage.loginAccount.Text = userName;
                Pages.DoorAssortment.loginAccount.Text = userName;

                Pages.DoorPage.AddNewDoor.IsEnabled = true;
                Pages.DoorPage.EditCatalog.IsEnabled = true;
                Pages.DoorPage.DeleteFromCatalog.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Данные некорректны", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBox.Clear();
            }

        }

        private void guestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.MainPage);

            Pages.MainPage.loginAccount.Text = "";
            Pages.DoorPage.loginAccount.Text = "";
            Pages.ShopPage.loginAccount.Text = "";
            Pages.DoorAssortment.loginAccount.Text = "";

            Pages.DoorPage.AddNewDoor.IsEnabled = false;
            Pages.DoorPage.EditCatalog.IsEnabled = false;
            Pages.DoorPage.DeleteFromCatalog.IsEnabled = false;
        }
    }

}

//Login: dzlevin
//Password: adminpass162

//Login: sgefremov
//Password: teacherpass