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
using Newtonsoft.Json;

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
          List<SchoolWeek> schoolWeeks = await _context.SchoolWeek.ToListAsync();
          if (schoolWeeks.Count < 1)
          {
                return NotFound();
          }
            return schoolWeeks;
        }

        // GET: api/SchoolWeeks/TimetableInfo
        [HttpGet("TimetableInfo")]
        public async Task<ActionResult<TimeTableDTO>> TimetableInfo()
        {
            TimeTableDTO timeTable = new();
            List<SchoolWeek> schoolWeeks = await _context.SchoolWeek.ToListAsync();
            List<SchoolDay> schoolDays = await _context.SchoolDay.ToListAsync();
            int mostPeriods = 0;
            for(int i =0; i< schoolDays.Count; i++)
            {
                int thisPeriods = schoolDays[i].NumberOfPeriods;
                if(thisPeriods > mostPeriods)
                {
                    mostPeriods = thisPeriods;
                }
            }

            timeTable.MostPeriods = mostPeriods;

            timeTable.SameEveryDay = schoolWeeks[0].SameEveryDay;

            timeTable.MondayId = schoolDays.First(d => d.day == Models.DayOfWeek.Monday).Id;
            List<Guid>? MondayGuids = JsonConvert.DeserializeObject<List<Guid>>( schoolDays.First(d => d.day == Models.DayOfWeek.Monday).DaysPeriodsJsonArrayString);
            if (MondayGuids == null)
            {
                return NotFound();
            }
            MondayGuids.ForEach(id =>
            {
                SchoolPeriod? period = _context.SchoolPeriod.Find(id);
                if(period != null)
                {
                    timeTable.MondayColumn.Add(new PeriodDTO
                    {
                        PeriodId = id,
                        PeriodName = period.Name
                    });
                }
            });

            timeTable.TuesdayId = schoolDays.First(d => d.day == Models.DayOfWeek.Tuesday).Id;
            List<Guid>? TuesdayGuids = JsonConvert.DeserializeObject<List<Guid>>(schoolDays.First(d => d.day == Models.DayOfWeek.Tuesday).DaysPeriodsJsonArrayString);
            if(TuesdayGuids == null)
            {
                return NotFound();
            }
            TuesdayGuids.ForEach(id =>
            {
                SchoolPeriod? period = _context.SchoolPeriod.Find(id);
                if (period != null)
                {
                    timeTable.TuesdayColumn.Add(new PeriodDTO
                    {
                        PeriodId = id,
                        PeriodName = period.Name
                    });
                }
            });

            timeTable.WednesdayId = schoolDays.First(d => d.day == Models.DayOfWeek.Wednesday).Id;
            List<Guid>? WednesdayGuids = JsonConvert.DeserializeObject<List<Guid>>(schoolDays.First(d => d.day == Models.DayOfWeek.Wednesday).DaysPeriodsJsonArrayString);
            if(WednesdayGuids == null)
            {
                return NotFound();
            }
            WednesdayGuids.ForEach(id =>
            {
                SchoolPeriod? period = _context.SchoolPeriod.Find(id);
                if(period != null)
                {
                    timeTable.WednesdayColumn.Add(new PeriodDTO
                    {
                        PeriodId = id,
                        PeriodName = period.Name
                    });
                }
            });

            timeTable.ThursdayId = schoolDays.First(d => d.day == Models.DayOfWeek.Thursday).Id;
            List<Guid>? ThursdayGuids = JsonConvert.DeserializeObject<List<Guid>>(schoolDays.First(d => d.day == Models.DayOfWeek.Thursday).DaysPeriodsJsonArrayString);
            if(ThursdayGuids == null)
            {
                return NotFound();
            }
            ThursdayGuids.ForEach(id =>
            {
                SchoolPeriod? period = _context.SchoolPeriod.Find(id);
                if (period != null)
                {
                    timeTable.ThursdayColumn.Add(new PeriodDTO
                    {
                        PeriodId = id,
                        PeriodName = period.Name
                    });
                }
            });

            timeTable.FridayId = schoolDays.First(d => d.day == Models.DayOfWeek.Friday).Id;
            List<Guid>? FridayGuids = JsonConvert.DeserializeObject<List<Guid>>(schoolDays.First(d => d.day == Models.DayOfWeek.Friday).DaysPeriodsJsonArrayString);
            if(FridayGuids == null)
            {
                return NotFound();
            }
            FridayGuids.ForEach(id =>
            {
                SchoolPeriod? period = _context.SchoolPeriod.Find(id);
                if (period != null)
                {
                    timeTable.FridayColumn.Add(new PeriodDTO
                    {
                        PeriodId = id,
                        PeriodName = period.Name
                    });
                }
            });

            return timeTable;
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

            return schoolWeek == null ? (ActionResult<SchoolWeek>)NotFound() : (ActionResult<SchoolWeek>)schoolWeek;
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
    }
}
