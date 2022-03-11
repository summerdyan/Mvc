// creating Enrollment object with associated course, student, and grade
namespace Mvc.Models
{
    // establish the grade scale constants
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }   // enrollment ID
        public int CourseID { get; set; }       // course ID
        public int StudentID { get; set; }      // enrolled student's ID
        public Grade? Grade { get; set; }       // grade (? means it can be null if grade is currently unknown or not assigned)

        public Course Course { get; set; }      // Course object associated with Enrollment object
        public Student Student { get; set; }    // Student object associated with Enrollment object
    }
}
