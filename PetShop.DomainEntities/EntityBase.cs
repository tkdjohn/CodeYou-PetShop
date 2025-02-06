using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PetShop.DomainEntities {
    public class EntityBase {

        [Required]
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [Required]
        [JsonIgnore]
        public DateTime LastUpdatedDate { get; set; }

        public EntityBase() {
            LastUpdatedDate = DateTime.Now;
            CreatedDate = DateTime.Now;
        }
    }
}
