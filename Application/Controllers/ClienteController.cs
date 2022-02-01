using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SistemaVenda.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            return View(_clienteService.GetAll());
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (!Id.HasValue)
                return View(new ClienteViewModel());

            return View(_clienteService.Get((int)Id));
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel entidade)
        {
            if (!ModelState.IsValid)
                return View(entidade);

            _clienteService.Insert(entidade);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int Id)
        {
            _clienteService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
