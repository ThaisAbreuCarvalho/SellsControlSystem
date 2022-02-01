using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SistemaVenda.Controllers
{
    public class VendaController : Controller
    {
        private readonly IVendaService _vendaService;
        private readonly IProdutoService _produtoService;
        private readonly IClienteService _clienteService;
        public VendaController(IVendaService vendaService, IProdutoService produtoService, IClienteService clienteService)
        {
            _vendaService = vendaService;
            _produtoService = produtoService;
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            return View(_vendaService.GetAll());
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (!Id.HasValue)
            {
                var viewModel = new VendaViewModel();
                viewModel.ListaClientes = _clienteService.ListaClientes();
                viewModel.ListaProdutos = _produtoService.ListaProdutos();
                return View(viewModel);
            }

            return View(_vendaService.Get((int)Id));
        }

        [HttpPost]
        public IActionResult Cadastro(VendaViewModel entidade)
        {
            if (!ModelState.IsValid)
            {
                entidade.ListaClientes = _clienteService.ListaClientes();
                entidade.ListaProdutos = _produtoService.ListaProdutos();
                return View(entidade);
            }

            _vendaService.Insert(entidade);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Excluir(int Id)
        {
            _vendaService.Delete(Id);
            return RedirectToAction("Index");
        }

        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal? LerValorProduto(int CodigoProduto)
        {
            return _produtoService.Get(CodigoProduto).Valor;
        }

    }
}
