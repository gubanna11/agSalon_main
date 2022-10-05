using agSalon.Data;
using agSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            DateTime d = new DateTime(2020, 2, 1);
            //ViewBag.date = d.ToShortDateString();
            //return View();

            //return View((object)d.ToShortDateString());
            //return View("Index", d.ToShortDateString());
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
