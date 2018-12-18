using System;
using System.Collections.Generic;

namespace PizzaProject1.Library
{
    public class LibPizza
    {
        private int _id;
        private string _name;
        private decimal _price;

        /// <summary>
        /// Integer ID of the pizza.  0 (zero) indicates no ID was returned by the database.
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException(" - Pizza:  ID is missing -", nameof(value));
                }
                _id = value;
            }
        }

        /// <summary>
        /// Name of the pizza. Required, cannot by empty.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("- Pizza:  Name is missing -", nameof(value));
                }
                _name = value;
            }
        }

        /// <summary>
        /// Price of the pizza.  0 (zero) indicates no price was returned by the database, which is not allowed.
        /// </summary>
        public decimal Price
        {
            get => _price;

            set
            {
                if (value == 0)
                {
                    throw new ArgumentException(" - Pizza price is missing -", nameof(value));
                }
                _price = value;
            }
        }

        public List<LibOrderEntry> OrderEntries { get; set; } = new List<LibOrderEntry>();
        public List<LibPizzaTopping> PizzaToppings { get; set; } = new List<LibPizzaTopping>();
    }
}
