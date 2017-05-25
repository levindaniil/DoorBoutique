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
    /// Логика взаимодействия для EditDoor.xaml
    /// </summary>
    public partial class EditDoor : Page
    {
        public Door chosenDoor;
        ChangesHistory changes = new ChangesHistory();
        List<string> glasstype = new List<string>();
        List<string> enterOrRoom = new List<string>();

        List<Door> doorList;

        public EditDoor()
        {
            InitializeComponent();

            glasstype.Add("Глухое полотно");
            glasstype.Add("Со стеклом");

            enterOrRoom.Add("Входная");
            enterOrRoom.Add("Межкомнатная");

            EditEnterOrRoomBox.ItemsSource = enterOrRoom;
            EditGlassBox.ItemsSource = glasstype;

        }

        const string DoorFilePath = "doors.json";

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

        //заполнение textbox'ов информацией о выбранной двери
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            EditVandorCodeBox.Text = chosenDoor.VandorCode;
            EditHeightBox.Text = chosenDoor.DoorHeight.ToString();
            EditWidthBox.Text = chosenDoor.DoorWidth.ToString();
            EditThicknessBox.Text = chosenDoor.DoorThickness.ToString();
            EditSalePriceBox.Text = chosenDoor.SalePrice.ToString();
            EditPurchasePrice.Text = chosenDoor.PurchasePrice.ToString();
            EditEnterOrRoomBox.Text = chosenDoor.EnterOrRoom;
            EditGlassBox.Text = chosenDoor.Glass;
            EditColorBox.Text = chosenDoor.DoorColor;
        }
        
        //отмена изменения
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (mbResult == MessageBoxResult.OK)
            {
                NavigationService.Navigate(Pages.DoorAssortment);
                Pages.EditDoor.EditVandorCodeBox.Text = "";
                Pages.EditDoor.EditHeightBox.Text = "";
                Pages.EditDoor.EditWidthBox.Text = "";
                Pages.EditDoor.EditThicknessBox.Text = "";
                Pages.EditDoor.EditEnterOrRoomBox.SelectedIndex = -1;
                Pages.EditDoor.EditGlassBox.SelectedIndex = -1;
                Pages.EditDoor.EditColorBox.Text = "";
                Pages.EditDoor.EditSalePriceBox.Text = "";
                Pages.EditDoor.EditPurchasePrice.Text = "";
                Pages.DoorAssortment.SearchTextBox.Text = "поиск...";
                Pages.DoorAssortment.SearchTextBox.Foreground.Opacity = 0.5;
            }
        }

        //сохранение изменения информации о двери
        private void EditDoorConfirm_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show("Сохранить изменения?", "Подтверждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (mbResult == MessageBoxResult.OK)
            {
                doorList = LoadData();

                double editHeight;
                double editWidth;
                double editThickness;
                double editSalePrice;
                double editPurchasePrice;



                if (!double.TryParse(EditHeightBox.Text, out editHeight) || string.IsNullOrWhiteSpace(EditHeightBox.Text))
                {
                    Pages.EditDoor.EditHeightBox.Text = "";
                    MessageBox.Show("Некорретное значение высоты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    EditHeightBox.Focus();
                    return;
                }

                if (!double.TryParse(EditWidthBox.Text, out editWidth) || string.IsNullOrWhiteSpace(EditWidthBox.Text))
                {
                    Pages.EditDoor.EditWidthBox.Text = "";
                    MessageBox.Show("Некорретное значение ширины", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    EditWidthBox.Focus();
                    return;
                }

                if (!double.TryParse(EditThicknessBox.Text, out editThickness) || string.IsNullOrWhiteSpace(EditThicknessBox.Text))
                {
                    Pages.EditDoor.EditThicknessBox.Text = "";
                    MessageBox.Show("Некорретное значение толщины", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    EditThicknessBox.Focus();
                    return;
                }

                if (EditEnterOrRoomBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите тип двери", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (EditGlassBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберите тип двери", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(EditColorBox.Text))
                {
                    Pages.EditDoor.EditColorBox.Text = "";
                    MessageBox.Show("Введите цвет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    EditColorBox.Focus();
                    return;
                }

                if (!double.TryParse(EditSalePriceBox.Text, out editSalePrice) || string.IsNullOrWhiteSpace(EditSalePriceBox.Text))
                {
                    Pages.EditDoor.EditSalePriceBox.Text = "";
                    MessageBox.Show("Некорретное значение цены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    EditSalePriceBox.Focus();
                    return;
                }

                if (!double.TryParse(EditPurchasePrice.Text, out editPurchasePrice) || string.IsNullOrWhiteSpace(EditPurchasePrice.Text))
                {
                    Pages.EditDoor.EditPurchasePrice.Text = "";
                    MessageBox.Show("Некорретное значение цены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    EditPurchasePrice.Focus();
                    return;
                }

                foreach (var door in doorList)
                {
                    if (door.VandorCode == EditVandorCodeBox.Text)
                    {
                        door.DoorHeight = editHeight;
                        door.DoorWidth = editWidth;
                        door.DoorThickness = editThickness;
                        door.DoorColor = EditColorBox.Text;
                        door.EnterOrRoom = EditEnterOrRoomBox.Text;
                        door.Glass = EditGlassBox.Text;
                        door.SalePrice = editSalePrice;
                        door.PurchasePrice = editPurchasePrice;
                    }
                }

                changes.SaveHistory($"Отредактирована информация о двери: {EditVandorCodeBox.Text}");

                Pages.DoorAssortment.SearchTextBox.Text = "поиск...";
                Pages.DoorAssortment.SearchTextBox.Foreground.Opacity = 0.5;
                SaveData(doorList);
                NavigationService.Navigate(Pages.DoorAssortment);
            }
        }
    }
}
