using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieReg.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReg.Controllers
{
    public class HomeController : Controller
    {
        public MovieDAL MovieDB = new MovieDAL();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            List<Movie> movies = MovieDB.GetMovies();
            return View(movies);
        }

        public IActionResult Details(int id)
        {
            Movie m = MovieDB.GetMovie(id);

            if (m.Title != null)
            {
                return View(m);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Delete(int id)
        {
            Movie m = MovieDB.GetMovie(id);
            return View(m);
        }

        [HttpPost]
        public IActionResult DeleteFromDB(int id)
        {
            MovieDB.DeleteMovie(id);
            return RedirectToAction("index", "home");
        }

        //this action will display the view as well as pass the movie to the form
        public IActionResult Edit(int id)
        {
            Movie m = MovieDB.GetMovie(id);
            return View(m);
        }

        [HttpPost]
        public IActionResult Edit(Movie m)
        {
            MovieDB.UpdateMovie(m);
            return RedirectToAction("Index", "Home");
        }

        //making a new model so we just need to display the view
        public IActionResult Create()
        {
            return View();
        }

        //this is where we process form input for our create form
        [HttpPost]
        public IActionResult Create(Movie m)
        {
            //Modelstate is valid checks the model against its data anatations
            //it returns if all rules are met
            //it reutrns if any rules is violated

            if (ModelState.IsValid)
            {
                //if the model is going we will pass it to our DB and jump back to the index
                MovieDB.CreateMovie(m);
                return RedirectToAction("Index", "Home"); //we can just use this text here instead of the if statement
                //the if statement is an extra form of validation
            }
            else
            {
                //if the model is bad we will return to the same page
                return View(m);
            }
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
