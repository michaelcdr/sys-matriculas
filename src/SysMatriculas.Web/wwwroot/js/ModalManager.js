/**
 * Classe responsavel por controlar as modais do sistema que usam sweet alert
 * */
class ModalManager {

    constructor() {

    }

    mostrarLoader(allowOutsideClick, title) {
        allowOutsideClick = allowOutsideClick === null || allowOutsideClick === undefined ? true : allowOutsideClick;
        var allowEscapeKey = true;
        if (!allowOutsideClick)
            allowEscapeKey = false;

        title = title !== null && title !== "" ? title : "Processando...";
        swal.fire({
            title: title,
            allowEscapeKey: allowEscapeKey,
            allowOutsideClick: allowOutsideClick,
            confirmButtonColor: "#ff6600",
            onOpen: function () {
                swal.showLoading();
            }
        });
    }

    mostrarErro(title, text) {
        swal.fire({ title: title, text: text, type: "error" });
    }

    mostrarSucesso(title, text) {
        swal.fire({ title: title, text: text, type: "success" });
    }

    mostrarErroServidor() {
        swal.fire({
            title: "Ops, algo deu errado.",
            text: "Ocorreu um erro interno em nossos servidores, tente novamente mais tarde.",
            type: "error"
        });
    }

    mostrarConfirmacao(titulo, mensagem, callback) {
        Swal.fire({
            title: titulo,
            text: mensagem,
            type: "question",
            showCancelButton: true,
            cancelButtonColor: "#455a64",
            cancelButtonText: "Não",
            confirmButtonColor: "#e5231e",
            confirmButtonText: "Sim",
            showLoaderOnConfirm: true,
            allowOutsideClick: () => !Swal.isLoading(),
            preConfirm: (a) => {
                return new Promise(function (resolve, reject) {
                    if (callback !== null) {
                        if (typeof callback === 'function')
                            callback.call(this);
                    }
                });
            }
        });
    }
}