using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace week1Homework_LinChin.Models
{
    public class Department_InsertViewModel
    {
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public int? InstructorId { get; set; }
    }
}
