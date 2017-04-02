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
using System.Windows.Shapes;

namespace DoorBoutique
{
    /// <summary>
    /// Логика взаимодействия для AddNewDoor.xaml
    /// </summary>
    public partial class AddNewDoor : Window
    {
        List<string> glasstype = new List<string>();
        List<string> enterOrRoom = new List<string>();

        public AddNewDoor()
        {
            glasstype.Add("глухое полотно");
            glasstype.Add("со стеклом");

            enterOrRoom.Add("входная");
            enterOrRoom.Add("межкомнатная");

            InitializeComponent();

            comboBoxGlass.ItemsSource = glasstype;
            comboBoxType.ItemsSource = enterOrRoom;
        }

        Door _newDoor;

        public Door NewDoor
        {
            get
            {
                return _newDoor;
            }
        }

        private void AddDoorButton_Click(object sender, RoutedEventArgs e)
        {
            double height = double.Parse(textBoxHeight.Text);
            double width = double.Parse(textBoxWidth.Text);
            double thickness = double.Parse(textBoxThickness.Text);
            bool glass = false;
            if (comboBoxGlass.SelectedIndex == 0)
            {
                glass = true;
            }
            bool type = false;
            if (comboBoxType.SelectedIndex == 0)
            {
                type = true;
            }
            string color = textBoxColor.Text;
            double purchase = double.Parse(textBoxPurchase.Text);
            double sale = double.Parse(textBoxSale.Text);
            _newDoor = new Door(height, width, thickness, type, glass, color, purchase, sale);

            DialogResult = true;
        }
    }
}
