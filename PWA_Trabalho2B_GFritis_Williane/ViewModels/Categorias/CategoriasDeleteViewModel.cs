using System;
using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;

namespace PWA_Trabalho2B_GFritis_Williane.ViewModels.Categorias
{
    public class CategoriasDeleteViewModel
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public CategoriasEntity? CategoriaPai { get; set; }
        public string MsgErro { get; set; }
        public string MsgSuccess { get; set; }
    }
}