// creating a view model for the Instructor Index View
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Models.SchoolViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }    // collection of instructors
        public IEnumerable<Course> Courses { get; set; }            // collection of courses
        public IEnumerable<Enrollment> Enrollments { get; set; }    // collection of enrollments
    }
}
