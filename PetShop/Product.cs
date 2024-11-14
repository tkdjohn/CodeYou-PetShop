using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop
{
    public class Product
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int Qty { get; set; } = 0;
        public decimal Price { get; set; } = 0.0M;
    }
}
