﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DoorBoutique
{
    public class Shop
    {
        public const string ShopName = "Мир дверей";

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        private double _tradeArea;

        public double TradeArea
        {
            get { return _tradeArea; }
            set { _tradeArea = value; }
        }

        private double _storageArea;

        public double StorageArea
        {
            get { return _storageArea; }
            set { _storageArea = value; }
        }

        private string _directorName;

        public string Director
        {
            get { return _directorName; }
            set { _directorName = value; }
        }

        private int _amountOfDoors;

        public int AmountOfDoors
        {
            get { return _amountOfDoors; }
            set { _amountOfDoors = value; }
        }

        private List<Door> _doorList;

        public List<Door> DoorList
        {
            get { return _doorList; }
            set { _doorList = value; }
        }
        
        public Shop(string address, double tradeArea, double storageArea, string directorName, int amountOfDoors, List<Door> doorList)
        {
            _address = address;
            _tradeArea = tradeArea;
            _storageArea = storageArea;
            _directorName = directorName;
            _amountOfDoors = amountOfDoors;
            _doorList = doorList;
        }
    }
}