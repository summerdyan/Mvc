// creating the Instructor object
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc.Models
{
    public class Instructor
    {
        public int ID { get; set; } // ID property

        [Required]                              // require last name
        [Display(Name = "Last Name")]           // caption for text box should be Last Name
        [StringLength(50)]                      // max string length of 50
        public string LastName { get; set; }    // LastName property

        [Required]                              // require first name
        [Column("FirstName")]                   // column title is FirstName
        [Display(Name = "First Name")]          // caption for text box should be First Name
        [StringLength(50)]                      // max string length of 50
        public string FirstMidName { get; set; }   // FirstName property

        [DataType(DataType.Date)]               // should be considered Date datatype
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // formatting date
        [Display(Name = "Hire Date")]           // caption for text box is Hire Date
        public DateTime HireDate { get; set; }  // DateTime property

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return LastName + ", " + FirstMidName; }
        }

        public ICollection<CourseAssignment> CourseAssignments { get; set; }    // collection of course assignments
        public OfficeAssignment OfficeAssignment { get; set; }                  // office assignments

    }
}
