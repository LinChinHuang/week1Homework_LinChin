using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using week1Homework_LinChin.Models;

namespace week1Homework_LinChin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : MyControllerBase
    {
        private readonly ContosoUniversityContext db;
        public PersonController(ContosoUniversityContext db)
        {
            this.db = db;
            this.db.SavingChanges += context_SavingChanges;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return db.Person.Where(c => c.IsDeleted == false).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            return db.Person.Where(c =>  c.Id == id && c.IsDeleted == false).FirstOrDefault();
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
                delPerson.IsDeleted = true;
                this.db.Entry(delPerson).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            
            return null;
        }
    }
}