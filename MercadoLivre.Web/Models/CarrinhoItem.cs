using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Web.Models
{
    public class CarrinhoItem
    {
        public int Id { get; set; }
        
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; } = null!;
        
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
        public int Quantidade { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecoUnitario { get; set; }
        
        public DateTime DataAdicao { get; set; } = DateTime.Now;
        
        [NotMapped]
        public decimal SubTotal => Quantidade * PrecoUnitario;
    }
}
