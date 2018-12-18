using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaProject1.Library;
using PizzaProject1.Models;

namespace PizzaProject1.Controllers
{
    public class HomeController : Controller
    {
        public IPizzaRepository Repo { get; }

        public HomeController(IPizzaRepository repo)
        {
            Repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CustomerSelect()
        {
            IEnumerable<LibUser> libUsers = Repo.GetAllUsersOnly();
            IEnumerable<User> dispUsers = libUsers.Select(x => new User
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DefaultLocation = x.DefaultLocation
            });

            return View(dispUsers);
        }

        public IActionResult AdminHome()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
