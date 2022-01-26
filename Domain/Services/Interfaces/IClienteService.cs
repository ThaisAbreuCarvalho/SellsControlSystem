using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IClienteService
    {
        List<ClienteViewModel> GetAll();
        ClienteViewModel Get(int Id);
        void Insert(ClienteViewModel cliente);
        void Delete(int Id);
        List<SelectListItem> ListaClientes();
    }
}
