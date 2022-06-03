using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universityPlatform.Models.dataAccess
{   
    public class Courses:BaseEntity
    {   [Required]
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string? index { get; set; }
        
        public int? studentsId { get; set; }
        public int? categoriesId  { get; set; }


    }
}
