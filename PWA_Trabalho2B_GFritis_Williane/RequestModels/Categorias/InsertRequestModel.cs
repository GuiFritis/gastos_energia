using System;
using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;

namespace PWA_Trabalho2B_GFritis_Williane.RequestModels.Categorias
{
    public class InsertRequestModel : IDadosBasicosCategoriasModel
    {
        public string Descricao { get; set; }
        public string CategoriaPaiId { get; set; }

        public ICollection<string> ValidarEFiltrar()
        {
            var listaErros = new List<string>();

            return listaErros;
        }
    }
}