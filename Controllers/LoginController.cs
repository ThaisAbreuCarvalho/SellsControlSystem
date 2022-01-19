using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Helpers;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        protected DAL.sistemavendasContext mContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private Cryptograph cryptography;

        public LoginController(DAL.sistemavendasContext context, Cryptograph cryptograph, IHttpContextAccessor httpContext)
        {
            mContext = context;
            cryptography = cryptograph;
            httpContextAccessor = httpContext;
        }

        public IActionResult Index(int? Id)
        {
            if (Id.HasValue && Id == 0)
                httpContextAccessor.HttpContext.Session.Clear();

            var Login = new LoginViewModel();

            return View(Login);
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            ViewData["ErrorLogin"] = string.Empty;

            if (ModelState.IsValid)
            {
                var senha = cryptography.GetMD5Hash(model.Senha);

                var newLogin = mContext.Usuario.Where(x => x.Email == model.Login && x.Senha == senha.ToString()).ToList().FirstOrDefault();
                if(newLogin == null)
                {
                    ViewData["ErrorLogin"] = "Usuário não encontrado";
                    return View(model);
                }

                httpContextAccessor.HttpContext.Session.SetString(Session.UserName, newLogin.Nome);
                httpContextAccessor.HttpContext.Session.SetString(Session.UserEmail, newLogin.Email);
                httpContextAccessor.HttpContext.Session.SetString(Session.UserCode, newLogin.Codigo.ToString());
                httpContextAccessor.HttpContext.Session.SetString(Session.Logged, "true");

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}
