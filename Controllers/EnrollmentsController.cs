﻿using System;
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
    public class EnrollmentsController : ControllerBase
    {
        private readonly AttendanceSystemAPIContext _context;

        public EnrollmentsController(AttendanceSystemAPIContext context)
        {
            _context = context;
        }

        // GET: api/Enrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Enrollments>>> GetEnrollments()
        {
          if (_context.Enrollments == null)
          {
              return NotFound();
          }
            return await _context.Enrollments.ToListAsync();
        }

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public IQueryable<Enrollments> GetEnrollments(Guid id)
        {
            IQueryable<Enrollments> enrollments = _context.Enrollments.Where(c => c.ClassId == id);

            foreach(Enrollments enrollment in enrollments)
            {
                User tempUser = _context.User.First(x => x.Id == enrollment.StudentId);
                enrollment.StudentName = tempUser.FirstName + " " + tempUser.LastName;
            }

            return enrollments;
        }

        // POST: api/Enrollments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Enrollments>> PostEnrollments(Enrollments enrollments)
        {
          if (_context.Enrollments == null)
          {
              return Problem("Entity set 'AttendanceSystemAPIContext.Enrollments'  is null.");
          }
            _context.Enrollments.Add(enrollments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnrollments", new { id = enrollments.EnrollmentId }, enrollments);
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{studentid}/{classId}")]
        public async Task<IActionResult> DeleteEnrollments(Guid studentid, Guid classId)
        {
            if (_context.Enrollments == null)
            {
                return NotFound();
            }
            var enrollments = await _context.Enrollments.FirstAsync(en => en.StudentId == studentid && en.ClassId == classId);
            if (enrollments == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollments);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
