using System;
using System.Collections.Generic;

namespace PizzaProject1.DataAccess
{
    public partial class DatPizzaToppings
    {
        public int PtId { get; set; }
        public int PtPizza { get; set; }
        public int PtTopping { get; set; }
        public int PtQuantity { get; set; }

        public virtual DatPizzas PtPizzaNavigation { get; set; }
        public virtual DatToppings PtToppingNavigation { get; set; }
    }
}
