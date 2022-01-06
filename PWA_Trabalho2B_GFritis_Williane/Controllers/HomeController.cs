using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PWA_Trabalho2B_GFritis_Williane.Models;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;
using PWA_Trabalho2B_GFritis_Williane.ViewModels;

namespace PWA_Trabalho2B_GFritis_Williane.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AnaliseService _service;

        public HomeController(ILogger<HomeController> logger, AnaliseService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel();
            
            var consumoTotal = _service.GetConsumoTotal();
            viewModel.ConsumoTotal = new ConsumoTotalDTO
            {
                WattsTotal = consumoTotal.WattsTotal.ToString("F"),
                ValorTotal = consumoTotal.ValorTotal.ToString("F"),
                FaixaConsumo = consumoTotal.FaixaConsumo
            };

            var categorias = _service.GetCategoriasQueMaisConsomem();

            foreach (var cat in categorias)
            {
                viewModel.CategoriasConsumo.Add(new CategoriaConsumoDTO
                {
                    Categoria = cat.Categoria,
                    ConsumoMensalKw = cat.ConsumoMensalKw.ToString("F"),
                    ValorMensalKwh = cat.ValorMensalKwh.ToString("F")
                });
            }
            
            var itens = _service.GetItensQueMaisConsomem();

            foreach (var item in itens)
            {
                viewModel.ItensConsumo.Add(new ItemConsumoDTO
                {
                    Item = item.Item,
                    ConsumoMensalKw = item.ConsumoMensalKw.ToString("F"),
                    ValorMensalKwh = item.ValorMensalKwh.ToString("F")
                });
            }
            
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}