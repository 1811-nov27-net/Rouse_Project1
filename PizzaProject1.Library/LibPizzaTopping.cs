using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaProject1.Library
{
    public class LibPizzaTopping
    {
        private int _id;
        private int _pizzaId;
        private int _toppingId;
        private int _quantity;

        public int Id
        {
            get => _id;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Pizza topping:  ID is missing -", nameof(value));
                }
                _id = value;
            }
        }


        public int PizzaId
        {
            get => _pizzaId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Pizza topping:  Associated pizza ID is missing -", nameof(value));
                }
                _pizzaId = value;
            }
        }


        public int ToppingId
        {
            get => _toppingId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Pizza topping:  Associated topping ID is missing -", nameof(value));
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
                    throw new ArgumentException("- Pizza topping:  Quantity is missing -", nameof(value));
                }
                _quantity = value;
            }
        }

        public LibPizza ReferencedPizza { get; set; } = new LibPizza();
        public LibTopping ReferencedTopping { get; set; } = new LibTopping();
    }
}
