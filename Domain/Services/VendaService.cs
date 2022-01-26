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
        public VendaService(IVendaRepository vendaRepository, IClienteService clienteService, IProdutoService produtoService)
        {
            _vendaRepository = vendaRepository;
            _clienteService = clienteService;
            _produtoService = produtoService;
        }

        public List<VendaViewModel> GetAll()
        {
            var retorno = new List<VendaViewModel>();
            var vendas = _vendaRepository.Select(new Venda { });
            var clientes = _clienteService.ListaClientes();
            var produtos = _produtoService.ListaProdutos();

            vendas.ForEach(x => retorno.Add(new VendaViewModel
            {
                Codigo = x.Codigo,
                CodigoCliente = x.Codcliente,
                Data = x.Data,
                ListaClientes = clientes,
                ListaProdutos = produtos,
                Total = x.Total,
                NomeCliente = x.CodclienteNavigation?.Nome
            }));

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
                NomeCliente = venda.CodclienteNavigation?.Nome
            };

            return result;
        }

        public void Insert(VendaViewModel newVenda)
        {
            var venda = new Venda
            {
                Data = (DateTime)newVenda.Data,
                Total = newVenda.Total,
                Codcliente = newVenda.CodigoCliente,
            };

            venda.Vendaproduto = JsonConvert.DeserializeObject<ICollection<Vendaproduto>>(newVenda.JsonProdutos);
            _vendaRepository.Insert(venda);
        }

        public void Delete(int Id)
        {
            _vendaRepository.Delete(Id);
        }
    }
}
