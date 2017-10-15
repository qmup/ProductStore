using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStore
{
    public class Product
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public double unitPrice { get; set; }
        public int quantity { get; set; }
    }
}
