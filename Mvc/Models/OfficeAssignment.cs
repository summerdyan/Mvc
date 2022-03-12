// create OfficeAssignment object
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc.Models
{
    public class OfficeAssignment
    {
        [Key]                                   // InstructorID is the key of OfficeAssignment
        public int InstructorID { get; set; }   // InstructorID property

        [StringLength(50)]                      // max string length of 50
        [Display(Name = "Office Location")]     // caption for text box is Office Location
        public string Location { get; set; }    // Location property

        public Instructor Instructor { get; set; }  // Instructor associated with office
    }
}
