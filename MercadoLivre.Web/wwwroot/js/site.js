function atualizarContadorCarrinho() {
    $.get('/Carrinho/GetTotalItens', function(data) {
        $('#cart-count').text(data.totalItens);
        if (data.totalItens > 0) {
            $('#cart-count').show();
        } else {
            $('#cart-count').hide();
        }
    });
}

function adicionarAoCarrinho(produtoId, quantidade = 1) {
    $.post('/Carrinho/Adicionar', { produtoId: produtoId, quantidade: quantidade })
        .done(function(response) {
            if (response.sucesso) {
                atualizarContadorCarrinho();
                
                mostrarNotificacao(response.mensagem, 'success');
                
                if (response.totalItens !== undefined) {
                    $('#cart-count').text(response.totalItens);
                    if (response.totalItens > 0) {
                        $('#cart-count').show();
                    }
                }
            } else {
                mostrarNotificacao(response.mensagem, 'error');
            }
        })
        .fail(function() {
            mostrarNotificacao('Erro ao adicionar produto ao carrinho. Tente novamente.', 'error');
        });
}

function mostrarNotificacao(mensagem, tipo = 'info') {
    $('.notification').remove();
    
    const notificacao = $(`
        <div class="notification alert alert-${tipo === 'success' ? 'success' : tipo === 'error' ? 'danger' : 'info'} alert-dismissible fade show" 
             style="position: fixed; top: 20px; right: 20px; z-index: 9999; max-width: 300px;">
            ${mensagem}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `);
    
    $('body').append(notificacao);
    
    setTimeout(() => {
        notificacao.fadeOut(() => notificacao.remove());
    }, 5000);
}

$(document).ready(function() {
    atualizarContadorCarrinho();
    
    $('#search-form').on('submit', function(e) {
        const termo = $('#search-input').val().trim();
        if (!termo) {
            e.preventDefault();
            mostrarNotificacao('Digite um termo para buscar', 'warning');
        }
    });
});
