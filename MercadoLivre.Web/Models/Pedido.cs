using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Web.Models
{
    public enum StatusPedido
    {
        Pendente = 1,
        Confirmado = 2,
        Preparando = 3,
        Enviado = 4,
        Entregue = 5,
        Cancelado = 6
    }
    
    public enum FormaPagamento
    {
        CartaoCredito = 1,
        CartaoDebito = 2,
        Pix = 3,
        Boleto = 4
    }
    
    public class Pedido
    {
        public int Id { get; set; }
        
        [Required]
        public string NumeroPedido { get; set; } = string.Empty;
        
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotal { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorFrete { get; set; }
        
        public StatusPedido Status { get; set; } = StatusPedido.Pendente;
        
        public FormaPagamento FormaPagamento { get; set; }
        
        [StringLength(500)]
        public string? EnderecoEntrega { get; set; }
        
        [StringLength(10)]
        public string? CEPEntrega { get; set; }
        
        [StringLength(100)]
        public string? CidadeEntrega { get; set; }
        
        [StringLength(50)]
        public string? EstadoEntrega { get; set; }
        
        [StringLength(50)]
        public string? CodigoRastreamento { get; set; }
        
        public DateTime DataPedido { get; set; } = DateTime.Now;
        
        public DateTime? DataConfirmacao { get; set; }
        
        public DateTime? DataEnvio { get; set; }
        
        public DateTime? DataEntrega { get; set; }
        
        [StringLength(1000)]
        public string? Observacoes { get; set; }
        
        public virtual ICollection<PedidoItem> Itens { get; set; } = new List<PedidoItem>();
        
        [NotMapped]
        public decimal ValorTotalComFrete => ValorTotal + ValorFrete;
        
        [NotMapped]
        public string StatusDescricao => Status switch
        {
            StatusPedido.Pendente => "Pendente",
            StatusPedido.Confirmado => "Confirmado", 
            StatusPedido.Preparando => "Preparando",
            StatusPedido.Enviado => "Enviado",
            StatusPedido.Entregue => "Entregue",
            StatusPedido.Cancelado => "Cancelado",
            _ => "Desconhecido"
        };
    }
}
