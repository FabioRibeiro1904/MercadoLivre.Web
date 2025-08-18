using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MercadoLivre.Web.Data;
using MercadoLivre.Web.Models;
using BCrypt.Net;

namespace MercadoLivre.Web.Controllers
{
    public class ContaController : Controller
    {
        private readonly MercadoLivreContext _context;

        public ContaController(MercadoLivreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
            {
                ViewBag.Erro = "Email e senha são obrigatórios.";
                return View();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                ViewBag.Erro = "Email ou senha inválidos.";
                return View();
            }

            HttpContext.Session.SetInt32("UsuarioId", usuario.Id);
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            HttpContext.Session.SetString("UsuarioEmail", usuario.Email);
            HttpContext.Session.SetString("IsAdmin", usuario.IsAdmin.ToString());
            HttpContext.Session.SetString("UsuarioSaldo", usuario.Saldo.ToString("F2"));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Usuario usuario, string confirmarSenha)
        {
            if (usuario.Senha != confirmarSenha)
            {
                ViewBag.Erro = "As senhas não coincidem.";
                return View(usuario);
            }

            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
            {
                ViewBag.Erro = "Este email já está cadastrado.";
                return View(usuario);
            }

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            usuario.DataCadastro = DateTime.Now;
            usuario.IsAdmin = false;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Conta criada com sucesso! Faça login para continuar.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Perfil()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login");
            }

            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return RedirectToAction("Login");
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Perfil(Usuario usuario)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login");
            }

            var usuarioExistente = await _context.Usuarios.FindAsync(usuarioId);
            if (usuarioExistente == null)
            {
                return RedirectToAction("Login");
            }

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Telefone = usuario.Telefone;
            usuarioExistente.Endereco = usuario.Endereco;
            usuarioExistente.CEP = usuario.CEP;
            usuarioExistente.Cidade = usuario.Cidade;
            usuarioExistente.Estado = usuario.Estado;

            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("UsuarioNome", usuarioExistente.Nome);
            HttpContext.Session.SetString("UsuarioSaldo", usuarioExistente.Saldo.ToString("F2"));

            TempData["Sucesso"] = "Perfil atualizado com sucesso!";
            return View(usuarioExistente);
        }

        [HttpGet]
        public async Task<IActionResult> MeusPedidos()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return RedirectToAction("Login");
            }

            var pedidos = await _context.GetPedidosUsuarioAsync(usuarioId.Value);
            return View(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> SimularProximoStatus(int pedidoId)
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (usuarioId == null)
            {
                return Json(new { sucesso = false, mensagem = "Usuário não logado." });
            }

            var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.Id == pedidoId && p.UsuarioId == usuarioId);
            if (pedido == null)
            {
                return Json(new { sucesso = false, mensagem = "Pedido não encontrado." });
            }

            var novoStatus = pedido.Status switch
            {
                StatusPedido.Confirmado => StatusPedido.Preparando,
                StatusPedido.Preparando => StatusPedido.Enviado,
                StatusPedido.Enviado => StatusPedido.Entregue,
                _ => pedido.Status
            };

            if (novoStatus != pedido.Status)
            {
                pedido.Status = novoStatus;
                
                if (novoStatus == StatusPedido.Enviado)
                {
                    pedido.DataEnvio = DateTime.Now;
                    pedido.CodigoRastreamento = $"BR{DateTime.Now:yyyyMMddHHmmss}";
                }
                else if (novoStatus == StatusPedido.Entregue)
                {
                    pedido.DataEntrega = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                
                return Json(new { sucesso = true, novoStatus = pedido.StatusDescricao });
            }

            return Json(new { sucesso = false, mensagem = "Status já está no máximo ou não pode ser alterado." });
        }
    }
}
