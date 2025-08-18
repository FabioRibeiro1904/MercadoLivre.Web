using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Web.Models
{
    public class Avaliacao
    {
        public int Id { get; set; }
        
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; } = null!;
        
        [Range(1, 5, ErrorMessage = "Nota deve ser entre 1 e 5")]
        public int Nota { get; set; }
        
        [StringLength(1000)]
        public string? Comentario { get; set; }
        
        public DateTime DataAvaliacao { get; set; } = DateTime.Now;
        
        public bool Ativo { get; set; } = true;
    }
}
