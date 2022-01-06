using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWA_Trabalho2B_GFritis_Williane.Data;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;
using PWA_Trabalho2B_GFritis_Williane.RequestModels.Categorias;
using PWA_Trabalho2B_GFritis_Williane.ViewModels;
using PWA_Trabalho2B_GFritis_Williane.ViewModels.Categorias;

namespace PWA_Trabalho2B_GFritis_Williane.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly CategoriasService _service;

        public CategoriasController(DatabaseContext context, CategoriasService service)
        {
            _context = context;
            _service = service;
        }
        // GET
        public IActionResult Index()
        {
            var viewModel = new CategoriasIndexViewModel
            {
                MsgErro = (string) TempData["erro"],
                MsgSuccess = (string) TempData["success"],
                Categorias = new List<CategoriasViewDTO>()
            };

            var categorias = _service.GetAll();
            
            foreach (var cat in categorias)
            {
                viewModel.Categorias.Add(new CategoriasViewDTO
                {
                    Id = cat.Id.ToString(),
                    Descricao = cat.Descricao,
                    CategoriaPai = _service.GetById(cat.CategoriaPaiId)
                });
            }
            
            return View(viewModel);
        }
        
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new CategoriasCadastroViewModel
            {
                MsgErro = (string) TempData["erro"],
                MsgSuccess = (string) TempData["success"],
            };
            
            var categorias = _service.GetAll();
            
            foreach (var cat in categorias)
            {
                if (id != cat.Id)
                {
                    viewModel.Categorias.Add(new CategoriasViewDTO
                    {
                        Id = cat.Id.ToString(),
                        Descricao = cat.Descricao,
                        CategoriaPai = _service.GetById(cat.CategoriaPaiId)
                    });
                }
            }

            if (id != null)
            {
                var categoria = _service.GetById(id);

                viewModel.Id = categoria.Id.ToString();
                viewModel.Descricao = categoria.Descricao;
                viewModel.CategoriaPaiId = categoria.CategoriaPaiId.ToString();
            }

            return View(viewModel);
        }

        [HttpPost]
        public RedirectToActionResult InsertCategoria(InsertRequestModel requestModel)
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
                TempData["success"] = "Categoria inserida com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
                return RedirectToAction("Cadastro", new { @requestModel = requestModel });
            }

        }
        
        [HttpPost]
        public RedirectToActionResult UpdateCategoria(int id, UpdateRequestModel requestModel)
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
                TempData["success"] = "Categoria atualizada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.Message;
                return RedirectToAction("Cadastro", new { @id = id , @requestModel = requestModel});
            }

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var categoria = _service.GetById(id);
                
                var viewModel = new CategoriasDeleteViewModel()
                {
                    Id = categoria.Id.ToString(),
                    Descricao = categoria.Descricao,
                    CategoriaPai = _service.GetById(categoria.CategoriaPaiId),
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
        public IActionResult DeleteCategoria(int id)
        {
            try
            {
                _service.Delete(id);
                TempData["success"] = "Categoria deletada com sucesso!";
                
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