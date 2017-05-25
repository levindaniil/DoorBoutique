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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace DoorBoutique
{
    /// <summary>
    /// Логика взаимодействия для ShopCatalog.xaml
    /// </summary>
    public partial class ShopCatalog : Page
    {
        List<Shop> shopList;

        public ShopCatalog()
        {
            InitializeComponent();
        }

        const string ShopFilePath = "shops.json";

        #region ToolBox

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.MainPage);
            ShopListBox.SelectedIndex = -1;
            Pages.ShopCatalog.DeleteShop.Visibility = Visibility.Hidden;
            Pages.ShopCatalog.EditShop.Visibility = Visibility.Hidden;
        }

        private void DoorPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.DoorPage);
            ShopListBox.SelectedIndex = -1;
            Pages.ShopCatalog.DeleteShop.Visibility = Visibility.Hidden;
            Pages.ShopCatalog.EditShop.Visibility = Visibility.Hidden;
        }

        private void ShopPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.ShopPage);
            ShopListBox.SelectedIndex = -1;
            Pages.ShopCatalog.DeleteShop.Visibility = Visibility.Hidden;
            Pages.ShopCatalog.EditShop.Visibility = Visibility.Hidden;
        }

        private void loginToolboxButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.LogIn);
            ShopListBox.SelectedIndex = -1;
            Pages.ShopCatalog.DeleteShop.Visibility = Visibility.Hidden;
            Pages.ShopCatalog.EditShop.Visibility = Visibility.Hidden;
        }


        #endregion

        private List<Shop> LoadData()
        {
            List<Shop> _shopList;
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Shop>));
            if (File.Exists(ShopFilePath))
            {
                using (FileStream fs = new FileStream(ShopFilePath, FileMode.Open))
                {
                    _shopList = (List<Shop>)jsonFormatter.ReadObject(fs);
                    return _shopList;
                }
            }
            else
            {
                using (FileStream fs = new FileStream(ShopFilePath, FileMode.Create))
                {
                    _shopList = new List<Shop>();
                    jsonFormatter.WriteObject(fs, _shopList);
                    return _shopList;
                }
            }

        }

        private void SaveData(List<Shop> _shopList)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Shop>));
            using (FileStream fs = new FileStream(ShopFilePath, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, _shopList);
            }
        }

        private void ShopListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShopListBox.SelectedIndex != -1)
            {
                GridSelectedShop.Visibility = Visibility.Visible;
                int ind = ShopListBox.SelectedIndex;
                AddressBox.Text = shopList[ind].Address;
                StorageArea.Text = shopList[ind].StorageArea.ToString();
                TradeAreaBox.Text = shopList[ind].TradeArea.ToString();
                DirectorNameBox.Text = shopList[ind].Director;
                EditShop.IsEnabled = true;
                DeleteShop.IsEnabled = true;
                EditAssortment.IsEnabled = true;
            }
            else
                GridSelectedShop.Visibility = Visibility.Hidden;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            shopList = LoadData();
            ShopListBox.ItemsSource = shopList;
        }

        private void DeleteShop_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show("Удалить магазин?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbResult == MessageBoxResult.OK)
            {
                List<Shop> tempList = shopList;
                tempList.Remove(tempList[ShopListBox.SelectedIndex]);
                SaveData(tempList);
                shopList = LoadData();
                ShopListBox.ItemsSource = null;
                ShopListBox.ItemsSource = shopList;
            }
        }

        private void EditAssortment_Click(object sender, RoutedEventArgs e)
        {
            Pages.VandorPage.chosenShop = shopList[ShopListBox.SelectedIndex];
            NavigationService.Navigate(Pages.VandorPage);
        }

        private void EditShop_Click(object sender, RoutedEventArgs e)
        {
            Pages.EditShop.chosenShop = shopList[ShopListBox.SelectedIndex];
            NavigationService.Navigate(Pages.EditShop);
        }

        private void ShowShopCatalog_Click(object sender, RoutedEventArgs e)
        {
            Pages.DoorAssortment.chosenShop = shopList[ShopListBox.SelectedIndex];
            NavigationService.Navigate(Pages.DoorAssortment);
        }
    }
}
