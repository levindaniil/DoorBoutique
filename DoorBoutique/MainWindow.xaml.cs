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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Door> _doors = new List<Door>();

        public MainWindow()
        {
            InitializeComponent();


        }

        private void AddNewDoorButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddNewDoor();
            if (window.ShowDialog().Value)
            {
                _doors.Add(window.NewDoor);
            }
        }
    }
}
