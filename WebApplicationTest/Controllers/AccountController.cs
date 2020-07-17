using System;
using Microsoft.AspNetCore.Mvc;
using ApplicationOrigin.Models;
using ApplicationOrigin.Services;
using Microsoft.Extensions.Logging;

namespace ApplicationOrigin.Controllers
{ 
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public IDbLogic Db { get; }

        public AccountController(IDbLogic db, ILogger<AccountController> logger)
        {
            Db = db;
            _logger = logger;

            _logger.LogDebug("Controller AccountController.");
        }

        #region Авторизация
        [HttpGet]
        public IActionResult AuthorizationPage() => View();

        [HttpPost]
        public IActionResult AuthorizationPage(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            foreach (var item in Db.GetUsers())
                if (item.Login == user.Login && item.Password == user.Password)
                {
                    _logger.LogInformation("Авторизация выполнена успешно.", nameof(user));

                    return RedirectToAction("HomePage", "Home");
                }

            _logger.LogWarning("Авторизация не выполнена.", nameof(user));

            return View();
        }
        #endregion

        #region Регистрация
        [HttpGet]
        public IActionResult RegistrationPage() => View();

        [HttpPost]
        public IActionResult RegistrationPage(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            if (user == null) throw new ArgumentNullException("Данные пользователя не могут быть пустыми.", nameof(user));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Не все текстовые поля заполнены.");

                return View();
            }

            Db.Add(user);

            return RedirectToAction("HomePage", "Home");
        }
        #endregion
    }
}