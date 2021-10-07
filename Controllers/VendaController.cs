using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class VendaController : Controller
    {
        protected sistemavendasContext mContext;

        public VendaController(sistemavendasContext context)
        {
            mContext = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Venda> Lista = mContext.Venda.ToList();

            mContext.Dispose();

            return View(Lista);
        }

        private IEnumerable<SelectListItem> ListaProdutos()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty
            });
            foreach (var item in mContext.Produto.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                });
            }
            return lista;
        }

        private IEnumerable<SelectListItem> ListaClientes()
        {
            List<SelectListItem> lista = new List<SelectListItem>();

            lista.Add(new SelectListItem()
            {
                Value = string.Empty,
                Text = string.Empty
            });
            foreach (var item in mContext.Cliente.ToList())
            {
                lista.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Nome.ToString()
                });
            }
            return lista;
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            VendaViewModel viewModel = new VendaViewModel();

            viewModel.ListaClientes = ListaClientes();
            viewModel.ListaProdutos = ListaProdutos();

            if (Id != null)
            {
                Venda entidade = mContext.Venda.Where(x => x.Codigo == Id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Data = entidade.Data;
                viewModel.CodigoCliente = entidade.Codcliente;
                viewModel.Total = entidade.Total;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(VendaViewModel entidade)
        {
            if (ModelState.IsValid)
            {
               
                Venda objVenda = new Venda()
                {
                    Data = (DateTime)entidade.Data,
                    Total = entidade.Total,
                    Codcliente = entidade.CodigoCliente,
                    Vendaproduto = JsonConvert.DeserializeObject<ICollection<Vendaproduto>>(entidade.JsonProdutos)
                };

                var price = objVenda.Vendaproduto.ToList();
                objVenda.Total = price.Select(x=> x.Total).Sum();

                if (entidade.Codigo == null) 
                {
                    mContext.Venda.Add(objVenda);
                }
                else
                {
                    mContext.Entry(objVenda).State = EntityState.Modified;
                }
                mContext.SaveChanges();
            }
            else
            {
                entidade.ListaClientes = ListaClientes();
                entidade.ListaProdutos = ListaProdutos();
                return View(entidade);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int Id)
        {
            var ent = new Venda() { Codigo = Id };
            mContext.Attach(ent);
            mContext.Remove(ent);
            mContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal? LerValorProduto(int CodigoProduto)
        {
            return mContext.Produto.Where(x => x.Codigo == CodigoProduto).Select(x=> x.Valor).FirstOrDefault();
        }

    }
}
