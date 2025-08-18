using Microsoft.EntityFrameworkCore;
using MercadoLivre.Web.Models;

namespace MercadoLivre.Web.Data
{
    public class MercadoLivreContext : DbContext
    {
        public MercadoLivreContext(DbContextOptions<MercadoLivreContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
                .HasMany(c => c.Produtos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Produto>()
                .HasMany(p => p.ItensCarrinho)
                .WithOne(c => c.Produto)
                .HasForeignKey(c => c.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.ItensCarrinho)
                .WithOne(c => c.Usuario)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Pedidos)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens)
                .WithOne(i => i.Pedido)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PedidoItem>()
                .HasOne(pi => pi.Produto)
                .WithMany()
                .HasForeignKey(pi => pi.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Avaliacoes)
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Produto)
                .WithMany(p => p.Avaliacoes)
                .HasForeignKey(a => a.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Categoria>()
                .HasIndex(c => c.Nome);

            modelBuilder.Entity<Produto>()
                .HasIndex(p => p.Nome);

            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Produto>()
                .Property(p => p.PrecoOriginal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<PedidoItem>()
                .Property(pi => pi.PrecoUnitario)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.ValorTotal)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.ValorFrete)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Saldo)
                .HasPrecision(10, 2);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<Usuario?> GetUsuarioWithCarrinhoAsync(int usuarioId)
        {
            return await Usuarios
                .Include(u => u.ItensCarrinho)
                    .ThenInclude(c => c.Produto)
                        .ThenInclude(p => p.Categoria)
                .Where(u => u.Id == usuarioId)
                .FirstOrDefaultAsync();
        }

        public async Task<Produto?> GetProdutoWithDetailsAsync(int produtoId)
        {
            return await Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == produtoId);
        }

        public async Task<List<Produto>> GetProdutosPorCategoriaAsync(int categoriaId, int skip = 0, int take = 20)
        {
            return await Produtos
                .Include(p => p.Categoria)
                .Where(p => p.CategoriaId == categoriaId && p.Ativo)
                .OrderBy(p => p.Nome)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<Produto>> BuscarProdutosAsync(string termo, int skip = 0, int take = 20)
        {
            return await Produtos
                .Include(p => p.Categoria)
                .Where(p => p.Ativo && (p.Nome.Contains(termo) || p.Descricao.Contains(termo)))
                .OrderBy(p => p.Nome)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<List<Pedido>> GetPedidosUsuarioAsync(int usuarioId)
        {
            return await Pedidos
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Produto)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.DataPedido)
                .ToListAsync();
        }
    }
}
