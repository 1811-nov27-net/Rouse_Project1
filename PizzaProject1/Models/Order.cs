using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Location { get; set; }
        public DateTime Time { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalItems { get; set; }

        public User UserDetails { get; set; }
        public Location LocationDetails { get; set; }

        public IEnumerable<OrderEntry> OrderEntries { get; set; }
    }
}
