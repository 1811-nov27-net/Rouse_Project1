using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject1.Models
{
    public class OrderEntry
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int Pizza { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }

        public Pizza PizzaDetails { get; set; }
        public Order OrderDetails { get; set; }
        public Location LocationDetails { get; set; }
    }
}
