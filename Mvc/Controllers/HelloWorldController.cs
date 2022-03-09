// controller class that handles browser requests, retrieves model data, and calls
// view templates that return a response
// this controller class specifies a default action and calls View method
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // calls controller's View methods
        public IActionResult Index()
        {
            return View();
        }

        // Receives name and numTimes from query string
        // Displays message and calls controller's View methods
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            // ViewData contains data that will be passed to View
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}