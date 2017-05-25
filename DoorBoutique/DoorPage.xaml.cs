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
    /// Логика взаимодействия для DoorPage.xaml
    /// </summary>
    public partial class DoorPage : Page
    {
        public DoorPage()
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

        private void OpenCatalog_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.DoorAssortment);
        }

        private void EditOrDeleteFromCatalog_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.DoorAssortment);
            Pages.DoorAssortment.DeleteDoor.Visibility = Visibility.Visible;
            Pages.DoorAssortment.EditDoor.Visibility = Visibility.Visible;
        }

        private void AddNewDoor_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.AddNewDoor);
        }
    }
}
