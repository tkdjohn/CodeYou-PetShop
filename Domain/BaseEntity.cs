using System.ComponentModel.DataAnnotations;

namespace Domain {
    public class BaseEntity {
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastUpdatedDate { get; set; }

        public BaseEntity() {
            LastUpdatedDate = DateTime.Now;
            CreatedDate = DateTime.Now;
        }
    }
}
