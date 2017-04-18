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
    /// Логика взаимодействия для AddNewShop.xaml
    /// </summary>
    public partial class AddNewShop : Window
    {
        public AddNewShop()
        {
            InitializeComponent();
        }

        Shop _newShop;

        public Shop NewShop
        {
            get
            {
                return _newShop;
            }
        }

        private void AddDoorButton_Click(object sender, RoutedEventArgs e)
        {
            string address = textBoxAddress.Text;
            double tradeArea = double.Parse(textBoxTradeArea.Text);
            double storageArea = double.Parse(textBoxStorageArea.Text);
            string name = textBoxDirectorsName.Text;
            _newShop = new Shop(address, tradeArea, storageArea, name, 0);

            DialogResult = true;
        }
    }
}
