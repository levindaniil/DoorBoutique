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
    /// Логика взаимодействия для VandorPage.xaml
    /// </summary>
    public partial class VandorPage : Page
    {
        ChangesHistory changes = new ChangesHistory();
        public Shop chosenShop;
        List<string> otherVandors;
        List<Shop> shopList;
        List<Door> doorList;

        public VandorPage()
        {
            InitializeComponent();
        }

        const string ShopFilePath = "../../shops.json";

        const string DoorFilePath = "../../doors.json";

        private List<Door> LoadDoorData()
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

        private void SaveShopData(List<Shop> _shopList)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Shop>));
            using (FileStream fs = new FileStream(ShopFilePath, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, _shopList);
            }
        }

        //метод для определения дверей, не продающихся в выбранном магазине
        private List<string> otherVandorList()
        {
            List<string> _otherVandors = new List<string>();
            foreach (var door in doorList)
            {
                if (!chosenShop.VandorCodeList.Contains(door.VandorCode))
                    _otherVandors.Add(door.VandorCode);
            }
            return _otherVandors;
        }
             
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {          
            ShopName.Text = chosenShop.Address;
            doorList = LoadDoorData();
            shopList = LoadShopData();
            ContainedVandors.ItemsSource = chosenShop.VandorCodeList;
            otherVandors = otherVandorList();
            NewVandors.ItemsSource = otherVandors;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ContainedVandors.SelectedIndex = -1;
            NewVandors.SelectedIndex = -1;

            MessageBoxResult mbResult = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (mbResult == MessageBoxResult.OK)
            {
                foreach (var shop in shopList)
                {
                    if (shop.Address == chosenShop.Address)
                    {
                        shop.VandorCodeList = chosenShop.VandorCodeList;
                    }
                }
                SaveShopData(shopList);
                NavigationService.Navigate(Pages.ShopCatalog);
            }
        }

        //удаление выбранной двери
        private void ContainedVandors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ContainedVandors.SelectedIndex != -1)
            {
                string removedVandor = chosenShop.VandorCodeList[ContainedVandors.SelectedIndex];
                chosenShop.VandorCodeList.Remove(removedVandor);
                otherVandors.Add(removedVandor);
                ContainedVandors.ItemsSource = null;
                ContainedVandors.ItemsSource = chosenShop.VandorCodeList;
                NewVandors.ItemsSource = null;
                NewVandors.ItemsSource = otherVandors;
                changes.SaveHistory($"Из магазина по адресу {chosenShop.Address} удалена дверь {removedVandor}");
            }
        }

        //добавление выбранной двери
        private void NewVandors_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (NewVandors.SelectedIndex != -1)
            {                
                string addedVandor = otherVandors[NewVandors.SelectedIndex];
                chosenShop.VandorCodeList.Add(addedVandor);
                otherVandors.Remove(addedVandor);
                ContainedVandors.ItemsSource = null;
                ContainedVandors.ItemsSource = chosenShop.VandorCodeList;
                NewVandors.ItemsSource = null;
                NewVandors.ItemsSource = otherVandors;
                changes.SaveHistory($"В магазин по адресу {chosenShop.Address} добавлена дверь {addedVandor}");
            }

        }
    }
}
