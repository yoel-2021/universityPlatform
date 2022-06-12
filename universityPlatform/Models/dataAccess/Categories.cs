
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace universityPlatform.Models.dataAccess
{
    
    public class Category:BaseEntity
    {
        
        [Key]
        public int id { get; set; }
        public string categoryName { get; set; } = "Ninguna Categoria Asociada";

        public List<Courses>?courses { get; set; }

    }
}
