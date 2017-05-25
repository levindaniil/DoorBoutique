using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorBoutique
{
    static class Pages
    {
        private static DoorPage _doorPage = new DoorPage();
        private static ShopPage _shopPage = new ShopPage();
        private static MainPage _mainpage = new MainPage();
        private static LogIn _login = new LogIn();
        private static DoorAssortment _doorAssortment = new DoorAssortment();
        private static AddNewDoor _addNewDoor = new AddNewDoor();
        private static AddNewShop _addNewShop = new AddNewShop();
        private static ShopCatalog _shopCatalog = new ShopCatalog();
        private static EditShop _editShop = new EditShop();
        private static EditDoor _editDoor = new EditDoor();
        private static VandorPage _vandorPage = new VandorPage();

        public static DoorPage DoorPage
        {
            get
            {
                return _doorPage;
            }
        }
        public static ShopPage ShopPage
        {
            get
            {
                return _shopPage;
            }
        }

        public static MainPage MainPage
        {
            get
            {
                return _mainpage;
            }
        }

        public static LogIn LogIn
        {
            get
            {
                return _login;
            }
        }
        public static DoorAssortment DoorAssortment
        {
            get
            {
                return _doorAssortment;
            }
        }
        public static AddNewDoor AddNewDoor
        {
            get
            {
                return _addNewDoor;
            }
        }
        public static AddNewShop AddNewShop
        {
            get
            {
                return _addNewShop;
            }
        }
        public static ShopCatalog ShopCatalog
        {
            get
            {
                return _shopCatalog;
            }
        }
        public static EditShop EditShop
        {
            get
            {
                return _editShop;
            }
        }
        public static EditDoor EditDoor
        {
            get
            {
                return _editDoor;
            }
        }
        public static VandorPage VandorPage
        {
            get
            {
                return _vandorPage;
            }
        }
    }
}
