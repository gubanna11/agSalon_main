using agSalon.Data;
using agSalon.Data.Services;
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
    public class AttendancesController : Controller
    {
        private readonly IAttendancesService _service;
        private readonly AppDbContext _context;

        public AttendancesController(IAttendancesService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var attendances = await _service.GetAllAttendances();
            return View(attendances);
            //TimeOnly
            //var times = await _context.Attendances.Select(a => a.Time).ToListAsync();
            //.GetValueOrDefault()
            //return View(times);
        }

        public async Task<IActionResult> NewAttendance(int id = 1)
        {
            int groupId = id;

            AttendanceDropdownsVM dropdowns = await _service.GetAttendanceDropdownsValues(groupId);

            ViewBag.Groups = new SelectList(dropdowns.Groups, "Id", "Name", groupId);
            ViewBag.Services = new SelectList(dropdowns.Services, "Id", "Name");
            ViewBag.Workers = new SelectList(dropdowns.Workers, "Id", "Surname");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewAttendance(NewAttendanceVM newAttendance)
        {
            await _service.AddNewAttendance(newAttendance);

            return RedirectToAction("Index");
        }

        public IActionResult ServicesDropdown(int id)
        {
            var services = _context.Services.Include(s => s.Service_Group).Where(s => s.Service_Group.GroupId == id).OrderBy(s => s.Name).ToList();
            
            return PartialView(services);
        }

        public ActionResult WorkersDropdown(int id)
        {
            return PartialView(_context.Workers_Groups.Include(wg => wg.Worker).Where(wg => wg.GroupId == id).Select(wg => wg.Worker).ToList());

        }
    }
}
