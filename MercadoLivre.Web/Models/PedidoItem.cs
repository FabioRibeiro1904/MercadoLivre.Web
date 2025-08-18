using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Web.Models
{
    public class PedidoItem
    {
        public int Id { get; set; }
        
        public int PedidoId { get; set; }
        public virtual Pedido Pedido { get; set; } = null!;
        
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; } = null!;
        
        [Required]
        [StringLength(200)]
        public string NomeProduto { get; set; } = string.Empty;
        
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade deve ser maior que zero")]
        public int Quantidade { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecoUnitario { get; set; }
        
        [NotMapped]
        public decimal SubTotal => Quantidade * PrecoUnitario;
    }
}
