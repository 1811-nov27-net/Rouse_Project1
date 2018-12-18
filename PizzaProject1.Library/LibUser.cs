using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaProject1.Library
{
    public class LibUser
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private int _defaultLocation;

        public int Id
        {
            get => _id;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException(" - User:  ID is missing -", nameof(value));
                }
                _id = value;
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("- User:  First name is missing -", nameof(value));
                }
                _firstName = value;
            }
        }


        public string LastName
        {
            get => _lastName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("- User: Last name is missing -", nameof(value));
                }
                _lastName = value;
            }
        }


        public int DefaultLocation
        {
            get => _defaultLocation;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException(" - Default:  Associated location ID is missing -", nameof(value));
                }
                _defaultLocation = value;
            }
        }


        public LibLocation ReferencedLocation { get; set; } = new LibLocation();

        public List<LibOrder> Orders { get; set; } = new List<LibOrder>();


    }
}
