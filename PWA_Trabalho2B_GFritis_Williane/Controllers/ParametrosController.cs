using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA_Trabalho2B_GFritis_Williane.Data;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;
using PWA_Trabalho2B_GFritis_Williane.Models.Parametros;
using PWA_Trabalho2B_GFritis_Williane.RequestModels.Parametros;
using PWA_Trabalho2B_GFritis_Williane.ViewModels;
using PWA_Trabalho2B_GFritis_Williane.ViewModels.Parametros;

namespace PWA_Trabalho2B_GFritis_Williane.Controllers
{
    public class ParametrosController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly ParametrosService _service;

        public ParametrosController(DatabaseContext context, ParametrosService service)
        {
            _context = context;
            _service = service;
        }
        // GET
        public IActionResult Index()
        {
            var viewModel = new ParametrosIndexViewModel
            {
                MsgErro = (string) TempData["erro"],
                MsgSuccess = (string) TempData["success"],
                Parametros = new List<ParametrosViewDTO>()
            };

            var parametros = _service.GetAll();

            foreach (var par in parametros)
            {
                viewModel.Parametros.Add(new ParametrosViewDTO
                {
                    Id = par.Id.ToString(),
                    ValorKwh = par.ValorKwh.ToString("C"),
                    FaixaConsumoAlto = par.FaixaConsumoAlto.ToString("N"),
                    FaixaConsumoMedio = par.FaixaConsumoMedio.ToString("N")
                });
            }
            
            return View(viewModel);
        }
        
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new ParametrosCadastroViewModel
            {
                MsgErro = (string) TempData["erro"],
                MsgSuccess = (string) TempData["success"]
            };

            var parametro = _service.GetById(id);
            
            if (parametro != null)
            {
                viewModel.Id = parametro.Id.ToString();
                viewModel.ValorKwh = parametro.ValorKwh.ToString("C");
                viewModel.FaixaConsumoAlto = parametro.FaixaConsumoAlto.ToString("N");
                viewModel.FaixaConsumoMedio = parametro.FaixaConsumoMedio.ToString("N");
            }
            
            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult InsertParametro(InsertRequestModel requestModel)
        {
            var erros = requestModel.ValidarEFiltrar();

            if (erros.Count > 0)
            {
                TempData["erro"] = "";
                foreach (var erro in erros)
                {
                    TempData["erro"] += erro + "<br/>";
                }

                return RedirectToAction("Cadastro");
            }

            try
            {
                _service.Insert(requestModel);
                TempData["success"] = "Parâmetro inserido com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
                return RedirectToAction("Cadastro");
            }
        }
        
        [HttpPost]
        public IActionResult UpdateParametro(UpdateRequestModel requestModel, int id)
        {
            var erros = requestModel.ValidarEFiltrar();

            if (erros.Count > 0)
            {
                TempData["erro"] = "";
                foreach (var erro in erros)
                {
                    TempData["erro"] += erro + "<br/>";
                }

                return RedirectToAction("Cadastro", id);
            }

            try
            {
                _service.Update(requestModel, id);
                TempData["success"] = "Parâmetro atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
                return RedirectToAction("Cadastro", id);
            }
        }
        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var parametro = _service.GetById(id);
                
                var viewModel = new ParametrosDeleteViewModel()
                {
                    Id = parametro.Id.ToString(),
                    ValorKwh = parametro.ValorKwh.ToString("C"),
                    FaixaConsumoAlto = parametro.FaixaConsumoAlto.ToString("N"),
                    FaixaConsumoMedio = parametro.FaixaConsumoMedio.ToString("N"),
                    MsgErro = (string) TempData["erro"],
                    MsgSuccess = (string) TempData["success"]
                };

                return View(viewModel);
                
            }
            catch (Exception e)
            {
                TempData["erro"] = "Nenhum parâmetro encontrada para deletar!";

                return RedirectToAction("Index");
            }
            
        }
        
        [HttpPost]
        public RedirectToActionResult DeleteParametro(int id)
        {
            try
            {
                _service.Delete(id);
                TempData["success"] = "Parâmetro deletado com sucesso!";

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
                
                return RedirectToAction("Delete",  new { @id = id });
            }
        }
        
    }
}