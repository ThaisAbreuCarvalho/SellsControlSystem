using Domain.Models;
using Domain.Services.Interfaces;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public List<ClienteViewModel> GetAll()
        {
            var result = new List<ClienteViewModel>();
            var clientes = _clienteRepository.Select(new Cliente { });

            clientes.ForEach(x => result.Add(new ClienteViewModel
            {
                Celular = x.Celular,
                CNPJ_CPF = x.CnpjCpf,
                Codigo = x.Codigo,
                Email = x.Email,
                Nome = x.Nome
            }));

            return result;
        }

        public ClienteViewModel Get(int? Id)
        {
            if (!Id.HasValue)
                return new ClienteViewModel(); ;

            var cliente = _clienteRepository.Select((int)Id);

            return new ClienteViewModel
            {
                Celular = cliente.Celular,
                CNPJ_CPF = cliente.CnpjCpf,
                Codigo = cliente.Codigo,
                Email = cliente.Email,
                Nome = cliente.Nome
            };
        }

        public void Insert(ClienteViewModel cliente)
        {
            _clienteRepository.Insert(new Cliente
            {
                Celular = cliente.Celular,
                CnpjCpf = cliente.CNPJ_CPF,
                Codigo = null,
                Email = cliente.Email,
                Nome = cliente.Nome
            });
        }

        public void Delete(int Id)
        {
            _clienteRepository.Delete(Id);
        }
    }
}
