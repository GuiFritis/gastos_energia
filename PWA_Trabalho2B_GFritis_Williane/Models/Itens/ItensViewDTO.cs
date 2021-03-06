using System;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;

namespace PWA_Trabalho2B_GFritis_Williane.Models.Itens
{
    public class ItensViewDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public CategoriasEntity Categoria { get; set; }
        public string Descricao { get; set; }
        public string DataFabricacao { get; set; }
        public string ConsumoWatts { get; set; }
        public string HorasUsoDiario { get; set; }
    }
}