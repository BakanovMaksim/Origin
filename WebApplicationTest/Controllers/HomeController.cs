using System;
using Microsoft.AspNetCore.Mvc;
using ApplicationOrigin.Models;
using ApplicationOrigin.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApplicationOrigin.Controllers
{
    public class AccountController : Controller
    {
        public User CurrentUser { get; private set; }

        public IDbLogic Db { get; }

        public AccountController(IDbLogic db) => Db = db;

        #region Авторизация
        [HttpGet]
        public IActionResult AuthorizationPage() => View();

        [HttpPost]
        public IActionResult AuthorizationPage(User user)
        {
            foreach (var item in Db.GetUsers())
                if (item.Login == user.Login && item.Password == user.Password)
                {
                    CurrentUser = item;
                    return RedirectToAction("HomePage");
                }

            return View();
        }
        #endregion

        #region Регистрация
        [HttpGet]
        public IActionResult RegistrationPage() => View();

        [HttpPost]
        public IActionResult RegistrationPage(User user)
        {
            if (user == null) throw new ArgumentNullException("Данные пользователя не могут быть пустыми.", nameof(user));

            if (!ModelState.IsValid) return View();

            Db.Add(user); ;

            return RedirectToAction("AuthorizationPage");
        }
        #endregion

        #region Главная
        public ViewResult HomePage() => View(Db.GetUsers());
        #endregion

        #region Информация
        public IActionResult InformationPage(int id)
        {
            if (Db.GetUser(id) != null) return View(Db.GetUser(id));

            return NotFound();
        }
        #endregion

        #region Изменение данных
        [HttpGet]
        public IActionResult EditPage(int id)
        { 
            if (Db.GetUser(id) != null) return View(Db.GetUser(id));

            return NotFound();
        }
        
        [HttpPost]
        public IActionResult EditPage(User user)
        {
            Db.Edit(user);
            return RedirectToAction("HomePage");
        }
        #endregion

        #region Удаление данных
        [HttpGet]
        [ActionName("RemovePage")]
        public IActionResult ConfirmRemovePage(int id)
        {
            if (Db.GetUser(id) != null) return View(Db.GetUser(id));

            return NotFound();
        }

        [HttpPost]
        public IActionResult RemovePage(int id)
        {
            if(Db.GetUser(id) != null) Db.Remove(Db.GetUser(id));

            return RedirectToAction("HomePage");
        }
        #endregion
    }
}
