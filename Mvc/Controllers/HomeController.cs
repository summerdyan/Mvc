// controls the home page
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc.Models;
using Microsoft.EntityFrameworkCore;
using Mvc.Data;
using Mvc.Models.SchoolViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Mvc.Controllers
{
    // allow anonymous users to access home
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcMovieContext _context; // variable for database context

        // get an instance of context from the ASP.NET Core DI
        public HomeController(ILogger<HomeController> logger, MvcMovieContext context)
        {
            _logger = logger;
            _context = context;
        }

        // The LINQ statement groups the student entities by enrollment date,
        // calculates the number of entities in each group,
        // and stores the results in a collection of EnrollmentDateGroup view model objects
        public async Task<ActionResult> About()
        {
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.EnrollmentDate into dateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            // display to view
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
