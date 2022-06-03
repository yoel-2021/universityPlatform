using System.ComponentModel.DataAnnotations;

namespace universityPlatform.Models.dataAccess
{
    public class Users: BaseEntity
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string name { get; set; } = string.Empty;

        [Required]
        public string lastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string email { get; set; } = string.Empty;

        [Required]
        public string password { get; set; } = string.Empty;

        [Required]
        public string role { get; set; }= string.Empty;

    }
}
