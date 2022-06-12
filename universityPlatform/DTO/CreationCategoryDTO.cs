using System.ComponentModel.DataAnnotations.Schema;

namespace universityPlatform.DTO
{
    public class CreationCategoryDTO
    {
        
        public int id { get; set; }
        public string categoryName { get; set; } = string.Empty;
    }
}
