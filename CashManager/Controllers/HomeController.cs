using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CashManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CashManager.Data;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(
                        UserManager<ApplicationUser> userManager,
                        ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var vm = new HomeIndexViewModel();

            vm.Products = _dbContext.Product;
            vm.Purchases = _dbContext.Purchase.Include(p => p.Product).Include(p => p.User);

            return View(vm);
        }

        [AllowAnonymous]
        [HttpPost("/admin/seed-data")]
        public async Task<IActionResult> InitData()
        {
            if (_userManager.Users.Any(u => u.NormalizedUserName == "cedric.tallichet@burningbox.ch".ToUpper()) == false)
            {
                await _userManager.CreateAsync(new ApplicationUser { UserName = "cedric.tallichet@burningbox.ch" }, "Test@123");
                await _userManager.CreateAsync(new ApplicationUser { UserName = "david.menoud@burningbox.ch" }, "Test@123");
            }

            if (_dbContext.Product.Any() == false)
            {
                Product coca, fanta;

                _dbContext.Product.AddRange(
                    (coca = new Product { Name = "Coca", Price = 1.5 }),
                    (fanta = new Product { Name = "Fanta", Price = 1.5 }),
                    new Product { Name = "Rivella Bleu", Price = 1.5 },
                    new Product { Name = "Mars", Price = 1 },
                    new Product { Name = "Twix", Price = .5 }
                );


                if (_dbContext.Purchase.Any() == false)
                {
                    var cedric = _dbContext.Users.FirstOrDefault(u => u.UserName == "cedric.tallichet@burningbox.ch");
                    _dbContext.Purchase.AddRange(
                        new Purchase { Product = coca, User = cedric },
                        new Purchase { Product = coca, User = cedric },
                        new Purchase { Product = coca, User = cedric },
                        new Purchase { Product = fanta, User = cedric }
                    );
                }
                await _dbContext.SaveChangesAsync();
            }

            return Ok();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
