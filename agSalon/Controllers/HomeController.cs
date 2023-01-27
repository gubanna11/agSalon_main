using agSalon.Data;
using agSalon.Data.Enums;
using agSalon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using static System.Net.Mime.MediaTypeNames;
using agSalon.Data.Services;

namespace agSalon.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGroupsService _groupsService;
        private readonly IWebHostEnvironment _webHostEnvironment;


		public HomeController(IGroupsService groupsService, IWebHostEnvironment webHostEnvironment)
        {
            _groupsService = groupsService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _groupsService.GetAllAsync();
            
			DirectoryInfo dir = new DirectoryInfo(_webHostEnvironment.WebRootPath + "\\img\\slider");
            var list = dir.GetFiles();

            List<string> fileNames = new List<string>();
            foreach (FileInfo file in list)
            {
                fileNames.Add(file.Name);
            }

            ViewBag.SliderImgs = fileNames;
			return View(groups);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
