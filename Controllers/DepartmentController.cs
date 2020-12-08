using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using week1Homework_LinChin.Models;


namespace week1Homework_LinChin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : MyControllerBase
    {
        private readonly ContosoUniversityContext db;
        private readonly ContosoUniversityContextProcedures dbsp;

        public DepartmentController(ContosoUniversityContext db, ContosoUniversityContextProcedures dbsp)
        {
            this.db = db;
            this.db.SavingChanges += context_SavingChanges;
            this.dbsp = dbsp;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return db.Department.Where(c => c.IsDeleted == false).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            return db.Department
                .Where(c => c.DepartmentId == id && c.IsDeleted == false)
                .FirstOrDefault();
        }

        [HttpPost("")]
        public async Task<Department_InsertResult[]> PostDepartment(Department_InsertViewModel model)
        {
             if (model != null) {
                 return  await this.dbsp.Department_Insert(model.Name, model.Budget, model.StartDate?? DateTime.Now, model.InstructorId);
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

        [HttpDelete("SP/{id}")]
        public ActionResult<Department_DeleteResult> SP_DeleteDepartmentById(int id)
        {
            int delCount = 0;
                var department = this.db.Department.Find(id);
                if (department != null) {
                    var departId = new SqlParameter("@DepartmentID", department.DepartmentId);
                    var oriVer = new SqlParameter("@RowVersion_Original", department.RowVersion);
                    delCount =  this.db.Database.ExecuteSqlRaw("EXEC [dbo].[Department_Delete] @DepartmentID,@RowVersion_Original",departId,oriVer);
                }
                            
            return new Department_DeleteResult();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department_DeleteResult> DeleteDepartmentById(int id)
        {
            
            var delDepartment = this.db.Department.Find(id);
            if (delDepartment != null)
            {
                delDepartment.IsDeleted = true;
                this.db.Entry(delDepartment).State = EntityState.Modified;
                this.db.SaveChanges();
            }

            return new Department_DeleteResult();
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