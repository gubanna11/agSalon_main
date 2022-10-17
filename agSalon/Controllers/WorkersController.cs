using agSalon.Data;
using agSalon.Data.ViewModels;
using agSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Controllers
{
    public class WorkersController : Controller
    {
        private readonly AppDbContext _context;

        public WorkersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var workers = _context.Workers.Include(w => w.Workers_Groups).ThenInclude(wg => wg.Group).ToList();

            return View(workers);
        }

        public async Task<IActionResult> Create()
        {
            List<GroupsOfServices> groups = await _context.Groups.OrderBy(g => g.Name).ToListAsync();
            ViewBag.Groups = new SelectList(groups, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewWorkerVM newWorker)
        {
            if (!ModelState.IsValid)
            {
                List<GroupsOfServices> groups = await _context.Groups.OrderBy(g => g.Name).ToListAsync();
                ViewBag.Groups = new SelectList(groups, "Id", "Name");

                return View(newWorker);
            }

            Worker worker = new Worker
            {
                Surname = newWorker.Surname,
                Initials = newWorker.Initials,
                Phone = newWorker.Phone,
                DateBirth = newWorker.DateBirth,
                Gender = newWorker.Gender,
                Address = newWorker.Address
            };

            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();

            List<Worker_Group> workerGroupList = new List<Worker_Group>();

            foreach (var groupId in newWorker.GroupsIds)
            {
                workerGroupList.Add(new Worker_Group
                {
                    WorkerId = worker.Id,
                    GroupId = groupId
                });
            }


            await _context.Workers_Groups.AddRangeAsync(workerGroupList);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
