using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaProject1.DataAccess
{
    public static class Mapper
    {
        // PIZZA MAPS ----------------------------------
        public static Library.LibPizza MapPizzaFull(DatPizzas pizza) => new Library.LibPizza
        {
            Id = pizza.PId,
            Name = pizza.PName,
            Price = pizza.PPrice,

            OrderEntries = MapOrderEntryOnly(pizza.OrderEntries).ToList(),
            PizzaToppings = Map(pizza.PizzaToppings).ToList()
        };

        public static DatPizzas MapPizzaFull(Library.LibPizza pizza) => new DatPizzas
        {
           PId = pizza.Id,
           PName = pizza.Name,
           PPrice = pizza.Price,

            OrderEntries = MapOrderEntryOnly(pizza.OrderEntries).ToList(),
            PizzaToppings = Map(pizza.PizzaToppings).ToList()
        };
        //----------------------------------------------

        // TOPPING MAPS --------------------------------
        public static Library.LibTopping Map(DatToppings topping) => new Library.LibTopping
        {
           Id = topping.TId,
           Name = topping.TName,

           LocationInventories = MapLocationInventoryOnly(topping.LocationInventory).ToList(),
           PizzaToppings = Map(topping.PizzaToppings).ToList()
        };

        public static DatToppings Map(Library.LibTopping topping) => new DatToppings
        {
           TId = topping.Id,
           TName = topping.Name,

           LocationInventory = MapLocationInventoryOnly(topping.LocationInventories).ToList(),
           PizzaToppings = Map(topping.PizzaToppings).ToList()
        };
        //----------------------------------------------

        // LOCATION MAPS --------------------------------
        public static Library.LibLocation MapLocationOnly(DatLocations location) => new Library.LibLocation
        {
            Id = location.LId,
            City = location.LCity,
            State = location.LState
        };

        public static DatLocations MapLocationOnly(Library.LibLocation location) => new DatLocations
        {
            LId = location.Id,
            LCity = location.City,
            LState = location.State
        };

        public static Library.LibLocation MapLocationFull(DatLocations location) => new Library.LibLocation
        {
            Id = location.LId,
            City = location.LCity,
            State = location.LState,

            LocationInventories = MapLocationInventoryOnly(location.LocationInventory).ToList(),
            Orders = MapOrderOnly(location.Orders).ToList(),
            Users = MapUserOnly(location.Users).ToList()
        };

        public static DatLocations MapLocationFull(Library.LibLocation location) => new DatLocations
        {
            LId = location.Id,
            LCity = location.City,
            LState = location.State,

            LocationInventory = MapLocationInventoryOnly(location.LocationInventories).ToList(),
            Orders = MapOrderOnly(location.Orders).ToList(),
            Users = MapUserOnly(location.Users).ToList()
        };
        //----------------------------------------------

        // USER MAPS --------------------------------
        public static Library.LibUser MapUserOnly(DatUsers user) => new Library.LibUser
        {
            Id = user.UId,
            FirstName = user.UFirstName,
            LastName = user.ULastName,
            DefaultLocation = user.UDefaultLocation
        };

        public static DatUsers MapUserOnly(Library.LibUser user) => new DatUsers
        {
            UId = user.Id,
            UFirstName = user.FirstName,
            ULastName = user.LastName,
            UDefaultLocation = user.DefaultLocation
        };

        public static Library.LibUser MapUserAndLocation(DatUsers user) => new Library.LibUser
        {
            Id = user.UId,
            FirstName = user.UFirstName,
            LastName = user.ULastName,
            DefaultLocation = user.UDefaultLocation,

            ReferencedLocation = MapLocationOnly(user.UDefaultLocationNavigation),
            // Orders = Map(user.Orders).ToList()
        };

        public static DatUsers MapUserAndLocation(Library.LibUser user) => new DatUsers
        {
            UId = user.Id,
            UFirstName = user.FirstName,
            ULastName = user.LastName,
            UDefaultLocation = user.DefaultLocation,

            UDefaultLocationNavigation = MapLocationOnly(user.ReferencedLocation),
            // Orders = Map(user.Orders).ToList()
        };
        //----------------------------------------------

        // ORDER MAPS --------------------------------
        public static Library.LibOrder MapOrderOnly(DatOrders order) => new Library.LibOrder
        {
            Id = order.OId,
            UserId = order.OUser,
            LocationId = order.OLocation,
            Time = order.OTime,
            TotalPrice = order.OTotalPrice,
            TotalItems = order.OTotalItems
        };

        public static DatOrders MapOrderOnly(Library.LibOrder order) => new DatOrders
        {
            OId = order.Id,
            OUser = order.UserId,
            OLocation = order.LocationId,
            OTime = order.Time,
            OTotalPrice = order.TotalPrice,
            OTotalItems = order.TotalItems
        };

        public static Library.LibOrder MapOrderWithUser(DatOrders order) => new Library.LibOrder
        {
            Id = order.OId,
            UserId = order.OUser,
            LocationId = order.OLocation,
            Time = order.OTime,
            TotalPrice = order.OTotalPrice,
            TotalItems = order.OTotalItems,

            ReferencedUser = MapUserOnly(order.OUserNavigation)
        };

        public static DatOrders MapOrderWithUser(Library.LibOrder order) => new DatOrders
        {
            OId = order.Id,
            OUser = order.UserId,
            OLocation = order.LocationId,
            OTime = order.Time,
            OTotalPrice = order.TotalPrice,
            OTotalItems = order.TotalItems,

            OUserNavigation = MapUserOnly(order.ReferencedUser)
        };

        public static Library.LibOrder MapOrderWithLocation(DatOrders order) => new Library.LibOrder
        {
            Id = order.OId,
            UserId = order.OUser,
            LocationId = order.OLocation,
            Time = order.OTime,
            TotalPrice = order.OTotalPrice,
            TotalItems = order.OTotalItems,

            ReferencedLocation = MapLocationOnly(order.OLocationNavigation)
        };

        public static DatOrders MapOrderWithLocation(Library.LibOrder order) => new DatOrders
        {
            OId = order.Id,
            OUser = order.UserId,
            OLocation = order.LocationId,
            OTime = order.Time,
            OTotalPrice = order.TotalPrice,
            OTotalItems = order.TotalItems,

            OLocationNavigation = MapLocationOnly(order.ReferencedLocation)
        };

        public static Library.LibOrder MapOrderWithLocationAndUser(DatOrders order) => new Library.LibOrder
        {
            Id = order.OId,
            UserId = order.OUser,
            LocationId = order.OLocation,
            Time = order.OTime,
            TotalPrice = order.OTotalPrice,
            TotalItems = order.OTotalItems,

            ReferencedLocation = MapLocationOnly(order.OLocationNavigation),
            ReferencedUser = MapUserOnly(order.OUserNavigation)
        };

        public static DatOrders MapOrderWithLocationAndUser(Library.LibOrder order) => new DatOrders
        {
            OId = order.Id,
            OUser = order.UserId,
            OLocation = order.LocationId,
            OTime = order.Time,
            OTotalPrice = order.TotalPrice,
            OTotalItems = order.TotalItems,

            OLocationNavigation = MapLocationOnly(order.ReferencedLocation),
            OUserNavigation = MapUserOnly(order.ReferencedUser)
        };

        public static Library.LibOrder MapOrderFull(DatOrders order) => new Library.LibOrder
        {
            Id = order.OId,
            UserId = order.OUser,
            LocationId = order.OLocation,
            Time = order.OTime,
            TotalPrice = order.OTotalPrice,
            TotalItems = order.OTotalItems,

            ReferencedLocation = MapLocationOnly(order.OLocationNavigation),
            ReferencedUser = MapUserOnly(order.OUserNavigation),

            OrderEntries = MapOrderEntryOnly(order.OrderEntries).ToList()
        };

        public static DatOrders MapOrderFull(Library.LibOrder order) => new DatOrders
        {
            OId = order.Id,
            OUser = order.UserId,
            OLocation = order.LocationId,
            OTime = order.Time,
            OTotalPrice = order.TotalPrice,
            OTotalItems = order.TotalItems,

            OLocationNavigation = MapLocationOnly(order.ReferencedLocation),
            OUserNavigation = MapUserOnly(order.ReferencedUser),

            OrderEntries = MapOrderEntryOnly(order.OrderEntries).ToList()
        };
        //----------------------------------------------

        // ORDER ENTRY MAPS --------------------------------
        public static Library.LibOrderEntry MapOrderEntryFull(DatOrderEntries orderEntry) => new Library.LibOrderEntry
        {
            Id = orderEntry.OeId,
            OrderId = orderEntry.OeOrder,
            PizzaId = orderEntry.OePizza,
            Quantity = orderEntry.OeQuantity,
            Subtotal = orderEntry.OeSubtotal,

            ReferencedOrder = MapOrderOnly(orderEntry.OeOrderNavigation),
            ReferencedPizza = MapPizzaFull(orderEntry.OePizzaNavigation)
        };

        public static DatOrderEntries MapOrderEntryFull(Library.LibOrderEntry orderEntry) => new DatOrderEntries
        {
            OeId = orderEntry.Id,
            OeOrder = orderEntry.OrderId,
            OePizza = orderEntry.PizzaId,
            OeQuantity = orderEntry.Quantity,
            OeSubtotal = orderEntry.Subtotal,

            OeOrderNavigation = MapOrderOnly(orderEntry.ReferencedOrder),
            OePizzaNavigation = MapPizzaFull(orderEntry.ReferencedPizza)
        };

        public static Library.LibOrderEntry MapOrderEntryFullWithOrderUser(DatOrderEntries orderEntry) => new Library.LibOrderEntry
        {
            Id = orderEntry.OeId,
            OrderId = orderEntry.OeOrder,
            PizzaId = orderEntry.OePizza,
            Quantity = orderEntry.OeQuantity,
            Subtotal = orderEntry.OeSubtotal,

            ReferencedOrder = MapOrderWithUser(orderEntry.OeOrderNavigation),
            ReferencedPizza = MapPizzaFull(orderEntry.OePizzaNavigation)
        };

        public static DatOrderEntries MapOrderEntryFullWithOrderUser(Library.LibOrderEntry orderEntry) => new DatOrderEntries
        {
            OeId = orderEntry.Id,
            OeOrder = orderEntry.OrderId,
            OePizza = orderEntry.PizzaId,
            OeQuantity = orderEntry.Quantity,
            OeSubtotal = orderEntry.Subtotal,

            OeOrderNavigation = MapOrderWithUser(orderEntry.ReferencedOrder),
            OePizzaNavigation = MapPizzaFull(orderEntry.ReferencedPizza)
        };

        public static Library.LibOrderEntry MapOrderEntryFullWithOrderLocation(DatOrderEntries orderEntry) => new Library.LibOrderEntry
        {
            Id = orderEntry.OeId,
            OrderId = orderEntry.OeOrder,
            PizzaId = orderEntry.OePizza,
            Quantity = orderEntry.OeQuantity,
            Subtotal = orderEntry.OeSubtotal,

            ReferencedOrder = MapOrderWithLocation(orderEntry.OeOrderNavigation),
            ReferencedPizza = MapPizzaFull(orderEntry.OePizzaNavigation)
        };

        public static DatOrderEntries MapOrderEntryFullWithOrderLocation(Library.LibOrderEntry orderEntry) => new DatOrderEntries
        {
            OeId = orderEntry.Id,
            OeOrder = orderEntry.OrderId,
            OePizza = orderEntry.PizzaId,
            OeQuantity = orderEntry.Quantity,
            OeSubtotal = orderEntry.Subtotal,

            OeOrderNavigation = MapOrderWithLocation(orderEntry.ReferencedOrder),
            OePizzaNavigation = MapPizzaFull(orderEntry.ReferencedPizza)
        };

        public static Library.LibOrderEntry MapOrderEntryWithPizza(DatOrderEntries orderEntry) => new Library.LibOrderEntry
        {
            Id = orderEntry.OeId,
            OrderId = orderEntry.OeOrder,
            PizzaId = orderEntry.OePizza,
            Quantity = orderEntry.OeQuantity,
            Subtotal = orderEntry.OeSubtotal,

            ReferencedPizza = MapPizzaFull(orderEntry.OePizzaNavigation)
        };

        public static DatOrderEntries MapOrderEntryWithPizza(Library.LibOrderEntry orderEntry) => new DatOrderEntries
        {
            OeId = orderEntry.Id,
            OeOrder = orderEntry.OrderId,
            OePizza = orderEntry.PizzaId,
            OeQuantity = orderEntry.Quantity,
            OeSubtotal = orderEntry.Subtotal,

            OePizzaNavigation = MapPizzaFull(orderEntry.ReferencedPizza)
        };

        public static Library.LibOrderEntry MapOrderEntryOnly(DatOrderEntries orderEntry) => new Library.LibOrderEntry
        {
            Id = orderEntry.OeId,
            OrderId = orderEntry.OeOrder,
            PizzaId = orderEntry.OePizza,
            Quantity = orderEntry.OeQuantity,
            Subtotal = orderEntry.OeSubtotal
        };

        public static DatOrderEntries MapOrderEntryOnly(Library.LibOrderEntry orderEntry) => new DatOrderEntries
        {
            OeId = orderEntry.Id,
            OeOrder = orderEntry.OrderId,
            OePizza = orderEntry.PizzaId,
            OeQuantity = orderEntry.Quantity,
            OeSubtotal = orderEntry.Subtotal
        };
        //----------------------------------------------

        // PIZZA TOPPING MAPS --------------------------------
        public static Library.LibPizzaTopping Map(DatPizzaToppings pizzaTopping) => new Library.LibPizzaTopping
        {
            Id = pizzaTopping.PtId,
            PizzaId = pizzaTopping.PtPizza,
            ToppingId = pizzaTopping.PtTopping,
            Quantity = pizzaTopping.PtQuantity,

            ReferencedPizza = MapPizzaFull(pizzaTopping.PtPizzaNavigation),
            ReferencedTopping = Map(pizzaTopping.PtToppingNavigation)
        };

        public static DatPizzaToppings Map(Library.LibPizzaTopping pizzaTopping) => new DatPizzaToppings
        {
            PtId = pizzaTopping.Id,
            PtPizza = pizzaTopping.PizzaId,
            PtTopping = pizzaTopping.ToppingId,
            PtQuantity = pizzaTopping.Quantity,

            PtPizzaNavigation = MapPizzaFull(pizzaTopping.ReferencedPizza),
            PtToppingNavigation = Map(pizzaTopping.ReferencedTopping)
        };
        //----------------------------------------------

        // LOCATION INVENTORY MAPS --------------------------------
        public static Library.LibLocationInventory MapLocationInventoryOnly(DatLocationInventory locationInventory) => new Library.LibLocationInventory
        {
            Id = locationInventory.LiId,
            LocationId = locationInventory.LiLocation,
            ToppingId = locationInventory.LiTopping,
            Quantity = locationInventory.LiQuantity
        };

        public static DatLocationInventory MapLocationInventoryOnly(Library.LibLocationInventory locationInventory) => new DatLocationInventory
        {
            LiId = locationInventory.Id,
            LiLocation = locationInventory.LocationId,
            LiTopping = locationInventory.ToppingId,
            LiQuantity = locationInventory.Quantity
        };

        public static Library.LibLocationInventory MapLocationInventoryWithTopping(DatLocationInventory locationInventory) => new Library.LibLocationInventory
        {
            Id = locationInventory.LiId,
            LocationId = locationInventory.LiLocation,
            ToppingId = locationInventory.LiTopping,
            Quantity = locationInventory.LiQuantity,

            ReferencedTopping = Map(locationInventory.LiToppingNavigation)
        };

        public static DatLocationInventory MapLocationInventoryWithTopping(Library.LibLocationInventory locationInventory) => new DatLocationInventory
        {
            LiId = locationInventory.Id,
            LiLocation = locationInventory.LocationId,
            LiTopping = locationInventory.ToppingId,
            LiQuantity = locationInventory.Quantity,

            LiToppingNavigation = Map(locationInventory.ReferencedTopping)
        };

        //public static Library.LibLocationInventory Map(DatLocationInventory locationInventory) => new Library.LibLocationInventory
        //{
        //    Id = locationInventory.LiId,
        //    LocationId = locationInventory.LiLocation,
        //    ToppingId = locationInventory.LiTopping,
        //    Quantity = locationInventory.LiQuantity,

        //    ReferencedLocation = MapLocationOnly(locationInventory.LiLocationNavigation),
        //    ReferencedTopping = Map(locationInventory.LiToppingNavigation)
        //};

        //public static DatLocationInventory Map(Library.LibLocationInventory locationInventory) => new DatLocationInventory
        //{
        //    LiId = locationInventory.Id,
        //    LiLocation = locationInventory.LocationId,
        //    LiTopping = locationInventory.ToppingId,
        //    LiQuantity = locationInventory.Quantity,

        //    LiLocationNavigation = MapLocationOnly(locationInventory.ReferencedLocation),
        //    LiToppingNavigation = Map(locationInventory.ReferencedTopping)
        //};
        //----------------------------------------------

        // LIST MAPS -----------------------------------
            // PIZZA LISTS --------------------------
                // PIZZA FULL -----------------------
        public static IEnumerable<Library.LibPizza> MapPizzaFull(IEnumerable<DatPizzas> pizzas) => pizzas.Select(MapPizzaFull);
        public static IEnumerable<DatPizzas> MapPizzaFull(IEnumerable<Library.LibPizza> pizzas) => pizzas.Select(MapPizzaFull);

            // LOCATION LISTS --------------------------
                // LOCATION ONLY -----------------------
        public static IEnumerable<Library.LibLocation> MapLocationOnly(IEnumerable<DatLocations> locations) => locations.Select(MapLocationOnly);
        public static IEnumerable<DatLocations> MapLocationOnly(IEnumerable<Library.LibLocation> locations) => locations.Select(MapLocationOnly);
                // LOCATION FULL -----------------------
        public static IEnumerable<Library.LibLocation> MapLocationFull(IEnumerable<DatLocations> locations) => locations.Select(MapLocationFull);
        public static IEnumerable<DatLocations> MapLocationFull(IEnumerable<Library.LibLocation> locations) => locations.Select(MapLocationFull);

            // ORDER ENTRY LISTS -----------------------
                // ORDER ENTRY WITH PIZZA --------------
        public static IEnumerable<Library.LibOrderEntry> MapOrderEntryWithPizza(IEnumerable<DatOrderEntries> orderEntries) => orderEntries.Select(MapOrderEntryWithPizza);
        public static IEnumerable<DatOrderEntries> MapOrderEntryWithPizza(IEnumerable<Library.LibOrderEntry> orderEntries) => orderEntries.Select(MapOrderEntryWithPizza);
                // ORDER ENTRY ONLY --------------------
        public static IEnumerable<Library.LibOrderEntry> MapOrderEntryOnly(IEnumerable<DatOrderEntries> orderEntries) => orderEntries.Select(MapOrderEntryOnly);
        public static IEnumerable<DatOrderEntries> MapOrderEntryOnly(IEnumerable<Library.LibOrderEntry> orderEntries) => orderEntries.Select(MapOrderEntryOnly);
                // ORDER ENTRY FULL --------------------
        public static IEnumerable<Library.LibOrderEntry> MapOrderEntryFull(IEnumerable<DatOrderEntries> orderEntries) => orderEntries.Select(MapOrderEntryFull);
        public static IEnumerable<DatOrderEntries> MapOrderEntryFull(IEnumerable<Library.LibOrderEntry> orderEntries) => orderEntries.Select(MapOrderEntryFull);
                // ORDER ENTRY FULL (WITH ORDER USER) --
        public static IEnumerable<Library.LibOrderEntry> MapOrderEntryFullWithOrderUser(IEnumerable<DatOrderEntries> orderEntries) => orderEntries.Select(MapOrderEntryFullWithOrderUser);
        public static IEnumerable<DatOrderEntries> MapOrderEntryFullWithOrderUser(IEnumerable<Library.LibOrderEntry> orderEntries) => orderEntries.Select(MapOrderEntryFullWithOrderUser);
                // ORDER ENTRY FULL (WITH ORDER LOCATION) --
        public static IEnumerable<Library.LibOrderEntry> MapOrderEntryFullWithOrderLocation(IEnumerable<DatOrderEntries> orderEntries) => orderEntries.Select(MapOrderEntryFullWithOrderLocation);
        public static IEnumerable<DatOrderEntries> MapOrderEntryFullWithOrderLocation(IEnumerable<Library.LibOrderEntry> orderEntries) => orderEntries.Select(MapOrderEntryFullWithOrderLocation);

        // PIZZA TOPPING LISTS ---------------------
        // MAP ---------------------------------
        public static IEnumerable<Library.LibPizzaTopping> Map(IEnumerable<DatPizzaToppings> pizzaToppings) => pizzaToppings.Select(Map);
        public static IEnumerable<DatPizzaToppings> Map(IEnumerable<Library.LibPizzaTopping> pizzaToppings) => pizzaToppings.Select(Map);

            // LOCATION INVENTORY LISTS ----------------
                // LOCATION INVENTORY ONLY -------------
        public static IEnumerable<Library.LibLocationInventory> MapLocationInventoryOnly(IEnumerable<DatLocationInventory> locationInventories) => locationInventories.Select(MapLocationInventoryOnly);
        public static IEnumerable<DatLocationInventory> MapLocationInventoryOnly(IEnumerable<Library.LibLocationInventory> locationInventories) => locationInventories.Select(MapLocationInventoryOnly);
                // LOCATION INVENTORY WITH TOPPING -------------
        public static IEnumerable<Library.LibLocationInventory> MapLocationInventoryWithTopping(IEnumerable<DatLocationInventory> locationInventories) => locationInventories.Select(MapLocationInventoryWithTopping);
        public static IEnumerable<DatLocationInventory> MapLocationInventoryWithTopping(IEnumerable<Library.LibLocationInventory> locationInventories) => locationInventories.Select(MapLocationInventoryWithTopping);

            // ORDER LISTS -----------------------------
                // ORDER ONLY---------------------------
        public static IEnumerable<Library.LibOrder> MapOrderOnly(IEnumerable<DatOrders> orders) => orders.Select(MapOrderOnly);
        public static IEnumerable<DatOrders> MapOrderOnly(IEnumerable<Library.LibOrder> orders) => orders.Select(MapOrderOnly);
                // ORDER WITH USER ----------------------
        public static IEnumerable<Library.LibOrder> MapOrderWithUser(IEnumerable<DatOrders> orders) => orders.Select(MapOrderWithUser);
        public static IEnumerable<DatOrders> MapOrderWithUser(IEnumerable<Library.LibOrder> orders) => orders.Select(MapOrderWithUser);
                // ORDER WITH LOCATION AND USER ---------
        public static IEnumerable<Library.LibOrder> MapOrderWithLocationAndUser(IEnumerable<DatOrders> orders) => orders.Select(MapOrderWithLocationAndUser);
        public static IEnumerable<DatOrders> MapOrderWithLocationAndUser(IEnumerable<Library.LibOrder> orders) => orders.Select(MapOrderWithLocationAndUser);

            // USER LISTS -------------------------------
                // USER ONLY ----------------------------
        public static IEnumerable<Library.LibUser> MapUserOnly(IEnumerable<DatUsers> users) => users.Select(MapUserOnly);
        public static IEnumerable<DatUsers> MapUserOnly(IEnumerable<Library.LibUser> users) => users.Select(MapUserOnly);
                // USER WITH LOCATION -------------------
        public static IEnumerable<Library.LibUser> MapUserAndLocation(IEnumerable<DatUsers> users) => users.Select(MapUserAndLocation);
        public static IEnumerable<DatUsers> MapUserAndLocation(IEnumerable<Library.LibUser> users) => users.Select(MapUserAndLocation);
        //----------------------------------------------
    }
}
