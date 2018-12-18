using System;
using System.Collections.Generic;

namespace PizzaProject1.DataAccess
{
    public partial class DatOrderEntries
    {
        public int OeId { get; set; }
        public int OeOrder { get; set; }
        public int OePizza { get; set; }
        public int OeQuantity { get; set; }
        public decimal OeSubtotal { get; set; }

        public virtual DatOrders OeOrderNavigation { get; set; }
        public virtual DatPizzas OePizzaNavigation { get; set; }
    }
}
