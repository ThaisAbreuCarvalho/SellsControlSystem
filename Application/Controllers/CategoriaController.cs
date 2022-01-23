using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SistemaVenda.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public IActionResult Index()
        {
            return View(_categoriaService.GetAll());
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            return View(_categoriaService.Get(Id));
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel entidade)
        {
            _categoriaService.Insert(entidade);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir (int Id)
        {
            _categoriaService.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
