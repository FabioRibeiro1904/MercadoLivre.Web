using MercadoLivre.Web.Models;

namespace MercadoLivre.Web.Models
{
    public class CategoriaViewModel
    {
        public Categoria Categoria { get; set; } = null!;
        public List<Produto> Produtos { get; set; } = new();
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
    }
}
