using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using universityPlatform.Models.dataAccess;
using universityPlatform.dataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using universityPlatform.DTO;
using Microsoft.AspNetCore.Cors;

namespace universityPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly UniversityContext _context;

        public CoursesController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<List<Courses>>> GetCourse()
        {
          if (_context.Course == null)
          {
              return NotFound();
          }
            return await _context.Course.Include(x => x.categories).Include(s => s.students).ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Courses>>> GetCourses(int id)
        {
          if (_context.Course == null)
          {
              return NotFound();
          }
            var courses = await _context.Course.Where(c => c.id == id).Include(c => c.categories).Include(s=> s.students).ToListAsync();

            if (courses == null)
            {
                return NotFound();
            }

            return courses;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutCourses(int id, Courses courses)
        {
            if (id != courses.id)
            {
                return BadRequest();
            }

            _context.Entry(courses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoursesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        
        public async Task<ActionResult<List<Courses>>> Create(CreationCourseDTO courses)
        {
           

              if (_context.Course == null)
               {
                   return Problem("Entity set 'UniversityContext.Course'  is null.");
               }
            var newCourse = new Courses
            {
                id = courses.id,
                name = courses.name,
                index = courses.index,
            };
            _context.Course.Add(newCourse);
                 await _context.SaveChangesAsync();

                return CreatedAtAction("GetCourses", new { id = courses.id }, courses);
        }
        [HttpPost("Categories")]
        public async Task<ActionResult<Courses>> AddCourseCategory(AddCategoryCourseDTOcs courses)
        {
            var Course = await _context.Course.Where(c => c.id == courses.coursesid)
                                                   .Include(c => c.categories)
                                                   .FirstOrDefaultAsync();
            if (Course == null)
                return NotFound();

            var category = await _context.Category.FindAsync(courses.categoriesid);

            if (category == null)
                return NotFound();
            Course.categories.Add(category);
            await _context.SaveChangesAsync();

            return Course;
        }

        [HttpPost("Students")]
        public async Task<ActionResult<Courses>> AddCourseStudent(AddStudentCourseDTO courses)
        {
            var Course = await _context.Course.Where(c => c.id == courses.coursesid)
                                                   .Include(c => c.students)
                                                   .FirstOrDefaultAsync();
            if (Course == null)
                return NotFound();

            var student = await _context.Student.FindAsync(courses.studentsid);

            if (student == null)
                return NotFound();
            Course.students.Add(student);
            await _context.SaveChangesAsync();

            return Course;
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteCourses(int id)
        {
            if (_context.Course == null)
            {
                return NotFound();
            }
            var courses = await _context.Course.FindAsync(id);
            if (courses == null)
            {
                return NotFound();
            }

            _context.Course.Remove(courses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoursesExists(int id)
        {
            return (_context.Course?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
