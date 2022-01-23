using Domain.Models;
using Domain.Services.Interfaces;
using Repository.Entities;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public List<CategoriaViewModel> GetAll()
        {
            var retorno = new List<CategoriaViewModel>();
            var categories = _categoriaRepository.Select(new Repository.Entities.Categoria { });

            categories.ForEach(x => retorno.Add(new CategoriaViewModel
            {
                Codigo = x.Codigo,
                Descricao = x.Descricao
            }));

            return retorno;
        }

        public CategoriaViewModel Get(int? Id)
        {
            if (!Id.HasValue)
                return new CategoriaViewModel();

            var categories = _categoriaRepository.Select((int)Id);

            return new CategoriaViewModel
            {
                Codigo = categories.Codigo,
                Descricao = categories.Descricao
            };
        }

        public void Insert(CategoriaViewModel newCategory)
        {
            _categoriaRepository.Insert(new Categoria
            {
                Codigo = null,
                Descricao = newCategory.Descricao
            });
        }

        public void Delete(int Id)
        {
            _categoriaRepository.Delete(Id);
        }
    }
}
