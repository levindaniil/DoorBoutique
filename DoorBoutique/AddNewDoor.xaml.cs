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
    /// Логика взаимодействия для AddNewDoor.xaml
    /// </summary>
    public partial class AddNewDoor : Page
    {
        List<Door> doorList;
        ChangesHistory changes = new ChangesHistory();
        List<string> glasstype = new List<string>();
        List<string> enterOrRoom = new List<string>();
        public AddNewDoor()
        {
            InitializeComponent();

            glasstype.Add("Глухое полотно");
            glasstype.Add("Со стеклом");

            enterOrRoom.Add("Входная");
            enterOrRoom.Add("Межкомнатная");

            NewEnterOrRoomBox.ItemsSource = enterOrRoom;
            NewGlassBox.ItemsSource = glasstype;

        }

        const string DoorFilePath = "../../doors.json";

        //метод для сериализации данных о дверях
        private void SaveData(List<Door> _doorList)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<Door>));
            using (FileStream fs = new FileStream(DoorFilePath, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, _doorList);
            }
        }

        //метод для десериализации данных о дверях
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

        //выход со страницы
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (mbResult == MessageBoxResult.OK)
            {
                NavigationService.Navigate(Pages.DoorPage);
                Pages.AddNewDoor.NewVandorCodeBox.Text = "";
                Pages.AddNewDoor.NewHeightBox.Text = "";
                Pages.AddNewDoor.NewWidthBox.Text = "";
                Pages.AddNewDoor.NewThicknessBox.Text = "";
                Pages.AddNewDoor.NewEnterOrRoomBox.SelectedIndex = -1;
                Pages.AddNewDoor.NewGlassBox.SelectedIndex = -1;
                Pages.AddNewDoor.NewColorBox.Text = "";
                Pages.AddNewDoor.NewSalePriceBox.Text = "";
                Pages.AddNewDoor.NewPurchasePrice.Text = "";

            }
        }

        //добавление нового экземпляра двери
        private void NewDoorConfirm_Click(object sender, RoutedEventArgs e)
        {
            doorList = LoadData();

            double newHeight;
            double newWidth;
            double newThickness;
            double newSalePrice;
            double newPurchasePrice;

            List<string> existingVandorCodes = new List<string>();
            foreach (var door in doorList)
            {
                existingVandorCodes.Add(door.VandorCode);
            }

            if (string.IsNullOrWhiteSpace(NewVandorCodeBox.Text))
            {
                Pages.AddNewDoor.NewVandorCodeBox.Text = "";
                MessageBox.Show("Введите артикул", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewVandorCodeBox.Focus();
                return;
            }
            if (existingVandorCodes.Contains(NewVandorCodeBox.Text))
            {
                Pages.AddNewDoor.NewVandorCodeBox.Text = "";
                MessageBox.Show("Дверь с заданным артикулом уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewVandorCodeBox.Focus();
                return;
            }


            if (!double.TryParse(NewHeightBox.Text, out newHeight) || string.IsNullOrWhiteSpace(NewHeightBox.Text))
            {
                Pages.AddNewDoor.NewHeightBox.Text = "";
                MessageBox.Show("Некорретное значение высоты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewHeightBox.Focus();
                return;
            }

            if (!double.TryParse(NewWidthBox.Text, out newWidth) || string.IsNullOrWhiteSpace(NewWidthBox.Text))
            {
                Pages.AddNewDoor.NewWidthBox.Text = "";
                MessageBox.Show("Некорретное значение ширины", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewWidthBox.Focus();
                return;
            }

            if (!double.TryParse(NewThicknessBox.Text, out newThickness) || string.IsNullOrWhiteSpace(NewThicknessBox.Text))
            {
                Pages.AddNewDoor.NewThicknessBox.Text = "";
                MessageBox.Show("Некорретное значение толщины", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewThicknessBox.Focus();
                return;
            }

            if (NewEnterOrRoomBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тип двери", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NewGlassBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тип двери", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewColorBox.Text))
            {
                Pages.AddNewDoor.NewColorBox.Text = "";
                MessageBox.Show("Введите цвет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewColorBox.Focus();
                return;
            }

            if (!double.TryParse(NewSalePriceBox.Text, out newSalePrice) || string.IsNullOrWhiteSpace(NewSalePriceBox.Text))
            {
                Pages.AddNewDoor.NewSalePriceBox.Text = "";
                MessageBox.Show("Некорретное значение цены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewSalePriceBox.Focus();
                return;
            }

            if (!double.TryParse(NewPurchasePrice.Text, out newPurchasePrice) || string.IsNullOrWhiteSpace(NewPurchasePrice.Text))
            {
                Pages.AddNewDoor.NewPurchasePrice.Text = "";
                MessageBox.Show("Некорретное значение цены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                NewPurchasePrice.Focus();
                return;
            }

            doorList.Add(new Door(NewVandorCodeBox.Text, newHeight, newWidth, newThickness, NewEnterOrRoomBox.Text,
                NewGlassBox.Text, NewColorBox.Text, newSalePrice, newPurchasePrice));

            changes.SaveHistory($"Добавлена дверь: {NewVandorCodeBox.Text}");
            SaveData(doorList);
            existingVandorCodes = null;
            MessageBox.Show("Товар успешно добавлен", "Подтверждение", MessageBoxButton.OK, MessageBoxImage.Information);
            Pages.AddNewDoor.NewVandorCodeBox.Text = "";
            Pages.AddNewDoor.NewHeightBox.Text = "";
            Pages.AddNewDoor.NewWidthBox.Text = "";
            Pages.AddNewDoor.NewThicknessBox.Text = "";
            Pages.AddNewDoor.NewEnterOrRoomBox.SelectedIndex = -1;
            Pages.AddNewDoor.NewGlassBox.SelectedIndex = -1;
            Pages.AddNewDoor.NewColorBox.Text = "";
            Pages.AddNewDoor.NewSalePriceBox.Text = "";
            Pages.AddNewDoor.NewPurchasePrice.Text = "";

            NewVandorCodeBox.Focus();
        }
    }
}
