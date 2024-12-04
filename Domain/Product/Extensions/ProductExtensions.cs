using Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Product.Extensions
{
    public static class ProductExtensions
    {
        // Change to an IEnumberable (from a List) to avoid calling .ToList()
        // b/c ToList will traverse the list unnecessarily leading to inefficiencies.
        public static IEnumerable<T> InStock<T>(this List<T> list) where T : IProduct
        {
            return list.Where(p => p.Qty > 0);
        }
    }
}
