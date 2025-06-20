function formatarTelefone(input) {
    let x = input.value.replace(/\D/g, '').substring(0, 11);
    let formatted = '';

    if (x.length > 0) {
        formatted += '(' + x.substring(0, 2) + ') ';
    }
    if (x.length > 7) {
        formatted += x.substring(2, 7) + '-' + x.substring(7, 11);
    } else if (x.length > 2) {
        formatted += x.substring(2, x.length);
    }

    input.value = formatted;
}
function mascaraTelefone(input) {
    input.addEventListener('input', function (e) {
        formatarTelefone(e.target);
    });
    formatarTelefone(input);
}
function formatarCPF(input) {
    let v = input.value.replace(/\D/g, '').substring(0, 11);

    v = v.replace(/(\d{3})(\d)/, '$1.$2');
    v = v.replace(/(\d{3})(\d)/, '$1.$2');
    v = v.replace(/(\d{3})(\d{1,2})$/, '$1-$2');

    input.value = v;
}

function mascaraCPF(input) {
    input.addEventListener('input', function (e) {
        formatarCPF(e.target);
    });

    formatarCPF(input);
}

document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.telefone-mask').forEach(mascaraTelefone);

    document.querySelectorAll('.cpf-mask').forEach(mascaraCPF);

    const form = document.getElementById('formulario');
    if (!form) return;

    form.addEventListener('submit', function (e) {
        let valido = true;

        document.querySelectorAll('.telefone-mask').forEach(input => {
            input.value = input.value.replace(/\D/g, '');
        });

        document.querySelectorAll('.cpf-mask').forEach(input => {
            input.value = input.value.replace(/\D/g, '');
        });
    });
});
