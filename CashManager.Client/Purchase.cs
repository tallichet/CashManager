using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashManager.Client
{
    public class Purchase
    {
        public int id { get; set; }
        public Product product { get; set; }
    }

    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
    }


}
