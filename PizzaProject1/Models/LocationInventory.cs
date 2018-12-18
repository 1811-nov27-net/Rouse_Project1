using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject1.Models
{
    public class LocationInventory
    {
        public int Id { get; set; }
        public int Location { get; set; }
        public int Topping { get; set; }
        public int Quantity { get; set; }

        public Topping ToppingDetails { get; set; }
    }
}
