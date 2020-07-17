using System;
using Microsoft.AspNetCore.Mvc;
using ApplicationOrigin.Models;
using ApplicationOrigin.Services;
using Microsoft.Extensions.Logging;

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
            if (Db.GetUser(id) != null) Db.Remove(Db.GetUser(id));

            _logger.LogInformation("Пользователь успешно удален.", id);

            return RedirectToAction("HomePage");
        }
        #endregion
    }
}