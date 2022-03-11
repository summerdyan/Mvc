// controls the aspects of the Student tab of the web app
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc.Data;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class StudentsController : Controller
    {
        // takes MvcMovieContext as construction parameter
        private readonly MvcMovieContext _context;

        public StudentsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Students
        // displays all students in the database
        // async, Task<T>, await, and ToListAsync make the code execute asynchronously
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                // Include and ThenInclude methods cause the context to load the
                // Student.Enrollments navigation property, and within each enrollment
                // the Enrollment.Course navigation property
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)

                // AsNoTracking method improves performance in scenarios where the
                // entities returned won't be updated in the current context's lifetime
                .AsNoTracking()

                // FirstOrDefaultAsync method to retrieve a single Student entity
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        // displays view so that user can choose to create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // adds the Student entity created by the ASP.NET Core MVC model binder to the
        // Students entity set and then saves the changes to the database
        [HttpPost]
        [ValidateAntiForgeryToken] // helps prevent cross-site request forgery
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            try
            {
                // model validation check
                if (ModelState.IsValid)
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                // log the error (uncomment ex variable name and write a log)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // allows user to update certain attributes about the Student
        [HttpPost, ActionName("Edit")] // uses action method to connect to Edit method
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // reads the existing entity
            var studentToUpdate = await _context.Students.FirstOrDefaultAsync(s => s.ID == id);
            // update fields in the retrieved entity based on user input in the posted form data
            if (await TryUpdateModelAsync<Student>(
                // updateable attributes
                studentToUpdate,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                try
                {
                    // SaveChanges -> Entity Framework creates SQL statements to update the database row
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Students/Delete/5
        // displays view so that user can choose to delete
        // manages error reporting
        // accepts an optional parameter that indicates whether the method was called after a failure to save changes
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(student);
        }

        // POST: Students/Delete/5
        // deletes selected entity
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // retrieves selected entity
            var student = await _context.Students.FindAsync(id);
            if(student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                // calls Remove method to set entity's setting to Deleted
                _context.Students.Remove(student);
                // SQL DELETE command is generated
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                // log the error (uncomment ex variable name and write a log)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
