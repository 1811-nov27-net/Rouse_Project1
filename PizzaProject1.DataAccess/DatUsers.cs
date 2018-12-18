using System;
using System.Collections.Generic;

namespace PizzaProject1.DataAccess
{
    public partial class DatUsers
    {
        public DatUsers()
        {
            Orders = new HashSet<DatOrders>();
        }

        public int UId { get; set; }
        public string UFirstName { get; set; }
        public string ULastName { get; set; }
        public int UDefaultLocation { get; set; }

        public virtual DatLocations UDefaultLocationNavigation { get; set; }
        public virtual ICollection<DatOrders> Orders { get; set; }
    }
}
