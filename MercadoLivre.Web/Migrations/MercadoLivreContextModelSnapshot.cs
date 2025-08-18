
using System;
using MercadoLivre.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MercadoLivre.Web.Migrations
{
    [DbContext(typeof(MercadoLivreContext))]
    partial class MercadoLivreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.8");

            modelBuilder.Entity("MercadoLivre.Web.Models.Avaliacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comentario")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataAvaliacao")
                        .HasColumnType("TEXT");

                    b.Property<int>("Nota")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Avaliacoes");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.CarrinhoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataAdicao")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PrecoUnitario")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("CarrinhoItens");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ativa")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagemUrl")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Nome");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CEPEntrega")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("CidadeEntrega")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("CodigoRastreamento")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataConfirmacao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataEntrega")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataEnvio")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataPedido")
                        .HasColumnType("TEXT");

                    b.Property<string>("EnderecoEntrega")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("EstadoEntrega")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("FormaPagamento")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NumeroPedido")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Observacoes")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ValorFrete")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("ValorTotal")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.PedidoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomeProduto")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("PedidoId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("PrecoUnitario")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProdutoId1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("ProdutoId1");

                    b.ToTable("PedidoItens");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ativo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<int>("Estoque")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImagemPrincipal")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagensSecundarias")
                        .HasColumnType("TEXT");

                    b.Property<string>("Marca")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Modelo")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Preco")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("PrecoOriginal")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("Promocao")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("Nome");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CEP")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Cidade")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Endereco")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Saldo")
                        .HasPrecision(10, 2)
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefone")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Avaliacao", b =>
                {
                    b.HasOne("MercadoLivre.Web.Models.Produto", "Produto")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MercadoLivre.Web.Models.Usuario", "Usuario")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.CarrinhoItem", b =>
                {
                    b.HasOne("MercadoLivre.Web.Models.Produto", "Produto")
                        .WithMany("ItensCarrinho")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MercadoLivre.Web.Models.Usuario", "Usuario")
                        .WithMany("ItensCarrinho")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Pedido", b =>
                {
                    b.HasOne("MercadoLivre.Web.Models.Usuario", "Usuario")
                        .WithMany("Pedidos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.PedidoItem", b =>
                {
                    b.HasOne("MercadoLivre.Web.Models.Pedido", "Pedido")
                        .WithMany("Itens")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MercadoLivre.Web.Models.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MercadoLivre.Web.Models.Produto", null)
                        .WithMany("ItensPedido")
                        .HasForeignKey("ProdutoId1");

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Produto", b =>
                {
                    b.HasOne("MercadoLivre.Web.Models.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Categoria", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Pedido", b =>
                {
                    b.Navigation("Itens");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Produto", b =>
                {
                    b.Navigation("Avaliacoes");

                    b.Navigation("ItensCarrinho");

                    b.Navigation("ItensPedido");
                });

            modelBuilder.Entity("MercadoLivre.Web.Models.Usuario", b =>
                {
                    b.Navigation("Avaliacoes");

                    b.Navigation("ItensCarrinho");

                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
