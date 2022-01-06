using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http.Features;
using PWA_Trabalho2B_GFritis_Williane.Data;
using PWA_Trabalho2B_GFritis_Williane.Models.Itens;
using PWA_Trabalho2B_GFritis_Williane.Models.Parametros;

namespace PWA_Trabalho2B_GFritis_Williane.Models.Categorias
{
    public class AnaliseService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly CategoriasService _categoriasService;
        private readonly ItensService _itensService;
        private readonly ParametrosService _parametrosService;

        public AnaliseService(DatabaseContext databaseContext, CategoriasService categoriasService, ItensService itensService, ParametrosService parametrosService)
        {
            _databaseContext = databaseContext;
            _categoriasService = categoriasService;
            _itensService = itensService;
            _parametrosService = parametrosService;
        }

        public ConsumoTotal GetConsumoTotal()
        {
            var consumo = new ConsumoTotal();
            var itens = _itensService.GetAll();
            var parametro = _parametrosService.GetAtivo();

            decimal kwTotal = 0;
            decimal valorTotal = 0;

            foreach (var item in itens)
            {
                 kwTotal += item.ConsumoMensalKw();
            }

            consumo.WattsTotal = (kwTotal * 1000);
            consumo.ValorTotal = (kwTotal * parametro.ValorKwh);
            if (kwTotal >= parametro.FaixaConsumoAlto)
            {
                consumo.FaixaConsumo = "Alto";
            } else if (kwTotal >= parametro.FaixaConsumoMedio)
            {
                consumo.FaixaConsumo = "Médio";
            }
            else
            {
                consumo.FaixaConsumo = "Baixo";
            }

            return consumo;
        }

        public List<CategoriaConsumo> GetCategoriasQueMaisConsomem()
        {
            var parametro = _parametrosService.GetAtivo();
            var categorias = _categoriasService.GetAll();

            var listaConsumo = new List<CategoriaConsumo>();

            foreach (var cat in categorias)
            {
                var itensCat = _itensService.GetItensCategoria(cat.Id);
                decimal consumoMensal = 0;

                foreach (var item in itensCat)
                {
                    consumoMensal += item.ConsumoMensalKw();
                }

                listaConsumo.Add(new CategoriaConsumo
                {
                    Categoria = cat.Descricao,
                    ConsumoMensalKw = consumoMensal,
                    ValorMensalKwh = consumoMensal * parametro.ValorKwh
                });
            }


            return listaConsumo.OrderByDescending(c => c.ConsumoMensalKw).Take(3).ToList();
        }

        public List<ItemConsumo> GetItensQueMaisConsomem()
        {
            var parametro = _parametrosService.GetAtivo();
            var listaConsumo = new List<ItemConsumo>();

            var itens = _itensService.GetAll();

            foreach (var item in itens)
            {
                listaConsumo.Add(new ItemConsumo
                {
                    Item = item.Nome,
                    ConsumoMensalKw = item.ConsumoMensalKw(),
                    ValorMensalKwh = item.ConsumoMensalKw() * parametro.ValorKwh
                });
            }

            return listaConsumo.OrderByDescending(i => i.ConsumoMensalKw).Take(3).ToList();
        }
    }

    public class ConsumoTotal
    {
        public decimal ValorTotal { get; set; }
        public decimal WattsTotal { get; set; }
        public string FaixaConsumo { get; set; }
    }

    public class CategoriaConsumo
    {
        public string Categoria { get; set; }
        public decimal ConsumoMensalKw { get; set; }
        public decimal ValorMensalKwh { get; set; }
    }

    public class ItemConsumo
    {
        public string Item { get; set; }
        public decimal ConsumoMensalKw { get; set; }
        public decimal ValorMensalKwh { get; set; }
    }
}