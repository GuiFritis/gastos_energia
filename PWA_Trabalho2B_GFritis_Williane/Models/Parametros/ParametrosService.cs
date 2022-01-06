

using System;
using System.Collections.Generic;
using System.Linq;
using PWA_Trabalho2B_GFritis_Williane.Data;

namespace PWA_Trabalho2B_GFritis_Williane.Models.Parametros
{
    public class ParametrosService
    {
        private readonly DatabaseContext _databaseContext;

        public ParametrosService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ParametrosEntity GetById(int? id)
        {
            return _databaseContext.Parametros.Find(id);
        }
        
        public ICollection<ParametrosEntity> GetAll()
        {
            return _databaseContext.Parametros.ToList();
        }

        public ParametrosEntity GetAtivo()
        {
            return _databaseContext.Parametros.OrderBy(p => p.Id).Last();
        }
        
        public ParametrosEntity Insert(IDadosBasicosParametrosModel requestModel)
        {

            var parametro = ValidarDados(requestModel);
            _databaseContext.Parametros.Add(parametro);
            _databaseContext.SaveChanges();
            
            return parametro;
        }
        
        public ParametrosEntity Update(IDadosBasicosParametrosModel requestModel, int id)
        {
            var parametro = GetById(id);
            parametro = ValidarDados(requestModel, parametro);
            _databaseContext.SaveChanges();
            
            return parametro;
        }

        public bool Delete(int id)
        {
            var parametro = GetById(id);
            
            _databaseContext.Remove(parametro);
            
            _databaseContext.SaveChanges();

            return true;
        }

        private ParametrosEntity ValidarDados(IDadosBasicosParametrosModel requestModel, ParametrosEntity par = null)
        {
            var parametro = par ?? new ParametrosEntity();

            if (requestModel.ValorKwh != null)
            {
                try
                {
                    parametro.ValorKwh = decimal.Parse(requestModel.ValorKwh);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao converter Valor Kwh!");
                }
            }
            
            if (requestModel.FaixaConsumoAlto != null)
            {
                try
                {
                    parametro.FaixaConsumoAlto = decimal.Parse(requestModel.FaixaConsumoAlto);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao converter Faixa de Consumo Alto!");
                }
            }
            
            if (requestModel.FaixaConsumoMedio != null)
            {
                try
                {
                    parametro.FaixaConsumoMedio = decimal.Parse(requestModel.FaixaConsumoMedio);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao converter Faixa de Consumo Médio!");
                }
            }

            return parametro;
        }
        
    }

    public interface IDadosBasicosParametrosModel
    {
        public string ValorKwh { get; set; }
        public string FaixaConsumoAlto { get; set; }
        public string FaixaConsumoMedio { get; set; }
    }
    
}
