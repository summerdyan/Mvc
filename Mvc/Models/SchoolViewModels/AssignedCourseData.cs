// provides data to view for list of course checkboxes in instructor page
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Models.SchoolViewModels
{
    public class AssignedCourseData
    {
        public int CourseID { get; set; }   // course ID
        public string Title { get; set; }   // title of course
        public bool Assigned { get; set; }  // assigned to instructor
    }
}
