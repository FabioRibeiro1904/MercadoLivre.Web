// MercadoLivre JavaScript Functions

// Atualizar contador do carrinho
function updateCartCount() {
    $.get('/Carrinho/GetTotalItens', function(data) {
        $('#cart-count').text(data.totalItens || 0);
    });
}

// Adicionar produto ao carrinho
function adicionarAoCarrinho(produtoId, quantidade = 1) {
    const btn = $(`[data-produto-id="${produtoId}"]`);
    
    // Evitar múltiplos cliques
    if (btn.prop('disabled')) {
        return;
    }
    
    const originalText = btn.html();
    const originalClasses = btn.attr('class');
    
    // Função para restaurar o botão
    function restoreButton() {
        btn.attr('class', originalClasses)
           .prop('disabled', false)
           .html(originalText);
    }
    
    // Mostrar loading
    btn.prop('disabled', true)
       .html('<i class="fas fa-spinner fa-spin"></i> Adicionando...');
    
    // Timeout de segurança para evitar loading infinito
    const timeoutId = setTimeout(() => {
        restoreButton();
        showToast('error', 'Tempo limite excedido. Tente novamente.');
    }, 10000); // 10 segundos
    
    $.ajax({
        url: '/Carrinho/Adicionar',
        type: 'POST',
        data: {
            produtoId: produtoId,
            quantidade: quantidade
        },
        timeout: 8000, // 8 segundos de timeout
        success: function(response) {
            clearTimeout(timeoutId);
            
            if (response && response.sucesso) {
                // Atualizar contador
                $('#cart-count').text(response.totalItens || 0);
                
                // Mostrar mensagem de sucesso
                showToast('success', response.mensagem || 'Produto adicionado ao carrinho!');
                
                // Mostrar feedback visual de sucesso
                btn.attr('class', 'btn btn-success')
                   .html('<i class="fas fa-check"></i> Adicionado');
                
                setTimeout(restoreButton, 2000);
            } else {
                // Mostrar mensagem de erro
                showToast('error', response.mensagem || 'Erro ao adicionar produto.');
                
                // Se não estiver logado, oferecer redirecionamento
                if (response.mensagem && response.mensagem.toLowerCase().includes('login')) {
                    setTimeout(() => {
                        if (confirm('Você precisa fazer login. Deseja ir para a página de login?')) {
                            window.location.href = '/Conta/Login';
                        }
                    }, 1500);
                }
                
                restoreButton();
            }
        },
        error: function(xhr, status, error) {
            clearTimeout(timeoutId);
            restoreButton();
            
            if (status === 'timeout') {
                showToast('error', 'Tempo limite excedido. Verifique sua conexão.');
            } else {
                showToast('error', 'Erro ao adicionar produto. Tente novamente.');
            }
            
            console.error('Erro ao adicionar ao carrinho:', error);
        }
    });
}

// Atualizar quantidade no carrinho
function atualizarQuantidade(itemId, novaQuantidade) {
    if (novaQuantidade < 0) return;
    
    if (novaQuantidade === 0) {
        removerItem(itemId);
        return;
    }
    
    $.post('/Carrinho/AtualizarQuantidade', {
        itemId: itemId,
        quantidade: novaQuantidade
    })
    .done(function(response) {
        if (response.sucesso) {
            location.reload();
        } else {
            showToast('error', response.mensagem);
        }
    })
    .fail(function() {
        showToast('error', 'Erro ao atualizar quantidade');
    });
}

// Remover item do carrinho
function removerItem(itemId) {
    if (!confirm('Tem certeza que deseja remover este item?')) {
        return;
    }
    
    $.post('/Carrinho/Remover', {
        itemId: itemId
    })
    .done(function(response) {
        if (response.sucesso) {
            location.reload();
        } else {
            showToast('error', response.mensagem);
        }
    })
    .fail(function() {
        showToast('error', 'Erro ao remover item');
    });
}

// Mostrar toast notification
function showToast(type, message) {
    const toastClass = type === 'success' ? 'alert-success' : 'alert-danger';
    const icon = type === 'success' ? 'fa-check-circle' : 'fa-exclamation-circle';
    
    const toast = $(`
        <div class="alert ${toastClass} alert-dismissible fade show position-fixed" 
             style="top: 20px; right: 20px; z-index: 9999; max-width: 400px;" role="alert">
            <i class="fas ${icon}"></i> ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `);
    
    $('body').append(toast);
    
    // Auto remove after 5 seconds
    setTimeout(() => {
        toast.alert('close');
    }, 5000);
}

// Busca com autocomplete (simulado)
function initSearchAutocomplete() {
    let searchTimeout;
    
    $('.search-input').on('input', function() {
        const termo = $(this).val();
        
        clearTimeout(searchTimeout);
        
        if (termo.length < 2) {
            hideSearchSuggestions();
            return;
        }
        
        searchTimeout = setTimeout(() => {
            // Aqui você pode fazer uma chamada AJAX para buscar sugestões
            // Por enquanto, vamos simular algumas sugestões
            const suggestions = [
                'iPhone 15',
                'Samsung Galaxy',
                'Notebook Dell',
                'Tênis Nike',
                'Fone Bluetooth'
            ].filter(item => item.toLowerCase().includes(termo.toLowerCase()));
            
            showSearchSuggestions(suggestions);
        }, 300);
    });
}

function showSearchSuggestions(suggestions) {
    hideSearchSuggestions();
    
    if (suggestions.length === 0) return;
    
    const suggestionsList = $(`
        <div class="search-suggestions position-absolute bg-white border rounded shadow-sm w-100" 
             style="top: 100%; left: 0; z-index: 1000;">
            ${suggestions.map(item => `
                <div class="suggestion-item p-2 border-bottom" style="cursor: pointer;">
                    <i class="fas fa-search text-muted me-2"></i>${item}
                </div>
            `).join('')}
        </div>
    `);
    
    $('.search-form').addClass('position-relative').append(suggestionsList);
    
    // Handle suggestion click
    $('.suggestion-item').on('click', function() {
        const text = $(this).text().trim();
        $('.search-input').val(text);
        $('.search-form').submit();
    });
}

function hideSearchSuggestions() {
    $('.search-suggestions').remove();
}

// Filtros de produtos
function initProductFilters() {
    // Filtro por preço
    $('#filtro-preco').on('change', function() {
        const faixaPreco = $(this).val();
        const url = new URL(window.location);
        
        if (faixaPreco) {
            url.searchParams.set('preco', faixaPreco);
        } else {
            url.searchParams.delete('preco');
        }
        
        window.location.href = url.toString();
    });
    
    // Filtro por categoria
    $('#filtro-categoria').on('change', function() {
        const categoria = $(this).val();
        const url = new URL(window.location);
        
        if (categoria) {
            url.searchParams.set('categoria', categoria);
        } else {
            url.searchParams.delete('categoria');
        }
        
        window.location.href = url.toString();
    });
}

// Galeria de imagens do produto
function initProductGallery() {
    $('.product-thumbnail').on('click', function() {
        const newSrc = $(this).attr('src');
        $('#main-product-image').attr('src', newSrc);
        
        $('.product-thumbnail').removeClass('border-primary');
        $(this).addClass('border-primary');
    });
}

// Avaliação com estrelas
function initStarRating() {
    $('.star-rating').on('click', '.star', function() {
        const rating = $(this).data('rating');
        const container = $(this).closest('.star-rating');
        
        container.find('.star').each(function(index) {
            if (index < rating) {
                $(this).removeClass('far').addClass('fas');
            } else {
                $(this).removeClass('fas').addClass('far');
            }
        });
        
        container.find('input[name="nota"]').val(rating);
    });
}

// Simular próximo status do pedido
function simularProximoStatus(pedidoId) {
    $.post('/Conta/SimularProximoStatus', {
        pedidoId: pedidoId
    })
    .done(function(response) {
        if (response.sucesso) {
            location.reload();
        } else {
            showToast('error', response.mensagem);
        }
    })
    .fail(function() {
        showToast('error', 'Erro ao atualizar status do pedido');
    });
}

// Máscara para CEP
function initCEPMask() {
    $('input[name="cep"]').on('input', function() {
        let value = $(this).val().replace(/\D/g, '');
        value = value.replace(/^(\d{5})(\d)/, '$1-$2');
        $(this).val(value);
    });
}

// Buscar CEP via API
function buscarCEP(cep) {
    cep = cep.replace(/\D/g, '');
    
    if (cep.length !== 8) return;
    
    $.get(`https://viacep.com.br/ws/${cep}/json/`)
        .done(function(data) {
            if (!data.erro) {
                $('input[name="endereco"]').val(data.logradouro);
                $('input[name="cidade"]').val(data.localidade);
                $('input[name="estado"]').val(data.uf);
            }
        })
        .fail(function() {
            showToast('error', 'Erro ao buscar CEP');
        });
}

// Inicializar quando o documento estiver pronto
$(document).ready(function() {
    // Inicializar funcionalidades
    initSearchAutocomplete();
    initProductFilters();
    initProductGallery();
    initStarRating();
    initCEPMask();
    
    // Event listeners
    $(document).on('click', '[data-action="add-to-cart"]', function() {
        const produtoId = $(this).data('produto-id');
        const quantidade = parseInt($('#quantidade').val()) || 1;
        adicionarAoCarrinho(produtoId, quantidade);
    });
    
    $(document).on('click', '.quantity-btn', function() {
        const input = $(this).siblings('.quantity-input');
        const currentValue = parseInt(input.val()) || 0;
        const isIncrement = $(this).hasClass('increment');
        const newValue = isIncrement ? currentValue + 1 : Math.max(0, currentValue - 1);
        
        input.val(newValue);
        
        if ($(this).closest('.cart-item').length) {
            const itemId = $(this).closest('.cart-item').data('item-id');
            atualizarQuantidade(itemId, newValue);
        }
    });
    
    $(document).on('click', '[data-action="remove-item"]', function() {
        const itemId = $(this).data('item-id');
        removerItem(itemId);
    });
    
    $(document).on('click', '[data-action="simular-status"]', function() {
        const pedidoId = $(this).data('pedido-id');
        simularProximoStatus(pedidoId);
    });
    
    $(document).on('blur', 'input[name="cep"]', function() {
        const cep = $(this).val();
        if (cep.length >= 8) {
            buscarCEP(cep);
        }
    });
    
    // Esconder sugestões quando clicar fora
    $(document).on('click', function(e) {
        if (!$(e.target).closest('.search-form').length) {
            hideSearchSuggestions();
        }
    });
    
    // Smooth scroll para âncoras
    $('a[href^="#"]').on('click', function(e) {
        e.preventDefault();
        const target = $($(this).attr('href'));
        if (target.length) {
            $('html, body').animate({
                scrollTop: target.offset().top - 100
            }, 500);
        }
    });
    
    // Lazy loading para imagens
    if ('IntersectionObserver' in window) {
        const imageObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const img = entry.target;
                    img.src = img.dataset.src;
                    img.classList.remove('lazy');
                    imageObserver.unobserve(img);
                }
            });
        });
        
        document.querySelectorAll('img[data-src]').forEach(img => {
            imageObserver.observe(img);
        });
    }
    
    // Animações ao scroll
    const animateOnScroll = () => {
        $('.fade-in-up').each(function() {
            const elementTop = $(this).offset().top;
            const windowBottom = $(window).scrollTop() + $(window).height();
            
            if (elementTop < windowBottom - 100) {
                $(this).addClass('animate');
            }
        });
    };
    
    $(window).on('scroll', animateOnScroll);
    animateOnScroll(); // Execute na carga inicial
});

// Funções utilitárias
const Utils = {
    formatCurrency: function(value) {
        return new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        }).format(value);
    },
    
    formatDate: function(date) {
        return new Date(date).toLocaleDateString('pt-BR');
    },
    
    debounce: function(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    }
};
