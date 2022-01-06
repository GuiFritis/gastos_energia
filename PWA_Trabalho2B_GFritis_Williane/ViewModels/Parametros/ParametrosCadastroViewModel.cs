using System;
using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;

namespace PWA_Trabalho2B_GFritis_Williane.ViewModels.Parametros
{
    public class ParametrosCadastroViewModel
    {
        
        public string Id { get; set; }
        public string ValorKwh { get; set; }
        public string FaixaConsumoAlto { get; set; }
        public string FaixaConsumoMedio { get; set; }
        public string MsgErro { get; set; }
        public string MsgSuccess { get; set; }
        
        public ParametrosCadastroViewModel()
        {
        }
    }
}