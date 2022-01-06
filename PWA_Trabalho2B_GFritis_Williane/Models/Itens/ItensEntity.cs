using System;

namespace PWA_Trabalho2B_GFritis_Williane.Models.Itens
{
    public class ItensEntity
    {
        public Guid Id { get; set; }
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public decimal ConsumoWatts { get; set; }
        public int HorasUsoDiario { get; set; }

        public decimal ConsumoMensalKw()
        {
            return (ConsumoWatts * HorasUsoDiario * 30) / 1000;
        }
    }
}