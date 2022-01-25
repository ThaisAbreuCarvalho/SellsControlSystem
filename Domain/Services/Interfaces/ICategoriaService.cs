using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ICategoriaService
    {
        List<CategoriaViewModel> GetAll();
        CategoriaViewModel Get(int Id);
        void Insert(CategoriaViewModel newCategory);
        void Delete(int Id);
        List<SelectListItem> ListaCategoria();
    }
}
