/*
 * Classe responsavel pelas funcionalidades da página de cadastro de curso.
 * */
class CadastroCurriculo {
    constructor() {
        this._btnSalvar = $("#btn-salvar");
        this._formEl = $('#form-cadastrar');

        this._btnSalvar.button('reset');
        this.iniciarEventos();
        
    }

    get tableEl() {
        return this._tableEl;
    }

    iniciarEventos() {
        let _self = this;
       
        _self._formEl.submit(function () {
            _self._btnSalvar.button('loading');
            
            let formEstaoValidos = _self._formEl.validate().form();
            if (formEstaoValidos) 
                return true;

            _self._btnSalvar.button('reset');
            return false;
        });
    }
}