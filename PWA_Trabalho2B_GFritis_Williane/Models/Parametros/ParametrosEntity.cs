using System;

namespace PWA_Trabalho2B_GFritis_Williane.Models.Parametros
{
    public class ParametrosEntity
    {
        public int Id { get; set; }
        public decimal ValorKwh { get; set; }
        public decimal FaixaConsumoAlto { get; set; }
        public decimal FaixaConsumoMedio { get; set; }
    }
}