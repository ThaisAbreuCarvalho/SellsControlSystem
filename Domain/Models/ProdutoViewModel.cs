using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Models
{
    public class ProdutoViewModel
    {

        public int? Codigo { get; set; }
        [Required(ErrorMessage = "Informe a descrição do produto")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Informe a quantidade em estoque")]    
        public int? Quantidade { get; set; }
        [Required(ErrorMessage = "Informe o valor unitario do produto")]
        [Range (0.1, Double.PositiveInfinity)]
        public decimal? Valor { get; set; }
        [Required(ErrorMessage = "Informe a categoria do produto")]

        public int? CodigoCategoria { get; set; }

        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
