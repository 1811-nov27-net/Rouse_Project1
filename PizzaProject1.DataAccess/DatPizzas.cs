using System;
using System.Collections.Generic;

namespace PizzaProject1.DataAccess
{
    public partial class DatPizzas
    {
        public DatPizzas()
        {
            OrderEntries = new HashSet<DatOrderEntries>();
            PizzaToppings = new HashSet<DatPizzaToppings>();
        }

        public int PId { get; set; }
        public string PName { get; set; }
        public decimal PPrice { get; set; }

        public virtual ICollection<DatOrderEntries> OrderEntries { get; set; }
        public virtual ICollection<DatPizzaToppings> PizzaToppings { get; set; }
    }
}
