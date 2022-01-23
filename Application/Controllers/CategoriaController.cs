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
            if (ModelState.IsValid)
            {
                //Categoria objCategoria = new Categoria()
                //{
                //    Codigo = entidade.Codigo ?? 0,
                //    Descricao = entidade.Descricao
                //};
                //if (entidade.Codigo == null)
                //{
                //    mContext.Categoria.Add(objCategoria);
                //}
                //else
                //{
                //    mContext.Entry(objCategoria).State = EntityState.Modified;
                //}
                //mContext.SaveChanges();
            }
            else
            {
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir (int Id)
        {
            //var ent = new Categoria() { Codigo = Id };
            //mContext.Attach(ent);
            //mContext.Remove(ent);
            //mContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
