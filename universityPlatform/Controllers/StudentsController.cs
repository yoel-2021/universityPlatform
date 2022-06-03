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
using universityPlatform.Models.DTO;

namespace universityPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityContext _context;

        public StudentsController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudent()
        {
            if (_context.Student == null)
            {
                return NotFound();
            }
            return await _context.Student.ToListAsync();
        }
        /*public IQueryable<StudentDto> GetBooks()
        {
           
            var students = from b in _context.Student
                           select new StudentDto()
                           {
                               id = b.id,
                               name = b.name,
                               lastName = b.lastName,
                               birthday = b.birthday,
                               email = b.email,
                               
                               
                     

                        };
           

            return students;
        }*/



        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudents(int id)
        {
          if (_context.Student == null)
          {
              return NotFound();
          }
            var students = await _context.Student.FindAsync(id);

            if (students == null)
            {
                return NotFound();
            }

            return students;


        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutStudents(int id, Students students)
        {
            if (id != students.id)
            {
                return BadRequest();
            }

            _context.Entry(students).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Students>> PostStudents(Students students)
        {
          if (_context.Student == null)
          {
              return Problem("Entity set 'UniversityContext.Student'  is null.");
          }
            _context.Student.Add(students);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudents", new { id = students.id }, students);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteStudents(int id)
        {
            if (_context.Student == null)
            {
                return NotFound();
            }
            var students = await _context.Student.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }

            _context.Student.Remove(students);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentsExists(int id)
        {
            return (_context.Student?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
