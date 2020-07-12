using System;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Models;
using WebApplicationTest.Services;

namespace WebApplicationTest.Controllers
{
    public class HomeController : Controller
    {
        public IDbLogic Db { get; }

        public HomeController(IDbLogic dB) => Db = dB;

        [HttpGet]
        public IActionResult HomePage() => View();

        [HttpPost]
        public ViewResult HomePage(User user)
        {
            if (user == null) throw new ArgumentNullException("Данные пользователя не могут быть пустыми.", nameof(user));

            if (!ModelState.IsValid) return View();
           
            Db.Add(user); ;

            return View();
        }  
    }
}
