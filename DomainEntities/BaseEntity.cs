using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DomainEntities {
    public class BaseEntity {

        [Required]
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }

        [Required]
        [JsonIgnore]
        public DateTime LastUpdatedDate { get; set; }

        public BaseEntity() {
            LastUpdatedDate = DateTime.Now;
            CreatedDate = DateTime.Now;
        }
    }
}
