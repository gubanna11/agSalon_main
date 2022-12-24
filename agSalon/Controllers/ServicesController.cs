using agSalon.Data;
using agSalon.Data.Enums;
using agSalon.Data.ViewModels;
using agSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index(int id)
        {
            var services = _context.Services.Where(s => s.Service_Group.GroupId == id);
            ViewBag.GroupName = _context.Groups.Where(n => n.Id == id).Select(n => n.Name).FirstOrDefault();
            return View(services);
        }

        public async Task<IActionResult> Create()
        {
            List<GroupsOfServices> groups = await _context.Groups.OrderBy(g => g.Name).ToListAsync();
            ViewBag.Groups = new SelectList(groups, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewServiceVM newService)
        {
            //NAME DUPLICATE
            if (!ModelState.IsValid)
            {
                List<GroupsOfServices> groups = await _context.Groups.OrderBy(g => g.Name).ToListAsync();
                ViewBag.Groups = new SelectList(groups, "Id", "Name");

                return View(newService);
            }

            Service service = new Service
            {
                Name = newService.Name,
                Price = newService.Price
            };

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            Service_Group serviceGroup = new Service_Group
            {
                ServiceId = service.Id,
                GroupId = newService.GroupId
            };

            await _context.Services_Groups.AddAsync(serviceGroup);
            await _context.SaveChangesAsync();

            return Redirect("Index/" + serviceGroup.GroupId);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var service = _context.Services.Where(s => s.Id == id).Include(s => s.Service_Group).FirstOrDefault();
            int groupId = service.Service_Group.GroupId;

			if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }

			ViewBag.GroupName = _context.Groups.Where(n => n.Id == id).Select(n => n.Name).FirstOrDefault();

			return Redirect("~/Services/Index/" + groupId);
        }
    }
}
