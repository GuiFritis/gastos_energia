using System;
using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;

namespace PWA_Trabalho2B_GFritis_Williane.ViewModels.Categorias
{
    public class CategoriasIndexViewModel
    {
        
        public List<CategoriasViewDTO> Categorias { get; set; }
        public string MsgErro { get; set; }
        public string MsgSuccess { get; set; }
        
        public CategoriasIndexViewModel()
        {
            Categorias = new List<CategoriasViewDTO>();
        }
    }
}