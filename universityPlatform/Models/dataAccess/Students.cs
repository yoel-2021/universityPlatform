using System.ComponentModel.DataAnnotations;

namespace universityPlatform.Models.dataAccess
{
    public class Students : BaseEntity
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; } = string.Empty;

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

        public int? coursesId { get; set; }
    }
}
