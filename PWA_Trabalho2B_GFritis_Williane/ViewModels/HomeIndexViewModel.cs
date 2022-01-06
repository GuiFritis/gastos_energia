using System.Collections.Generic;
using PWA_Trabalho2B_GFritis_Williane.Models;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;

namespace PWA_Trabalho2B_GFritis_Williane.ViewModels
{
    public class HomeIndexViewModel
    {
        public ConsumoTotalDTO ConsumoTotal { get; set; }
        public List<CategoriaConsumoDTO> CategoriasConsumo { get; set; }
        public List<ItemConsumoDTO> ItensConsumo { get; set; }

        public HomeIndexViewModel()
        {
            ConsumoTotal = new ConsumoTotalDTO();
            CategoriasConsumo = new List<CategoriaConsumoDTO>();
            ItensConsumo = new List<ItemConsumoDTO>();
        }
    }
}