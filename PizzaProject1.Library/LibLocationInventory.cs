using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaProject1.Library
{
    public class LibLocationInventory
    {
        private int _id;
        private int _locationId;
        private int _toppingId;
        private int _quantity;

        public int Id
        {
            get => _id;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Location inventory:  ID is missing -", nameof(value));
                }
                _id = value;
            }
        }


        public int LocationId
        {
            get => _locationId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Location inventory:  Associated location ID is missing -", nameof(value));
                }
                _locationId = value;
            }
        }


        public int ToppingId
        {
            get => _toppingId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Location inventory:  ID is missing -", nameof(value));
                }
                _toppingId = value;
            }
        }


        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Location inventory:  ID is missing -", nameof(value));
                }
                _quantity = value;
            }
        }


        public LibLocation ReferencedLocation { get; set; } = new LibLocation();
        public LibTopping ReferencedTopping { get; set; } = new LibTopping();
    }
}
