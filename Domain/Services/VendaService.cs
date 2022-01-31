using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IClienteService _clienteService;
        private readonly IProdutoService _produtoService;
        private readonly IVendaProdutoRepository _vendaProduto;
        public VendaService(IVendaRepository vendaRepository,
                            IClienteService clienteService,
                            IProdutoService produtoService,
                            IVendaProdutoRepository vendaProduto)
        {
            _vendaRepository = vendaRepository;
            _clienteService = clienteService;
            _produtoService = produtoService;
            _vendaProduto = vendaProduto;
        }

        public List<VendaViewModel> GetAll()
        {
            var retorno = new List<VendaViewModel>();
            var vendas = _vendaRepository.Select(new Venda { });
            var clientes = _clienteService.ListaClientes();
            var produtos = _produtoService.ListaProdutos();

            foreach (var venda in vendas)
            {
                var newVenda = new VendaViewModel
                {
                    Codigo = venda.Codigo,
                    CodigoCliente = venda.Codcliente,
                    Data = venda.Data,
                    ListaClientes = clientes,
                    ListaProdutos = produtos,
                    Total = venda.Total,
                    NomeCliente = venda.CodclienteNavigation?.Nome,
                    vendas = new List<ProdutoViewModel>()
                };

                venda.Vendaproduto.ToList().ForEach(x => newVenda.vendas.Add(new ProdutoViewModel
                {
                    Codigo = x.Codigoproduto,
                    Quantidade = x.Quantidade,
                    Valor = x.Total,
                    Descricao = produtos.Find(y => y.Value == x.Codigoproduto.ToString()).Text
                }));

                retorno.Add(newVenda);
            }

            return retorno;
        }

        public VendaViewModel Get(int Id)
        {
            var venda = _vendaRepository.Select(Id);
            var clientes = _clienteService.ListaClientes();
            var produtos = _produtoService.ListaProdutos();

            var result = new VendaViewModel
            {
                Codigo = venda.Codigo,
                CodigoCliente = venda.Codcliente,
                Data = venda.Data,
                ListaClientes = clientes,
                ListaProdutos = produtos,
                Total = venda.Total,
                NomeCliente = venda.CodclienteNavigation?.Nome,
                vendas = new List<ProdutoViewModel>()
            };

            venda.Vendaproduto.ToList().ForEach(x => result.vendas.Add(new ProdutoViewModel
            {
                Codigo = x.Codigoproduto,
                Quantidade = x.Quantidade,
                Valor = x.Total,
                Descricao = produtos.Find(y => y.Value == x.Codigoproduto.ToString()).Text
            }));

            return result;
        }

        public void Insert(VendaViewModel newVenda)
        {
            var venda = new Venda
            {
                Data = (DateTime)newVenda.Data,
                Codcliente = newVenda.CodigoCliente,
            };

            venda.Vendaproduto = JsonConvert.DeserializeObject<ICollection<VendaProduto>>(newVenda.JsonProdutos);

            var total = venda.Vendaproduto.Sum(x => x.Total);
            venda.Total = total;

            _vendaRepository.Insert(venda);
        }

        public void Delete(int Id)
        {
            _vendaRepository.Delete(Id);
        }

        public GraficoViewModel GraphData()
        {
            var vendas = _vendaProduto.Select(new VendaProduto { });
            var produtos = _produtoService.GetAll();

            string labelArray = string.Empty;
            string valuesArray = string.Empty;
            string coresArray = string.Empty;
            var random = new Random();

            foreach (var produto in produtos)
            {
                var itemVenda = vendas.FindAll(x => x.Codigoproduto == produto.Codigo);
                var quantidade = itemVenda.Sum(x => x.Quantidade);

                labelArray += "'" + produto.Descricao.ToString() + "', ";
                valuesArray += quantidade.ToString() + ", ";
                coresArray += "'" + $"#{random.Next(0x1000000)}" + "', ";
            }

            return new GraficoViewModel
            {
                colors = coresArray,
                labels = labelArray,
                values = valuesArray
            };
        }
    }
}
