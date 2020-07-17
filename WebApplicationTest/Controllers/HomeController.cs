using System;
using Microsoft.AspNetCore.Mvc;
using ApplicationOrigin.Models;
using ApplicationOrigin.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace ApplicationOrigin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public IDbLogic Db { get; }

        public HomeController(IDbLogic db, ILogger<AccountController> logger)
        {
            Db = db;
            _logger = logger;

            _logger.LogDebug("Controller HomeController.");
        }

        #region Главная
        [AllowAnonymous]
        public ViewResult HomePage()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name)) ViewBag.Message = $"Добро пожаловать {User.Identity.Name}";
            return View(Db.GetUsers());
        }
        #endregion

        #region Информация
        [Authorize(Roles ="Administrator, Visitor")]
        public IActionResult InformationPage(int id)
        {
            if (Db.GetUser(id) != null) return View(Db.GetUser(id));

            _logger.LogWarning("Просмотр информации не возможен.", id);

            return NotFound();
        }
        #endregion

        #region Изменение данных
        [HttpGet]
        [Authorize(Roles= "Administrator")]
        public IActionResult EditPage(int id)
        {
            if (Db.GetUser(id) != null) return View(Db.GetUser(id));

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public IActionResult ConfirmRemovePage(int id)
        {
            if (Db.GetUser(id) != null) return View(Db.GetUser(id));

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult RemovePage(int id)
        {
            if (Db.GetUser(id) != null) Db.Remove(Db.GetUser(id));

            _logger.LogInformation("Пользователь успешно удален.", id);

            return RedirectToAction("HomePage");
        }
        #endregion
    }
}