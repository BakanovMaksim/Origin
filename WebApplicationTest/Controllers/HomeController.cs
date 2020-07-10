using System;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.DB;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class HomeController : Controller
    {
        public DBDependency DB { get; }

        public HomeController(IDBDependency dB) => DB = (DBDependency)dB;

        [HttpGet]
        public IActionResult HomePage() => View();

        [HttpPost]
        public ViewResult HomePage(User user)
        {
            if (user == null) throw new ArgumentNullException("Данные пользователя не могут быть пустыми.", nameof(user));

            if (!ModelState.IsValid) return View();

            if (!DB.CheckNewUser(user))
            {
                ViewBag.MessageError = "Такой пользователь уже существует.";
                return View();
            }
           
            DB.Add(user); ;

            return View();
        }  
    }
}
