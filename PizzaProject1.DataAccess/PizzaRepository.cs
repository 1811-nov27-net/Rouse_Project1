using Microsoft.EntityFrameworkCore;
using PizzaProject1.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzaProject1.DataAccess
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly _1811proj1Context _db;

        public PizzaRepository(_1811proj1Context db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<LibUser> GetAllUsersOnly()
        {
            return Mapper.MapUserOnly(_db.Users.AsNoTracking().OrderBy(u => u.UFirstName).OrderBy(u => u.ULastName));
        }

        public IEnumerable<LibUser> GetAllUsersWithLocation()
        {
            return Mapper.MapUserAndLocation(_db.Users.Include(l => l.UDefaultLocationNavigation).AsNoTracking().OrderBy(u => u.UFirstName).OrderBy(u => u.ULastName));
        }

        public LibUser GetUserByIdWithLocation(int id)
        {
            return Mapper.MapUserAndLocation(_db.Users.Include(l => l.UDefaultLocationNavigation).AsNoTracking().FirstOrDefault(u => u.UId == id));
        }

        public IEnumerable<LibOrder> GetAllOrdersWithUserAndLocation()
        {
            return Mapper.MapOrderWithLocationAndUser(_db.Orders.Include(u => u.OUserNavigation).Include(l => l.OLocationNavigation).AsNoTracking());
        }

        public IEnumerable<LibLocation> GetAllLocationsOnly()
        {
            return Mapper.MapLocationOnly(_db.Locations.AsNoTracking());
        }

        public IEnumerable<LibOrderEntry> GetAllOrderEntriesForOrder(int id)
        {
            return Mapper.MapOrderEntryWithPizza(_db.OrderEntries.Include(p => p.OePizzaNavigation).AsNoTracking().Where(oe => oe.OeOrder == id));          
        }

        public IEnumerable<LibOrderEntry> GetAllOrderEntriesForOrderWithUser(int id)
        {
            return Mapper.MapOrderEntryFullWithOrderUser(_db.OrderEntries.Include(p => p.OePizzaNavigation).Include(u => u.OeOrderNavigation.OUserNavigation).AsNoTracking().Where(oe => oe.OeOrder == id));
        }

        public IEnumerable<LibOrderEntry> GetAllOrderEntriesForOrderWithLocation(int id)
        {
            return Mapper.MapOrderEntryFullWithOrderLocation(_db.OrderEntries.Include(p => p.OePizzaNavigation).Include(u => u.OeOrderNavigation.OLocationNavigation).AsNoTracking().Where(oe => oe.OeOrder == id));
        }

        public IEnumerable<LibLocationInventory> GetLocationInventory(int id)
        {
            return Mapper.MapLocationInventoryWithTopping(_db.LocationInventory.Include(t => t.LiToppingNavigation).AsNoTracking().Where(li => li.LiLocation == id));
        }

        public int CreateOrder(int userId, int locationId)
        {
            DatOrders order = new DatOrders
            {
                OUser = userId,
                OLocation = locationId,
                OTime = DateTime.Now,
                OTotalPrice = 0M,
                OTotalItems = 0
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            return _db.Orders.Where(u => u.OUser == userId).Where(l => l.OLocation == locationId).LastOrDefault().OId;
        }

        public void SetLocation(int userId, int locationId)
        {
            DatUsers user = _db.Users.FirstOrDefault(u => u.UId == userId);

            user.UDefaultLocation = locationId;

            _db.SaveChanges();
        }

        public void GetSuggestedOrder(int userId)
        {
            // FINISH ME!!!!
            double avgPizzasPerOrder = Math.Round((double)(_db.Orders.Where(u => u.OUser == userId).Sum(o => o.OTotalItems)) / (_db.Orders.Where(u => u.OUser == userId).Count()));

            double avgTypesPerOrder = Math.Round((double)(_db.OrderEntries.Join(_db.Orders, oe => oe.OeOrder, o => o.OId, (oe, o) => new { oe, o })
                .Join(_db.Users, oeo => oeo.o.OUser, u => u.UId, (oeo, u) => new { oeo, u })
                .Count()) / (_db.Orders.Where(u => u.OUser == userId).Count()));

            var pizzaList = _db.Pizzas.ToList();

            Dictionary<int, int> pizzaDistribution = null;

            foreach(var item in pizzaList)
            {
                int pizzaCount = _db.OrderEntries.Join(_db.Orders, oe => oe.OeOrder, o => o.OId, (oe, o) => new { oe, o })
                .Join(_db.Users, oeo => oeo.o.OUser, u => u.UId, (oeo, u) => new { oeo, u }).Where(p => p.oeo.oe.OePizza == item.PId).Sum(p => p.oeo.oe.OeQuantity);



                pizzaDistribution.Add(item.PId, pizzaCount);
            }

            int criticalDistTotalOfSuggestion = 0;

            for (int i = 0; i < avgTypesPerOrder; i++)
            {
                criticalDistTotalOfSuggestion += pizzaDistribution[i];
            }
        }

        public LibOrder GetOrderByIdWithEntries(int id)
        {
            return Mapper.MapOrderOnly(_db.Orders.Include(oe => oe.OrderEntries).Where(o => o.OId == id).FirstOrDefault());
        }

        public IEnumerable<LibOrderEntry> GetEntriesForOrder(int id)
        {
            return Mapper.MapOrderEntryWithPizza(_db.OrderEntries.Include(p => p.OePizzaNavigation).Where(o => o.OeOrder == id).ToList());
        }

        public IEnumerable<LibPizza> GetAllPizzas()
        {
            return Mapper.MapPizzaFull(_db.Pizzas.AsNoTracking());
        }

        public int AddOrderEntryGetIdBack(int orderId, int pizzaId, int quantity)
        {
            DatOrders workingOrder = _db.Orders.Where(o => o.OId == orderId).FirstOrDefault();
            DatPizzas orderedPizza = _db.Pizzas.Where(p => p.PId == pizzaId).FirstOrDefault();

            DatOrderEntries newEntry = new DatOrderEntries
            {
                OeOrder = orderId,
                OePizza = pizzaId,
                OeQuantity = quantity,
                OeSubtotal = orderedPizza.PPrice * quantity
            };

            if (!OrderIsValidPrice(workingOrder.OTotalPrice + newEntry.OeSubtotal))
            {
                return -1;
            }
            else if (!OrderIsValidSize(workingOrder.OTotalItems + quantity)){
                return -2;
            }

            workingOrder.OTotalPrice += newEntry.OeSubtotal;
            workingOrder.OTotalItems += quantity;
            _db.OrderEntries.Add(newEntry);
            _db.SaveChanges();

            return _db.OrderEntries.Where(o => o.OeOrder == orderId).Where(p => p.OePizza == pizzaId).LastOrDefault().OeId;

        }

        public bool OrderIsValidPrice(decimal price)
        {
            if (price > 500 || price <= 0)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

        public bool OrderIsValidSize(int size)
        {
            if (size > 12 || size <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RemoveOrderEntry(int entryId)
        {
            DatOrderEntries removeMe = _db.OrderEntries.Where(oe => oe.OeId == entryId).FirstOrDefault();

            DatOrders orderChanged = _db.Orders.Where(o => o.OId == removeMe.OeOrder).FirstOrDefault();

            orderChanged.OTotalItems -= removeMe.OeQuantity;
            orderChanged.OTotalPrice -= removeMe.OeSubtotal;

            _db.OrderEntries.Remove(removeMe);
            _db.SaveChanges();
        }

        public void RemoveOrder(int orderId)
        {
            DatOrders removeOrder = _db.Orders.Where(o => o.OId == orderId).FirstOrDefault();

            _db.Orders.Remove(removeOrder);
            _db.SaveChanges();
        }

        public IEnumerable<LibOrderEntry> GetOrderEntriesForDeletion(int orderId)
        {
            return Mapper.MapOrderEntryOnly(_db.OrderEntries.Where(oe => oe.OeOrder == orderId).AsNoTracking()).ToList();
        }

        public bool EnoughIngredients(LibOrder order)
        {
            bool clearsCheck = true;
            int selectedLocation = order.LocationId;
            IEnumerable<LibLocationInventory> inventory = Mapper.MapLocationInventoryWithTopping(_db.LocationInventory.Include(m => m.LiToppingNavigation).Where(m => m.LiLocation == selectedLocation)).ToList();

            foreach (var invTop in inventory)
            {
                int orderNeed = _db.OrderEntries
                .Join(_db.Pizzas, oe => oe.OePizza, p => p.PId, (oe, p) => new { oe, p })
                .Join(_db.PizzaToppings, oep => oep.p.PId, pt => pt.PtPizza, (oep, pt) => new { oep, pt })
                .Where(m => m.oep.oe.OeOrder == order.Id).Where(m => m.pt.PtTopping == invTop.Id).ToList().Sum(m => (m.pt.PtQuantity * m.oep.oe.OeQuantity));

                if (invTop.Quantity < orderNeed)
                {
                    clearsCheck = false;
                }
            }
            return clearsCheck;
        }

        public void UseIngredients(LibOrder order)
        {
            int selectedLocation = order.LocationId;
            IEnumerable<DatLocationInventory> inventory = _db.LocationInventory.Include(m => m.LiToppingNavigation).Where(m => m.LiLocation == selectedLocation).ToList();

            IEnumerable<DatOrderEntries> orderEntries = _db.OrderEntries.Where(o => o.OeOrder == order.Id).ToList();

            IEnumerable<DatPizzas> pizzas = _db.Pizzas.ToList();
            IEnumerable<DatPizzaToppings> pizzaToppings = _db.PizzaToppings.ToList();

            foreach (var entry in orderEntries)
            {
                foreach (var pizza in entry.OePizzaNavigation.PizzaToppings)
                {
                    int toppingNeeded = pizza.PtQuantity * entry.OeQuantity;

                    foreach (var topping in inventory)
                    {
                        if (topping.LiTopping == pizza.PtTopping)
                        {
                            topping.LiQuantity -= toppingNeeded;
                        }
                    }
                }
                    
            }
            _db.SaveChanges();
           
        }
    }
}
