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
        ChangesHistory changes = new ChangesHistory();
        List<LoginData> logins = new List<LoginData>();
        public LogIn()
        {
            InitializeComponent();
            loginTextBox.Focus();
            if (File.Exists("../../login.txt"))
            {
                using (StreamReader sr = new StreamReader("../../login.txt"))
                {
                    logins = LoginList(sr);
                }
            }            
        }

        //считаем хэш-функцию от введенного пароля
        private string HashFunction(string password)
        {
            MD5 md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        //класс для хранения информации о пользователях
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

        //считывание информации о пользователях из файла
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

        //проверка введенных данных и переход на главную страницу
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (logins.Count == 0)
                MessageBox.Show("Данные о пользователях отсутствуют, возможен только гостевой вход", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
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
                    Pages.ShopCatalog.loginAccount.Text = userName;

                    Pages.DoorPage.AddNewDoor.IsEnabled = true;
                    Pages.DoorPage.EditOrDeleteFromCatalog.IsEnabled = true;
                    Pages.ShopPage.AddNewShop.IsEnabled = true;
                    Pages.ShopPage.EditOrDeleteShops.IsEnabled = true;

                    Pages.DoorAssortment.PurchasePrice.Visibility = Visibility.Visible;
                    Pages.DoorAssortment.PurchasePriceBox.Visibility = Visibility.Visible;

                    Pages.MainPage.loginToolboxButton.Visibility = Visibility.Hidden;
                    Pages.DoorPage.loginToolboxButton.Visibility = Visibility.Hidden;
                    Pages.ShopPage.loginToolboxButton.Visibility = Visibility.Hidden;
                    Pages.DoorAssortment.loginToolboxButton.Visibility = Visibility.Hidden;
                    Pages.ShopCatalog.loginToolboxButton.Visibility = Visibility.Hidden;

                    Pages.DoorPage.AddToolTip.Visibility = Visibility.Hidden;
                    Pages.DoorPage.EditToolTip.Visibility = Visibility.Hidden;
                    Pages.ShopPage.AddToolTip.Visibility = Visibility.Hidden;
                    Pages.ShopPage.EditToolTip.Visibility = Visibility.Hidden;

                    changes.SaveHistory($"Выполнен вход, пользователь: {userName}");
                }

                else
                {
                    MessageBox.Show("Данные некорректны", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    passwordBox.Clear();
                    passwordBox.Focus();
                }
            }

        }


        //переход на главную страницу при гостевом входе
        private void guestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.MainPage);

            Pages.DoorAssortment.PurchasePrice.Visibility = Visibility.Hidden;
            Pages.DoorAssortment.PurchasePriceBox.Visibility = Visibility.Hidden;

            Pages.DoorPage.AddNewDoor.IsEnabled = false;
            Pages.DoorPage.EditOrDeleteFromCatalog.IsEnabled = false;

            changes.SaveHistory($"Выполнен гостевой вход");
        }
    }

}

//Login: dzlevin
//Password: adminpass162

//Login: sgefremov
//Password: teacherpass