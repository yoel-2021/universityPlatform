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
using universityPlatform.DTO;

namespace universityPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly UniversityContext _context;


        public CategoriesController(UniversityContext context)
        {
            _context = context;
        }


        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategory()
        {
            if (_context.Category == null) {

                return NotFound();
            }
            return await _context.Category.Include(x => x.courses).ToListAsync();

        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Category>>> GetCategories(int id)
        {

            if (_context.Category == null)
            {
                return NotFound();
            }
            var categories = await _context.Category.Where(c => c.id == id).Include(c => c.courses).ToListAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }


        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<List<Category>>> PostCategories(CreationCategoryDTO categories)
        {
            if (_context.Category == null)
            {
                return Problem("Entity set 'UniversityContext.Category'  is null.");
            }
            var newCategory = new Category
            {
                id = categories.id,
                categoryName = categories.categoryName,
            };

            _context.Category.Add(newCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = categories.id }, categories);
        }

        [HttpPost("Courses")]
        public async Task<ActionResult<Category>> AddCatergoryCourse(AddCategoryCourseDTOcs categories)
        {
            var category = await _context.Category.Where(c=> c.id == categories.categoriesid)
                                                   .Include(c => c.courses)
                                                   .FirstOrDefaultAsync();
            if (category == null)
                return NotFound();

            var course = await _context.Course.FindAsync(categories.coursesid);
            if (category == null)
                return NotFound();
            category.courses.Add(course);
            await _context.SaveChangesAsync();

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<List<Category>>> PutCategories(int id, Category categories)
        {
            if (id != categories.id)
            {
                return BadRequest("ID NO FOUND");
            }

            
            _context.Update(categories);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            if (_context.Category == null)
            {
                return NotFound();
            }
            var categories = await _context.Category.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            _context.Category.Remove(categories);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriesExists(int id)
        {
            return (_context.Category?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
