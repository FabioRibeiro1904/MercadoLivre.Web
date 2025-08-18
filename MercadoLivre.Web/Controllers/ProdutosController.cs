using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MercadoLivre.Web.Data;
using MercadoLivre.Web.Models;

namespace MercadoLivre.Web.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly MercadoLivreContext _context;

        public ProdutosController(MercadoLivreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Avaliacoes.Where(a => a.Ativo))
                    .ThenInclude(a => a.Usuario)
                .FirstOrDefaultAsync(p => p.Id == id && p.Ativo);

            if (produto == null)
            {
                return NotFound();
            }

            var produtosRelacionados = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.CategoriaId == produto.CategoriaId && p.Id != produto.Id && p.Ativo)
                .Take(4)
                .ToListAsync();

            ViewBag.ProdutosRelacionados = produtosRelacionados;
            
            return View(produto);
        }

        public async Task<IActionResult> Categoria(string categoria, int pagina = 1)
        {
            if (string.IsNullOrEmpty(categoria))
            {
                return RedirectToAction("Index", "Home");
            }

            categoria = Uri.UnescapeDataString(categoria);

            const int itensPorPagina = 20;
            var skip = (pagina - 1) * itensPorPagina;

            var categoriaObj = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Nome.ToLower().Trim() == categoria.ToLower().Trim() && c.Ativa);

            if (categoriaObj == null)
            {
                TempData["Erro"] = $"Categoria '{categoria}' não encontrada.";
                return RedirectToAction("Index", "Home");
            }

            var totalItens = await _context.Produtos
                .Where(p => p.CategoriaId == categoriaObj.Id && p.Ativo)
                .CountAsync();

            var produtos = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.CategoriaId == categoriaObj.Id && p.Ativo)
                .OrderBy(p => p.Nome)
                .Skip(skip)
                .Take(itensPorPagina)
                .ToListAsync();

            var viewModel = new CategoriaViewModel
            {
                Categoria = categoriaObj,
                Produtos = produtos,
                PaginaAtual = pagina,
                TotalPaginas = (int)Math.Ceiling((double)totalItens / itensPorPagina)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAvaliacao(int produtoId, int nota, string? comentario)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Conta");
            }

            var jaAvaliado = await _context.Avaliacoes
                .AnyAsync(a => a.ProdutoId == produtoId && a.UsuarioId == usuarioId);

            if (jaAvaliado)
            {
                TempData["Erro"] = "Você já avaliou este produto.";
                return RedirectToAction("Detalhes", new { id = produtoId });
            }

            var avaliacao = new Avaliacao
            {
                ProdutoId = produtoId,
                UsuarioId = usuarioId.Value,
                Nota = nota,
                Comentario = comentario,
                DataAvaliacao = DateTime.Now,
                Ativo = true
            };

            _context.Avaliacoes.Add(avaliacao);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Avaliação adicionada com sucesso!";
            return RedirectToAction("Detalhes", new { id = produtoId });
        }
    }
}
