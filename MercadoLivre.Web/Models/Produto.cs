using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Web.Models
{
    public class Produto
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome do produto é obrigatório")]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(2000)]
        public string Descricao { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Preço é obrigatório")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Preço deve ser maior que zero")]
        public decimal Preco { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal? PrecoOriginal { get; set; }
        
        [Required(ErrorMessage = "Estoque é obrigatório")]
        [Range(0, int.MaxValue, ErrorMessage = "Estoque não pode ser negativo")]
        public int Estoque { get; set; }
        
        [StringLength(100)]
        public string? Marca { get; set; }
        
        [StringLength(100)]
        public string? Modelo { get; set; }
        
        [StringLength(500)]
        public string? ImagemPrincipal { get; set; }
        
        public string? ImagensSecundarias { get; set; }
        
        public bool Ativo { get; set; } = true;
        
        public bool Promocao { get; set; } = false;
        
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; } = null!;
        
        public virtual ICollection<PedidoItem> ItensPedido { get; set; } = new List<PedidoItem>();
        public virtual ICollection<CarrinhoItem> ItensCarrinho { get; set; } = new List<CarrinhoItem>();
        public virtual ICollection<Avaliacao> Avaliacoes { get; set; } = new List<Avaliacao>();
        
        [NotMapped]
        public decimal PrecoComDesconto => PrecoOriginal.HasValue ? Preco : Preco;
        
        [NotMapped]
        public decimal? PercentualDesconto => PrecoOriginal.HasValue 
            ? Math.Round(((PrecoOriginal.Value - Preco) / PrecoOriginal.Value) * 100, 0)
            : null;
            
        [NotMapped]
        public double MediaAvaliacoes => Avaliacoes.Any() 
            ? Math.Round(Avaliacoes.Average(a => a.Nota), 1) 
            : 0;
            
        [NotMapped]
        public int TotalAvaliacoes => Avaliacoes.Count;
    }
}
