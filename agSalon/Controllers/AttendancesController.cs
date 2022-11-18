using agSalon.Data;
using agSalon.Data.Enums;
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
            var attendances = await _service.GetNotRenderedAttendances();

            ViewBag.IsPaid = await _service.GetNotRenderedIsPaidAttendances();
            ViewBag.NotPaid = await _service.GetNotRenderedNotPaidAttendances();

            ViewBag.Total = _service.GetTotal();

            return View(attendances);
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
            return PartialView(_context.Workers_Groups
                //.Include(wg => wg.Worker).Where(wg => wg.GroupId == id).Select(wg => wg.Worker)
                .ToList());
        }


        public async Task<ActionResult> FilterNotPaid()
        {
            var model = await _service.GetNotRenderedNotPaidAttendances();
            return View("Index", model);
        }

        public async Task<ActionResult> FilterIsPaid()
        {
            var model = await _service.GetNotRenderedIsPaidAttendances();
            return View("Index", model);
        }



        public async Task<IActionResult> History()
        {
            var attendances = await _service.GetIsRenderedAttendances();

            return View(attendances);
        }


        public async Task<IActionResult> CompletePayment()
        {
            var attendances = await _service.GetNotRenderedNotPaidAttendances();

            foreach (var item in attendances)
                item.IsPaid = YesNoEnum.Yes;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var attendance = await _service.GetAttendanceById(id);

            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int id)
        {
            var attendance = await _service.GetAttendanceById(id);

            return View(attendance);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> SaveChanges(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
