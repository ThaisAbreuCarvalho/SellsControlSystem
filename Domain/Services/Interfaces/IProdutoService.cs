using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IProdutoService
    {
        List<ProdutoViewModel> GetAll();
        ProdutoViewModel Get(int Id);
        void Insert(ProdutoViewModel newProduct);
        void Delete(int Id);
        List<SelectListItem> ListaProdutos();
    }
}
