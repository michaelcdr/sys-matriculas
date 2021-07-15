/*
 * Classe responsavel pelas funcionalidades da página de cadastro de curso.
 * */
class CadastroDisciplina {
    constructor() {
        this._btnSalvar = $("#btn-salvar");
        this._btnSalvar.button('reset');
        this._curriculoEl = $("#CurriculoId");
        this._coRequisitosEl = $("#CoRequisitos");
        this._preRequisitosEl = $("#PreRequisitos");
        this._formEl = $("#form-cadastro");
        this.iniciarEventos();
    }


    iniciarEventos() {
        let _self = this;

        //if ($("#DisciplinaId").val()){
        //    _self.carregarDisciplinas();
        //}

        _self._formEl.submit(function () {
            _self._btnSalvar.button('loading');
            
            let formEstaoValidos = _self._formEl.validate().form();
            if (formEstaoValidos) 
                return true;

            _self._btnSalvar.button('reset');
            return false;
        });

        _self._curriculoEl.change(function () {
            let _this = $(this);
            if (_this.val() !== "")
                _self.carregarDisciplinas();
            else {
                _self._coRequisitosEl.attr('disabled','disabled');
                _self._preRequisitosEl.attr('disabled','disabled');
            }
        });
    }

    preencherOptionsDeDisciplinas(disciplinas) {
        let _self = this;
        let options = [];
        $(disciplinas).each(function () {
            options.push('<option value="' + this.disciplinaId + '">'+this.nome+'</option>');
        });

        _self._coRequisitosEl.html(options.join(''));
        _self._preRequisitosEl.html(options.join(''));

        _self._coRequisitosEl.removeAttr('disabled');
        _self._preRequisitosEl.removeAttr('disabled');
    }

    carregarDisciplinas() {
        let _self = this;
        
        let curriculoId = _self._curriculoEl.val();
        $.get('/Disciplina/ObterDisciplinasDoCurriculo', { id: curriculoId }, function (data) {
            if (data.sucesso) {
                _self.preencherOptionsDeDisciplinas(data.disciplinas);
            } else {
                modalManager.mostrarErro(data.titulo, data.mensagem);
            }
        }).fail(function () {
            modalManager.mostrarErroServidor();
        });
    }
}