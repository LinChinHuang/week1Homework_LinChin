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
    public class DepartmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;

        public DepartmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return db.Department;
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            return db.Department.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Department> PostDepartment(Department model)
        {
             if (model != null) {
                this.db.Department.Add(model);
                this.db.SaveChanges();
                return Ok(model);
            }
            return null;
        }

        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, Department model)
        {
            var course = this.db.Department.Find(id);
            if(course == null) {
                return NotFound();
            }
            course.InjectFrom(model);//使用ValueInjecter範例
            this.db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteDepartmentById(int id)
        {
           var delDepartment = this.db.Department.Find(id);
            if (delDepartment != null) {
                this.db.Entry(delDepartment).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                this.db.SaveChanges();
            }
            
            return null;
        }
    }
}