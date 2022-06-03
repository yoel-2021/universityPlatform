namespace universityPlatform.Models.DTO
{
    public class StudentDto
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public DateTime birthday { get; set; } = new DateTime(2018, 9, 10);
        public string email { get; set; } = string.Empty;
    }
}
