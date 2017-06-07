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
    /// Логика взаимодействия для EditShop.xaml
    /// </summary>
    public partial class EditShop : Page
    {
        ChangesHistory changes = new ChangesHistory();
        public Shop chosenShop;
        List<Shop> shopList;
        public EditShop()
        {
            InitializeComponent();
        }

        const string ShopFilePath = "../../shops.json";

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

        //заполнение textbox'ов информацией о выбранном магазине
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Pages.EditShop.EditAddressBox.Text = chosenShop.Address;
            Pages.EditShop.EditStorageAreaBox.Text = chosenShop.StorageArea.ToString();
            Pages.EditShop.EditTradeAreaBox.Text = chosenShop.TradeArea.ToString();
            Pages.EditShop.EditDirectorBox.Text = chosenShop.Director;
        }

        //отмена изменения
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (mbResult == MessageBoxResult.OK)
            {
                NavigationService.Navigate(Pages.ShopCatalog);
                Pages.EditShop.EditAddressBox.Text = "";
                Pages.EditShop.EditDirectorBox.Text = "";
                Pages.EditShop.EditStorageAreaBox.Text = "";
                Pages.EditShop.EditTradeAreaBox.Text = "";
            }
        }

        //сохранение изменения информации о магазине
        private void EditShopConfirm_Click(object sender, RoutedEventArgs e)
        {
            shopList = LoadData();

            MessageBoxResult mbResult = MessageBox.Show("Сохранить изменения?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbResult == MessageBoxResult.OK)
            {
                shopList = LoadData();

                List<string> vandorCode = new List<string>();
                double editTradeArea;
                double editStorageArea;

                if (!double.TryParse(EditTradeAreaBox.Text, out editTradeArea) || string.IsNullOrWhiteSpace(EditTradeAreaBox.Text))
                {
                    Pages.EditShop.EditTradeAreaBox.Text = "";
                    MessageBox.Show("Некорретное значение площади", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    EditTradeAreaBox.Focus();
                    return;
                }

                if (!double.TryParse(EditStorageAreaBox.Text, out editStorageArea) || string.IsNullOrWhiteSpace(EditStorageAreaBox.Text))
                {
                    Pages.EditShop.EditStorageAreaBox.Text = "";
                    MessageBox.Show("Некорретное значение площади", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    EditStorageAreaBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(EditDirectorBox.Text))
                {
                    Pages.EditShop.EditDirectorBox.Text = "";
                    MessageBox.Show("Введите имя директора", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    EditDirectorBox.Focus();
                    return;
                }

                Shop newShop = new Shop("", 0, 0, "", new List<string>());

                if (chosenShop.VandorCodeList == null || chosenShop.VandorCodeList.Count == 0)
                    newShop.VandorCodeList = new List<string>();
                else
                    newShop.VandorCodeList = chosenShop.VandorCodeList;
                newShop.Address = EditAddressBox.Text;
                newShop.Director = EditDirectorBox.Text;
                newShop.StorageArea = editStorageArea;
                newShop.TradeArea = editTradeArea;


                Shop shopToEdit = null;
                foreach (var shop in shopList)
                {
                    if ((shop.Address == chosenShop.Address) && (shop.StorageArea == chosenShop.StorageArea) && (shop.TradeArea == chosenShop.TradeArea) && (shop.Director == chosenShop.Director))
                        shopToEdit = shop;
                }

                if (shopToEdit != null)
                    shopList.Remove(shopToEdit);

                shopList.Add(newShop);

                changes.SaveHistory($"Отредактирована информация о магазине по адресу: {chosenShop.Address}");
                SaveData(shopList);
                NavigationService.Navigate(Pages.ShopCatalog);
            }
        }
    }
}
