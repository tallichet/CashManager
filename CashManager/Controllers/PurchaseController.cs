using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly Data.ApplicationDbContext _applicationDbContext;

        public PurchaseController(Data.ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet("/products/{productId}/purchases")]
        public IEnumerable<Purchase> GetPurchaseForByProduct(int productId)
        {
            return _applicationDbContext.Purchase.Include("User").ToList().Where(p => p.Product.Id == productId);
        }

        [HttpGet("/user/{username}/purchases")]
        public IEnumerable<Purchase> GetPurchaseForByUser(string username)
        {
            return _applicationDbContext.Purchase.Where(p => p.User.NormalizedUserName == username.ToUpper());
        }

        [HttpGet("/user/{username}/purchases/add")]
        public async Task<IActionResult> Add(string username, int productId)
        {

            Purchase newPurchase = new Purchase()
            {
                Product = _applicationDbContext.Product.Find(productId),
                User = _applicationDbContext.Users.Single(u => u.NormalizedUserName == username.ToUpper()),
            };
            _applicationDbContext.Purchase.Add(newPurchase);
            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }

    }
}