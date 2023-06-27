using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystemAPI.Data;
using AttendanceSystemAPI.Models;

namespace AttendanceSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolClassesController : ControllerBase
    {
        private readonly AttendanceSystemAPIContext _context;

        public SchoolClassesController(AttendanceSystemAPIContext context)
        {
            _context = context;
        }

        // GET: api/SchoolClasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolClass>>> GetSchoolClass()
        {
          if (_context.SchoolClass == null)
          {
              return NotFound();
          }
            return await _context.SchoolClass.ToListAsync();
        }

        // GET: api/SchoolClasses/5
        [HttpGet("{id}/{teachersClasses}")]
        public  IQueryable<SchoolClass> GetSchoolClass(Guid id, bool teachersClasses)
        {
            IQueryable<SchoolClass> schoolClasses;
            if (teachersClasses)
            {
                schoolClasses = _context.SchoolClass.Where(c => c.TeacherId == id);
            }
            else
            {
                schoolClasses = _context.SchoolClass.Where(c => c.Id == id);
            }

            return schoolClasses;
        }

        // GET: api/SchoolClasses/5
        [HttpGet("GetClassInfo/{id}")]
        public IActionResult GetClassInfo(Guid id)
        {
            if(_context == null)
            {
                return BadRequest();
            }
            
            SchoolClass? schoolClass = _context.SchoolClass.Find(id);
            
            if (schoolClass == null)
            {
                return NotFound();
            }
            User? teacher = _context.User.Find(schoolClass.TeacherId);
            if (teacher == null)
            {
                return NotFound();
            }
            schoolClass.TeachersName = teacher.FirstName + " " + teacher.LastName;



            return Ok(schoolClass);
        }

        // PUT: api/SchoolClasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolClass(Guid id, SchoolClass schoolClass)
        {
            if (id != schoolClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(schoolClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolClassExists(id))
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

        // POST: api/SchoolClasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SchoolClass>> PostSchoolClass(SchoolClass schoolClass)
        {
          if (_context.SchoolClass == null)
          {
              return Problem("Entity set 'AttendanceSystemAPIContext.SchoolClass'  is null.");
          }
            
            _context.SchoolClass.Add(schoolClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchoolClass", new { id = schoolClass.Id }, schoolClass);
        }

        // DELETE: api/SchoolClasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolClass(Guid id)
        {
            if (_context.SchoolClass == null)
            {
                return NotFound();
            }
            var schoolClass = await _context.SchoolClass.FindAsync(id);
            if (schoolClass == null)
            {
                return NotFound();
            }

            _context.SchoolClass.Remove(schoolClass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolClassExists(Guid id)
        {
            return (_context.SchoolClass?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
