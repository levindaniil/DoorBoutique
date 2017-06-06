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
    /// Логика взаимодействия для DoorAssortment.xaml
    /// </summary>
    public partial class DoorAssortment : Page
    {
        public Shop chosenShop;
        ChangesHistory changes = new ChangesHistory();
        List<Door> doorList;
        List<Shop> shopList;


        public DoorAssortment()
        {
            InitializeComponent();

        }

        #region ToolBox
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.MainPage);
            GridSelectedDoor.Visibility = Visibility.Hidden;
            SearchTextBox.Text = "поиск...";
            SearchTextBox.Foreground.Opacity = 0.5;
            DoorListView.SelectedIndex = -1;
            Pages.DoorAssortment.DeleteDoor.Visibility = Visibility.Hidden;
            Pages.DoorAssortment.EditDoor.Visibility = Visibility.Hidden;

            chosenShop = null;
        }

        private void DoorPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.DoorPage);
            GridSelectedDoor.Visibility = Visibility.Hidden;
            SearchTextBox.Text = "поиск...";
            SearchTextBox.Foreground.Opacity = 0.5;
            DoorListView.SelectedIndex = -1;
            Pages.DoorAssortment.DeleteDoor.Visibility = Visibility.Hidden;
            Pages.DoorAssortment.EditDoor.Visibility = Visibility.Hidden;

            chosenShop = null;
        }

        private void ShopPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.ShopPage);
            GridSelectedDoor.Visibility = Visibility.Hidden;
            SearchTextBox.Text = "поиск...";
            SearchTextBox.Foreground.Opacity = 0.5;
            DoorListView.SelectedIndex = -1;
            Pages.DoorAssortment.DeleteDoor.Visibility = Visibility.Hidden;
            Pages.DoorAssortment.EditDoor.Visibility = Visibility.Hidden;

            chosenShop = null;
        }

        private void loginToolboxButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(Pages.LogIn);
            GridSelectedDoor.Visibility = Visibility.Hidden;
            SearchTextBox.Text = "поиск...";
            SearchTextBox.Foreground.Opacity = 0.5;
            DoorListView.SelectedIndex = -1;
            Pages.DoorAssortment.DeleteDoor.Visibility = Visibility.Hidden;
            Pages.DoorAssortment.EditDoor.Visibility = Visibility.Hidden;

            chosenShop = null;
        }

        #endregion

        const string DoorFilePath = "doors.json";
        
        private List<Shop> LoadShopData()
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

        const string ShopFilePath = "shops.json";

        //метод для выбора дверей, находящихся в выбранном магазине
        private List<Door> SelectedShopDoors()
        {
            doorList = LoadData();
            List<Door> _selectedShopDoors = new List<Door>();
            foreach (var door in doorList)
            {
                if (chosenShop.VandorCodeList.Contains(door.VandorCode))
                    _selectedShopDoors.Add(door);
            }
            return _selectedShopDoors;
        }

        //метод для определения магазинов, в которых имеется выбранная дверь
        private List<string> SelectedDoorShops(Door _door)
        {
            List<string> _selectedDoorShops = new List<string>();
            foreach (var shop in shopList)
            {
                if (shop.VandorCodeList.Contains(_door.VandorCode))
                    _selectedDoorShops.Add(shop.Address);
            }
            return _selectedDoorShops;
        }
        
        private void SaveShopData(List<Shop> _shopList)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Shop>));
            using (FileStream fs = new FileStream(ShopFilePath, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, _shopList);
            }
        }


        private List<Door> LoadData()
        {
            List<Door> _doorList;
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Door>));
            if (File.Exists(DoorFilePath))
            {
                using (FileStream fs = new FileStream(DoorFilePath, FileMode.Open))
                {
                    _doorList = (List<Door>)jsonFormatter.ReadObject(fs);
                    return _doorList;
                }
            }
            else
            {
                using (FileStream fs = new FileStream(DoorFilePath, FileMode.Create))
                {
                    _doorList = new List<Door>();
                    jsonFormatter.WriteObject(fs, _doorList);
                    return _doorList;
                }
            }
        }


        private void SaveData(List<Door> _doorList)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Door>));
            using (FileStream fs = new FileStream(DoorFilePath, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, _doorList);
            }
        }

        //вывод информации о выбранной двери
        private void DoorListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DoorListView.SelectedIndex != -1)
            {
                EditDoor.IsEnabled = true;
                DeleteDoor.IsEnabled = true;
                GridSelectedDoor.Visibility = Visibility.Visible;
                int index = DoorListView.SelectedIndex;
                VandorCodeBox.Text = doorList[index].VandorCode;
                HeightBox.Text = doorList[index].DoorHeight.ToString();
                WidthBox.Text = doorList[index].DoorWidth.ToString();
                ThicknessBox.Text = doorList[index].DoorThickness.ToString();
                SalePriceBox.Text = doorList[index].SalePrice.ToString();
                PurchasePriceBox.Text = doorList[index].PurchasePrice.ToString();
                EnterBox.Text = doorList[index].EnterOrRoom;
                GlassBox.Text = doorList[index].Glass;
                ColorBox.Text = doorList[index].DoorColor;
                ShopBox.ItemsSource = SelectedDoorShops(doorList[index]);
            }
            else
                GridSelectedDoor.Visibility = Visibility.Hidden;

        }
               
        //обновление данных в listbox'е при загрузке Grid
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            shopList = null;
            shopList = LoadShopData();
            doorList = null;
            DoorListView.ItemsSource = null;
            doorList = LoadData();

            if (chosenShop != null)
                doorList = SelectedShopDoors();

            DoorListView.ItemsSource = doorList;

        }

        //удаление экземпляра двери
        private void DeleteDoor_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show("Удалить дверь?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbResult == MessageBoxResult.OK)
            {
                List<Door> tempList = doorList;
                
                foreach (var shop in shopList)
                {
                    if(shop.VandorCodeList.Contains(tempList[DoorListView.SelectedIndex].VandorCode))
                        shop.VandorCodeList.Remove(tempList[DoorListView.SelectedIndex].VandorCode);
                }

                changes.SaveHistory($"Удалена дверь: {tempList[DoorListView.SelectedIndex].VandorCode}");
                tempList.Remove(tempList[DoorListView.SelectedIndex]);                
                SaveData(tempList);
                SaveShopData(shopList);
                doorList = LoadData();
                DoorListView.ItemsSource = null;
                DoorListView.ItemsSource = doorList;
                
            }
        }

        //изменение экземпляра двери
        private void EditDoor_Click(object sender, RoutedEventArgs e)
        {
            Pages.EditDoor.chosenDoor = doorList[DoorListView.SelectedIndex];
            NavigationService.Navigate(Pages.EditDoor);
        }

        //динамический поиск
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTextBox.Text != "поиск...")
            {
                if (chosenShop != null)
                    doorList = SelectedShopDoors();
                else
                    doorList = LoadData();

                List<Door> tempDoorList = new List<Door>();
                string temp = SearchTextBox.Text;
                foreach (var door in doorList)
                {
                    if (door.DoorColor.Contains(temp) || door.VandorCode.Contains(temp) || door.EnterOrRoom.Contains(temp))
                        tempDoorList.Add(door);
                }
                doorList = tempDoorList;
                DoorListView.ItemsSource = doorList;
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "")
            {
                SearchTextBox.Text = "поиск...";
                SearchTextBox.Foreground.Opacity = 0.5;
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text == "поиск...")
            {
                SearchTextBox.Text = "";
                SearchTextBox.Foreground.Opacity = 1;
            }
        }
    }
}
//