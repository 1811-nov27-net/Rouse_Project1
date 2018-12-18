using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaProject1.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public IEnumerable<LocationInventory> LocationInventories{ get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
