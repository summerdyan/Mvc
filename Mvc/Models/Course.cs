// create Course object with associated ID, title, credits, and enrollments
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }   // course's ID
        public string Title { get; set; }   // title of course
        public int Credits { get; set; }    // number of credits of course

        public ICollection<Enrollment> Enrollments { get; set; }    // who's enrolled in the course
    }
}
