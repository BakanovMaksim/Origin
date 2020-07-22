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
        public IActionResult HomePage()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name)) ViewBag.Message = User.Identity.Name;
            else ViewBag.Message = "None";

            var user = Db.GetUserLogin(User.Identity.Name);
            if(user != null) ViewBag.CurrentCulture = user.Culture;

            return View(Db.GetUsers());
        }
        #endregion

        #region Информация
        public IActionResult InformationPage(int id)
        {
            if (Db.GetUserId(id) != null) return View(Db.GetUserId(id));

            _logger.LogWarning("Просмотр информации не возможен.", id);

            return NotFound();
        }
        #endregion

        #region Изменение данных
        [HttpGet]
        [Authorize(Policy = "RolePolicy")]
        public IActionResult EditPage(int id)
        {
            if (Db.GetUserId(id) != null) return View(Db.GetUserId(id));

            return NotFound();
        }

        [HttpPost]
        [Authorize(Policy = "RolePolicy")]
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
        [Authorize(Policy = "RolePolicy")]
        public IActionResult ConfirmRemovePage(int id)
        {
            if (Db.GetUserId(id) != null) return View(Db.GetUserId(id));

            return NotFound();
        }

        [HttpPost]
        [Authorize(Policy = "RolePolicy")]
        public IActionResult RemovePage(int id)
        {
            if (Db.GetUserId(id) != null) Db.Remove(Db.GetUserId(id));

            _logger.LogInformation("Пользователь успешно удален.", id);

            return RedirectToAction("HomePage");
        }
        #endregion

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var user = Db.GetUserLogin(User.Identity.Name);
            if (user != null)
            {
                user.Culture = culture;
                Db.Edit(user);
            }
            
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl);
        }
    }
}