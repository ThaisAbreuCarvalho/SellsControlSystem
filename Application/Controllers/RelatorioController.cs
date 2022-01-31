using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly IVendaService _vendaService;
        public RelatorioController( IVendaService vendaService)
        {
            _vendaService = vendaService;
        }

        public IActionResult Grafico()
        {
            var data = _vendaService.GraphData();

            ViewBag.values = data.values;
            ViewBag.labels = data.labels;
            ViewBag.cores = data.colors;

            return View();
        }
    }
}
