using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceSystemAPI.Data;
using AttendanceSystemAPI.Models;
using AttendanceSystemAPI.DTO;

namespace AttendanceSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly AttendanceSystemAPIContext _context;

        public AttendancesController(AttendanceSystemAPIContext context)
        {
            _context = context;
        }

        // GET: api/Attendances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendance()
        {
          if (_context.Attendance == null)
          {
              return NotFound();
          }
            return await _context.Attendance.ToListAsync();
        }

        // GET: api/Attendances
        [HttpGet("GetUnjustifiedAbsences")]
        public async Task<ActionResult<IEnumerable<AbsenceDTO>>> GetUnjustifiedAbsences()
        {
            if (_context.Attendance == null)
            {
                return NotFound();
            }
            List<AbsenceDTO> absences = new List<AbsenceDTO>();
            List<Attendance> attendance = await _context.Attendance.Where(att => att.UnjustifiedResolved == false).ToListAsync();
            for(int i = 0; i < attendance.Count(); i++)
            {
                AbsenceDTO thisAb = new AbsenceDTO();
                
                thisAb.AttendanceId = attendance[i].Id;
                thisAb.StudentId = attendance[i].StudentId;
                thisAb.ClassName = _context.SchoolClass.Where(c => c.Id == attendance[i].ClassId).FirstOrDefault().ClassName;
                User thisUser = _context.User.Find(attendance[i].StudentId);
                thisAb.StudentName = thisUser.FirstName + " " + thisUser.LastName;
                thisAb.Status = attendance[i].Status;
                absences.Add(thisAb);
            }

            return absences;
        }

        // GET: api/Attendances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(Guid id)
        {
          if (_context.Attendance == null)
          {
              return NotFound();
          }
            var attendance = await _context.Attendance.FindAsync(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return attendance;
        }

        // PUT: api/Attendances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttendance(Guid id, Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return BadRequest();
            }

            _context.Entry(attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(id))
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

        // POST: api/Attendances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance(Attendance attendance)
        {
          if (_context.Attendance == null)
          {
              return Problem("Entity set 'AttendanceSystemAPIContext.Attendance'  is null.");
          }
            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttendance", new { id = attendance.Id }, attendance);
        }

        // DELETE: api/Attendances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(Guid id)
        {
            if (_context.Attendance == null)
            {
                return NotFound();
            }
            var attendance = await _context.Attendance.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            _context.Attendance.Remove(attendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttendanceExists(Guid id)
        {
            return (_context.Attendance?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
