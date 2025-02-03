using System.Text.Json;

namespace DomainEntities {
    public static class ProductExtensions {
        public static string Serialize<T>(this T entity) where T : BaseEntity {
            if (entity is Product product && product != null) {
                return JsonSerializer.Serialize(product);
            }
            if (entity is Order order && order != null) {
                return JsonSerializer.Serialize(order);
            }
            return JsonSerializer.Serialize(entity);
        }
        public static T? Deserialize<T>(this string json) where T : BaseEntity {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
