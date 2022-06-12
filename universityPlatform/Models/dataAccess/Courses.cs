
using System.ComponentModel.DataAnnotations;


namespace universityPlatform.Models.dataAccess
{
    
    public class Courses:BaseEntity
    {   
        [Key]
        public int id { get; set; }
        public string name { get; set; } = "Ningún Curso Asociado";
        public string? index { get; set; }
      
        public List<Category>? categories { get; set; }
        public List<Students>? students { get; set; }


    }
}
