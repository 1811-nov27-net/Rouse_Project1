using System;
using System.Collections.Generic;

namespace PizzaProject1.DataAccess
{
    public partial class DatToppings
    {
        public DatToppings()
        {
            LocationInventory = new HashSet<DatLocationInventory>();
            PizzaToppings = new HashSet<DatPizzaToppings>();
        }

        public int TId { get; set; }
        public string TName { get; set; }

        public virtual ICollection<DatLocationInventory> LocationInventory { get; set; }
        public virtual ICollection<DatPizzaToppings> PizzaToppings { get; set; }
    }
}
