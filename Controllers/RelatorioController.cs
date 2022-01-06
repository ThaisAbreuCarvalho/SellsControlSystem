using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Entities;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class RelatorioController : Controller
    {
        protected sistemavendasContext mContext;

        public RelatorioController(sistemavendasContext context)
        {
            mContext = context;
        }

        public IActionResult Grafico()
        {
            List<Produto> Lista = mContext.Produto.ToList();
            List<Vendaproduto> ListaVenda = mContext.Vendaproduto.ToList();
            mContext.Dispose();

            string labelArray = string.Empty;
            string valuesArray = string.Empty;
            string coresArray = string.Empty;
            var random = new Random();

            for(int i=0; i< Lista.Count(); i++)
            {
                var itemVenda = ListaVenda.Where(x => x.Codigoproduto == Lista[i].Codigo).ToList();
                var quantidade = itemVenda.Sum(x => x.Quantidade);

                labelArray +=  "'" + Lista[i].Descricao.ToString() + "', ";
                valuesArray +=  quantidade.ToString() + ", ";
                coresArray += "'" + $"#{random.Next(0x10000000)}" + "', ";
            }

            ViewBag.values = valuesArray;
            ViewBag.labels = labelArray;
            ViewBag.cores = coresArray;

            return View();
        }
    }
}
