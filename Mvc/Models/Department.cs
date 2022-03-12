// create Department object
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }   // ID associated with department

        [StringLength(50, MinimumLength = 3)]   // name between 3 and 50 characters
        public string Name { get; set; }        // name of department

        [DataType(DataType.Currency)]           // data type associated with currency
        [Column(TypeName = "money")]            // column will be defined using the SQL Server money type in the database
        public decimal Budget { get; set; }     // budget of the department

        [DataType(DataType.Date)]               // data type associated with date
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // format the date
        [Display(Name = "Start Date")]          // caption of text bos will be Start Date
        public DateTime StartDate { get; set; } // Start Date of department

        public int? InstructorID { get; set; }  // administrator's ID (? means it can be null)
        public Instructor Administrator { get; set; }       // administrator

        public ICollection<Course> Courses { get; set; }    // collection of courses offered by department
    }
}
