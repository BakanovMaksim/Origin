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

        public User CurrentUser { get; private set; }

        public AccountController(IDbLogic db,ILogger<AccountController> logger)
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

                    CurrentUser = item;
                    return RedirectToAction("HomePage");
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

            _logger.LogWarning("Просмотр информации не возможен.", id);

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
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

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

            _logger.LogInformation("Пользователь успешно удален.", id);

            return RedirectToAction("HomePage");
        }
        #endregion
    }
}
