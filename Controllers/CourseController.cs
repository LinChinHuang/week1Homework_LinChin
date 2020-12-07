using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using week1Homework_LinChin.Models;

namespace week1Homework_LinChin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : MyControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseController(ContosoUniversityContext db)
        {
            this.db = db;
            this.db.SavingChanges += context_SavingChanges;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return this.db.Course.Where(c => c.IsDeleted == false).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            var course = this.db.Course.Where(c => c.CourseId == id && c.IsDeleted == false).FirstOrDefault();
            if(course == null) {
                return NotFound();
            }
            return course;
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
                delCourse.IsDeleted = true;
                this.db.Entry(delCourse).State = EntityState.Modified;
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