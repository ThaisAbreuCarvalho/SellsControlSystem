using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : Controller
    {
        protected sistemavendasContext mContext;

        public ProdutoController(sistemavendasContext context)
        {
            mContext = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Produto> Lista = mContext.Produto.Include(x => x.CodcategoriaNavigation).ToList();

            mContext.Dispose();

            return View(Lista);
        }

        private IEnumerable<SelectListItem> ListaCategoria()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty
            });
            foreach( var item in mContext.Categoria.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                });
            }
            return lista;
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            ProdutoViewModel viewModel = new ProdutoViewModel();

            viewModel.ListaCategorias = ListaCategoria();

            if (Id != null)
            {
                Produto entidade = mContext.Produto.Where(x => x.Codigo == Id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Descricao = entidade.Descricao;
                viewModel.Valor = entidade.Valor;
                viewModel.Quantidade = entidade.Quantidade;
                viewModel.CodigoCategoria = entidade.Codcategoria;


            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                Produto objProduto = new Produto()
                {
                    Codigo = entidade.Codigo ?? 0,
                    Descricao = entidade.Descricao,
                    Valor = (decimal)entidade.Valor,
                    Quantidade = entidade.Quantidade,
                    Codcategoria = (int)entidade.CodigoCategoria,

                };
                if (entidade.Codigo == null)
                {
                    mContext.Produto.Add(objProduto);
                }
                else
                {
                    mContext.Entry(objProduto).State = EntityState.Modified;
                }
                mContext.SaveChanges();
            }
            else
            {
                entidade.ListaCategorias = ListaCategoria();
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir (int Id)
        {
            var ent = new Produto() { Codigo = Id };
            mContext.Attach(ent);
            mContext.Remove(ent);
            mContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
