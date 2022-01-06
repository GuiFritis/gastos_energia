using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using PWA_Trabalho2B_GFritis_Williane.Data;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;
using PWA_Trabalho2B_GFritis_Williane.Models.Itens;
using PWA_Trabalho2B_GFritis_Williane.RequestModels.Itens;
using PWA_Trabalho2B_GFritis_Williane.ViewModels;
using PWA_Trabalho2B_GFritis_Williane.ViewModels.Itens;

namespace PWA_Trabalho2B_GFritis_Williane.Controllers
{
    public class ItensController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly ItensService _service;
        private readonly CategoriasService _catservice;

        public ItensController(DatabaseContext context, ItensService service, CategoriasService catservice)
        {
            _context = context;
            _service = service;
            _catservice = catservice;
        }
        // GET
        public IActionResult Index()
        {
            var viewModel = new ItensIndexViewModel
            {
                MsgErro = (string) TempData["erro"],
                MsgSuccess = (string) TempData["success"],
                Itens = new List<ItensViewDTO>()
            };
            
            var itens = _service.GetAll();
            
            foreach (var i in itens)
            {
                viewModel.Itens.Add(new ItensViewDTO()
                {
                    Id = i.Id.ToString(),
                    Nome = i.Nome.ToString(),
                    Categoria = _catservice.GetById(i.CategoriaId),
                    Descricao = i.Descricao,
                    DataFabricacao = i.DataFabricacao.ToString("d"),
                    ConsumoWatts = i.ConsumoWatts.ToString("N"),
                    HorasUsoDiario = i.HorasUsoDiario.ToString()
                });
            }
            
            return View(viewModel);
        }
        
        public IActionResult Cadastro(Guid? id)
        {
            var viewModel = new ItensCadastroViewModel
            {
                MsgErro = (string) TempData["erro"],
                MsgSuccess = (string) TempData["success"]
            };

            var categorias = _catservice.GetAll();
            
            foreach (var cat in categorias)
            {
                viewModel.Categorias.Add(new CategoriasViewDTO
                {
                    Id = cat.Id.ToString(),
                    Descricao = cat.Descricao,
                    CategoriaPai = _catservice.GetById(cat.CategoriaPaiId)
                });
            }
            
            if (id != null)
            {
                var item = _service.GetById(id);

                viewModel.Id = item.Id.ToString();
                viewModel.Nome = item.Nome;
                viewModel.CategoriaId = item.CategoriaId.ToString();
                viewModel.Descricao = item.Descricao;
                viewModel.DataFabricacao = item.DataFabricacao.ToString("yyyy-MM-dd");
                viewModel.ConsumoWatts = item.ConsumoWatts.ToString("N");
                viewModel.HorasUsoDiario = item.HorasUsoDiario.ToString();

            }
            
            return View(viewModel);
        }
        
        [HttpPost]
        public RedirectToActionResult InsertItem(InsertRequestModel requestModel)
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
                TempData["success"] = "Item inserido com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
                return RedirectToAction("Cadastro", new { @requestModel = requestModel });
            }
            
        }
        
        [HttpPost]
        public RedirectToActionResult UpdateItem(Guid id, UpdateRequestModel requestModel)
        {
            var erros = requestModel.ValidarEFiltrar();

            if (erros.Count > 0)
            {
                TempData["erro"] = "";
                foreach (var erro in erros)
                {
                    TempData["erro"] += erro + "<br/>";
                }

                return RedirectToAction("Cadastro", new { @id = id , @requestModel = requestModel});
            }

            try
            {
                _service.Update(requestModel, id);
                TempData["success"] = "Item atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
                return RedirectToAction("Cadastro", new { @id = id , @requestModel = requestModel});
            }

        }
        
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var item = _service.GetById(id);
                
                var viewModel = new ItensDeleteViewModel()
                {
                    Id = item.Id.ToString(),
                    Nome = item.Nome,
                    Categoria = _catservice.GetById(item.CategoriaId),
                    Descricao = item.Descricao,
                    DataFabricacao = item.DataFabricacao.ToString("yyyy-MM-dd"),
                    ConsumoWatts = item.ConsumoWatts.ToString("N"),
                    HorasUsoDiario = item.HorasUsoDiario.ToString(),
                    MsgErro = (string) TempData["erro"],
                    MsgSuccess = (string) TempData["success"]
                };

                return View(viewModel);
                
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;

                return RedirectToAction("Index");
            }
        }
        
        [HttpPost]
        public IActionResult DeleteItem(Guid id)
        {
            try
            {
                _service.Delete(id);
                TempData["success"] = "Item deletado com sucesso!";
                
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