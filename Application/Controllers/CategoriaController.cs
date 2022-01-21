using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.Entities;
using SistemaVenda.Models;
using System.Collections.Generic;
using System.Linq;

namespace SistemaVenda.Controllers
{
    public class CategoriaController : Controller
    {
        protected DAL.sistemavendasContext mContext;

        public CategoriaController(DAL.sistemavendasContext context)
        {
            mContext = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Categoria> Lista = mContext.Categoria.ToList();

            mContext.Dispose();

            return View(Lista);
        }
        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            CategoriaViewModel viewModel = new CategoriaViewModel();

            if(Id != null)
            {
                var entidade = mContext.Categoria.Where(x => x.Codigo == Id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Descricao = entidade.Descricao;

            }
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                Categoria objCategoria = new Categoria()
                {
                    Codigo = entidade.Codigo ?? 0,
                    Descricao = entidade.Descricao
                };
                if (entidade.Codigo == null)
                {
                    mContext.Categoria.Add(objCategoria);
                }
                else
                {
                    mContext.Entry(objCategoria).State = EntityState.Modified;
                }
                mContext.SaveChanges();
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
            var ent = new Categoria() { Codigo = Id };
            mContext.Attach(ent);
            mContext.Remove(ent);
            mContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
