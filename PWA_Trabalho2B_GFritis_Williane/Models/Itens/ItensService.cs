
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PWA_Trabalho2B_GFritis_Williane.Data;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;

namespace PWA_Trabalho2B_GFritis_Williane.Models.Itens
{
    public class ItensService
    {
        private readonly DatabaseContext _databaseContext;

        public ItensService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public ItensEntity GetById(Guid? id)
        {
            return _databaseContext.Itens.Find(id);
        }

        public List<ItensEntity> GetItensCategoria(int id)
        {
            return _databaseContext.Itens.Where(i => i.CategoriaId.Equals(id)).ToList();
        }
        
        public ICollection<ItensEntity> GetAll()
        {
            return _databaseContext.Itens.ToList();
        }

       public ItensEntity Insert(IDadosBasicosItensModel requestModel)
        {

            var item = ValidarDados(requestModel);
            _databaseContext.Itens.Add(item);
            _databaseContext.SaveChanges();
            
            return item;
        }
        
        public ItensEntity Update(IDadosBasicosItensModel requestModel, Guid id)
        {

            var item = _databaseContext.Itens.Find(id);
            
            item = ValidarDados(requestModel, item);
            _databaseContext.SaveChanges();
            
            return item;
        }

        public bool Delete(Guid id)
        {
            
            var item = _databaseContext.Itens.Find(id);

            _databaseContext.Itens.Remove(item);

            _databaseContext.SaveChanges();

            return true;

        }

        private ItensEntity ValidarDados(IDadosBasicosItensModel requestModel, ItensEntity i = null)
        {
            var item = i ?? new ItensEntity();

            if (requestModel.Descricao.Equals(String.Empty))
            {
                throw new Exception("Descrição do Item é obrigatória!");
            }
            
            item.Descricao = requestModel.Descricao;
            
            if (requestModel.Nome.Equals(String.Empty))
            {
                throw new Exception("Nome do Item é obrigatório!");
            }
            
            item.Nome = requestModel.Nome;
            
            if (requestModel.ConsumoWatts.Equals(String.Empty))
            {
                throw new Exception("Consumo do Item é obrigatório!");
            }
            
            if (requestModel.DataFabricacao.Equals(String.Empty))
            {
                throw new Exception("Data de Fabricação do Item é obrigatório!");
            }
            
            if (requestModel.HorasUsoDiario.Equals(String.Empty))
            {
                throw new Exception("Horas de Uso do Item são obrigatórias!");
            }
            
            if (i != null){
                if(_databaseContext.Itens.Any(it => it.Nome.Equals(requestModel.Nome) && it.Id != i.Id))
                    throw new Exception("Já existe um Item com esse nome!");
            }
            else
            {
                if(_databaseContext.Itens.Any(it => it.Nome.Equals(requestModel.Nome)))
                    throw new Exception("Já existe um Item com esse nome!");
            }
            
            if (requestModel.CategoriaId != null)
            {
                try
                {
                    item.CategoriaId = int.Parse(requestModel.CategoriaId.ToString());
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao converter categoria!");
                }
            }
            
            if (requestModel.ConsumoWatts != null)
            {
                try
                {
                    item.ConsumoWatts = decimal.Parse(requestModel.ConsumoWatts);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao converter Consumo de Watts!");
                }
            }
            
            if (requestModel.HorasUsoDiario != null)
            {
                try
                {
                    item.HorasUsoDiario = int.Parse(requestModel.HorasUsoDiario);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao converter Horas de Uso Diário!");
                }
            }
            
            if (requestModel.DataFabricacao != null)
            {
                try
                {
                    item.DataFabricacao = DateTime.Parse(requestModel.DataFabricacao);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao converter Data de Fabricação!");
                }
            }
           
            return item;

        }
        
    }

    public interface IDadosBasicosItensModel
    {
        public string Nome { get; set; }
        public string CategoriaId { get; set; }
        public string DataFabricacao { get; set; }
        public string Descricao { get; set; }
        public string ConsumoWatts { get; set; }
        public string HorasUsoDiario { get; set; }
    }
}
