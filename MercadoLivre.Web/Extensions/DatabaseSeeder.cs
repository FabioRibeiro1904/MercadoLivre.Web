using Microsoft.EntityFrameworkCore;
using MercadoLivre.Web.Data;
using MercadoLivre.Web.Models;

namespace MercadoLivre.Web.Extensions
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MercadoLivreContext>();

            if (!await context.Categorias.AnyAsync())
            {
                var categorias = new List<Categoria>
                {
                    new() { Nome = "Eletrônicos", Descricao = "Smartphones, notebooks, TVs e mais", Ativa = true },
                    new() { Nome = "Roupas e Calçados", Descricao = "Moda masculina e feminina", Ativa = true },
                    new() { Nome = "Casa e Decoração", Descricao = "Móveis, decoração e utensílios", Ativa = true },
                    new() { Nome = "Esportes e Fitness", Descricao = "Equipamentos esportivos e academia", Ativa = true },
                    new() { Nome = "Livros", Descricao = "Literatura, técnicos e educativos", Ativa = true },
                    new() { Nome = "Beleza e Cuidados", Descricao = "Cosméticos e produtos de higiene", Ativa = true }
                };

                context.Categorias.AddRange(categorias);
                await context.SaveChangesAsync();
            }

            if (!await context.Usuarios.AnyAsync())
            {
                var adminUser = new Usuario
                {
                    Nome = "Administrador",
                    Email = "admin@mercadolivre.com",
                    Senha = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    IsAdmin = true,
                    DataCadastro = DateTime.Now,
                    Endereco = "Rua das Flores, 123",
                    CEP = "01234-567",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Telefone = "(11) 99999-9999"
                };

                var demoUser = new Usuario
                {
                    Nome = "João Silva",
                    Email = "joao@email.com",
                    Senha = BCrypt.Net.BCrypt.HashPassword("123456"),
                    IsAdmin = false,
                    DataCadastro = DateTime.Now,
                    Endereco = "Av. Paulista, 1000",
                    CEP = "01310-100",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Telefone = "(11) 88888-8888"
                };

                context.Usuarios.AddRange(adminUser, demoUser);
                await context.SaveChangesAsync();
            }

            if (!await context.Produtos.AnyAsync())
            {
                var categorias = await context.Categorias.ToListAsync();
                var eletronicos = categorias.First(c => c.Nome == "Eletrônicos");
                var roupas = categorias.First(c => c.Nome == "Roupas e Calçados");
                var casa = categorias.First(c => c.Nome == "Casa e Decoração");
                var esportes = categorias.First(c => c.Nome == "Esportes e Fitness");

                var produtos = new List<Produto>
                {
                    new()
                    {
                        Nome = "Smartphone Galaxy S24",
                        Descricao = "Smartphone Samsung Galaxy S24 128GB, Tela 6.1, Câmera Tripla 50MP, 5G",
                        Preco = 2499.99m,
                        PrecoOriginal = 2999.99m,
                        Estoque = 50,
                        Marca = "Samsung",
                        Modelo = "Galaxy S24",
                        CategoriaId = eletronicos.Id,
                        Ativo = true,
                        Promocao = true,
                        DataCadastro = DateTime.Now
                    },
                    new()
                    {
                        Nome = "Notebook Dell Inspiron 15",
                        Descricao = "Notebook Dell Inspiron 15 3000, Intel Core i5, 8GB RAM, SSD 256GB, Tela 15.6",
                        Preco = 2199.99m,
                        PrecoOriginal = 2599.99m,
                        Estoque = 25,
                        Marca = "Dell",
                        Modelo = "Inspiron 15 3000",
                        CategoriaId = eletronicos.Id,
                        Ativo = true,
                        Promocao = true,
                        DataCadastro = DateTime.Now
                    },
                    new()
                    {
                        Nome = "TV Smart 55\" 4K",
                        Descricao = "Smart TV LED 55\" UHD 4K Samsung, HDR, Alexa Built-in, Tizen OS",
                        Preco = 1899.99m,
                        PrecoOriginal = 2299.99m,
                        Estoque = 15,
                        Marca = "Samsung",
                        Modelo = "UN55AU7700",
                        CategoriaId = eletronicos.Id,
                        Ativo = true,
                        Promocao = true,
                        DataCadastro = DateTime.Now
                    },
                    new()
                    {
                        Nome = "Tênis Nike Air Max",
                        Descricao = "Tênis Nike Air Max SC Masculino - Branco e Preto, Conforto e Estilo",
                        Preco = 299.99m,
                        PrecoOriginal = 399.99m,
                        Estoque = 100,
                        Marca = "Nike",
                        Modelo = "Air Max SC",
                        CategoriaId = roupas.Id,
                        Ativo = true,
                        Promocao = true,
                        DataCadastro = DateTime.Now
                    },
                    new()
                    {
                        Nome = "Camiseta Polo Ralph Lauren",
                        Descricao = "Camiseta Polo Ralph Lauren Masculina Original - Algodão Premium",
                        Preco = 189.99m,
                        PrecoOriginal = 249.99m,
                        Estoque = 80,
                        Marca = "Polo Ralph Lauren",
                        Modelo = "Classic Fit",
                        CategoriaId = roupas.Id,
                        Ativo = true,
                        Promocao = true,
                        DataCadastro = DateTime.Now
                    },
                    new()
                    {
                        Nome = "Sofá 3 Lugares",
                        Descricao = "Sofá 3 Lugares Retrátil e Reclinável, Tecido Suede, Cor Cinza",
                        Preco = 1299.99m,
                        PrecoOriginal = 1599.99m,
                        Estoque = 10,
                        Marca = "MadeiraMadeira",
                        CategoriaId = casa.Id,
                        Ativo = true,
                        Promocao = true,
                        DataCadastro = DateTime.Now
                    },
                    new()
                    {
                        Nome = "Mesa de Jantar 6 Lugares",
                        Descricao = "Mesa de Jantar Retangular 6 Lugares, Madeira Maciça, Cor Mogno",
                        Preco = 899.99m,
                        PrecoOriginal = 1199.99m,
                        Estoque = 8,
                        Marca = "Tok&Stok",
                        CategoriaId = casa.Id,
                        Ativo = true,
                        Promocao = true,
                        DataCadastro = DateTime.Now
                    },
                    new()
                    {
                        Nome = "Esteira Ergométrica",
                        Descricao = "Esteira Ergométrica Elétrica, Velocidade até 16km/h, Display LCD",
                        Preco = 1799.99m,
                        PrecoOriginal = 2199.99m,
                        Estoque = 12,
                        Marca = "Athletic",
                        Modelo = "Advanced 2.0",
                        CategoriaId = esportes.Id,
                        Ativo = true,
                        Promocao = true,
                        DataCadastro = DateTime.Now
                    }
                };

                context.Produtos.AddRange(produtos);
                await context.SaveChangesAsync();

                var usuario = await context.Usuarios.FirstAsync(u => u.Email == "joao@email.com");
                var smartphone = produtos.First(p => p.Nome.Contains("Smartphone"));

                var avaliacao = new Avaliacao
                {
                    UsuarioId = usuario.Id,
                    ProdutoId = smartphone.Id,
                    Nota = 5,
                    Comentario = "Excelente produto! Superou minhas expectativas.",
                    DataAvaliacao = DateTime.Now,
                    Ativo = true
                };

                context.Avaliacoes.Add(avaliacao);
                await context.SaveChangesAsync();
            }
        }
    }
}
