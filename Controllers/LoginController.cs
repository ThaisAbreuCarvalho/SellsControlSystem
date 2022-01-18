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
        private Cryptograph cryptography;

        public LoginController(DAL.sistemavendasContext context, Cryptograph cryptograph)
        {
            mContext = context;
            cryptography = cryptograph;
        }

        public IActionResult Index()
        {
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

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}
