using System.ComponentModel.DataAnnotations;

namespace MercadoLivre.Web.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome da categoria é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Descricao { get; set; }
        
        [StringLength(200)]
        public string? ImagemUrl { get; set; }
        
        public bool Ativa { get; set; } = true;
        
        public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
