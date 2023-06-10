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
    public class SchoolPeriodsController : ControllerBase
    {
        private readonly AttendanceSystemAPIContext _context;

        public SchoolPeriodsController(AttendanceSystemAPIContext context)
        {
            _context = context;
        }

        // GET: api/SchoolPeriods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolPeriod>>> GetSchoolPeriod()
        {
          if (_context.SchoolPeriod == null)
          {
              return NotFound();
          }
            return await _context.SchoolPeriod.ToListAsync();
        }

        // GET: api/SchoolPeriods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolPeriod>> GetSchoolPeriod(Guid id)
        {
          if (_context.SchoolPeriod == null)
          {
              return NotFound();
          }
            var schoolPeriod = await _context.SchoolPeriod.FindAsync(id);

            if (schoolPeriod == null)
            {
                return NotFound();
            }

            return schoolPeriod;
        }

        // PUT: api/SchoolPeriods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchoolPeriod(Guid id, SchoolPeriod schoolPeriod)
        {
            if (id != schoolPeriod.Id)
            {
                return BadRequest();
            }

            _context.Entry(schoolPeriod).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolPeriodExists(id))
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

        // POST: api/SchoolPeriods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SchoolPeriod>> PostSchoolPeriod(SchoolPeriod schoolPeriod)
        {
          if (_context.SchoolPeriod == null)
          {
              return Problem("Entity set 'AttendanceSystemAPIContext.SchoolPeriod'  is null.");
          }
            _context.SchoolPeriod.Add(schoolPeriod);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSchoolPeriod", new { id = schoolPeriod.Id }, schoolPeriod);
        }

        // DELETE: api/SchoolPeriods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolPeriod(Guid id)
        {
            if (_context.SchoolPeriod == null)
            {
                return NotFound();
            }
            var schoolPeriod = await _context.SchoolPeriod.FindAsync(id);
            if (schoolPeriod == null)
            {
                return NotFound();
            }

            _context.SchoolPeriod.Remove(schoolPeriod);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolPeriodExists(Guid id)
        {
            return (_context.SchoolPeriod?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
