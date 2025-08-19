# 🛒 MercadoLivre Clone - E-commerce ASP.NET Core MVC

Um e-commerce completo inspirado no MercadoLivre, desenvolvido com ASP.NET Core MVC (.NET 9.0) e Entity Framework Core.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-blue)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-green)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple)
![SQLite](https://img.shields.io/badge/SQLite-Database-orange)

## 🚀 Características

### Frontend Responsivo
-  **Design inspirado no MercadoLivre** com cores e layout autênticos
-  **Totalmente responsivo** com Bootstrap 5
-  **Animações CSS** e transições suaves
-  **Font Awesome** para ícones
-  **jQuery** para interatividade

### Funcionalidades Principais
-  **Página inicial** com carousel, categorias e produtos em destaque
-  **Sistema de busca** com autocomplete e filtros
-  **Catálogo de produtos** com visualização em grid/lista
-  **Carrinho de compras** com gerenciamento de itens
-  **Processo de checkout** completo
-  **Sistema de usuários** (login, registro, perfil)
-  **Acompanhamento de pedidos** com timeline visual
-  **Sistema de avaliações** de produtos
-  **Páginas institucionais** (Sobre, Contato)

### Tecnologias Backend
- **ASP.NET Core MVC 9.0**
- **Entity Framework Core** com SQLite
- **BCrypt.Net** para hash de senhas
- **Auto-migrations** configuradas
- **Seed data** para demonstração

##  Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- [SQLite](https://www.sqlite.org/) (incluído no projeto)

## 🛠️ Instalação e Configuração

### 1. Clone o repositório
```bash
git clone https://github.com/seu-usuario/aspnet-mvc-ecommerce.git
cd aspnet-mvc-ecommerce
```

### 2. Restaure as dependências
```bash
cd MercadoLivre.Web
dotnet restore
```

### 3. Execute as migrações
```bash
dotnet ef database update
```

### 4. Execute o projeto
```bash
dotnet run
```

### 5. Acesse a aplicação
Abra seu navegador e vá para: `https://localhost:5001` ou `http://localhost:5000`

## 📁 Estrutura do Projeto

```
MercadoLivre.Web/
├── Controllers/           # Controladores MVC
│   ├── HomeController.cs     # Página inicial, sobre, contato
│   ├── ProdutosController.cs # Catálogo e detalhes de produtos
│   ├── CarrinhoController.cs # Carrinho e checkout
│   └── ContaController.cs    # Autenticação e perfil
├── Models/               # Modelos de dados
│   ├── Usuario.cs           # Modelo de usuário
│   ├── Produto.cs          # Modelo de produto
│   ├── Pedido.cs           # Modelo de pedido
│   ├── CarrinhoItem.cs     # Item do carrinho
│   └── ...
├── Views/                # Views Razor
│   ├── Shared/            # Layout e componentes compartilhados
│   ├── Home/              # Páginas principais
│   ├── Produtos/          # Catálogo e detalhes
│   ├── Carrinho/          # Carrinho e checkout
│   └── Conta/             # Login, registro, perfil
├── wwwroot/              # Arquivos estáticos
│   ├── css/
│   │   └── mercadolivre.css  # Estilos customizados
│   └── js/
│       └── mercadolivre.js   # JavaScript customizado
├── Data/                 # Contexto do banco de dados
└── Migrations/           # Migrações do EF Core
```

## 🎨 Design e UI/UX

### Cores do MercadoLivre
- **Amarelo primário**: `#FFE600` (cor principal da marca)
- **Azul secundário**: `#3483FA` (links e elementos de destaque)
- **Tons de cinza**: Para textos e elementos neutros

### Componentes UI
- **Cards de produto** com hover effects
- **Timeline de pedidos** com ícones de status
- **Formulários responsivos** com validação
- **Carrossel de imagens** nos detalhes do produto
- **Filtros avançados** de busca
- **Breadcrumbs** para navegação

## 📊 Modelos de Dados

### Usuario
```csharp
public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaHash { get; set; }
    public decimal Saldo { get; set; }
    public DateTime DataCriacao { get; set; }
}
```

### Produto
```csharp
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public string ImagemUrl { get; set; }
    public int CategoriaId { get; set; }
    public double AvaliacaoMedia { get; set; }
}
```

### Pedido
```csharp
public class Pedido
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataPedido { get; set; }
    public decimal Total { get; set; }
    public StatusPedido Status { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public List<PedidoItem> Itens { get; set; }
}
```

## 🔧 Funcionalidades Técnicas

### Autenticação
- Sistema de login/logout com sessões
- Hash de senhas com BCrypt
- Proteção de rotas privadas

### Carrinho de Compras
- Armazenamento em sessão
- Cálculo automático de totais
- Validação de estoque

### Sistema de Busca
- Busca por nome e descrição
- Filtros por categoria e preço
- Autocomplete com JavaScript

### Responsividade
- Design mobile-first
- Breakpoints otimizados
- Imagens responsivas

## 🚀 Deploy

### Publicação local
```bash
dotnet publish -c Release -o ./publish
```

### Docker (opcional)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY publish/ .
ENTRYPOINT ["dotnet", "MercadoLivre.Web.dll"]
```

## 🤝 Contribuições

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📝 Próximas Funcionalidades

- [ ] Sistema de notificações
- [ ] Chat de atendimento
- [ ] Integração com APIs de pagamento
- [ ] Sistema de cupons de desconto
- [ ] Relatórios administrativos
- [ ] API REST para mobile
- [ ] Testes unitários e de integração
- [ ] Cache Redis
- [ ] CDN para imagens

## 📄 Licença

Este projeto é licenciado sob a MIT License - veja o arquivo [LICENSE.md](LICENSE.md) para detalhes.

## 👨‍💻 Autor

**Fábio Lúcio Ribeiro**
- GitHub: [@fabiolucioribeiro](https://github.com/fabiolucioribeiro)
- Portfolio: [https://fabiolucioribeiro.github.io](https://fabiolucioribeiro.github.io)

---

⭐ Se este projeto te ajudou, considera dar uma estrela no repositório!

## 📱 Páginas Implementadas

### Frontend Completo
- **Página Inicial** - Carousel, categorias, produtos em destaque
- **Catálogo de Produtos** - Grid responsivo com filtros
- **Detalhes do Produto** - Galeria de imagens, avaliações, produtos relacionados
- **Carrinho de Compras** - Gerenciamento de itens e quantidades
- **Checkout** - Processo completo de finalização de compra
- **Login/Registro** - Sistema de autenticação completo
- **Perfil do Usuário** - Gerenciamento de dados pessoais
- **Meus Pedidos** - Timeline visual de acompanhamento
- **Página Sobre** - Informações institucionais
- **Página Contato** - Formulário de contato e FAQ
- **Página de Erro** - Tratamento elegante de erros

### Características do Design
- **Cores autênticas** do MercadoLivre (#FFE600, #3483FA)
- **Layout responsivo** para desktop, tablet e mobile
- **Animações CSS** com fadeIn, hover effects e transições
- **Tipografia** consistente com hierarquia visual
- **Componentes reutilizáveis** (cards, botões, formulários)
- **Loading states** e feedbacks visuais
- **Acessibilidade** com labels, alt texts e navegação por teclado
