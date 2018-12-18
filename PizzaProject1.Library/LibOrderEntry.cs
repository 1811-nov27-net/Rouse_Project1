using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaProject1.Library
{
    public class LibOrderEntry
    {
        private int _id;
        private int _orderId;
        private int _PizzaId;
        private int _quantity;
        private decimal _subtotal;

        public int Id
        {
            get => _id;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Order entry:  ID is missing -", nameof(value));
                }
                _id = value;
            }
        }


        public int OrderId
        {
            get => _orderId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Order entry:  Associated order ID is missing -", nameof(value));
                }
                _orderId = value;
            }
        }


        public int PizzaId
        {
            get => _PizzaId;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Order entry:  Associated pizza ID is missing -", nameof(value));
                }
                _PizzaId = value;
            }
        }


        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Order entry:  Quantity is missing -", nameof(value));
                }
                _quantity = value;
            }
        }


        public decimal Subtotal
        {
            get => _subtotal;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("- Order entry:  Subtotal is missing -", nameof(value));
                }
                _subtotal = value;
            }
        }


        public LibOrder ReferencedOrder { get; set; } = new LibOrder();
        public LibPizza ReferencedPizza { get; set; } = new LibPizza();
    }
}
