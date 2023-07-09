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
    public class NoticesController : ControllerBase
    {
        private readonly AttendanceSystemAPIContext _context;

        public NoticesController(AttendanceSystemAPIContext context)
        {
            _context = context;
        }

        // GET: api/Notices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notice>>> GetNotice()
        {
          if (_context.Notice == null)
          {
              return NotFound();
          }
            return await _context.Notice.ToListAsync();
        }

        // GET: api/Notices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notice>> GetNotice(Guid id)
        {
          if (_context.Notice == null)
          {
              return NotFound();
          }
            var notice = await _context.Notice.FindAsync(id);

            return notice == null ? (ActionResult<Notice>)NotFound() : (ActionResult<Notice>)notice;
        }

        // POST: api/Notices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notice>> PostNotice(Notice notice)
        {
          if (_context.Notice == null)
          {
              return Problem("Entity set 'AttendanceSystemAPIContext.Notice'  is null.");
          }
            notice.Id = Guid.NewGuid();
            _context.Notice.Add(notice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotice", new { id = notice.Id }, notice);
        }

        // DELETE: api/Notices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotice(Guid id)
        {
            if (_context.Notice == null)
            {
                return NotFound();
            }
            var notice = await _context.Notice.FindAsync(id);
            if (notice == null)
            {
                return NotFound();
            }

            _context.Notice.Remove(notice);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
