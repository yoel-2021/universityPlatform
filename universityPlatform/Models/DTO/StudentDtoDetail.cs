namespace universityPlatform.Models.DTO
{
    public class StudentDtoDetail
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public DateTime birthday { get; set; } = new DateTime(2018, 9, 10);
        public string email { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string street { get; set; } = string.Empty;
        public string community { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public int zipCode { get; set; }
        public string? cursosMatriculados { get; set; }
    }
}
