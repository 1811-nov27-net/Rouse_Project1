using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaProject1.Library
{
    public class LibOrder
    {
        private int _id;
        private int _userId;
        private int _locationId;
        private DateTime _time;

        public int Id
        {
            get => _id;
            set
            {
                if(value == 0)
                {
                    throw new ArgumentException("- Order:  ID is missing -", nameof(value));
                }
                _id = value;
            }
        }


        public int UserId
        {
            get => _userId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Order:  Associated user ID is missing -", nameof(value));
                }
                _userId = value;
            }
        }


        public int LocationId
        {
            get => _locationId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Order:  Associated location ID is missing -", nameof(value));
                }
                _locationId = value;
            }
        }


        public DateTime Time
        {
            get => _time;
            set
            {
                if (value == default(DateTime))
                {
                    throw new ArgumentException("- Order:  Time is missing -", nameof(value));
                }
                _time = value;
            }
        }


        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }



        public LibLocation ReferencedLocation { get; set; } = new LibLocation();

        public LibUser ReferencedUser { get; set; } = new LibUser();

        public List<LibOrderEntry> OrderEntries { get; set; } = new List<LibOrderEntry>();
    }
}
