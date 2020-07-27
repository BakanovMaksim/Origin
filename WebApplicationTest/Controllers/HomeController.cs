using ApplicationOrigin.Models;
using ApplicationOrigin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace ApplicationOrigin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IDbLogic _db;

        public HomeController(IDbLogic db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;

            _logger.LogDebug("Controller HomeController.");
        }

        #region Главная
        public IActionResult HomePage()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name)) ViewBag.Message = User.Identity.Name;
            else ViewBag.Message = "None";

            return View(_db.GetUsers().ToList());
        }
        #endregion

        #region Информация
        public IActionResult InformationPage(int id)
        {
            if (_db.GetUserId(id) != null) return View(_db.GetUserId(id));

            _logger.LogWarning("Просмотр информации не возможен.", id);

            return NotFound();
        }
        #endregion

        #region Изменение данных
        [HttpGet]
        [Authorize(Policy = "RolePolicy")]
        public IActionResult EditPage(int id)
        {
            if (_db.GetUserId(id) != null) return View(_db.GetUserId(id));

            return NotFound();
        }

        [HttpPost]
        [Authorize(Policy = "RolePolicy")]
        public IActionResult EditPage(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            _db.Edit(user);

            return RedirectToAction("HomePage");
        }
        #endregion

        #region Удаление данных
        [HttpGet]
        [ActionName("RemovePage")]
        [Authorize(Policy = "RolePolicy")]
        public IActionResult ConfirmRemovePage(int id)
        {
            if (_db.GetUserId(id) != null) return View(_db.GetUserId(id));

            return NotFound();
        }

        [HttpPost]
        [Authorize(Policy = "RolePolicy")]
        public IActionResult RemovePage(int id)
        {
            if (_db.GetUserId(id) != null) _db.Remove(_db.GetUserId(id));

            _logger.LogInformation("Пользователь успешно удален.", id);

            return RedirectToAction("HomePage");
        }
        #endregion

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var user = _db.GetUserLogin(User.Identity.Name);
            if (user != null)
            {
                user.Culture = culture;
                _db.Edit(user);
            }

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl);
        }
    }
}