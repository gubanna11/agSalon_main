using agSalon.Data;
using agSalon.Data.Enums;
using agSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Controllers
{
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;

        public ServicesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var services = _context.Services.Where(s => s.Service_Group.GroupId == id);
            return View(services);
        }

        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(GroupsOfServices newGroup)
        //{
        //    await _context.Groups.AddAsync(newGroup);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
    }
}
