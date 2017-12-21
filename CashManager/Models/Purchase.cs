using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashManager.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
