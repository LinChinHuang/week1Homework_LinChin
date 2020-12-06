using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;
using week1Homework_LinChin.Models;
using week1Homework_LinChin.Models.Models;

namespace week1Homework_LinChin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseInstructorController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseInstructorController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<CourseInstructor>> GetCourseInstructors()
        {
            return db.CourseInstructor;
        }

        [HttpGet("{id}")]
        public ActionResult<CourseInstructor> GetCourseInstructorById(int id)
        {
            return db.CourseInstructor.Find(id);
        }

        [HttpPost("")]
        public ActionResult<CourseInstructor> PostCourseInstructor(CourseInstructor model)
        {
            if (model != null) {
                this.db.CourseInstructor.Add(model);
                this.db.SaveChanges();
                return Ok(model);
            }
            return null;
        }

        [HttpPut("{id}")]
        public IActionResult PutCourseInstructor(int id, CourseInstructor model)
        {
            var course = this.db.CourseInstructor.Find(id);
            if(course == null) {
                return NotFound();
            }
            course.InjectFrom(model);//使用ValueInjecter範例
            this.db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<CourseInstructor> DeleteCourseInstructorById(int id)
        {
            var delCourseInstructor = this.db.CourseInstructor.Find(id);
            if (delCourseInstructor != null) {
                this.db.Entry(delCourseInstructor).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                this.db.SaveChanges();
            }
            
            return null;
        }
    }
}