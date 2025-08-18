# E-commerce MercadoLivre Clone

Sistema de teste, inspirado no MercadoLivre, feito com ASP.NET MVC. Demonstra como criar uma loja virtual com carrinho, pagamentos e gestão de produtos.

## O que é este projeto

Este é um e-commerce completo que simula o MercadoLivre:
- Catálogo de produtos com busca e filtros
- Carrinho de compras persistente
- Sistema de usuários e perfis
- Processo de checkout completo
- Diferentes formas de pagamento
- Painel administrativo
- Design responsivo

## Tecnologias usadas

- **ASP.NET MVC** - Framework para aplicações web
- **Entity Framework Core** - ORM para banco de dados
- **SQLite** - Banco de dados
- **Bootstrap 5** - Framework CSS
- **BCrypt** - Criptografia de senhas
- **jQuery** - JavaScript para interatividade

## Como usar

### Executar o projeto

```bash
cd MercadoLivre.Web
dotnet restore
dotnet run
```

### Acessar

- Loja: https://localhost:7149
- Login admin: admin@mercadolivre.com / admin123

## O que o sistema faz

**Catálogo de Produtos:**
- Listagem de produtos com imagens
- Busca por nome do produto
- Filtros por categoria e preço
- Páginas de detalhes do produto
- Sistema de avaliações

**Carrinho de Compras:**
- Adicionar/remover produtos
- Alterar quantidades
- Cálculo automático de totais
- Persistência entre sessões
- Cálculo de frete

**Sistema de Usuários:**
- Registro de novos usuários
- Login e logout
- Perfil do usuário editável
- Histórico de pedidos
- Endereços salvos

**Processo de Compra:**
- Checkout com validação
- Escolha de endereço de entrega
- Múltiplas formas de pagamento
- Confirmação do pedido
- Acompanhamento do status

**Painel Administrativo:**
- Gestão de produtos
- Gestão de categorias
- Relatórios de vendas
- Gestão de usuários

## Funcionalidades implementadas

**Frontend:**
- Design responsivo (funciona no celular)
- Interface similar ao MercadoLivre
- Busca com autocomplete
- Filtros avançados
- Carrinho dinâmico com JavaScript

**Backend:**
- Autenticação com sessões
- Validação de dados
- Upload de imagens
- Cálculo de frete
- Processamento de pedidos

**Integração Externa:**
- API ViaCEP para busca automática de endereços
- Simulação de gateway de pagamento
- Validação de CPF/CNPJ

## Estrutura do código

```
MercadoLivre.Web/
├── Controllers/          # Controladores MVC
│   ├── HomeController       # Página inicial e catálogo
│   ├── ProdutosController   # Detalhes dos produtos
│   ├── CarrinhoController   # Carrinho e checkout
│   ├── ContaController      # Login e registro
│   └── AdminController      # Painel administrativo
├── Views/               # Páginas HTML (Razor)
├── Models/              # Classes de dados
├── Data/                # Configuração do banco
├── wwwroot/             # Arquivos estáticos (CSS, JS, imagens)
└── Program.cs           # Configuração da aplicação
```

## Formas de pagamento

- Cartão de Crédito
- Cartão de Débito
- PIX
- Boleto Bancário

## Dados de exemplo

O sistema cria automaticamente:
- **Usuário admin:** admin@mercadolivre.com / admin123
- **Produtos:** Eletrônicos, roupas, casa, etc.
- **Categorias:** Organizadas hierarquicamente

## Páginas principais

**Públicas:**
- `/` - Página inicial com produtos em destaque
- `/produtos/{id}` - Detalhes do produto
- `/buscar?termo=...` - Resultados da busca
- `/categoria/{id}` - Produtos por categoria

**Usuário logado:**
- `/carrinho` - Carrinho de compras
- `/checkout` - Finalizar compra
- `/conta/perfil` - Editar perfil
- `/conta/pedidos` - Histórico de pedidos

**Administrativas:**
- `/admin` - Dashboard administrativo
- `/admin/produtos` - Gestão de produtos
- `/admin/relatorios` - Relatórios de vendas

## Funcionalidades de destaque

**Busca Inteligente:**
- Busca por nome, descrição, categoria
- Sugestões automáticas
- Filtros por preço, categoria, avaliação

**Carrinho Avançado:**
- Salvo na sessão do usuário
- Cálculo automático de totais
- Validação de estoque
- Cupons de desconto

**Checkout Completo:**
- Validação de dados
- Integração com ViaCEP
- Múltiplas formas de pagamento
- Confirmação por email (simulado)

## Observações

- Interface inspirada no MercadoLivre real
- Sistema completo de e-commerce
- Código organizado seguindo padrões MVC
- Banco SQLite criado automaticamente
- Dados de exemplo inseridos na primeira execução

## Próximos passos

Para um e-commerce real:
- Gateway de pagamento real (PagSeguro, Stripe)
- Sistema de entregas com tracking
- Chat de suporte ao cliente
- Sistema de avaliações e comentários
- Programa de fidelidade
- Cupons de desconto
- Múltiplos vendedores
- API REST para mobile
