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
    public class CategoriesController : ControllerBase
    {
        private readonly UniversityContext _context;

        public CategoriesController(UniversityContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategory()
        {
          if (_context.Category == null)
          {
              return NotFound();
          }
            return await _context.Category.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategories(int id)
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

            return categories;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutCategories(int id, Categories categories)
        {
            if (id != categories.id)
            {
                return BadRequest();
            }

            _context.Entry(categories).State = EntityState.Modified;

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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Categories>> PostCategories(Categories categories)
        {
          if (_context.Category == null)
          {
              return Problem("Entity set 'UniversityContext.Category'  is null.");
          }
            _context.Category.Add(categories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = categories.id }, categories);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
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
