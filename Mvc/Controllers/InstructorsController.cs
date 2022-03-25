// Controls the actions of the Instructor page
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc.Data;
using Mvc.Models;
using Mvc.Models.SchoolViewModels;

namespace Mvc.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly MvcMovieContext _context;

        public InstructorsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Instructors
        // accepts optional route data (id) and a query string parameter (courseID)
        // that provide the ID values of the selected instructor and selected course
        public async Task<IActionResult> Index(int? id, int? courseID)
        {
            // creates an instance of the view model and puts list of instructors
            // (with associated properties) in it
            var viewModel = new InstructorIndexData();
            viewModel.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .ThenInclude(i => i.Department)
                .OrderBy(i => i.LastName)
                .ToListAsync();

            // returns selected instructor's info if instructor is selected
            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instructor = viewModel.Instructors.Where(
                    i => i.ID == id.Value).Single();
                viewModel.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            // returns selected course's info if course is selected
            if(courseID != null)
            {
                ViewData["CourseID"] = courseID.Value;
                // load enrollment data only if it's requested
                var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
                await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                }
                viewModel.Enrollments = selectedCourse.Enrollments;
            }
            return View(viewModel);
        }

        // GET: Instructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        // create a new instructor
        // only admin can create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            // create Instructor object
            var instructor = new Instructor();
            // assign instructor to course assignments
            instructor.CourseAssignments = new List<CourseAssignment>();
            // loads instructor's courses as checkbox array
            PopulateAssignedCourseData(instructor);
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // create a new instructor
        // only admin can create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("FirstMidName,HireDate,LastName,OfficeAssignment")] Instructor instructor, string[] selectedCourses)
        {
            // add selected courses to instructor's course assignments
            if (selectedCourses != null)
            {
                instructor.CourseAssignments = new List<CourseAssignment>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = new CourseAssignment { InstructorID = instructor.ID, CourseID = int.Parse(course) };
                    instructor.CourseAssignments.Add(courseToAdd);
                }
            }

            // save the changes
            if (ModelState.IsValid)
            {
                _context.Add(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // create checkbox with instructor's courses
            PopulateAssignedCourseData(instructor);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        // admin and manager can edit
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .Include(i => i.OfficeAssignment) // loads instructor's office assignment navigation property
                .Include(i => i.CourseAssignments).ThenInclude(i => i.Course) // loads courses navigation property
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }
            PopulateAssignedCourseData(instructor); // provide info for checkbox array of courses
            return View(instructor);
        }

        // loads courses to view model class as a checkbox array
        // checks off those classes that the instructor teaches
        private void PopulateAssignedCourseData(Instructor instructor)
        {
            var allCourses = _context.Courses;
            var instructorCourses = new HashSet<int>(instructor.CourseAssignments.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach(var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewData["Courses"] = viewModel;
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // admin and manager can edit
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Gets current Instructor entity from database
            var instructorToUpdate = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                .ThenInclude(i => i.Course)
                .FirstOrDefaultAsync(s => s.ID == id);

            // updates the retrieved instructor with vaues from model binder
            if (await TryUpdateModelAsync<Instructor>(
                instructorToUpdate,
                "",
                i => i.FirstMidName, i => i.LastName, i => i.HireDate, i => i.OfficeAssignment))
            {
                // if office assignment is blank, set it to  null
                if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment?.Location))
                {
                    instructorToUpdate.OfficeAssignment = null;
                }
                // updates Courses navigation property of the Instructor entity
                UpdateInstructorCourses(selectedCourses, instructorToUpdate);

                // save changes to the databases
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    // log the error (uncomment ex variable name and write a log)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            // updates Courses navigation property of the Instructor entity
            UpdateInstructorCourses(selectedCourses, instructorToUpdate);
            // create check box with instructor's courses
            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);
        }

        // updates the Courses navigation property of the Instructor entity
        private void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            // if no checkboxes selected, it initializes the CourseAssignments navigation
            // property with an empty collection and returns
            if (selectedCourses == null)
            {
                instructorToUpdate.CourseAssignments = new List<CourseAssignment>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.CourseAssignments.Select(c => c.Course.CourseID));
            foreach (var course in _context.Courses)
            {
                // loops through all courses in the database and checks each course against
                // the ones currently assigned to the instructor versus that ones that were
                // selected
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))
                {
                    // if course isn't in the Instructor.CourseAssignments navigation property,
                    // course is added to the collection in the navigation property
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.CourseAssignments.Add(new CourseAssignment
                        {
                            InstructorID = instructorToUpdate.ID,
                            CourseID = course.CourseID
                        });
                    }
                    // if the checkbox for the course wasn't selected but the course is in the
                    // Instructor.CourseAssignments navigation property, the course is removed from
                    // the navigation property
                    else
                    {
                        if (instructorCourses.Contains(course.CourseID))
                        {
                            CourseAssignment courseToRemove = instructorToUpdate.CourseAssignments.FirstOrDefault(i => i.CourseID == course.CourseID);
                            _context.Remove(courseToRemove);
                        }
                    }
                }
            }
        }

        // GET: Instructors/Delete/5
        // only admin can delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        // only admin can delete
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        // deletes an instructor
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // eager loading for CourseAssignments navigation property
            Instructor instructor = await _context.Instructors
                .Include(i => i.CourseAssignments)
                .SingleAsync(i => i.ID == id);

            // if instructor is deleted and they are the administrator of a department,
            // remove instructor assignment from those departments
            var departments = await _context.Departments
                .Where(d => d.InstructorID == id)
                .ToListAsync();
            departments.ForEach(d => d.InstructorID = null);

            _context.Instructors.Remove(instructor);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }
    }
}
