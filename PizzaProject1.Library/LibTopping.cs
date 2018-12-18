using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaProject1.Library
{
    public class LibTopping
    {
        private int _id;
        private string _name;

        /// <summary>
        /// Integer ID of the topping.  0 (zero) indicates no ID was returned by the database.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException(" - Topping:  ID is missing -", nameof(value));
                }
                _id = value;
            }
        }

        /// <summary>
        /// Name of the topping. Required, cannot by empty.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("- Topping:  Name is missing -", nameof(value));
                }
                _name = value;
            }
        }

        public List<LibLocationInventory> LocationInventories { get; set; } = new List<LibLocationInventory>();
        public List<LibPizzaTopping> PizzaToppings { get; set; } = new List<LibPizzaTopping>();
    }
}
