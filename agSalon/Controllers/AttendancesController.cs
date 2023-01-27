using agSalon.Data;
using agSalon.Data.Enums;
using agSalon.Data.Services;
using agSalon.Data.Static;
using agSalon.Data.ViewModels;
using agSalon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace agSalon.Controllers
{
	[Authorize]
	public class AttendancesController : Controller
    {
        private readonly IAttendancesService _service;

        public AttendancesController(IAttendancesService service)
        {
            _service = service;
        }

		public async Task<IActionResult> Index()
        {
            var attendances = await _service.GetNotRenderedAttendances();

            string clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.IsPaid = await _service.GetNotRenderedIsPaidAttendances(clientId);

            ViewBag.NotPaid = await _service.GetNotRenderedNotPaidAttendances(clientId);

            ViewBag.Total = _service.GetTotal(clientId);

            return View(attendances);
        }


        public async Task<IActionResult> NewAttendance(int id = 1)
        {
            int groupId = id;

            AttendanceDropdownsVM dropdowns = await _service.GetAttendanceDropdownsValues(groupId);

            ViewBag.Groups = new SelectList(dropdowns.Groups, "Id", "Name", groupId);

			//ViewBag.Services = new SelectList(dropdowns.Services, "Id", "Name");

			ViewBag.Services = dropdowns.Services;
			ViewBag.Workers = dropdowns.Workers;

            //ViewBag.ServiceObjects = dropdowns.Services;


			//List<Client> workersList= dropdowns.Workers;
			//List<WorkerListItemVM> workers = new List<WorkerListItemVM>();

			//foreach (var worker in workersList)
			//{
			//    workers.Add(new WorkerListItemVM
			//    {
			//        Id = worker.Id,
			//        FullName = worker.Name + " " + worker.Surname,
			//    });
			//}

			//ViewBag.Workers = new SelectList(workers, "Id", "FullName");

			return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewAttendance(NewAttendanceVM newAttendance)
        {
            string clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _service.AddNewAttendance(newAttendance, clientId);
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ServicesDropdown(int groupId)
            => PartialView((await _service.GetAttendanceDropdownsValues(groupId)).Services);
        

        public async Task<IActionResult> WorkersDropdown(int groupId)
			=> PartialView((await _service.GetAttendanceDropdownsValues(groupId)).Workers);
        


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
            await _service.CompletePaymentAll(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var attendance = await _service.GetAttendanceById(id);

            if (attendance != null)
            {
                await _service.DeleteAsync(id);
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
            await _service.UpdateAsync(attendance);

            return RedirectToAction("AllAttendances");
        }

		[Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Worker}")]
		public async Task<IActionResult> AllAttendances()
        {
            var attendances = await _service.GeAllAttendances(User.FindFirstValue(ClaimTypes.NameIdentifier), User.FindFirstValue(ClaimTypes.Role));

			return View(attendances);
        }

    }
}
