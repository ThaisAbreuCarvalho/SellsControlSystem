using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface ICategoriaService
    {
        List<CategoriaViewModel> GetAll();
        CategoriaViewModel Get(int? Id);
    }
}
