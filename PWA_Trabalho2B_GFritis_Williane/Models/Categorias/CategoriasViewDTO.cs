namespace PWA_Trabalho2B_GFritis_Williane.Models.Categorias
{
    public class CategoriasViewDTO
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public CategoriasEntity? CategoriaPai { get; set; }
    }
}