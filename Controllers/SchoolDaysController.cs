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
    public class SchoolDaysController : ControllerBase
    {
        private readonly AttendanceSystemAPIContext _context;

        public SchoolDaysController(AttendanceSystemAPIContext context)
        {
            _context = context;
        }

        // GET: api/SchoolDays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDay>>> GetSchoolDay()
        {
          if (_context.SchoolDay == null)
          {
              return NotFound();
          }
            return await _context.SchoolDay.ToListAsync();
        }

        // GET: api/SchoolDays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDay>> GetSchoolDay(Guid id)
        {
          if (_context.SchoolDay == null)
          {
              return NotFound();
          }
            var schoolDay = await _context.SchoolDay.FindAsync(id);

            if (schoolDay == null)
            {
                return NotFound();
            }

            return schoolDay;
        }

        // PUT: api/SchoolDays/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolDay(Guid id, SchoolDay schoolDay)
        {
            if (id != schoolDay.Id)
            {
                return BadRequest();
            }

            _context.Entry(schoolDay).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolDayExists(id))
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

        // POST: api/SchoolDays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SchoolDay>> PostSchoolDay(SchoolDay schoolDay)
        {
          if (_context.SchoolDay == null)
          {
              return Problem("Entity set 'AttendanceSystemAPIContext.SchoolDay'  is null.");
          }
            schoolDay.Id = Guid.NewGuid();
            _context.SchoolDay.Add(schoolDay);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchoolDay", new { id = schoolDay.Id }, schoolDay);
        }

        // DELETE: api/SchoolDays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolDay(Guid id)
        {
            if (_context.SchoolDay == null)
            {
                return NotFound();
            }
            var schoolDay = await _context.SchoolDay.FindAsync(id);
            if (schoolDay == null)
            {
                return NotFound();
            }

            _context.SchoolDay.Remove(schoolDay);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolDayExists(Guid id)
        {
            return (_context.SchoolDay?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
