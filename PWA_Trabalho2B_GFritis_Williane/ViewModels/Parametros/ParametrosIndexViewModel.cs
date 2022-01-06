using System;
using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;
using PWA_Trabalho2B_GFritis_Williane.Models.Parametros;

namespace PWA_Trabalho2B_GFritis_Williane.ViewModels.Parametros
{
    public class ParametrosIndexViewModel
    {
        
        public List<ParametrosViewDTO> Parametros { get; set; }
        public string MsgErro { get; set; }
        public string MsgSuccess { get; set; }
        
        public ParametrosIndexViewModel()
        {
            Parametros = new List<ParametrosViewDTO>();
        }
    }
}