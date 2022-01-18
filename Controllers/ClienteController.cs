using Microsoft.AspNetCore.Mvc;
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
    public class ClienteController : Controller
    {
        protected sistemavendasContext mContext;

        public ClienteController(sistemavendasContext context)
        {
            mContext = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Cliente> Lista = mContext.Cliente.ToList();

            mContext.Dispose();

            return View(Lista);
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            ClienteViewModel viewModel = new ClienteViewModel();

            if (Id != null)
            {
                Cliente entidade = mContext.Cliente.Where(x => x.Codigo == Id).FirstOrDefault();
                viewModel.Codigo = entidade.Codigo;
                viewModel.Nome = entidade.Nome;
                viewModel.CNPJ_CPF = entidade.CnpjCpf;
                viewModel.Email = entidade.Email;
                viewModel.Celular = entidade.Celular;


            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                Cliente objCliente = new Cliente()
                {
                    Codigo = entidade.Codigo ?? 0,
                    Nome = entidade.Nome,
                    CnpjCpf = entidade.CNPJ_CPF,
                    Email = entidade.Email,
                    Celular = entidade.Celular,

                };
                if (entidade.Codigo == null)
                {
                    mContext.Cliente.Add(objCliente);
                }
                else
                {
                    mContext.Entry(objCliente).State = EntityState.Modified;
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
        public IActionResult Excluir(int Id)
        {
            var ent = new Cliente() { Codigo = Id };
            mContext.Attach(ent);
            mContext.Remove(ent);
            mContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
