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
    }
}
