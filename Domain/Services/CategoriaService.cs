using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var categories = _categoriaRepository.Select(new Categoria { });

            categories.ForEach(x => retorno.Add(new CategoriaViewModel
            {
                Codigo = x.Codigo,
                Descricao = x.Descricao
            }));

            return retorno;
        }

        public CategoriaViewModel Get(int Id)
        {
            var categories = _categoriaRepository.Select(Id);

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

        public List<SelectListItem> ListaCategoria()
        {
            var categories = _categoriaRepository.Select(new Categoria { });
            var result = new List<SelectListItem>();

            categories.ForEach(x =>
            result.Add(new SelectListItem()
            {
                Value = x.Codigo.ToString(),
                Text = x.Descricao.ToString()
            }));

            return result;
        }
    }
}
