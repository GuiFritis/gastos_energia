using System;
using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;

namespace PWA_Trabalho2B_GFritis_Williane.ViewModels.Categorias
{
    public class CategoriasCadastroViewModel
    {
        
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string CategoriaPaiId { get; set; }
        public string MsgErro { get; set; }
        public string MsgSuccess { get; set; }
        public List<CategoriasViewDTO> Categorias { get; set; }
        
        public CategoriasCadastroViewModel()
        {
            Categorias = new List<CategoriasViewDTO>();
        }
    }
}