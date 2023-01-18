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
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GroupsController(IGroupsService service, AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        //public async Task<IEnumerable<T>> GetAllAsync<T>(params Expression<Func<T, object>>[] includeProperties) where T:class
        //{
        //    IQueryable<T> query = _context.Set<T>();

        //    query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        //    return await query.ToListAsync();
        //}

        public async Task<IActionResult> Index()
        {
			var allGroups = await _service.GetAllAsync();

			return View(allGroups);
        }

        public IActionResult Create()
        {
            return View();
        }

        private string UploadFIle(IFormFile file)
        {
            string uniqueFileName = null;

            if(file != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img/groups");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupOfServices newGroup)
        {
            newGroup.ImgUrl = UploadFIle(newGroup.Img);

            await _service.AddAsync(newGroup);

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
            if(group.Img != null)
			    group.ImgUrl = UploadFIle(group.Img);

			await _service.UpdateAsync(group);

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
