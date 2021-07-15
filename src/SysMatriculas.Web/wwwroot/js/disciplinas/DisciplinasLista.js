class DisciplinasLista {
    constructor() {
        this._tableEl = $("#tb-disciplina");
        this._oTable = null;
        this._controller = "disciplina";
        this._token = $('input[name=__RequestVerificationToken]');
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
            "Deseja remover a disciplina ?",
            "Esta ação não pode ser desfeita.", function () {
                let dados = {
                    id: el.data("id"),
                    __RequestVerificationToken: _self.token.val()
                };
                $.post('Disciplina/Deletar/', dados, function (data) {
                    if (data.sucesso) {
                        _self._oTable.fnDraw();
                        swal.close();
                    } else {
                        modalManager.mostrarErro(data.titulo, data.mensagem);
                    }
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
                    "data": "disciplinaId", "width":"60px", "orderable": false, "render": function (colVal, b, dataObj) {
                        let editarUrl = `/${_self._controller}/Editar/${dataObj.disciplinaId}`;
                        let retorno = dataTablesAux.gerarBotao('delete', dataObj.disciplinaId, 'Remover disciplina','trash');
                        retorno += dataTablesAux.gerarLink('edit', dataObj.disciplinaId, 'Editar disciplina', editarUrl,'edit');
                        return retorno;
                    }
                },
                { "data": "nome" }
            ]
        });
        _self.oTable.fnAdjustColumnSizing();
    }
}
