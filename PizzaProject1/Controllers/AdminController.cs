using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaProject1.Library;
using PizzaProject1.Models;

namespace PizzaProject1.Controllers
{
    public class AdminController : Controller
    {
        public IPizzaRepository Repo { get; }

        public AdminController(IPizzaRepository repo)
        {
            Repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            IEnumerable<LibUser> libUsers = Repo.GetAllUsersWithLocation();
            IEnumerable<User> dispUsers = libUsers.Select(x => new User
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DefaultLocation = x.ReferencedLocation.Id,

                DefLocDetails = new Location
                {
                    City = x.ReferencedLocation.City,
                    State = x.ReferencedLocation.State
                }
            });

            return View(dispUsers);
        }

        // Get all orders
        public IActionResult Orders()
        {
            IEnumerable<LibOrder> libOrders = Repo.GetAllOrdersWithUserAndLocation();
            IEnumerable<Order> dispOrders = libOrders.Select(x => new Order
            {
                Id = x.Id,
                User = x.ReferencedUser.Id,
                Location = x.ReferencedLocation.Id,
                Time = x.Time,
                TotalPrice = x.TotalPrice,
                TotalItems = x.TotalItems,

                UserDetails = new User
                {
                    FirstName = x.ReferencedUser.FirstName,
                    LastName = x.ReferencedUser.LastName,
                },

                LocationDetails = new Location
                {
                    City = x.ReferencedLocation.City,
                    State = x.ReferencedLocation.State
                }
            });

            return View(dispOrders);
        }

        public IActionResult UserOrders(int id)
        {
            IEnumerable<LibOrder> libOrders = Repo.GetAllOrdersWithUserAndLocation().Where(u => u.ReferencedUser.Id == id);
            IEnumerable<Order> dispOrders = libOrders.Select(x => new Order
            {
                Id = x.Id,
                User = x.ReferencedUser.Id,
                Location = x.ReferencedLocation.Id,
                Time = x.Time,
                TotalPrice = x.TotalPrice,
                TotalItems = x.TotalItems,

                UserDetails = new User
                {
                    FirstName = x.ReferencedUser.FirstName,
                    LastName = x.ReferencedUser.LastName,
                },

                LocationDetails = new Location
                {
                    City = x.ReferencedLocation.City,
                    State = x.ReferencedLocation.State
                }
            });

            return View(dispOrders);
        }

        public IActionResult Locations()
        {
            IEnumerable<LibLocation> libLocations = Repo.GetAllLocationsOnly();
            IEnumerable<Location> dispLocations = libLocations.Select(x => new Location
            {
                Id = x.Id,
                City = x.City,
                State = x.State
            });

            // Possible code to provide sort within a single method?
            //var locations = Mapper.MapLocationOnly(_db.Locations.AsNoTracking());

            //if(sortClause == "byState")
            //{
            //    locations = locations.OrderBy(l => l.City).OrderBy(l => l.State);
            //}
            //else
            //{
            //    locations = locations.OrderBy(l => l.Id);
            //}

            //return locations;

            // Put sortCluase in method call for an httpPost version of Locations(), use above logic to sort

            return View(dispLocations);
        }

        public IActionResult OrderDetails(int id)
        {
            IEnumerable<LibOrderEntry> libOrderEntries = Repo.GetAllOrderEntriesForOrder(id);
            IEnumerable<OrderEntry> dispOrderEntries = libOrderEntries.Select(x => new OrderEntry
            {
                Id = x.Id,
                Order = x.OrderId,
                Pizza = x.PizzaId,
                Quantity = x.Quantity,
                Subtotal = x.Subtotal,

                PizzaDetails = new Pizza
                {
                    Name = x.ReferencedPizza.Name,
                    Price = x.ReferencedPizza.Price
                }
            });
            return View(dispOrderEntries);
        }

        public IActionResult UserOrderDetails(int id)
        {
            IEnumerable<LibOrderEntry> libOrderEntries = Repo.GetAllOrderEntriesForOrderWithUser(id);
            IEnumerable<OrderEntry> dispOrderEntries = libOrderEntries.Select(x => new OrderEntry
            {
                Id = x.Id,
                Order = x.OrderId,
                Pizza = x.PizzaId,
                Quantity = x.Quantity,
                Subtotal = x.Subtotal,

                PizzaDetails = new Pizza
                {
                    Name = x.ReferencedPizza.Name,
                    Price = x.ReferencedPizza.Price
                },

                OrderDetails = new Order
                {
                    User = x.ReferencedOrder.UserId
                }
            });
            return View(dispOrderEntries);
        }

        public IActionResult LocationInventory(int id)
        {
            IEnumerable<LibLocationInventory> libLocationInventory = Repo.GetLocationInventory(id);
            IEnumerable<LocationInventory> dispLocationInventory = libLocationInventory.Select(x => new LocationInventory
            {
                ToppingDetails = new Topping
                {
                    Name = x.ReferencedTopping.Name
                },

                Quantity = x.Quantity
            });

            return View(dispLocationInventory);
        }

        public IActionResult LocationOrders(int id)
        {
            IEnumerable<LibOrder> libOrders = Repo.GetAllOrdersWithUserAndLocation().Where(u => u.ReferencedLocation.Id == id);
            IEnumerable<Order> dispOrders = libOrders.Select(x => new Order
            {
                Id = x.Id,
                User = x.ReferencedUser.Id,
                Location = x.ReferencedLocation.Id,
                Time = x.Time,
                TotalPrice = x.TotalPrice,
                TotalItems = x.TotalItems,

                UserDetails = new User
                {
                    FirstName = x.ReferencedUser.FirstName,
                    LastName = x.ReferencedUser.LastName,
                },

                LocationDetails = new Location
                {
                    City = x.ReferencedLocation.City,
                    State = x.ReferencedLocation.State
                }
            });

            return View(dispOrders);
        }

        public IActionResult LocationOrderDetails(int id)
        {
            IEnumerable<LibOrderEntry> libOrderEntries = Repo.GetAllOrderEntriesForOrderWithLocation(id);
            IEnumerable<OrderEntry> dispOrderEntries = libOrderEntries.Select(x => new OrderEntry
            {
                Id = x.Id,
                Order = x.OrderId,
                Pizza = x.PizzaId,
                Quantity = x.Quantity,
                Subtotal = x.Subtotal,

                PizzaDetails = new Pizza
                {
                    Name = x.ReferencedPizza.Name,
                    Price = x.ReferencedPizza.Price
                },

                OrderDetails = new Order
                {
                    Location = x.ReferencedOrder.LocationId
                }
            });
            return View(dispOrderEntries);
        }

        public IActionResult SortOrders(IFormCollection form)
        {
            int sortKey = -1;
            if (!Int32.TryParse(form["location_filter"], out sortKey))
            {
                throw new ArgumentException("Stored Order ID is not valid");
            }

            IEnumerable<LibOrder> libOrders = Repo.GetAllOrdersWithUserAndLocation();
            IEnumerable<Order> dispOrders = libOrders.Select(x => new Order
            {
                Id = x.Id,
                User = x.ReferencedUser.Id,
                Location = x.ReferencedLocation.Id,
                Time = x.Time,
                TotalPrice = x.TotalPrice,
                TotalItems = x.TotalItems,

                UserDetails = new User
                {
                    FirstName = x.ReferencedUser.FirstName,
                    LastName = x.ReferencedUser.LastName,
                },

                LocationDetails = new Location
                {
                    City = x.ReferencedLocation.City,
                    State = x.ReferencedLocation.State
                }
            });

            switch (sortKey)
            {
                case 1:
                    dispOrders = dispOrders.OrderBy(x => x.Time);
                    break;

                case 2:
                    dispOrders = dispOrders.OrderByDescending(x => x.Time);
                    break;

                case 3:
                    dispOrders = dispOrders.OrderBy(x => x.TotalPrice);
                    break;

                case 4:
                    dispOrders = dispOrders.OrderByDescending(x => x.TotalPrice);
                    break;

                default:
                    TempData["message"] = "- Error determining sort filter, please contact a system administrator -";
                    return RedirectToAction("Orders");
            }

            return View("Orders", dispOrders);
        }
    }
}