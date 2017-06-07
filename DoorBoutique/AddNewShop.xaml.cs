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
    /// Логика взаимодействия для AddNewShop.xaml
    /// </summary>
    public partial class AddNewShop : Page
    {
        List<Shop> shopList;
        ChangesHistory changes = new ChangesHistory();

        public AddNewShop()
        {
            InitializeComponent();
        }

        const string ShopFilePath = "../../shops.json";

        //метод для сериализации данных о магазинах
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

        //метод для десериализации данных о магазинах
        private void SaveData(List<Shop> _shopList)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Shop>));
            using (FileStream fs = new FileStream(ShopFilePath, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, _shopList);
            }
        }

        //выход со страницы
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (mbResult == MessageBoxResult.OK)
            {
                Pages.AddNewShop.NewAddressBox.Text = "";
                Pages.AddNewShop.NewDirectorBox.Text = "";
                Pages.AddNewShop.NewStorageAreaBox.Text = "";
                Pages.AddNewShop.NewTradeAreaBox.Text = "";
                NavigationService.Navigate(Pages.ShopPage);


            }
        }

        //добавление нового магазина
        private void NewShopConfirm_Click(object sender, RoutedEventArgs e)
        {
            shopList = LoadData();

            List<string> vandorCode = new List<string>();
            double newTradeArea;
            double newStorageArea;

            if (string.IsNullOrWhiteSpace(NewAddressBox.Text))
            {
                Pages.AddNewShop.NewAddressBox.Text = "";
                MessageBox.Show("Введите адрес", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewAddressBox.Focus();
                return;
            }

            if (!double.TryParse(NewTradeAreaBox.Text, out newTradeArea) || string.IsNullOrWhiteSpace(NewTradeAreaBox.Text))
            {
                Pages.AddNewShop.NewTradeAreaBox.Text = "";
                MessageBox.Show("Некорретное значение площади", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewTradeAreaBox.Focus();
                return;
            }

            if (!double.TryParse(NewStorageAreaBox.Text, out newStorageArea) || string.IsNullOrWhiteSpace(NewStorageAreaBox.Text))
            {
                Pages.AddNewShop.NewStorageAreaBox.Text = "";
                MessageBox.Show("Некорретное значение площади", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewStorageAreaBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(NewDirectorBox.Text))
            {
                Pages.AddNewShop.NewDirectorBox.Text = "";
                MessageBox.Show("Введите имя директора", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewDirectorBox.Focus();
                return;
            }

            shopList.Add(new Shop(NewAddressBox.Text, newTradeArea, newStorageArea, NewDirectorBox.Text, vandorCode));

            changes.SaveHistory($"Добавлен магазин по адресу: {NewAddressBox.Text}");
            MessageBox.Show("Магазин успешно добавлен", "Подтверждение", MessageBoxButton.OK, MessageBoxImage.Information);
            Pages.AddNewShop.NewAddressBox.Text = "";
            Pages.AddNewShop.NewDirectorBox.Text = "";
            Pages.AddNewShop.NewStorageAreaBox.Text = "";
            Pages.AddNewShop.NewTradeAreaBox.Text = "";
            NewAddressBox.Focus();

            SaveData(shopList);
        }
    }
}
