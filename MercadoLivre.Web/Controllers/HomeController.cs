using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MercadoLivre.Web.Data;
using MercadoLivre.Web.Models;

namespace MercadoLivre.Web.Controllers;

public class HomeController : Controller
{
    private readonly MercadoLivreContext _context;

    public HomeController(MercadoLivreContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var produtosDestaque = await _context.Produtos
            .Include(p => p.Categoria)
            .Where(p => p.Ativo && p.Promocao)
            .OrderByDescending(p => p.DataCadastro)
            .Take(8)
            .ToListAsync();

        var categorias = await _context.Categorias
            .Where(c => c.Ativa)
            .Take(6)
            .ToListAsync();

        ViewBag.Categorias = categorias;
        return View(produtosDestaque);
    }

    public async Task<IActionResult> Buscar(string termo, int? categoria, int pagina = 1)
    {
        const int itensPorPagina = 20;
        var skip = (pagina - 1) * itensPorPagina;

        var query = _context.Produtos
            .Include(p => p.Categoria)
            .Where(p => p.Ativo);

        if (!string.IsNullOrEmpty(termo))
        {
            query = query.Where(p => p.Nome.Contains(termo) || p.Descricao.Contains(termo));
        }

        if (categoria.HasValue)
        {
            query = query.Where(p => p.CategoriaId == categoria.Value);
        }

        var totalItens = await query.CountAsync();
        var produtos = await query
            .OrderBy(p => p.Nome)
            .Skip(skip)
            .Take(itensPorPagina)
            .ToListAsync();

        ViewBag.Termo = termo;
        ViewBag.CategoriaId = categoria;
        ViewBag.PaginaAtual = pagina;
        ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalItens / itensPorPagina);
        ViewBag.Categorias = await _context.Categorias.Where(c => c.Ativa).ToListAsync();

        return View(produtos);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }

    public IActionResult CentralAjuda()
    {
        return View();
    }

    public IActionResult ComoComprar()
    {
        return View();
    }

    public IActionResult Privacidade()
    {
        return View();
    }

    public IActionResult QuemSomos()
    {
        return View();
    }

    public IActionResult TrabalheConosco()
    {
        return View();
    }

    public IActionResult Imprensa()
    {
        return View();
    }

    public IActionResult CompraProtegida()
    {
        return View();
    }

    public IActionResult TermosUso()
    {
        return View();
    }

    public async Task<IActionResult> TodasCategorias()
    {
        var categorias = await _context.Categorias
            .Where(c => c.Ativa)
            .OrderBy(c => c.Nome)
            .ToListAsync();

        return View(categorias);
    }

    public async Task<IActionResult> OfertasDoDia()
    {
        var ofertas = await _context.Produtos
            .Include(p => p.Categoria)
            .Where(p => p.Ativo && p.Promocao)
            .OrderByDescending(p => p.DataCadastro)
            .ToListAsync();

        ViewBag.Categorias = await _context.Categorias
            .Where(c => c.Ativa)
            .ToListAsync();

        return View(ofertas);
    }

    public async Task<IActionResult> Supermercado()
    {
        var categoriaSupermercado = await _context.Categorias
            .Where(c => c.Ativa && (c.Nome.Contains("Alimentos") || c.Nome.Contains("Casa") || c.Nome.Contains("Bebidas")))
            .ToListAsync();

        var produtos = await _context.Produtos
            .Include(p => p.Categoria)
            .Where(p => p.Ativo && categoriaSupermercado.Select(c => c.Id).Contains(p.CategoriaId))
            .OrderBy(p => p.Nome)
            .ToListAsync();

        ViewBag.Categorias = await _context.Categorias
            .Where(c => c.Ativa)
            .ToListAsync();

        return View(produtos);
    }

    public async Task<IActionResult> Moda()
    {
        var categoriaModa = await _context.Categorias
            .Where(c => c.Ativa && (c.Nome.Contains("Roupas") || c.Nome.Contains("Moda") || c.Nome.Contains("Calçados") || c.Nome.Contains("Acessórios")))
            .ToListAsync();

        var produtos = await _context.Produtos
            .Include(p => p.Categoria)
            .Where(p => p.Ativo && categoriaModa.Select(c => c.Id).Contains(p.CategoriaId))
            .OrderBy(p => p.Nome)
            .ToListAsync();

        ViewBag.Categorias = await _context.Categorias
            .Where(c => c.Ativa)
            .ToListAsync();

        return View(produtos);
    }
}
