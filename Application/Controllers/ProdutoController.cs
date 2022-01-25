using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;

        public ProdutoController(IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        public IActionResult Index()
        {
            return View(_produtoService.GetAll());
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (!Id.HasValue)
                return View(new ProdutoViewModel { ListaCategorias = _categoriaService.ListaCategoria()});

            return View(_produtoService.Get((int)Id));
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel entidade)
        {
            if (!ModelState.IsValid)
                return View(entidade);

            _produtoService.Insert(entidade);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int Id)
        {
            _produtoService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
