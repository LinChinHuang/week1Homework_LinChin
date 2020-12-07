using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using week1Homework_LinChin.Models;

namespace week1Homework_LinChin.Controllers
{
    [ApiController]
    public class MyControllerBase : ControllerBase
    {
        protected void context_SavingChanges(object sender, EventArgs e)
        {
            ContosoUniversityContext context = sender as ContosoUniversityContext;

            var entites = context.ChangeTracker.Entries();
            foreach (var entry in entites)
            {
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                    entry.CurrentValues.SetValues(new { DateModified = DateTime.Now });
            }
        }
    }
}
