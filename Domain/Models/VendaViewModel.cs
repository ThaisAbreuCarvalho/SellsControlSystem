using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Models
{
    public class VendaViewModel
    {

        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Informe a data da venda")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Informe o codigo do cliente")]
        public int? CodigoCliente { get; set; }

        public decimal? Total { get; set; }
        public IEnumerable<SelectListItem> ListaClientes { get; set; }
        public IEnumerable<SelectListItem> ListaProdutos { get; set; }

        public string JsonProdutos { get; set; }
        
    }
}
