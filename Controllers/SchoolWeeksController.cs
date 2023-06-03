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
    public class SchoolWeeksController : ControllerBase
    {
        private readonly AttendanceSystemAPIContext _context;

        public SchoolWeeksController(AttendanceSystemAPIContext context)
        {
            _context = context;
        }

        // GET: api/SchoolWeeks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolWeek>>> GetSchoolWeek()
        {
          if (_context.SchoolWeek == null)
          {
              return NotFound();
          }
            return await _context.SchoolWeek.ToListAsync();
        }

        // GET: api/SchoolWeeks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolWeek>> GetSchoolWeek(Guid id)
        {
          if (_context.SchoolWeek == null)
          {
              return NotFound();
          }
            var schoolWeek = await _context.SchoolWeek.FindAsync(id);

            if (schoolWeek == null)
            {
                return NotFound();
            }

            return schoolWeek;
        }

        // PUT: api/SchoolWeeks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolWeek(Guid id, SchoolWeek schoolWeek)
        {
            if (id != schoolWeek.Id)
            {
                return BadRequest();
            }

            _context.Entry(schoolWeek).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolWeekExists(id))
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

        // POST: api/SchoolWeeks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SchoolWeek>> PostSchoolWeek(SchoolWeek schoolWeek)
        {
          if (_context.SchoolWeek == null)
          {
              return Problem("Entity set 'AttendanceSystemAPIContext.SchoolWeek'  is null.");
          }
            _context.SchoolWeek.Add(schoolWeek);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchoolWeek", new { id = schoolWeek.Id }, schoolWeek);
        }

        // DELETE: api/SchoolWeeks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolWeek(Guid id)
        {
            if (_context.SchoolWeek == null)
            {
                return NotFound();
            }
            var schoolWeek = await _context.SchoolWeek.FindAsync(id);
            if (schoolWeek == null)
            {
                return NotFound();
            }

            _context.SchoolWeek.Remove(schoolWeek);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolWeekExists(Guid id)
        {
            return (_context.SchoolWeek?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
