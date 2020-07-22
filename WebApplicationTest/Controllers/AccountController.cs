using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ApplicationOrigin.Models;
using ApplicationOrigin.Services;

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
        public async Task<IActionResult> AuthorizationPage(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            foreach (var item in Db.GetUsers())
                if (item.Login == user.Login && item.Password == user.Password)
                {
                    _logger.LogInformation("Авторизация выполнена успешно.", nameof(user));
 
                    await Authenticate(Db.GetUserLogin(user.Login));

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
        public async Task<IActionResult> RegistrationPage(User user)
        {
            _logger.LogInformation("Данные пользователя получены.", nameof(user));

            if (user == null) throw new ArgumentNullException("Данные пользователя не могут быть пустыми.", nameof(user));

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Не все текстовые поля заполнены.");

                return View();
            }

            Db.Add(user);

            await Authenticate(Db.GetUserLogin(user.Login));

            return RedirectToAction("HomePage", "Home");
        }
        #endregion

        #region Аутентификация
        private async Task Authenticate(User user)
        { 
               var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimTypes.Role, user.Role)
                };        

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimTypes.Role);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        #endregion
    }
}