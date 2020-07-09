using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.DB;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class UserController : Controller
    {
        public DBMySQL DB { get; }

        public UserController(UserContext userContext) => DB = new DBMySQL(userContext);

        [HttpGet]
        public IActionResult HomePage() => View();

        [HttpPost]
        public string HomePage(User user)
        {
            if (user == null) throw new ArgumentNullException("Данные пользователя не могут быть пустыми.", nameof(user));

            if (!ModelState.IsValid) return "Введены некорректные данные";

            return DB.ListPeople(user) ? DB.Add(user) : "Такой пользователь уже существует";
        }  
    }
}
