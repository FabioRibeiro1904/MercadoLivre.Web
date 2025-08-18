using System.ComponentModel.DataAnnotations;

namespace MercadoLivre.Web.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Senha é obrigatória")]
        [StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string? Telefone { get; set; }
        
        [StringLength(500)]
        public string? Endereco { get; set; }
        
        [StringLength(10)]
        public string? CEP { get; set; }
        
        [StringLength(100)]
        public string? Cidade { get; set; }
        
        [StringLength(50)]
        public string? Estado { get; set; }
        
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        
        public bool IsAdmin { get; set; } = false;
        
        public decimal Saldo { get; set; } = 10000.00m;
        
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public virtual ICollection<CarrinhoItem> ItensCarrinho { get; set; } = new List<CarrinhoItem>();
        public virtual ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
    }
}
