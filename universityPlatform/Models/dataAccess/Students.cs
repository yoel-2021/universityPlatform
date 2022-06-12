using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace universityPlatform.Models.dataAccess
{
    
    public class Students : BaseEntity
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; } = "Ningún Studiante Asociado";

        [Required]
        public string lastName { get; set; } = string.Empty;

        [Required]

        public DateTime birthday { get; set; } = new DateTime(2018, 9, 10);
        public string email { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string street { get; set; } = string.Empty;
        public string community { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public int zipCode { get; set; }

        public List<Courses>?courses { get; set; }
        
    }
}
