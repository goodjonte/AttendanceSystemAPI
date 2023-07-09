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
    public class ClassesPeriodsController : ControllerBase
    {
        private readonly AttendanceSystemAPIContext _context;

        public ClassesPeriodsController(AttendanceSystemAPIContext context)
        {
            _context = context;
        }

        // GET: api/ClassesPeriods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassesPeriods>>> GetClassesPeriods()
        {
          if (_context.ClassesPeriods == null)
          {
              return NotFound();
          }
            return await _context.ClassesPeriods.ToListAsync();
        }

        // GET: api/ClassesPeriods/5
        [HttpGet("{classId}")]
        public IQueryable<ClassesPeriods> GetClassesPeriods(Guid classId)
        {
            var classesPeriods = _context.ClassesPeriods.Where(c => c.ClassId == classId);
            return classesPeriods;
        }
        // GET: api/ClassesPeriods/5
        [HttpGet("ById/{id}")]
        public async Task<ActionResult<ClassesPeriods>> GetClassesPeriodsById(Guid id)
        {
            var classesPeriods = await _context.ClassesPeriods.FindAsync(id);
            return Ok(classesPeriods);
        }

        // POST: api/ClassesPeriods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassesPeriods>> PostClassesPeriods(ClassesPeriods classesPeriods)
        {
          if (_context.ClassesPeriods == null)
          {
              return Problem("Entity set 'AttendanceSystemAPIContext.ClassesPeriods'  is null.");
          }
            _context.ClassesPeriods.Add(classesPeriods);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassesPeriods", new { id = classesPeriods.Id }, classesPeriods);
        }

        // DELETE: api/ClassesPeriods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassesPeriods(Guid id)
        {
            if (_context.ClassesPeriods == null)
            {
                return NotFound();
            }
            var classesPeriods = await _context.ClassesPeriods.FindAsync(id);
            if (classesPeriods == null)
            {
                return NotFound();
            }

            _context.ClassesPeriods.Remove(classesPeriods);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
