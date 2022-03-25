// keeps track of how many students have enrolled at a specific enrollment date
// for About page of the web page
using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Models.SchoolViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)] // have data be in the form of a Date
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; } // number of students
    }
}
