using agSalon.Data;
using agSalon.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var clients = _context.Users.ToList();

            return View(clients);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid)
                return View(client);

            await _context.Users.AddAsync(client);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
