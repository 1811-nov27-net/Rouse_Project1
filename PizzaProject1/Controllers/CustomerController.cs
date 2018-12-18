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
    public class CustomerController : Controller
    {
        public IPizzaRepository Repo { get; }

        public CustomerController(IPizzaRepository repo)
        {
            Repo = repo;
        }

        public IActionResult Index(int id)
        {
            LibUser libUser = Repo.GetUserByIdWithLocation(id);
            //User currentUser = new User
            //{
            //    Id = libUser.Id,
            //    FirstName = libUser.FirstName,
            //    LastName = libUser.LastName,
            //    DefaultLocation = libUser.DefaultLocation,

            //    DefLocDetails = new Location
            //    {
            //        City = libUser.ReferencedLocation.City,
            //        State = libUser.ReferencedLocation.State
            //    }
            //};

            TempData["uId"] = libUser.Id;
            TempData["uFirstName"] = libUser.FirstName;
            TempData["uLastName"] = libUser.LastName;
            TempData["uDefaultLocation"] = libUser.DefaultLocation;
            TempData["lCity"] = libUser.ReferencedLocation.City;
            TempData["lState"] = libUser.ReferencedLocation.State;


            return View();
        }

        public IActionResult ViewOrders(int id)
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

        public IActionResult OrderDetails(int id)
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

        public IActionResult ChangeLocation()
        {
            IEnumerable<LibLocation> libLocations = Repo.GetAllLocationsOnly().OrderBy(l => l.City).ToList();
            IEnumerable<Location> dispLocations = libLocations.Select(x => new Location
            {
                Id = x.Id,
                City = x.City,
                State = x.State
            });

            return View(dispLocations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetLocation(IFormCollection form)
        {
            int currentUser = -1;
            if (!Int32.TryParse(TempData.Peek("uId").ToString(), out currentUser))
            {
                throw new ArgumentException("Stored User ID is not valid");
            }

            int locationId = -1;
            if (!Int32.TryParse(form["location_setter"], out locationId))
            {
                throw new ArgumentException("Set Location ID is not valid");
            }

            Repo.SetLocation(currentUser, locationId);

            return RedirectToAction("Index", new { id=currentUser});
        }

        public IActionResult StartNewOrder()
        {
            int orderUser = -1;
            int orderLocation = -1;

            if (!Int32.TryParse(TempData.Peek("uId").ToString(), out orderUser))
            {
                throw new ArgumentException("Set Location ID is not valid");
            }
            else if (!Int32.TryParse(TempData.Peek("uDefaultLocation").ToString(), out orderLocation))
            {
                throw new ArgumentException("Set Location ID is not valid");
            }

            TempData["workingOrderId"] = Repo.CreateOrder(orderUser, orderLocation);

            return RedirectToAction("PlaceOrder", new { id = orderUser });
        }


        public IActionResult PlaceOrder()
            {
            int workingOrderId = -1;
            if (!Int32.TryParse(TempData.Peek("workingOrderId").ToString(), out workingOrderId))
            {
                throw new ArgumentException("Working Order ID is not valid");
            }

            IEnumerable<LibOrderEntry> libOrderEntries = Repo.GetEntriesForOrder(workingOrderId);

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

            LibOrder libOrder = Repo.GetOrderByIdWithEntries(workingOrderId);
            Order dispOrder = new Order
            {
                Id = libOrder.Id,
                User = libOrder.UserId,
                Location = libOrder.LocationId,
                Time = libOrder.Time,
                TotalPrice = libOrder.TotalPrice,
                TotalItems = libOrder.TotalItems,

                OrderEntries = dispOrderEntries
            };

            IEnumerable<LibPizza> libPizzas = Repo.GetAllPizzas();
            IEnumerable<Pizza> pizzasList = libPizzas.Select(x => new Pizza
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            });

            OrderEntryAndPizzasJunction data = new OrderEntryAndPizzasJunction
            {
                OrderEntries = dispOrderEntries,
                AllPizzas = pizzasList
            };

            TempData["workingOrder"] = workingOrderId;
            TempData["runningTotal"] = dispOrder.TotalPrice;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPizzaToOrder(IFormCollection form)
        {
            int currentOrder = -1;
            int pizzaId = -1;
            int quantity = -1;
            if (!Int32.TryParse(TempData.Peek("workingOrder").ToString(), out currentOrder))
            {
                throw new ArgumentException("Stored Order ID is not valid");
            }
            else if (!Int32.TryParse(form["pizza_setter"], out pizzaId))
            {
                throw new ArgumentException("Set Pizza ID is not valid");
            } 
            else if (!Int32.TryParse(form["quantity_setter"], out quantity))
            {
                throw new ArgumentException("Set Pizza Quantity is not valid");
            }

            int entryId = Repo.AddOrderEntryGetIdBack(currentOrder, pizzaId, quantity);

            if(entryId == -1)
            {
                TempData["error"] = "Order must be between $0 and $500!";
            }
            else if(entryId == -2)
            {
                TempData["error"] = "Order must have at least 1 pizza, but no more than 12!";
            }       


            return RedirectToAction("PlaceOrder", new { id = TempData.Peek("workingOrder") });
        }

        public IActionResult RemoveEntry(int Id)
        {
            Repo.RemoveOrderEntry(Id);
            return RedirectToAction("PlaceOrder", new { id = TempData.Peek("uId") });
        }

        public IActionResult CancelOrder(int Id)
        {
            LibOrder deleteOrder = Repo.GetOrderByIdWithEntries(Id);
            IEnumerable<LibOrderEntry> deleteEntries = Repo.GetOrderEntriesForDeletion(Id);

            foreach (var item in deleteEntries)
            {
                Repo.RemoveOrderEntry(item.Id);
            }

            Repo.RemoveOrder(Id);

            return RedirectToAction("Index", new { id = TempData.Peek("uId") });
        }

        public IActionResult SubmitOrder(int Id)
        {
            LibOrder submittedOrder = Repo.GetOrderByIdWithEntries(Id);

            if (!Repo.OrderIsValidPrice(submittedOrder.TotalPrice))
            {
                TempData["error"] = "Order must be between $0 and $500!";
                return RedirectToAction("PlaceOrder", new { id = TempData.Peek("workingOrder") });
            }
            else if (!Repo.OrderIsValidSize(submittedOrder.TotalItems))
            {
                TempData["error"] = "Order must have at least 1 pizza, but no more than 12!";
                return RedirectToAction("PlaceOrder", new { id = TempData.Peek("workingOrder") });
            }
            else if (!Repo.EnoughIngredients(submittedOrder)){
                TempData["error"] = "Location does not have enough ingredients to fulfill order!";
                return RedirectToAction("PlaceOrder", new { id = TempData.Peek("workingOrder") });
            }
            else
            {
                Repo.UseIngredients(submittedOrder);
                TempData["message"] = "Order placed!";
                return RedirectToAction("Index", new { id = TempData.Peek("uId") });
            }
        }
    }
}