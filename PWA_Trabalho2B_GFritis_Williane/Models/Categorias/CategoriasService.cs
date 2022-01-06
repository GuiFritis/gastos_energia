
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PWA_Trabalho2B_GFritis_Williane.Data;

namespace PWA_Trabalho2B_GFritis_Williane.Models.Categorias
{
    public class CategoriasService
    {
        private readonly DatabaseContext _databaseContext;

        public CategoriasService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public CategoriasEntity GetById(int? id)
        {
            return _databaseContext.Categorias.Find(id);
        }

        public ICollection<CategoriasEntity> GetAll()
        {
            return _databaseContext.Categorias.ToList();
        }

        public CategoriasEntity Insert(IDadosBasicosCategoriasModel requestModel)
        {

            var categoria = ValidarDados(requestModel);
            _databaseContext.Categorias.Add(categoria);
            _databaseContext.SaveChanges();
            
            return categoria;
        }
        
        public CategoriasEntity Update(IDadosBasicosCategoriasModel requestModel, int id)
        {

            var categoria = _databaseContext.Categorias.Find(id);
            
            categoria = ValidarDados(requestModel, categoria);
            _databaseContext.SaveChanges();
            
            return categoria;
        }

        public bool Delete(int id)
        {
            if (_databaseContext.Itens.Any(i => i.CategoriaId.Equals(id)))
            {
                throw new Exception("Existem itens ligados a essa categoria, não é possível realizar a deleção!");
            }
            if(_databaseContext.Categorias.Any(c => c.CategoriaPaiId.Equals(id)))
            {
                throw new Exception("Essa categoria está como categoria pai de outras categorias, não é possível realizar a deleção!");
            }
            
            var categoria = _databaseContext.Categorias.Find(id);

            _databaseContext.Categorias.Remove(categoria);

            _databaseContext.SaveChanges();

            return true;

        }

        private CategoriasEntity ValidarDados(IDadosBasicosCategoriasModel requestModel, CategoriasEntity cat = null)
        {
            var categoria = cat ?? new CategoriasEntity();

            if (requestModel.Descricao.Equals(String.Empty))
            {
                throw new Exception("Descrição da categoria é obrigatória!");
            }
            
            if (_databaseContext.Categorias.Any(c => c.Descricao.Equals(requestModel.Descricao)))
            {
                throw new Exception("Já existe uma categoria com esse nome!");
            }

            categoria.Descricao = requestModel.Descricao;

            if (requestModel.CategoriaPaiId != null)
            {
                try
                {
                    categoria.CategoriaPaiId = int.Parse(requestModel.CategoriaPaiId);
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao converter categoria pai!");
                }
            }
            else
            {
                categoria.CategoriaPaiId = null;
            }

            return categoria;

        }
        
    }

    public interface IDadosBasicosCategoriasModel
    {
        public string Descricao { get; set; }
        public string CategoriaPaiId { get; set; }
        
    }
}
