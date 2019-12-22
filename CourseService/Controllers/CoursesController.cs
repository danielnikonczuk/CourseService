using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseService.Models;

namespace CourseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseServiceDbContext _context;

        public CoursesController(CourseServiceDbContext context)
        {
            _context = context;
        }

        [HttpPut]
        [Route("enroll/{userId}/{courseId}")]
        public async Task<ActionResult<IEnumerable<Course>>> Enroll(int userId, int courseId)
        {
            if (await _context.Courses.FindAsync(courseId) == null)
            {
                return BadRequest(new { message = $"Course with Id: {courseId} does not exist!" });
            }

            if (await _context.Users.FindAsync(userId) == null)
            {
                return BadRequest(new { message = $"User with Id: {userId} does not exist!" });
            }

            if (await _context.Enrollments.FindAsync(userId, courseId) != null)
            {
                return BadRequest(new { message = "User already enrolled to that course!" });
            }

            await _context.Enrollments.AddAsync(new Enrollment { UserId = userId, CourseId = courseId });

            await _context.SaveChangesAsync();

            return Ok(new { message = $"User: {userId} successfully enrolled to course: {courseId}!" });
        }

        [HttpDelete]
        [Route("withdraw/{userId}/{courseId}")]
        public async Task<ActionResult<IEnumerable<Course>>> Withdraw(int userId, int courseId)
        {
            var enrollment = await _context.Enrollments.FindAsync(userId, courseId);

            if (await _context.Courses.FindAsync(courseId) == null)
            {
                return BadRequest(new { message = $"Course with Id: {courseId} does not exist!" });
            }

            if (await _context.Users.FindAsync(userId) == null)
            {
                return BadRequest(new { message = $"User with Id: {userId} does not exist!" });
            }

            if (enrollment == null)
            {
                return BadRequest(new { message = "User was not enrolled to that course!" });
            }

            _context.Remove(enrollment);

            await _context.SaveChangesAsync();

            return Ok(new { message = $"User: {userId} successfully withdrawed from course: {courseId}!" });
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseWithEnrollments>>> GetCoursesWithEnrollments()
        {
            return await _context.Courses.Include(x => x.Enrollments).Select(c => new CourseWithEnrollments(c)).ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("GetCourse", new { id = course.Id });
        }

        // POST: api/Courses
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Remove(course);

            await _context.SaveChangesAsync();

            return course;
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
