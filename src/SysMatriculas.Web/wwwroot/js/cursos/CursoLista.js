class CursoLista {
    constructor() {
        this._tableEl = $("#tb-cursos");
        this._token = $('input[name=__RequestVerificationToken]');
        this._oTable = null;
        this._controller = "curso";

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

    iniciarEventosBotoes() {
        let _self = this;
        $('.btn-delete').unbind('click');
        $('.btn-delete').click(function () {
            _self.remover($(this));
        });

        $('.btn-ativar').unbind('click');
        $('.btn-ativar').click(function () {
            _self.ativar($(this));
        });

        $('.btn-desativar').unbind('click');
        $('.btn-desativar').click(function () {
            _self.desativar($(this));
        });
    }

    desativar(el) {
        let _self = this;
        modalManager.mostrarConfirmacao(
            "Deseja desativar o curso ?",
            "", function () {
                let dados = {
                    id: el.data("id"),
                    __RequestVerificationToken: _self.token.val()
                };
                $.post('Curso/Desativar/', dados, function (data) {

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

    ativar(el) {
        let _self = this;
        modalManager.mostrarConfirmacao(
            "Deseja ativar o curso ?",
            "", function () {
                let dados = {
                    id: el.data("id"),
                    __RequestVerificationToken: _self.token.val()
                };
                $.post('Curso/Ativar/', dados, function (data) {
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

    remover(el) {
        let _self = this;
        modalManager.mostrarConfirmacao(
            "Deseja remover o curso ?",
            "Esta ação não pode ser desfeita.", function () {                
                let dados = {                    
                    id: el.data("id"),
                    __RequestVerificationToken: _self.token.val()
                };
                $.post('Curso/Deletar/', dados, function (data) {
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
                }
            },
            'createdRow': function (row, data, dataIndex) {
                
            },
            "fnDrawCallback": function () {
                _self.iniciarEventosBotoes();
            },
            "columns": [
                {
                    "data": "cursoId", "width":"110px", "orderable": false, "render": function (colVal, b, dataObj) {
                        let editarUrl = `/${_self._controller}/Editar/${dataObj.cursoId}`;
                        let retorno = dataTablesAux.gerarBotao('delete', dataObj.cursoId, 'Remover curso', 'trash');
                        retorno += dataTablesAux.gerarLink('edit', dataObj.cursoId, 'Editar curso', editarUrl, 'edit');
                        
                        if (dataObj.ativo) {
                            retorno += dataTablesAux.gerarBotao('desativar', dataObj.cursoId, 'Desativar curso', 'check');
                        } else {
                            retorno += dataTablesAux.gerarBotao('ativar', dataObj.cursoId, 'ativar curso', 'check');
                        }
                        return retorno;
                    }
                },
                { "data": "nome" },
                { "data": "turno" }
            ]
        });
        _self.oTable.fnAdjustColumnSizing();
    }
}
