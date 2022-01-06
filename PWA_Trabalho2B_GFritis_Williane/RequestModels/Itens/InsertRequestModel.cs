using System;
using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;
using PWA_Trabalho2B_GFritis_Williane.Models.Itens;
using PWA_Trabalho2B_GFritis_Williane.Models.Parametros;

namespace PWA_Trabalho2B_GFritis_Williane.RequestModels.Itens
{
    public class InsertRequestModel : IDadosBasicosItensModel
    {
        public string Nome { get; set; }
        
        public string CategoriaId { get; set; }
        public string Descricao { get; set; }
        public string DataFabricacao { get; set; }
        public string ConsumoWatts { get; set; }
        public string HorasUsoDiario { get; set; }

        public ICollection<string> ValidarEFiltrar()
        {
            var listaErros = new List<string>();

            return listaErros;
        }
    }
}