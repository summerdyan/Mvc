// create CourseAssignment object
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mvc.Models
{
    public class CourseAssignment
    {
        public int InstructorID { get; set; }       // ID of instructor teaching course
        public int CourseID { get; set; }           // ID of course being taught
        public Instructor Instructor { get; set; }  // instructor teaching course
        public Course Course { get; set; }          // course being taught
    }
}
