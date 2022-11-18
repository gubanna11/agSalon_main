using agSalon.Data;
using agSalon.Data.ViewModels;
using agSalon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agSalon.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Client> _userManager;
        private readonly SignInManager<Client> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<Client> userManager, SignInManager<Client> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (result.Succeeded)
                        return RedirectToAction("Index", "Home");
                }
                TempData["Error"] = "Wrong credentials(password). Please, try again";
                return View(loginVM);
            }
            
            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(loginVM);
        }
    }
}
