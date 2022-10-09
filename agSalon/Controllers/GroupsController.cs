using agSalon.Data;
using agSalon.Data.Enums;
using agSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace agSalon.Controllers
{
    public class GroupsController : Controller
    {
        private readonly AppDbContext _context;

        public GroupsController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includeProperties) where T:class
        {
            IQueryable<T> query = _context.Set<T>();

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }

        public async Task<IActionResult> Index()
        {
            //var allGroups = await GetAllAsync<GroupsOfServices>(n => n.Services_Groups);
            var allGroups = await _context.Groups.ToListAsync();

            ViewBag.Services = await GetAllAsync<Service_Group>(n => n.Service);

            return View(allGroups);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupsOfServices newGroup)
        {
            await _context.Groups.AddAsync(newGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
