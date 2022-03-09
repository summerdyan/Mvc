// controller class that handles browser requests, retrieves model data, and calls
// view templates that return a response
// this controller class specifies a default action and a welcome action
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
        //
        // GET: /HelloWorld/
        // this is an HTTP GET method that's invoked by appending /HelloWorld/ to the base URL
        public string Index()
        {
            return "This is my default action...";
        }

        //
        // GET: /HelloWorld/Welcome/
        // this is an HTTP GET method that's invoked by appending /HelloWorld/Welcome/ to the URL
        // displays name and ID
        public string Welcome(string name, int ID = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, ID is: {ID}");
        }
    }
}