using Microsoft.EntityFrameworkCore;
using PWA_Trabalho2B_GFritis_Williane.Models.Categorias;
using PWA_Trabalho2B_GFritis_Williane.Models.Itens;
using PWA_Trabalho2B_GFritis_Williane.Models.Parametros;

namespace PWA_Trabalho2B_GFritis_Williane.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ParametrosEntity> Parametros { get; set; }
        public DbSet<ItensEntity> Itens { get; set; }
        public DbSet<CategoriasEntity> Categorias { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
    }
}