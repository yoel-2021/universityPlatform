using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using universityPlatform.Models.dataAccess;
using universityPlatform.dataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


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
        public async Task<ActionResult<IEnumerable<Courses>>> GetCourse()
        {
          if (_context.Course == null)
          {
              return NotFound();
          }
            return await _context.Course.ToListAsync();
               
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Courses>> GetCourses(int id)
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

            return courses;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Courses>> PostCourses(Courses courses)
        {
          if (_context.Course == null)
          {
              return Problem("Entity set 'UniversityContext.Course'  is null.");
          }
            _context.Course.Add(courses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourses", new { id = courses.id }, courses);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
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
