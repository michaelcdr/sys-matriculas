/*
 * Classe responsavel pelas funcionalidades da página de cadastro de curso.
 * */
class CadastroCurso {
    constructor() {
        this._tableEl = $("#tb-curriculos");
        this._btnAddCurriculo = $("#btn-add-curriculo");
        this._btnSalvar = $("#btn-salvar-curso");
        this._formEl = $('#form-cadastrar');
        this._curriculos = [];

        this._btnSalvar.button('reset');
        this.iniciarEventos();

    }

    get tableEl() {
        return this._tableEl;
    }

    obterCurriculos() {
        let _self = this;
        $.get('/Curriculo/ObterLista', function (data) {

            if (data.sucesso) {
                var options = [];
                options.push('<option value="">Selecione um currículo</option>');
                $(data.curriculos).each(function () {
                    options.push('<option value="'+this.curriculoId+'">'+this.nome+'</option>');
                });
                _self._curriculos = options;

            } else {
                modalManager.mostrarErro(data.title, data.mensagem);
            }

        }).fail(function () {
            modalManager.mostrarErroServidor();
        });
    }

    iniciarEventos() {
        let _self = this;
        _self.obterCurriculos();

        _self._btnAddCurriculo.click(function () {
            _self.adicionarCurriculo();
        });

        _self.adicionarEventoRemoverCurriculo();

        _self._formEl.submit(function () {
            _self._btnSalvar.button('loading');
            
            let curriculosEstaoValidos = _self.validarCurriculos();
            let formEstaoValidos = _self._formEl.validate().form();

            if (curriculosEstaoValidos && formEstaoValidos) 
                return true;

            _self._btnSalvar.button('reset');
            return false;
        });
    }
    
    adicionarCurriculo() {
        let _self = this;
        if (_self.tableEl.find('tbody .empty').length === 1)
            _self.tableEl.find('tbody').empty();

        _self.tableEl.find('tbody').append(
            `<tr>
                <td>
                    <button class="btn btn-danger btn-delete-curriculo btn-sm"  
                        id="" type="button" data-toggle="tooltip" data-placement="top" 
                        title="Remover currículo" data-container="body" data-id="" 
                        data-loading-text="..."> <i class="fa fa-trash"></i>
                    </button>
                </td>
                <td>${_self.obterCurriculoEl()}</td>
            </tr>`
        );
        _self.adicionarEventoRemoverCurriculo();
    }

    obterCurriculoEl() {
        //return `<select type="text" class="form-control" name="Curriculos" >${_self._curriculos.join('')}</select>`;
        return '<input type="text" class="form-control" name="Curriculos" /> ';
    }

    limparCurriculos() {
        this.tableEl.find('tbody').empty();
        this.tableEl.find('tbody').html(
            `<tr><td colspan="2" class="empty">Nenhum currículo informado</td></tr>`
        );
    }

    adicionarEventoRemoverCurriculo() {
        let _self = this;
        $('.btn-delete-curriculo').unbind('click');
        $('.btn-delete-curriculo').click(function () {
            $(this).parent().parent().remove();

            if ($('.btn-delete-curriculo').length === 0)
                _self.limparCurriculos();
        });
    }

    obterCurriculosSelecionados() {
        let _self = this;
        let curriculosSelecionados = [];
        _self.tableEl.find('tbody input').each(function () {
            curriculosSelecionados.push($(this).val());
        });
        return curriculosSelecionados;
    }

    validarCurriculos() {
        let _self = this;
        let taValido = true;
        this.tableEl.find('tbody tr').removeClass('has-error');
        _self.tableEl.find('tbody > tr').each(function () {
            let tr = $(this);
            //if (tr.find('select').val() === '') {
            if (tr.find('input').val() === '') {
                taValido = false;
                tr.addClass('has-error');
            } else
                tr.removeClass('has-error');
        });

        $("#sp-curriculo").empty();
        if (_self.tableEl.find('tbody input').length === 0) {
            $("#sp-curriculo").html('Selecione ao menos um currículo.');
            return false;
        } else {
            //verificando se temos curriculos repetidos...
            let curriculosSelecionados = _self.obterCurriculosSelecionados();
            let valoresDistinct = [...new Set(curriculosSelecionados)];
            if (valoresDistinct.length !== curriculosSelecionados.length) {
                $("#sp-curriculo").html('Você não pode ter curriculos repetidos.');
                return false;
            }
        }

        return taValido;
    }
}