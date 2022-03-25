// class for creating CourseRegistrationInfo object
using System;
namespace Mvc.Models
{
    public class CourseRegistrationInfo
    {
        public int ID { get; set; }         // CourseRegistrationInfo id
        public int StudentID { get; set; }  // student's id
        public int CourseID { get; set; }   // course's id
        public Grade? Grade { get; set; }   // student's grade in course

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
