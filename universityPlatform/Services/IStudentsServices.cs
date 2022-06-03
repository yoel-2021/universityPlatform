using universityPlatform.Models.dataAccess;

namespace universityPlatform.Services
{
    public interface IStudentsServices
    {
        IEnumerable<Students> GetStudentsWithCourses();
        IEnumerable<Students> GetStudentsWithNoCourses();
    }
}
