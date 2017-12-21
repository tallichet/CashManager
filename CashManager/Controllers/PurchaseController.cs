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

        [HttpGet]
        public IEnumerable<Purchase> GetPurchaseForByProduct(int productId)
        {
            return _applicationDbContext.Purchase.Include("User").ToList().Where(p => p.Product.Id == productId);
        }

        [HttpGet]
        public IEnumerable<Purchase> GetPurchaseForByUser(string user)
        {
            return _applicationDbContext.Purchase.Where(p => p.User.Id == user);
        }

        [HttpGet]
        public void Add(string userId, int productId)
        {

            Purchase newPurchase = new Purchase()
            {
                Product = _applicationDbContext.Product.Find(productId),
                User = _applicationDbContext.Users.Find(userId),
            };
            _applicationDbContext.Purchase.Add(newPurchase);
        }

    }
}