using System.ComponentModel.DataAnnotations;

namespace universityPlatform.Models.dataAccess
{
    public class BaseEntity
    {
        
        public string? createBy { get; set; } = string.Empty;
        public DateTime? createdAt { get; set; } = new DateTime(2022,5,20);
        public string? updateBy { get; set; } = string.Empty;
        public DateTime? updatedAt { get; set; } = new DateTime(2022,5,20);
       
    }
}
