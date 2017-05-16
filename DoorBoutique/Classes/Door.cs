using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DoorBoutique
{    
    public class Door
    {
        public const double DoorHeightDefault = 2060;

        private string _vandorCode;

        public string VandorCode
        {
            get { return _vandorCode; }
            set { _vandorCode = value; }
        }


        private double _doorHeight;

        public double DoorHeight
        {
            get { return _doorHeight; }
            set { _doorHeight = value; }
        }
        private double _doorWidth;

        public double DoorWidth
        {
            get { return _doorWidth; }
            set { _doorWidth = value; }
        }

        private double _doorThickness;

        public double DoorThickness
        {
            get { return _doorThickness; }
            set { _doorThickness = value; }
        }

        private bool _enterOrRoom;

        public bool EnterOrRoom
        {
            get { return _enterOrRoom; }
            set { _enterOrRoom = value; }
        }

        private bool _glass;

        public bool Glass
        {
            get { return _glass; }
            set { _glass = value; }
        }

        private string _doorColor;

        public string DoorColor
        {
            get { return _doorColor; }
            set { _doorColor = value; }
        }

        private double _purchasePrice;

        public double PurchasePrice
        {
            get { return _purchasePrice; }
            set { _purchasePrice = value; }
        }

        private double _salePrice;

        public double SalePrice
        {
            get { return _salePrice; }
            set { _salePrice = value; }
        }

        private List<Shop> _shopList;

        public List<Shop> ShopList
        {
            get { return _shopList; }
            set { _shopList = value; }
        }


        public Door(string vandorCode, double doorHeight, double doorWidth, double doorThickness, bool enterOrRoom, bool glass, string doorColor, double purchasePrice, double salePrice, List<Shop> shopList)
        {
            _vandorCode = vandorCode;
            _doorHeight = doorHeight;
            _doorWidth = doorWidth;
            _doorThickness = doorThickness;
            _enterOrRoom = enterOrRoom;
            _glass = glass;
            _doorColor = doorColor;
            _purchasePrice = purchasePrice;
            _salePrice = salePrice;
            _shopList = shopList;
        }

        public Door(string vandorCode, double doorWidth, double doorThickness, bool enterOrRoom, bool glass, string doorColor, double purchasePrice, double salePrice, List<Shop> shopList)
            : this(vandorCode, DoorHeightDefault, doorWidth, doorThickness, enterOrRoom, glass, doorColor, purchasePrice, salePrice, shopList)
        {

        }
    }
}
