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
    public class PersonController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public PersonController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return db.Person;
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            return db.Person.Find(id);
        }

        [HttpPost("")]
        public ActionResult<Person> PostPerson(Person model)
        {
            if (model != null) {
                this.db.Person.Add(model);
                this.db.SaveChanges();
                return Ok(model);
            }
            return null;
        }

        [HttpPut("{id}")]
        public IActionResult PutPerson(int id, Person model)
        {
            var course = this.db.Person.Find(id);
            if(course == null) {
                return NotFound();
            }
            course.InjectFrom(model);//使用ValueInjecter範例
            this.db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePersonById(int id)
        {
             var delPerson = this.db.Person.Find(id);
            if (delPerson != null) {
                this.db.Entry(delPerson).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                this.db.SaveChanges();
            }
            
            return null;
        }
    }
}