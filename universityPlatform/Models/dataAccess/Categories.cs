using System.ComponentModel.DataAnnotations;

namespace universityPlatform.Models.dataAccess
{
    public class Categories:BaseEntity
    {
        [Required]
        public int id { get; set; }
        public string categoryName { get; set; } = string.Empty;

        public int? coursesId { get; set; }
    }
}
