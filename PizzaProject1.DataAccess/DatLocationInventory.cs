using System;
using System.Collections.Generic;

namespace PizzaProject1.DataAccess
{
    public partial class DatLocationInventory
    {
        public int LiId { get; set; }
        public int LiLocation { get; set; }
        public int LiTopping { get; set; }
        public int LiQuantity { get; set; }

        public virtual DatLocations LiLocationNavigation { get; set; }
        public virtual DatToppings LiToppingNavigation { get; set; }
    }
}
