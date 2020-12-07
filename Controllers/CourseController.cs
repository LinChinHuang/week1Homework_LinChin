using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using week1Homework_LinChin.Models;

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
            course.InjectFrom(model);//使用ValueInjecter範例
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


        [HttpGet("CourseStudents")]
        public ActionResult<IEnumerable<VwCourseStudents>> GetCourseStudents(int id)
        {
            if (id > 0)
                return this.db.VwCourseStudents.Where(v => v.CourseId == id).ToList();
            else
                return this.db.VwCourseStudents.ToList();
        }

        [HttpGet("CourseStudentCount")]
        public ActionResult<IEnumerable<VwCourseStudentCount>> GetCourseStudentCount(int id)
        {
            if (id > 0)
                return this.db.VwCourseStudentCount.Where(v => v.CourseId == id).ToList();
            else
                return this.db.VwCourseStudentCount.ToList();
        }

        
    }
}