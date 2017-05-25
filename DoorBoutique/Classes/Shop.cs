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
    public class Shop
    {
        private string _address;
        [DataMember]
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private double _tradeArea;
        [DataMember]
        public double TradeArea
        {
            get { return _tradeArea; }
            set { _tradeArea = value; }
        }

        private double _storageArea;
        [DataMember]
        public double StorageArea
        {
            get { return _storageArea; }
            set { _storageArea = value; }
        }

        private string _directorName;
        [DataMember]
        public string Director
        {
            get { return _directorName; }
            set { _directorName = value; }
        }

        private List<string> _vandorCodeList;
        [DataMember]
        public List<string> VandorCodeList
        {
            get { return _vandorCodeList; }
            set { _vandorCodeList = value; }
        }



        public Shop(string address, double tradeArea, double storageArea, string directorName, List<string> vandorCodeList)
        {
            _address = address;
            _tradeArea = tradeArea;
            _storageArea = storageArea;
            _directorName = directorName;
            _vandorCodeList = vandorCodeList;
        }
    }
}
