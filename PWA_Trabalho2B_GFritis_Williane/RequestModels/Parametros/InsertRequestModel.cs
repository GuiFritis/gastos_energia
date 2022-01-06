using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;
using PWA_Trabalho2B_GFritis_Williane.Models.Parametros;

namespace PWA_Trabalho2B_GFritis_Williane.RequestModels.Parametros
{
    public class InsertRequestModel : IDadosBasicosParametrosModel
    {
        public string ValorKwh { get; set; }
        public string FaixaConsumoAlto { get; set; }
        public string FaixaConsumoMedio { get; set; }

        public ICollection<string> ValidarEFiltrar()
        {
            var listaErros = new List<string>();

            return listaErros;
        }
    }
}