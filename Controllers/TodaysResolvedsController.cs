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
    public class TodaysResolvedsController : ControllerBase
    {
        private readonly AttendanceSystemAPIContext _context;

        public TodaysResolvedsController(AttendanceSystemAPIContext context)
        {
            _context = context;
        }

        // GET: api/TodaysResolveds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodaysResolved>>> GetTodaysResolved()
        {
          if (_context.TodaysResolved == null)
          {
              return NotFound();
          }
            return await _context.TodaysResolved.ToListAsync();
        }

        // GET: api/TodaysResolveds/5
        [HttpGet("{id}")]
        public ActionResult<TodaysResolved> GetTodaysResolved(Guid id)
        {
          if (_context.TodaysResolved == null)
          {
              return NotFound();
          }
            TodaysResolved? todaysResolved = _context.TodaysResolved.FirstOrDefault((tr) => tr.StudentId == id);
            if(todaysResolved == null)
            {
                return NotFound();
            }
            if(todaysResolved.DateValid.Date == DateTime.Today.Date && todaysResolved.DateValid.Month == DateTime.Today.Month && todaysResolved.DateValid.Year == DateTime.Today.Year)
            {
                return todaysResolved;
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/TodaysResolveds
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodaysResolved>> PostTodaysResolved(TodaysResolved todaysResolved)
        {
          if (_context.TodaysResolved == null)
          {
              return Problem("Entity set 'AttendanceSystemAPIContext.TodaysResolved'  is null.");
          }
            _context.TodaysResolved.Add(todaysResolved);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodaysResolved", new { id = todaysResolved.Id }, todaysResolved);
        }

        // DELETE: api/TodaysResolveds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodaysResolved(Guid id)
        {
            if (_context.TodaysResolved == null)
            {
                return NotFound();
            }
            var todaysResolved = await _context.TodaysResolved.FindAsync(id);
            if (todaysResolved == null)
            {
                return NotFound();
            }

            _context.TodaysResolved.Remove(todaysResolved);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
