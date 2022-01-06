namespace PWA_Trabalho2B_GFritis_Williane.Models.Categorias
{
    public class CategoriasEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int? CategoriaPaiId { get; set; }
    }
}