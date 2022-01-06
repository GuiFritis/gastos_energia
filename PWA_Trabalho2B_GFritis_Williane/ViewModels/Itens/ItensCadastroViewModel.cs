using System;
using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;

namespace PWA_Trabalho2B_GFritis_Williane.ViewModels.Itens
{
    public class ItensCadastroViewModel
    {
        
        public string Id { get; set; }
        public string Nome { get; set; }
        public string CategoriaId { get; set; }
        public string Descricao { get; set; }
        public string DataFabricacao { get; set; }
        public string ConsumoWatts { get; set; }
        public string HorasUsoDiario { get; set; }
        public List<CategoriasViewDTO> Categorias { get; set; }
        public string MsgErro { get; set; }
        public string MsgSuccess { get; set; }
        
        public ItensCadastroViewModel()
        {
            Categorias = new List<CategoriasViewDTO>();
        }
    }
}