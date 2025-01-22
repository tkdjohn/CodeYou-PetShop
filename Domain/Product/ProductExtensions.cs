using System.Text.Json;

namespace Domain.Product {
    public static class ProductExtensions {
        public static string Serialize<T>(this T product) where T: Product {
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
