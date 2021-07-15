class ListaCurriculo {
    constructor() {
        this._tableEl = $("#tb-curriculos");
        this._token = $('input[name=__RequestVerificationToken]');
        this._oTable = null;
        this._controller = "curriculo";

        this.construirDataTables();
    }

    get oTable() {
        return this._oTable;
    }

    get tableEl() {
        return this._tableEl;
    }

    get token() {
        return this._token;
    }

    remover(el) {
        let _self = this;
        modalManager.mostrarConfirmacao(
            "Deseja remover o curriculo ?",
            "Esta ação não pode ser desfeita.", function () {                
                let dados = {                    
                    id: el.data("id"),
                    __RequestVerificationToken: _self.token.val()
                };
                $.post('curriculo/Deletar/', dados, function (data) {
                    if (data.sucesso) {
                        _self._oTable.fnDraw();
                        swal.close();
                    } else {
                        modalManager.mostrarErro(data.titulo, data.mensagem);
                    }
                }).fail(function () {
                    modalManager.mostrarErroServidor();
                    });
            }
        );
    }

    construirDataTables() {
        let _self = this;
        
        _self._oTable = _self._tableEl.dataTable({
            "iDisplayLength": 100,
            "sPaginationType": "full_numbers",
            "processing": true,
            "serverSide": true,
            "language": dataTablesAux.obterLanguage(),
            "ajax": {
                "url": `/${_self._controller}/Listar`,
                "type": "POST",
                "data": function (d) {
                    //d.CategoryId = $('#CategoryId').val();
                }
            },
            'createdRow': function (row, data, dataIndex) {
                
            },
            "fnDrawCallback": function () {
                $('.btn-delete').unbind('click');
                $('.btn-delete').click(function () {
                    _self.remover($(this));
                });
            },
            "columns": [
                {
                    "data": "curriculoId", "width":"60px", "orderable": false, "render": function (colVal, b, dataObj) {
                        let editarUrl = `/${_self._controller}/Editar/${dataObj.curriculoId}`;
                        let retorno = dataTablesAux.gerarBotao('delete', dataObj.curriculoId, 'Remover curriculo', 'trash');
                        retorno += dataTablesAux.gerarLink('edit', dataObj.cursoId, 'Editar curriculo', editarUrl, 'edit');
                        
                        //if (dataObj.ativo) {
                        //    retorno += dataTablesAux.gerarBotao('desativar', dataObj.cursoId, 'Desativar curriculo', 'cancel');
                        //} else {
                        //    retorno += dataTablesAux.gerarBotao('ativar', dataObj.cursoId, 'ativar curriculo', 'check');
                        //}
                        return retorno;
                    }
                },
                { "data": "nome" }
            ]
        });
        _self.oTable.fnAdjustColumnSizing();
    }
}
