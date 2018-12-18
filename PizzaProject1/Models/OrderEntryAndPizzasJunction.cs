using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject1.Models
{
    public class OrderEntryAndPizzasJunction
    {
        public IEnumerable<OrderEntry> OrderEntries { get; set; }
        public IEnumerable<Pizza> AllPizzas { get; set; }
    }
}
