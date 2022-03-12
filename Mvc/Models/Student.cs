// creating Student object with associated ID, name, and enrollment date
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc.Models
{
    public class Student
    {
        public int ID { get; set; }                     // student's ID

        [Required]                                      // require last name property
        [StringLength(50)]                              // last name can be up to 50 characters
        [Display(Name ="Last Name")]                    // caption for text box should be Last Name
        public string LastName { get; set; }            // student's last name

        [Required]                                      // require first name property
        [StringLength(50)]                              // first mid name can be up to 50 characters
        [Column("FirstName")]                           // column for FirstMidName will be called FirstName
        [Display(Name = "First Name")]                  // caption for text box should be First Name
        public string FirstMidName { get; set; }        // student's first and middle name

        [DataType(DataType.Date)]                       // specify the Date data type
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] // format the date
        [Display(Name = "Enrollment Date")]             // caption for text box should be Enrollment Date
        public DateTime EnrollmentDate { get; set; }    // student's enrollment date

        // displays full name by connecting last and firstmid name
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        public ICollection<Enrollment> Enrollments { get; set; } // collection of student's enrollments
    }
}
