// creating Student object with associated ID, name, and enrollment date
using System;
using System.Collections.Generic;

namespace Mvc.Models
{
    public class Student
    {
        public int ID { get; set; }                     // student's ID
        public string LastName { get; set; }            // student's last name
        public string FirstMidName { get; set; }        // student's first and middle name
        public DateTime EnrollmentDate { get; set; }    // student's enrollment date


        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
