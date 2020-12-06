using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using week1Homework_LinChin.Models;
using week1Homework_LinChin.Models.Models;
using Omu.ValueInjecter;
namespace week1Homework_LinChin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return this.db.Course;
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            var Course = this.db.Course.Find(id);
            if(Course == null) {
                return NotFound();
            }
            return this.db.Course.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Course> PostCourse(Course model)
        {
            if (model != null) {
                this.db.Course.Add(model);
                this.db.SaveChanges();
                return Ok(model);
            }
            return null;
        }

        [HttpPut("{id}")]
        public ActionResult<Course> PutCourse(int id, Course model)
        {
            var course = this.db.Course.Find(id);
            if(course == null) {
                return NotFound();
            }
            course.InjectFrom(model);
            this.db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourseById(int id)
        {
            var delCourse = this.db.Course.Find(id);
            if (delCourse != null) {
                this.db.Entry(delCourse).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                this.db.SaveChanges();
            }
            
            return null;
        }
    }
}