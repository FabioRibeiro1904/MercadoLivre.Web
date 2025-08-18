using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MercadoLivre.Web.Data;
using MercadoLivre.Web.Models;

namespace MercadoLivre.Web.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly MercadoLivreContext _context;

        public CarrinhoController(MercadoLivreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Conta");
            }

            var usuario = await _context.GetUsuarioWithCarrinhoAsync(usuarioId.Value);
            if (usuario == null)
            {
                return RedirectToAction("Login", "Conta");
            }

            return View(usuario.ItensCarrinho);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(int produtoId, int quantidade = 1)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Json(new { sucesso = false, mensagem = "Faça login para adicionar produtos ao carrinho." });
            }

            var produto = await _context.Produtos.FindAsync(produtoId);
            if (produto == null || !produto.Ativo)
            {
                return Json(new { sucesso = false, mensagem = "Produto não encontrado." });
            }

            if (produto.Estoque < quantidade)
            {
                return Json(new { sucesso = false, mensagem = "Estoque insuficiente." });
            }

            var itemExistente = await _context.CarrinhoItens
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId && c.ProdutoId == produtoId);

            if (itemExistente != null)
            {
                if (produto.Estoque < itemExistente.Quantidade + quantidade)
                {
                    return Json(new { sucesso = false, mensagem = "Estoque insuficiente." });
                }
                
                itemExistente.Quantidade += quantidade;
                itemExistente.PrecoUnitario = produto.PrecoComDesconto;
            }
            else
            {
                var novoItem = new CarrinhoItem
                {
                    UsuarioId = usuarioId.Value,
                    ProdutoId = produtoId,
                    Quantidade = quantidade,
                    PrecoUnitario = produto.PrecoComDesconto,
                    DataAdicao = DateTime.Now
                };

                _context.CarrinhoItens.Add(novoItem);
            }

            await _context.SaveChangesAsync();

            var totalItens = await _context.CarrinhoItens
                .Where(c => c.UsuarioId == usuarioId)
                .SumAsync(c => c.Quantidade);

            return Json(new { sucesso = true, mensagem = "Produto adicionado ao carrinho!", totalItens = totalItens });
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarQuantidade(int itemId, int quantidade)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Json(new { sucesso = false, mensagem = "Usuário não logado." });
            }

            var item = await _context.CarrinhoItens
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(c => c.Id == itemId && c.UsuarioId == usuarioId);

            if (item == null)
            {
                return Json(new { sucesso = false, mensagem = "Item não encontrado." });
            }

            if (quantidade <= 0)
            {
                _context.CarrinhoItens.Remove(item);
            }
            else
            {
                if (item.Produto.Estoque < quantidade)
                {
                    return Json(new { sucesso = false, mensagem = "Estoque insuficiente." });
                }

                item.Quantidade = quantidade;
            }

            await _context.SaveChangesAsync();

            return Json(new { sucesso = true });
        }

        [HttpPost]
        public async Task<IActionResult> Remover(int itemId)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Json(new { sucesso = false, mensagem = "Usuário não logado." });
            }

            var item = await _context.CarrinhoItens
                .FirstOrDefaultAsync(c => c.Id == itemId && c.UsuarioId == usuarioId);

            if (item == null)
            {
                return Json(new { sucesso = false, mensagem = "Item não encontrado." });
            }

            _context.CarrinhoItens.Remove(item);
            await _context.SaveChangesAsync();

            return Json(new { sucesso = true });
        }

        public async Task<IActionResult> GetTotalItens()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Json(new { totalItens = 0 });
            }

            var totalItens = await _context.CarrinhoItens
                .Where(c => c.UsuarioId == usuarioId)
                .SumAsync(c => c.Quantidade);

            return Json(new { totalItens = totalItens });
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Conta");
            }

            var usuario = await _context.GetUsuarioWithCarrinhoAsync(usuarioId.Value);
            if (usuario == null || !usuario.ItensCarrinho.Any())
            {
                TempData["Erro"] = "Seu carrinho está vazio.";
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarPedido(FormaPagamento formaPagamento, string? endereco, string? cep, string? cidade, string? estado)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Conta");
            }

            var usuario = await _context.GetUsuarioWithCarrinhoAsync(usuarioId.Value);
            if (usuario == null || !usuario.ItensCarrinho.Any())
            {
                TempData["Erro"] = "Seu carrinho está vazio.";
                return RedirectToAction("Index");
            }

            var numeroPedido = $"ML{DateTime.Now:yyyyMMddHHmmss}{usuarioId}";
            var valorTotal = usuario.ItensCarrinho.Sum(i => i.SubTotal);
            var valorFrete = 15.90m;
            var valorTotalComFrete = valorTotal + valorFrete;

            if (usuario.Saldo < valorTotalComFrete)
            {
                TempData["Erro"] = $"Saldo insuficiente. Você tem R$ {usuario.Saldo:F2} e o pedido custa R$ {valorTotalComFrete:F2}.";
                return RedirectToAction("Checkout");
            }

            var pedido = new Pedido
            {
                NumeroPedido = numeroPedido,
                UsuarioId = usuarioId.Value,
                ValorTotal = valorTotal,
                ValorFrete = valorFrete,
                Status = StatusPedido.Confirmado,
                FormaPagamento = formaPagamento,
                EnderecoEntrega = endereco ?? usuario.Endereco,
                CEPEntrega = cep ?? usuario.CEP,
                CidadeEntrega = cidade ?? usuario.Cidade,
                EstadoEntrega = estado ?? usuario.Estado,
                DataPedido = DateTime.Now,
                DataConfirmacao = DateTime.Now
            };

            _context.Pedidos.Add(pedido);

            usuario.Saldo -= valorTotalComFrete;

            await _context.SaveChangesAsync();

            foreach (var item in usuario.ItensCarrinho)
            {
                var pedidoItem = new PedidoItem
                {
                    PedidoId = pedido.Id,
                    ProdutoId = item.ProdutoId,
                    NomeProduto = item.Produto.Nome,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.PrecoUnitario
                };

                _context.PedidoItens.Add(pedidoItem);

                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                if (produto != null)
                {
                    produto.Estoque -= item.Quantidade;
                }
            }

            _context.CarrinhoItens.RemoveRange(usuario.ItensCarrinho);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("UsuarioSaldo", usuario.Saldo.ToString("F2"));

            TempData["Sucesso"] = $"Pedido {numeroPedido} realizado com sucesso! Saldo restante: R$ {usuario.Saldo:F2}";
            return RedirectToAction("MeusPedidos", "Conta");
        }
    }
}
