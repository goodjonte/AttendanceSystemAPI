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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if (_context.Attendance == null || _context.SchoolClass == null)
            {
                return NotFound();
            }
            List<AbsenceDTO> absences = new();
            List<Attendance> attendance = await _context.Attendance.Where(att => att.UnjustifiedResolved == false).ToListAsync();
            for(int i = 0; i < attendance.Count; i++)
            {
                AbsenceDTO thisAb = new AbsenceDTO
                {
                    AttendanceId = attendance[i].Id,
                    StudentId = attendance[i].StudentId
                };
                SchoolClass? associatedClass = _context.SchoolClass.Where(c => c.Id == attendance[i].ClassId).FirstOrDefault();
                if(associatedClass == null)
                {
                    return NotFound();
                }
                thisAb.ClassName = associatedClass.ClassName;
                User? thisUser = _context.User.Find(attendance[i].StudentId);
                if(thisUser == null)
                {
                    return NotFound();
                }
                thisAb.StudentName = thisUser.FirstName + " " + thisUser.LastName;
                thisAb.Status = attendance[i].Status;
                absences.Add(thisAb);
            }

            return absences;
        }

        // POST: api/Attendances/ResolveAbsence
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("ResolveAbsence")]
        public async Task<ActionResult<Attendance>> PostAttendance(AbsenceDTO abs)
        {
            if (_context.Attendance == null)
            {
                return Problem("Entity set 'AttendanceSystemAPIContext.Attendance'  is null.");
            }
            Attendance? attendance = await _context.Attendance.FindAsync(abs.AttendanceId);
            if(attendance == null)
            {
                return NotFound();
            }
            if(attendance.UnjustifiedResolved == false)
            {
                attendance.UnjustifiedResolved = true;
            }
            attendance.Status = abs.Status;
            _context.Entry(attendance).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/Attendances/5 - by student id
        [HttpGet("{id}")]
        public List<AttendanceDTO> GetAttendance(Guid id)
        {
            List<AttendanceDTO> attList = new();

            var attendance = _context.Attendance.Where((att) => att.StudentId == id);

            foreach(Attendance thisAtt in attendance)
            {
                AttendanceDTO att = new()
                {
                    Id = thisAtt.Id,
                    ClassId = thisAtt.ClassId,
                    Date = thisAtt.Date,
                    IsLate = thisAtt.IsLate,
                    IsPresent = thisAtt.IsPresent,
                    Status = thisAtt.Status
                };
                ClassesPeriods? cp = _context.ClassesPeriods.Find(thisAtt.ClassesPeriodId);
                if (cp != null) {
                    SchoolPeriod? sp = _context.SchoolPeriod.Find(cp.PeriodId);
                    if (sp != null)
                    {
                        att.ClassesPeriod = sp.Name;
                    }
                }
                attList.Add(att);
            }

            List<AttendanceDTO> attListSorted = new();

            foreach(AttendanceDTO attToBeSorted in attList)
            {
                DateTime thisDate = attToBeSorted.Date;
                if (attListSorted.Count == 0)
                {
                    attListSorted.Add(attToBeSorted);
                }
                else
                {
                    bool added = false;
                    for (int i = 0; i < attListSorted.Count; i++)
                    {
                        if (thisDate < attListSorted[i].Date)
                        {
                            attListSorted.Insert(i, attToBeSorted);
                            added = true;
                            break;
                        }
                    }
                    if (!added)
                    {
                        attListSorted.Add(attToBeSorted);
                    }
                }
            }


            return attListSorted;
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
            catch (DbUpdateConcurrencyException) when (!AttendanceExists(id))
            {
                return NotFound();
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
            var todayResolve = _context.TodaysResolved.FirstOrDefault((tr) => tr.StudentId == attendance.StudentId);

            if (todayResolve != null)
            { 
                if (todayResolve.DateValid.Date == DateTime.Today.Date && todayResolve.DateValid.Month == DateTime.Today.Month && todayResolve.DateValid.Year == DateTime.Today.Year)
                {
                    attendance.Status = todayResolve.Status;
                    attendance.UnjustifiedResolved = true;
                }
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
