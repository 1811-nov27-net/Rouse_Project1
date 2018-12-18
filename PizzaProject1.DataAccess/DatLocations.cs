using System;
using System.Collections.Generic;

namespace PizzaProject1.DataAccess
{
    public partial class DatLocations
    {
        public DatLocations()
        {
            LocationInventory = new HashSet<DatLocationInventory>();
            Orders = new HashSet<DatOrders>();
            Users = new HashSet<DatUsers>();
        }

        public int LId { get; set; }
        public string LCity { get; set; }
        public string LState { get; set; }

        public virtual ICollection<DatLocationInventory> LocationInventory { get; set; }
        public virtual ICollection<DatOrders> Orders { get; set; }
        public virtual ICollection<DatUsers> Users { get; set; }
    }
}
