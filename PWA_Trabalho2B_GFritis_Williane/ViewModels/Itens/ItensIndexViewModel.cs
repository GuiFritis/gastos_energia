using System;
using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;
using PWA_Trabalho2B_GFritis_Williane.Models.Itens;

namespace PWA_Trabalho2B_GFritis_Williane.ViewModels.Itens
{
    public class ItensIndexViewModel
    {
        
        public List<ItensViewDTO> Itens { get; set; }
        public string MsgErro { get; set; }
        public string MsgSuccess { get; set; }
        
        public ItensIndexViewModel()
        {
            Itens = new List<ItensViewDTO>();
        }
    }
    
}