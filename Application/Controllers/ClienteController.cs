using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return View(_clienteService.Get(Id));
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel entidade)
        {
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
