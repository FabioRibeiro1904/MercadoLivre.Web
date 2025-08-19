# MercadoLivre Clone - E-commerce ASP.NET Core MVC

Um e-commerce completo inspirado no MercadoLivre, desenvolvido com ASP.NET Core MVC (.NET 9.0) e Entity Framework Core.

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-blue)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-green)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5.3-purple)
![SQLite](https://img.shields.io/badge/SQLite-Database-orange)

## Características principais

### Frontend moderno e responsivo
- Design fiel ao MercadoLivre com cores e layout autênticos
- Interface totalmente responsiva desenvolvida com Bootstrap 5
- Animações fluidas e transições suaves em CSS
- Ícones do Font Awesome para melhor experiência visual
- Interatividade aprimorada com jQuery

### Funcionalidades implementadas
- Página inicial atrativa com carousel, categorias e produtos em destaque
- Sistema de busca inteligente com autocomplete e filtros avançados
- Catálogo completo de produtos com visualização em grid
- Carrinho de compras com gerenciamento dinâmico de itens
- Processo de checkout intuitivo e completo
- Sistema robusto de usuários incluindo login, registro e perfil
- Acompanhamento visual de pedidos com timeline interativa
- Sistema de avaliações e comentários de produtos
- Páginas institucionais profissionais

### Stack tecnológico
- ASP.NET Core MVC 9.0 como framework principal
- Entity Framework Core com banco SQLite para persistência
- BCrypt.Net para segurança na criptografia de senhas
- Auto-migrations configuradas para facilitar deployments
- Dados de demonstração incluídos para testes

## Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- [SQLite](https://www.sqlite.org/) (já incluído no projeto)

## Como executar o projeto

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

### 3. Execute as migrações do banco de dados
```bash
dotnet ef database update
```

### 4. Inicie a aplicação
```bash
dotnet run
```

### 5. Acesse no navegador
Vá para: `https://localhost:5001` ou `http://localhost:5000`

## Estrutura do projeto

```
MercadoLivre.Web/
├── Controllers/           # Controladores MVC
│   ├── HomeController.cs     # Página inicial e seções principais
│   ├── ProdutosController.cs # Catálogo e detalhes de produtos
│   ├── CarrinhoController.cs # Carrinho e processo de compra
│   └── ContaController.cs    # Sistema de usuários
├── Models/               # Modelos de dados
│   ├── Usuario.cs           # Usuários do sistema
│   ├── Produto.cs          # Produtos do catálogo
│   ├── Pedido.cs           # Pedidos realizados
│   ├── CarrinhoItem.cs     # Itens do carrinho
│   └── ...
├── Views/                # Páginas da interface
│   ├── Shared/            # Layout e componentes compartilhados
│   ├── Home/              # Páginas principais do site
│   ├── Produtos/          # Catálogo e detalhes de produtos
│   ├── Carrinho/          # Carrinho e checkout
│   └── Conta/             # Login, registro e perfil
├── wwwroot/              # Arquivos públicos
│   ├── css/
│   │   └── mercadolivre.css  # Estilos personalizados
│   └── js/
│       └── mercadolivre.js   # Funcionalidades JavaScript
├── Data/                 # Configuração do banco
└── Migrations/           # Migrações do Entity Framework
```

## Design e experiência do usuário

### Paleta de cores
- Amarelo principal: `#FFE600` (cor característica do MercadoLivre)
- Azul de destaque: `#3483FA` (para links e elementos importantes)
- Tons de cinza: Para textos e elementos neutros

### Componentes da interface
- Cards de produto com efeitos visuais no hover
- Timeline interativa para acompanhamento de pedidos
- Formulários responsivos com validação em tempo real
- Galeria de imagens nos detalhes dos produtos
- Sistema de filtros avançados para busca
- Navegação breadcrumb para melhor orientação

## Estrutura dos dados

### Modelo Usuario
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

### Modelo Produto
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

### Modelo Pedido
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

## Recursos implementados

### Sistema de autenticação
- Login e logout seguros com sessões
- Criptografia de senhas usando BCrypt
- Proteção de páginas que requerem autenticação

### Carrinho de compras
- Dados salvos na sessão do usuário
- Cálculo automático de valores totais
- Verificação de disponibilidade em estoque

### Sistema de busca
- Pesquisa por nome e descrição dos produtos
- Filtros por categoria, preço e outros critérios
- Sugestões automáticas durante a digitação

### Design responsivo
- Interface adaptável para diferentes tamanhos de tela
- Otimização para dispositivos móveis
- Carregamento otimizado de imagens

## Deploy e publicação

### Para ambiente local
```bash
dotnet publish -c Release -o ./publish
```

### Usando Docker
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY publish/ .
ENTRYPOINT ["dotnet", "MercadoLivre.Web.dll"]
```

## Como contribuir

1. Faça um fork do repositório
2. Crie uma branch para sua funcionalidade (`git checkout -b feature/NovaFuncionalidade`)
3. Faça commit das suas alterações (`git commit -m 'Adiciona nova funcionalidade'`)
4. Envie para a branch (`git push origin feature/NovaFuncionalidade`)
5. Abra um Pull Request

## Próximas melhorias planejadas

- Sistema de notificações em tempo real
- Chat de atendimento ao cliente
- Integração com gateways de pagamento
- Sistema de cupons e descontos
- Painel administrativo com relatórios
- API REST para aplicações móveis
- Testes automatizados
- Sistema de cache para melhor performance
- CDN para otimização de imagens

## Licença

Este projeto está licenciado sob a MIT License - consulte o arquivo [LICENSE.md](LICENSE.md) para mais detalhes.

## Autor

**Fábio Lúcio Ribeiro**
- GitHub: [@fabiolucioribeiro](https://github.com/fabiolucioribeiro)
- Portfolio: [https://fabiolucioribeiro.github.io](https://fabiolucioribeiro.github.io)

---

Se este projeto foi útil para você, considere dar uma estrela no repositório!

## Páginas implementadas

### Interface completa desenvolvida
- Página inicial com carousel interativo, categorias e produtos em destaque
- Catálogo de produtos com layout em grid responsivo e filtros inteligentes
- Páginas detalhadas de produtos com galeria de imagens, avaliações e sugestões
- Carrinho de compras com gerenciamento dinâmico de itens e quantidades
- Processo completo de checkout com validações e confirmações
- Sistema de autenticação com login, registro e recuperação de senha
- Área do usuário para gerenciamento de perfil e dados pessoais
- Histórico de pedidos com timeline visual e acompanhamento de status
- Páginas institucionais profissionais incluindo sobre e contato
- Tratamento elegante de erros com páginas personalizadas

### Características visuais implementadas
- Fidelidade às cores características do MercadoLivre (#FFE600, #3483FA)
- Layout completamente responsivo otimizado para desktop, tablet e mobile
- Animações CSS suaves incluindo fadeIn, efeitos hover e transições fluidas
- Hierarquia tipográfica consistente e legível em todos os dispositivos
- Componentes reutilizáveis como cards, botões e formulários padronizados
- Estados de carregamento e feedback visual para melhor experiência
- Implementação de boas práticas de acessibilidade com labels, textos alternativos e navegação por teclado

Este projeto representa um e-commerce moderno e funcional, desenvolvido seguindo as melhores práticas de desenvolvimento web com ASP.NET Core MVC. A interface oferece uma experiência de usuário fluida e intuitiva, enquanto o backend garante performance e segurança.
