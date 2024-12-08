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
            "bLengthChange": false,
            "sPaginationType": "full_numbers",
            "processing": true,
            "serverSide": true,
            "filter":false,
            "scrollCollapse": true,
            "scrollY": 'calc(100vh - 450px)',
            "language": dataTablesAux.obterLanguage(),
            "ajax": {
                "url": `/${_self._controller}/Listar`,
                "type": "POST",
                "data": function (d) {
                    d.curriculoId = $('#curriculoId').val();
                }
            },
            "initComplete": function () {
                $("#curriculoId").off("change");
                $("#curriculoId").on("change", function () {
                    _self._oTable.fnDraw();
                });

                $("#pesquisa").on("keyup",function () {
                    _self._oTable.fnFilter($(this).val());
                });
            },
            "fnDrawCallback": function () {
                $('.btn-delete').off('click');
                $('.btn-delete').on("click",function () {
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
                { "data": "nome" },
                { "data": "curriculo" }
            ]
        });
    }
}