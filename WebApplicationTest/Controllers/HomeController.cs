using System;
using Microsoft.AspNetCore.Mvc;
using ApplicationOrigin.Models;
using ApplicationOrigin.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace ApplicationOrigin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IDbLogic Db { get; }

        public HomeController(IDbLogic db, ILogger<HomeController> logger)
        {
            Db = db;
            _logger = logger;

            _logger.LogDebug("Controller HomeController.");
        }

        #region Главная
        [AllowAnonymous]
        public IActionResult HomePage()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name)) ViewBag.Message = User.Identity.Name;
            else ViewBag.Message = "None";

            return View(Db.GetUsers());
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl);
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