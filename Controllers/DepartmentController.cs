using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;
using week1Homework_LinChin.Models;
using week1Homework_LinChin.Models.Models;
using Microsoft.Data.SqlClient;


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
        public ActionResult<Department_InsertResult> PostDepartment(Department model)
        {
             if (model != null) {
                 var name = new SqlParameter("@Name", model.Name);
                 var budget = new SqlParameter("@Budget", model.Budget);
                 var startDate = new SqlParameter("@StartDate", DateTime.Now);
                 var instructorID = new SqlParameter("@InstructorID", model.InstructorId);
                 this.db.Database.ExecuteSqlRaw("EXEC [dbo].[Department_Insert] @Name,@Budget,@StartDate,@InstructorID",name,budget,startDate,instructorID);
                return Ok(model);//Department_InsertResult return object need to update
            }
            return null;
        }

        [HttpPut("{id}")]
        public ActionResult<Department_UpdateResult> PutDepartment(int id, Department model)
        {
            var department = this.db.Department.Find(id);
            if (department != null) {
                var departId = new SqlParameter("@DepartmentID", id);
                var name = new SqlParameter("@Name", model.Name);
                var budget = new SqlParameter("@Budget", model.Budget);
                var startDate = new SqlParameter("@StartDate", model.StartDate);
                var instructorID = new SqlParameter("@InstructorID", model.InstructorId);
                var oriVer = new SqlParameter("@RowVersion_Original", model.RowVersion);
                this.db.Database.ExecuteSqlRaw("EXEC [dbo].[Department_Update] @DepartmentID,@Name,@Budget,@StartDate,@InstructorID,@RowVersion_Original",departId,name,budget,startDate,instructorID,oriVer);
            }
                            
            return new Department_UpdateResult(){  RowVersion = department.RowVersion };
        }

        [HttpDelete("{id}")]
        public ActionResult<Department_DeleteResult> DeleteDepartmentById(int id)
        {
            int delCount = 0;
                var department = this.db.Department.Find(id);
                if (department != null) {
                    var departId = new SqlParameter("@DepartmentID", department.DepartmentId);
                    var oriVer = new SqlParameter("@RowVersion_Original", department.RowVersion);
                    delCount =  this.db.Database.ExecuteSqlRaw("EXEC [dbo].[Department_Delete] @DepartmentID,@RowVersion_Original",departId,oriVer);
                }
                            
            return new Department_DeleteResult(){ DeleteCount = delCount };
        }

        [HttpGet("DepartmentCourseCount")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> GetDepartmentCourseCount(int id)
        {
            //FromSqlRaw + SqlParameter
            var departmentID = new SqlParameter("@id",id);
            return await this.db.VwDepartmentCourseCount.FromSqlRaw($"SELECT * FROM [dbo].[vwDepartmentCourseCount] WHERE DepartmentId = @id",departmentID).ToListAsync<VwDepartmentCourseCount>();
        }
    }
}