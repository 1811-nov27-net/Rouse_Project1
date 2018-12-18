using System;
using System.Collections.Generic;

namespace PizzaProject1.DataAccess
{
    public partial class DatOrders
    {
        public DatOrders()
        {
            OrderEntries = new HashSet<DatOrderEntries>();
        }

        public int OId { get; set; }
        public int OUser { get; set; }
        public int OLocation { get; set; }
        public DateTime OTime { get; set; }
        public decimal OTotalPrice { get; set; }
        public int OTotalItems { get; set; }

        public virtual DatLocations OLocationNavigation { get; set; }
        public virtual DatUsers OUserNavigation { get; set; }
        public virtual ICollection<DatOrderEntries> OrderEntries { get; set; }
    }
}
