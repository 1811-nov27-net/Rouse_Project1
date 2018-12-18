using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaProject1.Library
{
    public class LibLocation
    {
        private int _id;
        private string _city;
        private string _state;

        public int Id
        {
            get => _id;
            set
            {
                if(value == 0)
                {
                    throw new ArgumentException(" - Location:  ID is missing -", nameof(value));
                }
                _id = value;
            }
        }


        public string City
        {
            get => _city;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("- Location:  City is missing -", nameof(value));
                }
                _city = value;
            }
        }


        public string State
        {
            get => _state;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("- Location:  State is missing -", nameof(value));
                }
                _state = value;
            }
        }

        public List<LibLocationInventory> LocationInventories { get; set; } = new List<LibLocationInventory>();
        public List<LibOrder> Orders { get; set; } = new List<LibOrder>();
        public List<LibUser> Users { get; set; } = new List<LibUser>();

    }
}
