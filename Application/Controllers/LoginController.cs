using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Helpers;
using System.Linq;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService _userService;

        public LoginController(IHttpContextAccessor httpContext, IUserService userService)
        {
            httpContextAccessor = httpContext;
            _userService = userService;
        }

        public IActionResult Index(int? Id)
        {
            if (Id.HasValue && Id == 0)
                httpContextAccessor.HttpContext.Session.Clear();

            return View(new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            ViewData["ErrorLogin"] = string.Empty;

            if (!ModelState.IsValid)
                return View(model);

            var validateLogin = _userService.ValidateLogin(model.Login, model.Senha);

            if (!validateLogin.IsValid)
            {
                ViewData["ErrorLogin"] = validateLogin.Error;
                return View(model);
            }

            httpContextAccessor.HttpContext.Session.SetString(Session.UserName, validateLogin.Name);
            httpContextAccessor.HttpContext.Session.SetString(Session.UserCode, validateLogin.Code);
            httpContextAccessor.HttpContext.Session.SetString(Session.UserEmail, validateLogin.Email);
            httpContextAccessor.HttpContext.Session.SetString(Session.Logged, $"{validateLogin.IsValid}");

            return RedirectToAction("Index", "Home");
        }
    }
}
