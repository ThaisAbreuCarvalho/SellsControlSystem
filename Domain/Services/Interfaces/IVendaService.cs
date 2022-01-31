using Domain.Models;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IVendaService
    {
        List<VendaViewModel> GetAll();
        VendaViewModel Get(int Id);
        void Insert(VendaViewModel newVenda);
        void Delete(int Id);
        GraficoViewModel GraphData();
    }
}
