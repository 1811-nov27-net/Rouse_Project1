using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject1.Models
{
    public class PizzaTopping
    {
        public int Id { get; set; }
        public int Pizza { get; set; }
        public int Topping { get; set; }
        public int Quantity { get; set; }
    }
}
