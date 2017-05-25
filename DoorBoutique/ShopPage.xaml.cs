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

namespace DoorBoutique
{
    /// <summary>
    /// Логика взаимодействия для ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        public ShopPage()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.MainPage);
        }

        private void DoorPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.DoorPage);
        }

        private void ShopPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.ShopPage);
        }

        private void loginToolboxButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.LogIn);
        }

        private void AddNewShop_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.AddNewShop);
        }

        private void OpenShops_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.ShopCatalog);
            Pages.ShopCatalog.DeleteShop.Visibility = Visibility.Hidden;
            Pages.ShopCatalog.EditShop.Visibility = Visibility.Hidden;
            Pages.ShopCatalog.EditAssortment.Visibility = Visibility.Hidden;
        }

        private void EditOrDeleteShops_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.ShopCatalog);
            Pages.ShopCatalog.DeleteShop.Visibility = Visibility.Visible;
            Pages.ShopCatalog.EditShop.Visibility = Visibility.Visible;
            Pages.ShopCatalog.EditAssortment.Visibility = Visibility.Visible;
        }
    }
}
