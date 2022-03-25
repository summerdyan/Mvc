// class for determining how many students are taking a particular course
using System;
namespace Mvc.Models.SchoolViewModels
{
    public class CourseGroup
    {
        public string CourseTitle { get; set; }     // course title
        public int StudentCount { get; set; }       // how  many students are in each course
    }
}
