using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace DoorBoutique
{    
    [DataContract]
    public class Door
    {
        private string _vandorCode;
        [DataMember]
        public string VandorCode
        {
            get { return _vandorCode; }
            set { _vandorCode = value; }
        }


        private double _doorHeight;
        [DataMember]
        public double DoorHeight
        {
            get { return _doorHeight; }
            set { _doorHeight = value; }
        }
        private double _doorWidth;
        [DataMember]
        public double DoorWidth
        {
            get { return _doorWidth; }
            set { _doorWidth = value; }
        }

        private double _doorThickness;
        [DataMember]
        public double DoorThickness
        {
            get { return _doorThickness; }
            set { _doorThickness = value; }
        }

        private string _enterOrRoom;
        [DataMember]
        public string EnterOrRoom
        {
            get { return _enterOrRoom; }
            set { _enterOrRoom = value; }
        }

        private string _glass;
        [DataMember]
        public string Glass
        {
            get { return _glass; }
            set { _glass = value; }
        }

        private string _doorColor;
        [DataMember]
        public string DoorColor
        {
            get { return _doorColor; }
            set { _doorColor = value; }
        }

        private double _purchasePrice;
        [DataMember]
        public double PurchasePrice
        {
            get { return _purchasePrice; }
            set { _purchasePrice = value; }
        }

        private double _salePrice;
        [DataMember]
        public double SalePrice
        {
            get { return _salePrice; }
            set { _salePrice = value; }
        }

        public Door(string vandorCode, double doorHeight, double doorWidth, double doorThickness, string enterOrRoom, string glass, string doorColor, double salePrice, double purchasePrice)
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
        }
    }
}
