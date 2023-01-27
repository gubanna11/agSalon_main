using agSalon.Data;
using agSalon.Data.Enums;
using agSalon.Data.Services;
using agSalon.Data.ViewModels;
using agSalon.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace agSalon.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupsService _service;
      

        public GroupsController(IGroupsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
			var allGroups = await _service.GetAllAsync();

			return View(allGroups);
        }

        public IActionResult Create()
        {
            return View();
        }

       

        [HttpPost]
        public async Task<IActionResult> Create(GroupOfServices newGroup)
        {
            await _service.AddNewGroupAsync(newGroup);

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int groupId)
        {
            var group = await _service.GetByIdAsync(groupId);
            
            return View(group);
        }

		[HttpPost]
		public async Task<IActionResult> Edit(GroupOfServices group)
		{
			await _service.UpdateGroupAsync(group);

			return RedirectToAction("Index");
		}


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

			return RedirectToAction("Index");
		}
	}
}
