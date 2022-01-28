using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Entities;
using Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaService _categoriaService;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaService categoriaService)
        {
            _produtoRepository = produtoRepository;
            _categoriaService = categoriaService;
        }

        public List<ProdutoViewModel> GetAll()
        {
            var retorno = new List<ProdutoViewModel>();
            var products = _produtoRepository.Select(new Produto { });
            var categorias = _categoriaService.ListaCategoria();
            foreach (var product in products)
            {
                retorno.Add(new ProdutoViewModel
                {
                    Codigo = product.Codigo,
                    CodigoCategoria = product.Codcategoria,
                    Descricao = product.Descricao,
                    Quantidade = product.Quantidade,
                    Valor = product.Valor,
                    CategoriaDescricao = product.CodcategoriaNavigation.Descricao
                });

                retorno.LastOrDefault().ListaCategorias = categorias;
            }

            return retorno;
        }

        public ProdutoViewModel Get(int Id)
        {
            var product = _produtoRepository.Select(Id);
            var categorias = _categoriaService.ListaCategoria();

            var result = new ProdutoViewModel
            {
                Codigo = product.Codigo,
                CodigoCategoria = product.Codcategoria,
                Descricao = product.Descricao,
                Quantidade = product.Quantidade,
                Valor = product.Valor,
                CategoriaDescricao = product.CodcategoriaNavigation.Descricao,
                ListaCategorias = categorias
            };

            return result;
        }

        public void Insert(ProdutoViewModel newProduct)
        {
            _produtoRepository.Insert(new Produto
            {
                Valor = newProduct.Valor,
                Quantidade = newProduct.Quantidade,
                Descricao = newProduct.Descricao,
                Codcategoria = newProduct.CodigoCategoria,
                Codigo = null
            }); ;
        }

        public void Delete(int Id)
        {
            _produtoRepository.Delete(Id);
        }

        public List<SelectListItem> ListaProdutos()
        {
            var categories = _produtoRepository.Select(new Produto { });
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
