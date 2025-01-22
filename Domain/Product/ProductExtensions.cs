using System.Text.Json;

namespace Domain.Product {
    public static class ProductExtensions {
        // Change to an IEnumberable (from a List) to avoid calling .ToList()
        // b/c ToList will traverse the list unnecessarily leading to inefficiencies.
        public static IEnumerable<T> InStock<T>(this List<T> list) where T : IProduct {
            return list.Where(p => p.Quantity > 0);
        }

        public static string Serialize<T>(this T product) where T: IProduct {
            if (product is DogLeash dogLeash && dogLeash != null) {
                return JsonSerializer.Serialize(dogLeash);
            }
            if (product is CatFood catFood && catFood != null) {
                return JsonSerializer.Serialize(catFood);
            }
            return JsonSerializer.Serialize(product);
        }
    }
}
