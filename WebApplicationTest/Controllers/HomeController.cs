using System;
using Microsoft.AspNetCore.Mvc;
using ApplicationOrigin.Models;
using ApplicationOrigin.Services;
using System.Linq;

namespace ApplicationOrigin.Controllers
{
    public class AccountController : Controller
    {
        public User CurrentUser { get; private set; }

        public IDbLogic Db { get; }

        public AccountController(IDbLogic db) => Db = db;

        #region Авторизация
        [HttpGet]
        public ViewResult AuthorizationPage() => View();

        [HttpPost]
        public ViewResult AuthorizationPage(User user)
        {
            foreach (var item in Db.Users)
                if (item.Login == user.Login && item.Password == user.Password)
                {
                    CurrentUser = item;
                    return View("PersonalAccountPage");
                }

            return View();
        }
        #endregion

        #region Регистрация
        [HttpGet]
        public ActionResult RegistrationPage() => View();

        [HttpPost]
        public ActionResult RegistrationPage(User user)
        {
            if (user == null) throw new ArgumentNullException("Данные пользователя не могут быть пустыми.", nameof(user));

            if (!ModelState.IsValid) return View();

            Db.Add(user); ;

            return Redirect("~/Account/AuthorizationPage");
        }
        #endregion

        #region Личный кабинет
        [HttpGet]
        public ActionResult PersonalAccountPage() => View();

        [HttpPost]
        public ActionResult PersonalAccountPage(User user)
        {
            Db.Remove(user);

            return RedirectToAction("~/Account/HomePage");
        }
        #endregion

        #region Главная
        public ViewResult HomePage() => View(Db.Users.ToList());
        #endregion
    }
}
